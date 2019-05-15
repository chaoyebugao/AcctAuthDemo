using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcctAuthDemo.Model.ExecuteResults;
using AcctAuthDemo.Model.Statics;
using AcctAuthDemo.Model.ViewModels;
using AcctAuthDemo.Service.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcctAuthDemo.Web.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(string name, string password, string deviceId, string deviceName)
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var (loginEndpointToken, loginExpiredTime) = userService.Register(name, password, deviceId, deviceName, ip);

            Response.Cookies.Append(CookieNames.LoginToken, loginEndpointToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = new DateTimeOffset(loginExpiredTime),
            });

            return Json(DataRet<LoginResVm>.Build(new LoginResVm()
            {
                RedirectUrl = "/",
            }));
        }

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
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var (loginEndpointToken, loginExpiredTime) = userService.Authorize(name, password, deviceId, deviceName, ip);

            Response.Cookies.Append(CookieNames.LoginToken, loginEndpointToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = new DateTimeOffset(loginExpiredTime),
            });

            return Json(DataRet<LoginResVm>.Build(new LoginResVm()
            {
                RedirectUrl = "/",
            }));
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Logout()
        {
            var token = Request.Cookies[CookieNames.LoginToken];
            var deviceId = Request.Cookies[CookieNames.DeviceId];

            userService.Unauthorize(token, deviceId);

            return Json(MsgRet.BuildSuccess());
        }
    }
}