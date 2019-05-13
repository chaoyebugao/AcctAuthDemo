using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Model.Database
{
    /// <summary>
    /// 登录令牌
    /// </summary>
    public class LoginToken : BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime ExpireAt { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
    }
}
