#if !NETCOREAPP1_0
using System.Web;
using System.Net.Http;
#else
using Microsoft.AspNetCore.Http;
#endif
using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetWebOauthCode(this HttpRequestMessage request) => request.GetQuery()["code"];
#endif
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
#if !NETCOREAPP1_0
        public static string GetWebOauthCode(this HttpRequestBase request) =>
#else
        public static string GetWebOauthCode(this HttpRequest request) =>
#endif
            request.GetQuery()["code"];
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static WebOauthData GetWebOauthData(this HttpRequestMessage request)
        {
            var query = request.GetQuery();
            return new WebOauthData()
            {
                Code = query["code"],
                State = query["state"]
            };
        }
#endif
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
#if !NETCOREAPP1_0
        public static WebOauthData GetWebOauthData(this HttpRequestBase request)
#else
        public static WebOauthData GetWebOauthData(this HttpRequest request)
#endif
        {
            var query = request.GetQuery();
            return new WebOauthData()
            {
                Code = query["code"],
                State = query["state"]
            };
        }
    }
}
