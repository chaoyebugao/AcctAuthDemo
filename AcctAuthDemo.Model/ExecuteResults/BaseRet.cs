using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.ExecuteResults
{
    /// <summary>
    /// 基础执行结果
    /// </summary>
    public class BaseRet
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public ErrorCodes ErrCode { get; set; }

        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="errCode">错误编码</param>
        /// <returns></returns>
        public static BaseRet Build(ErrorCodes errCode)
        {
            return new BaseRet()
            {
                ErrCode = errCode,
            };
        }
    }
}
