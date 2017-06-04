#if !NETCOREAPP1_0
using System.Web.Mvc;
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using OYMLCN.WeChat.Enum;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {       
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static WeChatBrowserType WhichWeChatBrowser(this ApiController controller) => controller.Request.WhichWeChatBrowser();
#endif
        /// <summary>
        /// 判断微信客户端设备类型
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static WeChatBrowserType WhichWeChatBrowser(this Controller controller) => controller.Request.WhichWeChatBrowser();
    }
}
