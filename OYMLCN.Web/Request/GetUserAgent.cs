#if NET461
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace OYMLCN
{
    /// <summary>
    /// RequestExtension
    /// </summary>
    public static partial class WebHttpRequestExtension
    {
#if NET461
        /// <summary>
        /// 获取浏览器UserAgent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpRequestBase request) => request.Headers["User-Agent"];
#endif
        /// <summary>
        /// 获取浏览器UserAgent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpRequest request) => request.Headers["User-Agent"];
    }
}
