using OYMLCN.WeChat.Enums;
#if NET461
using System.Web;
using System.Web.Mvc;
#else
using Microsoft.AspNetCore.Mvc;
#endif

namespace OYMLCN.WeChat
{
    /// <summary>
    /// AspNetMvcExtension
    /// </summary>
    public static partial class MvcExtension
    {
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this Controller controller) => controller.Request.GetPostModel();
        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static bool IsValidRequest(this Controller controller, Config cfg) => controller.Request.IsValidRequest(cfg);
        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this Controller controller, Config cfg) => controller.Request.ConfigVerify(cfg);
        /// <summary>
        /// 判断微信客户端设备类型
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static WeChatBrowserType WhichWeChatBrowser(this Controller controller) => controller.Request.WhichWeChatBrowser();       

    }
}
