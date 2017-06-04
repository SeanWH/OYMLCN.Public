using System;

// 本文件放置调用微信API所返回的数据

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
        public DateTime ExpiresTime
        {
            get
            {
                int expires = 0;
                if (expires_in > 3600)
                {
                    expires = expires_in - 600;//提前10分钟过期
                }
                else if (expires_in > 1800)
                {
                    expires = expires_in - 300;//提前5分钟过期
                }
                else if (expires_in > 300)
                {
                    expires = expires_in - 30;//提前30秒钟过期
                }
                return GetTime.AddSeconds(expires);
            }
        }
        /// <summary>
        /// 基础接口配置
        /// 辅助属性（用于接口模型扩展联合调用）
        /// </summary>
        public Config Config { get; set; }
    }

    /// <summary>
    /// 微信JS接口临时票据
    /// </summary>
    public class JsApiTicket : JsonResult
    {
        /// <summary>
        /// 微信JS接口临时票据
        /// </summary>
        public JsApiTicket() => GetTime = DateTime.Now;

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
        public DateTime GetTime { get; private set; }
        /// <summary>
        /// JsApiTicket过期刷新时间
        /// </summary>
        public DateTime ExpiresTime
        {
            get
            {
                int expires = 0;
                if (expires_in > 3600)
                {
                    expires = expires_in - 600;//提前10分钟过期
                }
                else if (expires_in > 1800)
                {
                    expires = expires_in - 300;//提前5分钟过期
                }
                else if (expires_in > 300)
                {
                    expires = expires_in - 30;//提前30秒钟过期
                }
                return GetTime.AddSeconds(expires);
            }
        }

        /// <summary>
        /// 基础接口配置
        /// 辅助属性（用于接口模型扩展联合调用）
        /// </summary>
        public Config Config { get; set; }
    }


    /// <summary>
    /// 微信服务器IP地址
    /// </summary>
    public class IPList : JsonResult
    {
        /// <summary>
        /// 微信服务器IP地址
        /// </summary>
        public IPList() { }
        /// <summary>
        /// 微信服务器IP地址数据
        /// </summary>
        public string[] ip_list { get; set; }
    }

    /// <summary>
    /// 场景二维码
    /// </summary>
    public class QRScene : JsonResult
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过604800（即7天）。 
        /// </summary>
        public int expire_seconds { get; set; }
        /// <summary>
        /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片 
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 短链接
    /// </summary>
    public class ShortUrl : JsonResult
    {
        /// <summary>
        /// 短链接
        /// </summary>
        public string short_url { get; set; }
    }
}