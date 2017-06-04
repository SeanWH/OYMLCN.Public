using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 网页授权获取用户基本信息的通讯及逻辑辅助
    /// </summary>
    public static class WebApi
    {
        /// <summary>
        /// 拼接Api调用的完整地址
        /// </summary>
        /// <param name="apiStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ApiUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", Config.OpenHost, string.Format(apiStr, param));

        /// <summary>
        /// 创建静默授权并自动跳转到回调页的授权Url
        /// </summary>
        /// <param name="config"></param>
        /// <param name="redirectUrl">回掉页面</param>
        /// <param name="state">重定向后会带上state参数，可以填写a-zA-Z0-9的参数值，最多128字节 </param>
        /// <returns></returns>
        public static string WebUrlScopeBase(this Config config, string redirectUrl, string state = null) =>
            ApiUrl("/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect", config.AppId, redirectUrl.UrlEncode(), state);

        /// <summary>
        /// 创建发起的网页授权并自动跳转到回调页的授权Url
        /// </summary>
        /// <param name="config"></param>
        /// <param name="redirectUrl">回掉页面</param>
        /// <param name="state">重定向后会带上state参数，可以填写a-zA-Z0-9的参数值，最多128字节 </param>
        /// <returns></returns>
        public static string WebUrlScopeUserInfo(this Config config, string redirectUrl, string state = null) =>
            ApiUrl("/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", config.AppId, redirectUrl.UrlEncode(), state);


        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        /// <param name="config"></param>
        /// <param name="code">获取到的code参数</param>
        /// <returns></returns>
        public static WebAccessToken WebAccessTokenGet(this Config config, string code) =>
            HttpClient.GetString(CoreApi.ApiUrl("/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", config.AppId, config.AppSecret, code)).DeserializeJsonString<WebAccessToken>();
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="config"></param>
        /// <param name="token">网页授权的access_token</param>
        /// <returns></returns>
        public static WebAccessToken WebAccessTokenRefresh(this Config config, WebAccessToken token) => config.WebAccessTokenRefresh(token.refresh_token);
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="config">基础接口配置</param>
        /// <returns></returns>
        public static WebAccessToken Refresh(this WebAccessToken token, Config config) => config.WebAccessTokenRefresh(token);
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="config"></param>
        /// <param name="refreshToken">通过access_token获取到的refresh_token参数</param>
        /// <returns></returns>
        public static WebAccessToken WebAccessTokenRefresh(this Config config, string refreshToken) =>
            HttpClient.GetString(CoreApi.ApiUrl("/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", config.AppId, refreshToken)).DeserializeJsonString<WebAccessToken>();


        /// <summary>
        /// 拉取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语 </param>
        /// <returns></returns>
        public static WebUserInfo WebUserInfo(this WebAccessToken token, Language lang = Language.zh_CN) =>
            HttpClient.GetString(CoreApi.ApiUrl("/sns/userinfo?access_token={0}&openid={1}&lang={2}", token.access_token, token.openid, lang.ToString())).DeserializeJsonString<WebUserInfo>();


        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JsonResult Check(this WebAccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/sns/auth?access_token={0}&openid={1}", token.access_token, token.openid)).DeserializeJsonString<JsonResult>();
    }
}
