using AcctAuthDemo.Model.ExecuteResults;
using AcctAuthDemo.Model.Statics;
using AcctAuthDemo.Service.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcctAuthDemo.Web.Filters
{
    /// <summary>
    /// 登录令牌检查
    /// </summary>
    public class CheckLoginTokenActionFilterAttribute : ActionFilterAttribute
    {
        private readonly UserService userService;

        public CheckLoginTokenActionFilterAttribute(UserService userService)
        {
            this.userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Cookies[CookieNames.LoginToken];
            var deviceId = context.HttpContext.Request.Cookies[CookieNames.DeviceId];

            void needToLogin()
            {
                if (context.HttpContext.Request.Method.ToUpper() == "GET")
                {
                    context.Result = new RedirectResult("/User/Login");
                }
                else
                {
                    context.Result = new JsonResult(MsgRet.Build(ErrorCodes.InvalidLoginToken, "请进行登录"));
                }

                context.HttpContext.Response.Cookies.Delete(CookieNames.LoginToken);
            }

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(deviceId))
            {
                needToLogin();
                return;
            }

            var (userId, errCode, errMsg) = userService.Valid(token, deviceId);
            if (errCode != ErrorCodes.Success)
            {
                //令牌失效
                needToLogin();
                return;
            }

        }
    }
}
