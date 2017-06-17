using System.Collections.Generic;
using System.Linq;
#if !NETCOREAPP1_0
using System.Web;
using System.Net.Http;
#else
using Microsoft.AspNetCore.Http;
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
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequestMessage request) =>  PostModel.Build(request.GetQuery());
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequestBase request) =>  PostModel.Build(request.GetQuery());
#endif
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequest request) => PostModel.Build(request.GetQuery());
    }
}