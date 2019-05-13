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
    /// 登录令牌
    /// </summary>
    public class LoginTokenRepository : Repository<LoginToken>
    {
        public LoginTokenRepository(AcctAuthDemoDbConfig dbConfig) : base(dbConfig)
        {
        }

        /// <summary>
        /// 查找用户Id
        /// </summary>
        /// <param name="token">票据</param>
        /// <param name="deviceId">设备Id</param>
        /// <returns></returns>
        public long? FindUser(string token, string deviceId)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = $@"
SELECT u.Id FROM {TableName} AS lt INNER JOIN User AS u ON u.Id = lt.UserId
WHERE lt.IsDeleted = 0 AND lt.ExpireAt > @Now AND lt.Token = @Token AND lt.DeviceId = @DeviceId
    AND u.IsDeleted = 0
";
                var dps = new DynamicParameters();
                dps.Add("Now", DateTime.Now);
                dps.Add("Token", token);
                dps.Add("DeviceId", deviceId);

                return conn.QueryFirstOrDefault<long?>(sql, dps);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="id">票据Id</param>
        /// <returns></returns>
        public int Delete(long userId, long id)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var dps = new DynamicParameters();
                dps.Add("Now", DateTime.Now);
                dps.Add("UserId", userId);
                dps.Add("Id", id);

                var sql = $@"
UPDATE {TableName} SET UpdateAt = @Now, IsDeleted = 1 WHERE IsDeleted = 0 AND UserId = @UserId AND Id = @Id
";
                return conn.Execute(sql, dps);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="deviceId">设备Id</param>
        /// <returns></returns>
        public int Delete(int userId, string deviceId)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var dps = new DynamicParameters();
                dps.Add("Now", DateTime.Now);
                dps.Add("UserId", userId);
                dps.Add("DeviceId", deviceId);

                var sql = $@"
UPDATE {TableName} SET UpdateAt = @Now, IsDeleted = 1
WHERE IsDeleted = 0 AND UserId = @UserId AND DeviceId = @DeviceId
";
                return conn.Execute(sql, dps);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="deviceId">设备Id</param>
        /// <returns></returns>
        public int Delete(int userId)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var dps = new DynamicParameters();
                dps.Add("Now", DateTime.Now);
                dps.Add("UserId", userId);

                var sql = $@"
UPDATE {TableName} SET UpdateAt = @Now, IsDeleted = 1
WHERE IsDeleted = 0 AND UserId = @UserId
";
                return conn.Execute(sql, dps);
            }
        }
    }
}
