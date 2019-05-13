using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcctAuthDemo.Web.Controllers
{
    /// <summary>
    /// 通用
    /// </summary>
    public class CommonController : Controller
    {

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Fail(string errorMsg)
        {
            ViewData["ErrorMsg"] = errorMsg;
            return View();
        }
    }
}
