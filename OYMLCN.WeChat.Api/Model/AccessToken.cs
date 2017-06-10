using System;

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
        public AccessToken()
        {
            GetTime = DateTime.Now;
            ErrorDescription[40001] = "AppSecret错误或者AppSecret不属于这个公众号，请开发者确认AppSecret的正确性";
            ErrorDescription[40002] = "请确保grant_type字段值为client_credential";
        }
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
        public DateTime GetTime { get; }
        /// <summary>
        /// AccessToken过期刷新时间
        /// </summary>
        public DateTime ExpiresTime => GetTime.AddSeconds(expires_in);
    }
}
