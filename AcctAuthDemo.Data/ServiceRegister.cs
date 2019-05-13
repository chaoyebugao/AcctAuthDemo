using AcctAuthDemo.Data.DbConfig;
using AcctAuthDemo.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Data
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="services">服务</param>
        public static void AddDbConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            var acctAuthDemoDbConfigConnStr = configuration.GetConnectionString("AcctAuthDemo");
            var acctAuthDemoDbConfig = new AcctAuthDemoDbConfig("AcctAuthDemo", acctAuthDemoDbConfigConnStr);
            services.AddSingleton((sp) => { return acctAuthDemoDbConfig; });

        }

        /// <summary>
        /// 仓库添加
        /// </summary>
        /// <param name="services">服务</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<LoginTokenRepository>();
            services.AddScoped<DeviceRepository>();
        }
    }
}
