using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.ExecuteResults
{
    /// <summary>
    /// 带数据执行结果
    /// </summary>
    public class DataRet : MsgRet
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="data">附带数据</param>
        /// <param name="errCode">错误编号</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static BaseRet Build(object data, ErrorCodes errCode = ErrorCodes.Success, string errMsg = null)
        {
            var ret = new DataRet()
            {
                Data = data,
                ErrCode = errCode,
                ErrMsg = errMsg,
            };

            return ret;
        }

    }

    /// <summary>
    /// 带数据执行结果（泛型）
    /// </summary>
    /// <typeparam name="T">附带数据的类型</typeparam>
    public class DataRet<T> : MsgRet
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 构建成功实例
        /// </summary>
        /// <param name="data">泛型数据</param>
        /// <returns></returns>
        public static DataRet<T> Build(T data)
        {
            return new DataRet<T> { Data = data };
        }
    }
}
