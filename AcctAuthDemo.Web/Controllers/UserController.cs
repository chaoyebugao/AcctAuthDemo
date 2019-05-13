using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AcctAuthDemo.Web.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 进行登录
        /// </summary>
        /// <param name="name">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="deviceName">设备名</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(string name, string password, string deviceId, string deviceName)
        {
            return Json(null);
        }
    }
}