using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Oauth2
        {
            public static string ApiUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", OpenHost, string.Format(apiStr, param));

            public static string ScopeBaseUrl(Config config, string redirectUrl, string state = null) =>
                ApiUrl("/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect", config.AppId, redirectUrl.UrlEncode(), state);
            public static string ScopeUserInfoUrl(Config config, string redirectUrl, string state = null) =>
                ApiUrl("/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", config.AppId, redirectUrl.UrlEncode(), state);

            public static Oauth2AccessToken GetAccessToken(Config config, string code) =>
                ApiGet<Oauth2AccessToken>("/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", config.AppId, config.AppSecret, code);
            public static Oauth2AccessToken RefreshAccessToken(Config config, Oauth2AccessToken token) => RefreshAccessToken(config, token.refresh_token);
            public static Oauth2AccessToken RefreshAccessToken(Config config, string refreshToken) =>
                ApiGet<Oauth2AccessToken>("/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", config.AppId, refreshToken);

            public static Oauth2UserInfo GetUserInfo(Oauth2AccessToken token, Language lang = Language.zh_CN) =>
                ApiGet<Oauth2UserInfo>("/sns/userinfo?access_token={0}&openid={1}&lang={2}", token.access_token, token.openid, lang.ToString());

            public static JsonResult AuthAccessToken(Oauth2AccessToken token) =>
                ApiGet<JsonResult>("/sns/auth?access_token={0}&openid={1}", token.access_token, token.openid);
        }
    }
}
