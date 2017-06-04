namespace OYMLCN.WeChat
{
    /// <summary>
    /// 基础接口配置
    /// </summary>
    public partial class Config
    {
        /// <summary>
        /// 基础接口配置
        /// </summary>
        /// <param name="accountName">公众平台微信号</param>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <param name="token">令牌</param>
        /// <param name="aes">消息加解密密钥</param>
        public Config(string accountName, string appId, string appSecret, string token, string aes = null)
        {
            AppId = appId;
            AppSecret = appSecret;
            Token = token;
            EncodingAESKey = aes;
            AccountName = accountName;
        }

        /// <summary>
        /// 公众平台唯一账号名称
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; private set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppSecret { get; private set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; private set; }
        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public string EncodingAESKey { get; private set; }
    }

}
