using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Data.DbConfig
{
    /// <summary>
    /// 数据库配置基类
    /// </summary>
    public abstract class BaseDbConfig
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString { get; protected set; }
    }
}
