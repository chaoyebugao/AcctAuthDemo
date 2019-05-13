using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.ExecuteResults
{
    /// <summary>
    /// 错误编号
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 参数错误
        /// </summary>
        ParamsError = 1,

        /// <summary>
        /// 用户操作失败
        /// </summary>
        UserOptFail = 2,

        /// <summary>
        /// 系统错误
        /// </summary>
        SysError = 3,

        /// <summary>
        /// 无效登录票据
        /// </summary>
        InvalidLoginToken = 4,
    }
}
