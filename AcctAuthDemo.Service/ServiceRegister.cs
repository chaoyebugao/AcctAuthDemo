using AcctAuthDemo.Service.Account;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Service
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// 仓库添加
        /// </summary>
        /// <param name="services">服务</param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
        }
    }
}
