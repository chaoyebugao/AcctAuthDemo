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
    /// 设备
    /// </summary>
    public class DeviceRepository : Repository<Device>
    {
        public DeviceRepository(AcctAuthDemoDbConfig dbConfig) : base(dbConfig)
        {
        }

        /// <summary>
        /// 根据设别Id更新/插入
        /// </summary>
        /// <param name="deviceId">设备Id</param>
        /// <param name="deviceName">设备名</param>
        /// <returns></returns>
        public int CreateByDeviceId(string deviceId, string deviceName)
        {
            using (var conn = new MySqlConnection(dbConfig.ConnectionString))
            {
                var sql = GetConditionalInsertBeginSql("DeviceId = @DeviceId");
                return conn.Execute(sql, new Device()
                {
                    CreateAt = DateTime.Now,
                    DeviceId = deviceId,
                    DeviceName = deviceName,
                    UpdateAt = DateTime.Now,
                });
            }
        }
    }
}
