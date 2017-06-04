using System;
using System.Collections.Generic;
using OYMLCN.WeChat.Model;
using System.Threading;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 数据通讯核心辅助
    /// </summary>
    public static partial class CoreApi
    {
        /// <summary>
        /// 拼接Api调用的完整地址
        /// </summary>
        /// <param name="apiStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ApiUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", Config.ApiHost, string.Format(apiStr, param));
        /// <summary>
        /// 拼接公众平台Api公开调用的完整地址
        /// </summary>
        /// <param name="apiStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string MpUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", Config.MpHost, string.Format(apiStr, param));

        private static Dictionary<string, AccessToken> tokenDic = new Dictionary<string, AccessToken>();

        /// <summary>
        /// 获取Api调用的公众号全局唯一票据
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static AccessToken GetAccessToken(Config cfg)
        {
            var result = HttpClient.GetString(ApiUrl("/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", cfg.AppId.EncodeAsUrlData(), cfg.AppSecret.EncodeAsUrlData()));
            var data = result.DeserializeJsonString<AccessToken>();
            if (data != null && data.Success)
            {
                data.Config = cfg;
                tokenDic[cfg.AppId] = data;
                return data;
            }
            else
                throw data.Error;
        }
        /// <summary>
        /// 获取Api调用的公众号全局唯一票据
        /// （单例服务自管理，分布式请传入旧凭据或直接使用旧凭据）
        /// （自管理过期时间会提前一段时间）
        /// （判断条件 3600 -> 600 || 1800 -> 300 || 300 -> 30）
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="oldToken">旧的凭据（为空则会尝试用已经获取到的未过期票据）</param>
        /// <returns></returns>
        public static AccessToken GetAccessToken(this Config cfg, AccessToken oldToken = null)
        {
            if (oldToken == null)
                oldToken = tokenDic.Where(d => d.Key == cfg.AppId).OrderByDescending(d => d.Value.GetTime).Select(d => d.Value).FirstOrDefault();
            if (oldToken != null && oldToken.ExpiresTime > DateTime.Now)
                return oldToken;
            if (Monitor.TryEnter(tokenDic, TimeSpan.FromMilliseconds(100)))
            {
                var data = GetAccessToken(cfg);
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

        /// <summary>
        /// 获取微信服务器IP地址
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IPList GetIpAdress(this AccessToken token)
        {
            var result = HttpClient.GetString(ApiUrl("/cgi-bin/getcallbackip?access_token={0}", token.access_token));
            var data = result.DeserializeJsonString<IPList>();
            if (data.Success)
                return data;
            throw data.Error;
        }

        /// <summary>
        /// 长链接转短链接
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="url">长链接</param>
        /// <returns></returns>
        public static ShortUrl LongUrlToShort(this AccessToken token, string url) =>
            HttpClient.PostJsonString(ApiUrl("/cgi-bin/shorturl?access_token={0}", token.access_token), "{\"action\":\"long2short\",\"long_url\":\"" + url + "\"}").DeserializeJsonString<ShortUrl>();



    }
}
