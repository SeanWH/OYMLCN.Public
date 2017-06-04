#if !NETCOREAPP1_0
using System.Web.Mvc;
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
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
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string GetWebOauthCode(this ApiController controller) => controller.Request.GetWebOauthCode();
#endif
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string GetWebOauthCode(this Controller controller) => controller.Request.GetWebOauthCode();
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static WebOauthData GetWebOauthData(this ApiController controller) => controller.Request.GetWebOauthData();
#endif
        /// <summary>
        /// 获取网页授权需要的code
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static WebOauthData GetWebOauthData(this Controller controller) => controller.Request.GetWebOauthData();
    }
}
