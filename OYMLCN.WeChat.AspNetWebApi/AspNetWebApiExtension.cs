using OYMLCN.WeChat.Enums;
using System;
using System.Net.Http;
using System.Web.Http;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// AspNetWebApiExtension
    /// </summary>
    public static partial class AspNetWebApiExtension
    {


        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this ApiController controller) => controller.Request.GetPostModel();
        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static bool IsValidRequest(this ApiController controller, Config cfg) => controller.Request.IsValidRequest(cfg);
        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this ApiController controller, Config cfg) => controller.Request.ConfigVerify(cfg);
        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static WeChatBrowserType WhichWeChatBrowser(this ApiController controller) => controller.Request.WhichWeChatBrowser();




        /// <summary>
        /// 获取基本的请求信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostModel GetPostModel(this HttpRequestMessage request) => PostModel.Build(request.GetQuery());
        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static bool IsValidRequest(this HttpRequestMessage request, Config cfg)
        {
            var model = request.GetPostModel();
            return Signature.Create(model.Timestamp, model.Nonce, cfg.Token) == model.Signature;
        }
        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this HttpRequestMessage request, Config cfg) => request.IsValidRequest(cfg) ? request.GetQuery()["echostr"].ToString() : string.Empty;
        static WeChatBrowserType GetWeChatBrowserType(this string userAgent)
        {
            if (userAgent.Contains("micromessenger"))
                return userAgent.Contains("iphone") ? WeChatBrowserType.iPhone :
                        userAgent.Contains("ipad") ? WeChatBrowserType.iPad :
                        userAgent.Contains("android") ? WeChatBrowserType.Android :
                        WeChatBrowserType.Windows;
            return WeChatBrowserType.None;
        }
        /// <summary>
        /// 判断微信客户端设备类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>            
        public static WeChatBrowserType WhichWeChatBrowser(this HttpRequestMessage request) =>
            GetWeChatBrowserType(request.GetUserAgent());


    }
}
