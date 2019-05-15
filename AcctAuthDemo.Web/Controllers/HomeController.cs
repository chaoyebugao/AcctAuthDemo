using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcctAuthDemo.Web.Models;
using AcctAuthDemo.Web.Filters;

namespace AcctAuthDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 验证页面
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CheckLoginTokenActionFilterAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
