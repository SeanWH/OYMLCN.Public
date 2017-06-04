#if !NETCOREAPP1_0
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif
using System.IO;

namespace OYMLCN
{
    /// <summary>
    /// RequestExtension
    /// </summary>
    public static partial class RequestExtension
    {
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取请求正文内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Stream GetBody(this HttpRequestBase request) => request.InputStream;
#endif
        /// <summary>
        /// 获取请求正文内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Stream GetBody(this HttpRequest request)
        {
#if !NETCOREAPP1_0
            return request.InputStream;
#else
            return request.Body;
#endif
        }
    }
}
