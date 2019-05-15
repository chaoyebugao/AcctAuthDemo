using AcctAuthDemo.Data.DbConfig;
using AcctAuthDemo.Model.Database;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcctAuthDemo.Data.Repositories
{
    /// <summary>
    /// 仓库基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> where T : BaseModel
    {
        protected readonly BaseDbConfig dbConfig;

        public Repository(BaseDbConfig dbConfig)
        {
            this.dbConfig = dbConfig;
        }

        /// <summary>
        /// 自动增长主键ID语句
        /// </summary>
        protected const string SqlAutoIncIdentity = "SELECT LAST_INSERT_ID()";

        /// <summary>
        /// 表名
        /// </summary>
        protected string TableName => typeof(T).Name;

        /// <summary>
        /// 字段集合
        /// </summary>
        protected IEnumerable<string> Fields
        {
            get
            {
                return typeof(T).GetProperties().Select(m => m.Name);
            }
        }

        /// <summary>
        /// 排除主键的插入语句
        /// </summary>
        protected string InsertSql => GetInsertSql(nameof(BaseModel.Id));

        /// <summary>
        /// 获取插入语句
        /// </summary>
        /// <param name="excludeFields">排除的字段</param>
        /// <returns></returns>
        protected string GetInsertSql(params string[] excludeFields)
        {
            var fs = Fields.ToList();
            fs.RemoveAll(m => excludeFields.Contains(m, StringComparer.OrdinalIgnoreCase));

            return $@"INSERT INTO {TableName} ({string.Join(", ", fs)}) VALUES (@{string.Join(", @", fs)})";
        }

        /// <summary>
        /// 获取判断插入语句，排除主键Id
        /// </summary>
        /// <param name="conditions">判断条件</param>
        /// <returns></returns>
        protected string GetConditionalInsertBeginSql(string conditions)
        {
            return GetConditionalInsertBeginSql(conditions, "Id");
        }

        /// <summary>
        /// 获取判断插入语句
        /// </summary>
        /// <param name="conditions">判断条件</param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        protected string GetConditionalInsertBeginSql(string conditions, params string[] excludeFields)
        {
            var fs = Fields.ToList();
            fs.RemoveAll(m => excludeFields.Contains(m, StringComparer.OrdinalIgnoreCase));

            return $@"INSERT INTO {TableName} ({string.Join(", ", fs)}) SELECT @{string.Join(", @", fs)} FROM DUAL WHERE NOT EXISTS (
    SELECT 1 FROM {TableName} WHERE IsDeleted = 0 AND {conditions}
)";
        }

        /// <summary>
        /// 创建自增类型记录并返回新建记录的主键
        /// </summary>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="model">模型</param>
        /// <returns>
        /// 新建记录的主键
        /// </returns>
        public long Create(T model)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $"{InsertSql};{SqlAutoIncIdentity}";
                return conn.QueryFirstOrDefault<long>(sql, model);
            }
        }

        /// <summary>
        /// 创建自增类型记录并返回新建记录的主键
        /// </summary>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="model">模型</param>
        /// <returns>
        /// 新建记录的主键
        /// </returns>
        public TKey Create<TKey>(T model) where TKey : struct
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $"{InsertSql};{SqlAutoIncIdentity}";
                return conn.QueryFirstOrDefault<TKey>(sql, model);
            }
        }

        /// <summary>
        /// 创建非自增类型记录
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>
        /// 影响的行数
        /// </returns>
        public int Create<TNoAI>(TNoAI model)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = GetInsertSql();
                return conn.Execute(sql, model);
            }
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public T Get(long id)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $"SELECT * FROM {TableName} WHERE {nameof(BaseModel.IsDeleted)} = 0 AND {nameof(BaseModel.Id)} = @Id";
                var dps = new DynamicParameters();
                dps.Add("Id", id);
                return conn.QueryFirstOrDefault<T>(sql, dps);
            }
        }

        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public int Delete(long id)
        {
            return Delete(new long[] { id });
        }

        /// <summary>
        /// 删除多个
        /// </summary>
        /// <param name="ids">主键集合</param>
        /// <returns></returns>
        public int Delete(IEnumerable<long> ids)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $@"
UPDATE {TableName} SET {nameof(BaseModel.UpdateAt)} = @UpdateTime, {nameof(BaseModel.IsDeleted)} = 1
WHERE {nameof(BaseModel.IsDeleted)} = 0 AND {nameof(BaseModel.Id)} IN @Ids
";
                var dps = new DynamicParameters();
                dps.Add("UpdateTime", DateTime.Now);
                dps.Add("Ids", ids);
                return conn.Execute(sql, dps);
            }
        }

    }
}
