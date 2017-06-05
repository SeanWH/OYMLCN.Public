using OYMLCN.WeChat.Model;
using System;

namespace OYMLCN.WeChat
{
    public partial class Api
    {

        public static AccessToken GetAccessToken(string appid, string secret) =>
            ApiGet<AccessToken>("/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);

        public static AccessToken GetAccessToken(string appid, string secret, AccessToken oldToken) =>
            oldToken?.ExpiresTime < DateTime.Now ? oldToken : GetAccessToken(appid, secret);


        public static string[] GetCallbackIP(string access_token) =>
            ApiJTokenGet("/cgi-bin/getcallbackip?access_token={0}", access_token).GetStringArray("ip_list");

    }
}
