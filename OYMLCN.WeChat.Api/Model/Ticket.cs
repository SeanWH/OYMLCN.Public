using System;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信JS接口临时票据
    /// </summary>
    public class JsTicket : JsonResult
    {
        /// <summary>
        /// 微信JS接口临时票据
        /// </summary>
        public JsTicket() => GetTime = DateTime.Now;

        /// <summary>
        /// jsapi_ticket
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 过期策略时间
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// JsApiTicket获取时间
        /// </summary>
        public DateTime GetTime { get; }
        /// <summary>
        /// JsApiTicket过期刷新时间
        /// </summary>
        public DateTime ExpiresTime => GetTime.AddSeconds(expires_in);
    }
}
