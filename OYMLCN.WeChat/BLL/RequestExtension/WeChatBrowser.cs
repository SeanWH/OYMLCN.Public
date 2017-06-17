#if !NETCOREAPP1_0
using System.Web;
using System.Net.Http;
#else
using Microsoft.AspNetCore.Http;
#endif
using OYMLCN.WeChat.Enums;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        static WeChatBrowserType GetWeChatBrowserType(this string userAgent)
        {
            if (userAgent.Contains("micromessenger"))
                return userAgent.Contains("iphone") ? WeChatBrowserType.iPhone :
                        userAgent.Contains("ipad") ? WeChatBrowserType.iPad :
                        userAgent.Contains("android") ? WeChatBrowserType.Android :
                        WeChatBrowserType.Windows;
            return WeChatBrowserType.None;
        }
#if !NETCOREAPP1_0
        /// <summary>
        /// 判断微信客户端设备类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>            
        public static WeChatBrowserType WhichWeChatBrowser(this HttpRequestMessage request) =>
            request.GetUserAgent().GetWeChatBrowserType();
#endif
        /// <summary>
        /// 判断微信客户端设备类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
#if !NETCOREAPP1_0
        public static WeChatBrowserType WhichWeChatBrowser(this HttpRequestBase request) =>
#else
        public static WeChatBrowserType WhichWeChatBrowser(this HttpRequest request) =>
#endif
            request.GetUserAgent().GetWeChatBrowserType();
    }
}
