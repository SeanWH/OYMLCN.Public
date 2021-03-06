using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.Mvc.Routing;

namespace OYMLCN.AspNetCore
{
    /// <summary>
    /// Controller
    /// </summary>
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        /// <summary>
        /// 用户真实IP地址
        /// </summary>
        public IPAddress RequestSourceIP =>
            HttpContext.Request.Headers["X-Tencent-Ua"].Contains("Qcloud") && HttpContext.Request.Headers.ContainsKey("X-Forwarded-For") ?
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
        public string UserId => User.Claims.Where(d => d.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

        /// <summary>
        /// 用户登陆
        /// 需要在 Startup 中配置Session及Cookie基本信息
        /// services.AddSessionAndCookie();
        /// app.UseAuthentication();
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userId">用户ID</param>
        /// <param name="role">用户角色</param>
        /// <param name="claims">其他标识</param>
        public void UserSignIn(string userName, long userId, string[] role = null, params Claim[] claims)
        {
            var newClaims = claims.ToList();
            newClaims.Add(new Claim(ClaimTypes.Name, userName));
            if (role.IsNotEmpty())
                newClaims.Add(new Claim(ClaimTypes.Role, role?.Join(",")));
            newClaims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(newClaims, CookieAuthenticationDefaults.AuthenticationScheme)
                        )
                    )
                ).Wait();
        }
        /// <summary>
        /// 注销登陆
        /// </summary>
        public void UserSignOut() => HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

        /// <summary>
        /// 上一路径
        /// </summary>
        public string RefererPath => (Request.Headers as FrameRequestHeaders).HeaderReferer.FirstOrDefault()?.ToUri()?.AbsolutePath;
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

        UrlHelper urlHelper;
        /// <summary>
        /// UrlHelper
        /// </summary>
        public UrlHelper UrlHelper => urlHelper ?? (urlHelper = new UrlHelper(ControllerContext));
    }
}