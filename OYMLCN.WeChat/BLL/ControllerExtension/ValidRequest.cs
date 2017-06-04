#if !NETCOREAPP1_0
using System.Web.Mvc;
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
#endif

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
#if !NETCOREAPP1_0
        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static bool IsValidRequest(this ApiController controller, Config cfg) => controller.Request.IsValidRequest(cfg);
#endif
        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static bool IsValidRequest(this Controller controller, Config cfg) => controller.Request.IsValidRequest(cfg);
        
#if !NETCOREAPP1_0
        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this ApiController controller, Config cfg) => controller.Request.ConfigVerify(cfg);
#endif

        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this Controller controller, Config cfg) => controller.Request.ConfigVerify(cfg);
    }
}
