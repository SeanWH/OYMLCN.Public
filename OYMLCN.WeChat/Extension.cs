using OYMLCN.WeChat.Enums;
using System.Collections.Generic;
#if NET461
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace OYMLCN.WeChat
{
    /// <summary>
    /// AspNetMvcExtension
    /// </summary>
    public static partial class Extension
    {
#if NET461
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


        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
#if NET461
        public static bool IsValidRequest(this HttpRequestBase request, Config cfg)
#else
        public static bool IsValidRequest(this HttpRequest request, Config cfg)
#endif
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
#if NET461
        public static string ConfigVerify(this HttpRequestBase request, Config cfg) =>
#else
        public static string ConfigVerify(this HttpRequest request, Config cfg) =>
#endif
            request.IsValidRequest(cfg) ? request.GetQuery()["echostr"].ToString() : string.Empty;


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
#if NET461
        public static WeChatBrowserType WhichWeChatBrowser(this HttpRequestBase request) =>
#else
        public static WeChatBrowserType WhichWeChatBrowser(this HttpRequest request) =>
#endif
            request.GetUserAgent().GetWeChatBrowserType();



    }
}
