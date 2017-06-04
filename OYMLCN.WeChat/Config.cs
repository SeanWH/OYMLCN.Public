using Newtonsoft.Json;
using OYMLCN.WeChat.Model;
using System;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 基础接口配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// API调用域名
        /// </summary>
        public const string ApiHost = "api.weixin.qq.com";
        /// <summary>
        /// 公众平台域名
        /// </summary>
        public const string MpHost = "mp.weixin.qq.com";
        /// <summary>
        /// 开放平台域名
        /// </summary>
        public const string OpenHost = "open.weixin.qq.com";



        /// <summary>
        /// 基础接口配置
        /// （建议设置完整参数）
        /// </summary>
        public Config() { }
        /// <summary>
        /// 基础接口配置
        /// （建议设置完整参数）
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <param name="token">令牌</param>
        /// <param name="aes">消息加解密密钥</param>
        /// <param name="name">唯一的公众平台账号（调用客服接口时使用）</param>
        public Config(string appId, string appSecret, string token, string aes = null, string name = null)
        {
            this.AppId = appId;
            this.AppSecret = appSecret;
            this.Token = token;
            this.EncodingAESKey = aes;
            this.Name = name;
        }

        /// <summary>
        /// 公众平台唯一账号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [JsonIgnore]
        public string AppId { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        [JsonIgnore]
        public string AppSecret { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        [JsonIgnore]
        public string Token { get; set; }
        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        [JsonIgnore]
        public string EncodingAESKey { get; set; }
    }
}
