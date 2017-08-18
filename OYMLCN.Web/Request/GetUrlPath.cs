﻿#if NET452
using System.Web;
#else
using Microsoft.AspNetCore.Http;
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
        /// 获取域名以后的路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUrlPath(this HttpRequestBase request) => request.Url.AbsolutePath;
#endif
        /// <summary>
        /// 获取域名以后的路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUrlPath(this HttpRequest request)
        {
#if NET452
            return request.Url.AbsolutePath;
#else
            return request.Path.Value;
#endif
        }
    }
}
