using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System;

namespace OYMLCN.Web.Mvc
{
    /// <summary>
    /// Controller
    /// </summary>
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        /// <summary>
        /// 是否来自腾讯云CDN加速服务
        /// </summary>
        public bool IsQcloudCDNRequest => HttpContext.Request.Headers["X-Tencent-Ua"].Contains("Qcloud");
        /// <summary>
        /// 用户真实IP地址
        /// </summary>
        public IPAddress RequestSourceIP =>
            IsQcloudCDNRequest ?
                IPAddress.Parse(HttpContext.Request.Headers["X-Forwarded-For"]) :
                (Request.HttpContext.Features?.Get<IHttpConnectionFeature>()?.RemoteIpAddress ?? HttpContext.Connection.RemoteIpAddress);

        /// <summary>
        /// 登陆标识
        /// </summary>
        public bool IsAuthenticated => User.Identity.IsAuthenticated;
        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string UserName => User.Identity.Name;
        /// <summary>
        /// 登陆用户唯一标识
        /// </summary>
        public long UserId => User.Claims.Where(d => d.Type == ClaimTypes.NameIdentifier).Select(d => d.Value.ConvertToLong()).FirstOrDefault();

        /// <summary>
        /// 请求域名
        /// </summary>
        public string RequestHost => Request.Host.Value;
        /// <summary>
        /// 请求路径
        /// </summary>
        public string RequestPath => Request.Path.Value;
        /// <summary>
        /// 请求标识
        /// </summary>
        public string RequestUserAgent => Request.Headers["User-Agent"];

        private Dictionary<string, string> requestQueryParams;
        /// <summary>
        /// 请求参数集合
        /// </summary>
        public Dictionary<string, string> RequestQueryParams
        {
            get
            {
                if (requestQueryParams.IsNotNull())
                    return requestQueryParams;

                requestQueryParams = new Dictionary<string, string>();
                foreach (var item in Request.Query)
                    requestQueryParams[item.Key] = item.Value;

                if (Request.Method != "POST")
                    return requestQueryParams;
                else
                {
                    if (Request.Form.IsNotEmpty())
                        foreach (var item in Request.Form)
                            requestQueryParams[item.Key] = item.Value;
                    else if (Request.ContentType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
                        foreach (var item in Request.Body.ReadToEnd().SplitBySign("&"))
                        {
                            var query = item.SplitBySign("=");
                            requestQueryParams.Add(query.FirstOrDefault(), query.Skip(1).FirstOrDefault());
                        }
                    return requestQueryParams;
                }
            }
        }


    }
}