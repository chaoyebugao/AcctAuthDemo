using AcctAuthDemo.Data.DbConfig;
using AcctAuthDemo.Model.Database;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Data.Repositories
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserRepository : Repository<User>
    {
        public UserRepository(AcctAuthDemoDbConfig dbConfig) : base(dbConfig)
        {
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public User Get(string name, string password)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $@"SELECT * FROM {TableName} WHERE IsDeleted = 0 AND Name = @Name AND Password = @Password";
                var dps = new DynamicParameters();
                dps.Add("Name", name);
                dps.Add("Password", password);

                return conn.QueryFirstOrDefault<User>(sql, dps);
            }
        }

        /// <summary>
        /// 登录名是否已存在
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public bool NameExists(string name)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $@"SELECT COUNT(1) FROM {TableName} WHERE IsDeleted = 0 AND Name = @Name";
                var dps = new DynamicParameters();
                dps.Add("Name", name);

                var count = conn.QueryFirstOrDefault<int>(sql, dps);
                return count > 0;
            }
        }

    }
}
