using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.ExecuteResults
{
    /// <summary>
    /// 错误信息
    /// </summary>
    public class MsgRet
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public ErrorCodes ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
