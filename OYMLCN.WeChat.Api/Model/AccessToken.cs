using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 公众号全局唯一票据
    /// </summary>
    public class AccessToken : JsonResult
    {
        /// <summary>
        /// 公众号全局唯一票据
        /// </summary>
        public AccessToken() => GetTime = DateTime.Now;
        /// <summary>
        /// AccessToken
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 过期策略时间
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// AccessToken获取时间
        /// </summary>
        public DateTime GetTime { get; private set; }
        /// <summary>
        /// AccessToken过期刷新时间
        /// </summary>
        public DateTime ExpiresTime => GetTime.AddSeconds(expires_in);
    }
}
