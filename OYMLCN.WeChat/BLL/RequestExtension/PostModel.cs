using System.Collections.Generic;
using OYMLCN.WeChat.Model;
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
        private static PostModel FillPostModel(this Dictionary<string, string> param)
        {
            var model = new PostModel();
            model.Nonce = param.Where(d => d.Key == "nonce").Select(d => d.Value).FirstOrDefault();
            model.Signature = param.Where(d => d.Key == "signature").Select(d => d.Value).FirstOrDefault();
            model.Timestamp = param.Where(d => d.Key == "timestamp").Select(d => d.Value).FirstOrDefault();
            model.OpenId = param.Where(d => d.Key == "openid").Select(d => d.Value).FirstOrDefault();
            model.MsgSignature = param.Where(d => d.Key == "msg_signature").Select(d => d.Value).FirstOrDefault();
            return model;
        }
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequestMessage request) => request.GetQuery().FillPostModel();
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequestBase request) => request.GetQuery().FillPostModel();
#endif
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequest request) => request.GetQuery().FillPostModel();
    }
}