using System.Collections.Generic;
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
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this ApiController controller) => controller.Request.GetPostModel();
#endif
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this Controller controller) => controller.Request.GetPostModel();
    }
}
