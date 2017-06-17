using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 网页授权获取用户基本信息的通讯及逻辑辅助
    /// </summary>
    public static class WebApi
    {
        /// <summary>
        /// 创建静默授权并自动跳转到回调页的授权Url
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="redirectUrl">回掉页面</param>
        /// <param name="state">重定向后会带上state参数，可以填写a-zA-Z0-9的参数值，最多128字节 </param>
        /// <returns></returns>
        public static string WebUrlScopeBase(this Config cfg, string redirectUrl, string state = null) =>
            Api.Oauth2.ScopeBaseUrl(cfg.AppId, redirectUrl, state);
        /// <summary>
        /// 创建发起的网页授权并自动跳转到回调页的授权Url
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="redirectUrl">回掉页面</param>
        /// <param name="state">重定向后会带上state参数，可以填写a-zA-Z0-9的参数值，最多128字节 </param>
        /// <returns></returns>
        public static string WebUrlScopeUserInfo(this Config cfg, string redirectUrl, string state = null) =>
            Api.Oauth2.ScopeUserInfoUrl(cfg.AppId, redirectUrl, state);


        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="code">获取到的code参数</param>
        /// <returns></returns>
        public static Oauth2AccessToken Oauth2AccessTokenGet(this Config cfg, string code) =>
            Api.Oauth2.GetAccessToken(cfg.AppId, cfg.AppSecret, code);
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="token">网页授权的access_token</param>
        /// <returns></returns>
        public static Oauth2AccessToken Oauth2AccessTokenRefresh(this Config cfg, Oauth2AccessToken token) => cfg.Oauth2AccessTokenRefresh(token.refresh_token);
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static Oauth2AccessToken Refresh(this Oauth2AccessToken token, Config cfg) => cfg.Oauth2AccessTokenRefresh(token);
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="refreshToken">通过access_token获取到的refresh_token参数</param>
        /// <returns></returns>
        public static Oauth2AccessToken Oauth2AccessTokenRefresh(this Config cfg, string refreshToken) =>
            Api.Oauth2.RefreshAccessToken(cfg.AppId, refreshToken);

        /// <summary>
        /// 拉取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语 </param>
        /// <returns></returns>
        public static Oauth2UserInfo WebUserInfo(this Oauth2AccessToken token, Language lang = Language.zh_CN) =>
            Api.Oauth2.GetUserInfo(token.access_token, token.openid, lang);

        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JsonResult Check(this Oauth2AccessToken token) =>
            Api.Oauth2.AuthAccessToken(token.access_token, token.openid);
    }
}
