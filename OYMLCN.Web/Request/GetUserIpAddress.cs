#if NET452
using System.Web;
#else
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
#endif

namespace OYMLCN
{
    /// <summary>
    /// RequestExtension
    /// </summary>
    public static partial class RequestExtension
    {
#if NET452
        /// <summary>
        /// 获取用户客户端请求的Ip地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserIpAddress(this HttpRequestBase request) => request.UserHostAddress;
#endif
        /// <summary>
        /// 获取用户客户端请求的Ip地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserIpAddress(this HttpRequest request)
        {
#if NET452
            return request.UserHostAddress;
#else
            return request.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
#endif
        }
    }
}
