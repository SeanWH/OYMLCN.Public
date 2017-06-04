using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        private static Dictionary<string, AccessToken> tokenDic = new Dictionary<string, AccessToken>();

        public static AccessToken GetAccessToken(Config cfg) =>
            ApiGet<AccessToken>("/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", cfg.AppId.EncodeAsUrlData(), cfg.AppSecret.EncodeAsUrlData());
        public static AccessToken GetAccessToken(Config cfg, AccessToken oldToken = null)
        {
            if (oldToken == null)
                oldToken = tokenDic.Where(d => d.Key == cfg.AppId).OrderByDescending(d => d.Value.GetTime).Select(d => d.Value).FirstOrDefault();
            if (oldToken != null && oldToken.ExpiresTime > DateTime.Now)
                return oldToken;
            if (Monitor.TryEnter(tokenDic, TimeSpan.FromMilliseconds(100)))
            {
                var data = GetAccessToken(cfg);
                // 缓存获取凭据
                tokenDic[cfg.AppId] = data;
                // 清掉过时凭据
                var removeList = tokenDic.Where(d => d.Value.ExpiresTime < DateTime.Now).Select(d => d.Key).ToList();
                foreach (var item in removeList)
                    tokenDic.Remove(item);
                Monitor.Exit(tokenDic);
                return data;
            }
            else
                return oldToken;
        }


        public static string[] GetCallbackIP(AccessToken token) =>
            ApiJTokenGet("/cgi-bin/getcallbackip?access_token={0}", token.access_token).GetStringArray("ip_list");

    }
}
