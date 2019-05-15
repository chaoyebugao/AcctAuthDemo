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
        [ServiceFilter(typeof(CheckLoginTokenActionFilterAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
