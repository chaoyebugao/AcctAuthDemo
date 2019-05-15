using AcctAuthDemo.Web.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcctAuthDemo.Web
{
    /// <summary>
    /// 服务注册
    /// </summary>
    /// <returns></returns>
    public static class ServiceRegister
    {
        /// <summary>
        /// 服务添加
        /// </summary>
        /// <param name="services">服务</param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<CheckLoginTokenActionFilterAttribute>();
        }
    }
}
