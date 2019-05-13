using AcctAuthDemo.Model.ExecuteResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.Errors
{
    /// <summary>
    /// 特定错误
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ErrorException : Exception
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public ErrorCodes ErrorCode { get; private set; }

        private readonly string errorMessage;

        /// <summary>
        /// 初始化 <see cref="ErrorException"/> 类
        /// </summary>
        /// <param name="errorCode">错误编号</param>
        /// <param name="errorMessage">错误信息</param>
        public ErrorException(ErrorCodes errorCode, string errorMessage)
            : base(errorMessage)
        {
            this.ErrorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// 转化为附带数据的结果
        /// </summary>
        /// <returns></returns>
        public MsgRet ToMsgRet()
        {
            return new MsgRet()
            {
                ErrCode = ErrorCode,
                ErrMsg = errorMessage,
            };
        }
    }
}
