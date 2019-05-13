using AcctAuthDemo.Model.Errors;
using AcctAuthDemo.Model.ExecuteResults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Swifter.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AcctAuthDemo.Web.Middlewares
{
    /// <summary>
    /// 错误处理中间层
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ErrorException ex)
            {
                await HandleExceptionAsync(context, ex.Message, ex.ErrorCode);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    //TODO:502是啥了
                    msg = "请求错误";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, msg);
                }
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <param name="msg">消息</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, string msg, ErrorCodes errorCode = ErrorCodes.SysError)
        {
            var method = context.Request.Method.ToLower();
            if (method == "post")
            {
                var data = new MsgRet()
                {
                    ErrMsg = msg,
                    ErrCode = errorCode,
                };
                var result = JsonFormatter.SerializeObject(data);
                context.Response.Clear();
                context.Response.ContentType = "application/json;charset=utf-8";

                await context.Response.WriteAsync(result);
                return;
            }

            var url = "/Common/Fail?errorMsg=" + WebUtility.UrlEncode(msg);
            context.Response.Redirect(url, false);

            return;
        }

        private Task HandleUnauthorized(HttpContext context)
        {
            return context.Response.WriteAsync("Unauthorized");
        }
    }

    /// <summary>
    /// 静态扩展
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
