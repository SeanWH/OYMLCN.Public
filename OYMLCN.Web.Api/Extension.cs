using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;

namespace OYMLCN
{
    /// <summary>
    /// RequestExtension
    /// </summary>
    public static partial class WebRequestApiExtension
    {
        /// <summary>
        /// 获取域名以后的路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUrlPath(this HttpRequestMessage request) => request.RequestUri.AbsolutePath;
        /// <summary>
        /// 获取浏览器UserAgent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpRequestMessage request) => HttpContext.Current.Request.UserAgent;
        /// <summary>
        /// 获取请求参数集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQuery(this HttpRequestMessage request) => HttpContext.Current.Request.GetQuery();
        /// <summary>
        /// 获取指定键的请求参数值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetQuery(this HttpRequestMessage request, string key) => request.GetQuery()[key];
        /// <summary>
        /// 获取请求正文内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Stream GetBody(this HttpRequestMessage request) => request.Content.ReadAsStreamAsync().Result;
        /// <summary>
        /// 获取用户客户端请求的Ip地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserIpAddress(this HttpRequestMessage request) => HttpContext.Current.Request.UserHostAddress;

    }
}
