using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Oauth2
        {
            private static string ApiUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", OpenHost, string.Format(apiStr, param));

            public static string ScopeBaseUrl(string appid, string redirectUrl, string state = null) =>
                ApiUrl("/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect", appid, redirectUrl.UrlEncode(), state);
            public static string ScopeUserInfoUrl(string appid, string redirectUrl, string state = null) =>
                ApiUrl("/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, redirectUrl.UrlEncode(), state);
            public static Oauth2AccessToken GetAccessToken(string appid, string secret, string code) =>
                ApiGet<Oauth2AccessToken>("/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            public static Oauth2AccessToken RefreshAccessToken(string appid, string refreshToken) =>
                ApiGet<Oauth2AccessToken>("/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", appid, refreshToken);
            public static Oauth2UserInfo GetUserInfo(string access_token, string openid, Language lang = Language.zh_CN) =>
                ApiGet<Oauth2UserInfo>("/sns/userinfo?access_token={0}&openid={1}&lang={2}", access_token, openid, lang.ToString());
            public static JsonResult AuthAccessToken(string access_token, string openid) =>
                ApiGet<JsonResult>("/sns/auth?access_token={0}&openid={1}", access_token, openid);
        }
    }
}
