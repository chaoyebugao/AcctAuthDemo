using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.ExecuteResults
{
    /// <summary>
    /// 带信息执行结果
    /// </summary>
    public class MsgRet : BaseRet
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; } = string.Empty;

        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="errCode">错误编号</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static BaseRet Build(ErrorCodes errCode, string errMsg = null)
        {
            BaseRet ret = new MsgRet()
            {
                ErrCode = errCode,
                ErrMsg = errMsg,
            };
            return ret;
        }

        /// <summary>
        /// 构建执行成功实例
        /// </summary>
        /// <returns></returns>
        public static MsgRet BuildSuccess()
        {
            return new MsgRet();
        }

    }
}
