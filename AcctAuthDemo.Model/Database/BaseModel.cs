using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.Database
{
    /// <summary>
    /// 基础字段
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 是否是删除的
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }
    }
}
