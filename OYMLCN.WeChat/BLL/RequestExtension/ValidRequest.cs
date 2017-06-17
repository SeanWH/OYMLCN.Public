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
#endif

        /// <summary>
        /// 判断请求是否来自微信的有效请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
#if !NETCOREAPP1_0
        public static bool IsValidRequest(this HttpRequestBase request, Config cfg)
#else
        public static bool IsValidRequest(this HttpRequest request, Config cfg)
#endif
        {
            var model = request.GetPostModel();
            return Signature.Create(model.Timestamp, model.Nonce, cfg.Token) == model.Signature;
        }

#if !NETCOREAPP1_0
        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this HttpRequestMessage request, Config cfg) => request.IsValidRequest(cfg) ? request.GetQuery()["echostr"].ToString() : string.Empty;
#endif

        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
#if !NETCOREAPP1_0
        public static string ConfigVerify(this HttpRequestBase request, Config cfg) =>
#else
        public static string ConfigVerify(this HttpRequest request, Config cfg) =>
#endif
            request.IsValidRequest(cfg) ? request.GetQuery()["echostr"].ToString() : string.Empty;
    }
}
