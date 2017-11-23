using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using OYMLCN.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// StartupConfigureExtension
    /// </summary>
    public static class StartupConfigureExtension
    {
        /// <summary>
        /// 一句配置Session和登陆Cookie 需在Configure中加入 app.UseAuthentication() 以使得登陆配置生效 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sessionTimeOutHours">Session过期回收时间（默认2小时）</param>
        /// <param name="loginPath">用户登陆路径</param>
        /// <param name="accessDeniedPath">禁止访问路径，不设置则回到登陆页</param>
        /// <param name="returnUrlParameter">上一页面地址回传参数</param>
        /// <param name="cookieDomain">Cookie作用域</param>
        /// <param name="securePolicy">Cookie安全策略</param>
        /// <returns></returns>
        public static IServiceCollection AddSessionAndCookie(this IServiceCollection services, 
            double sessionTimeOutHours = 2, 
            string loginPath = "/Account/Login",
            string accessDeniedPath = null, 
            string returnUrlParameter = "ReturnUrl", 
            string cookieDomain = null, 
            CookieSecurePolicy securePolicy = CookieSecurePolicy.SameAsRequest)
        {
            services.AddMemoryCache();
            services
                .AddSession(options =>
                {
                    var cookie = options.Cookie;
                    cookie.HttpOnly = true;
                    cookie.SecurePolicy = securePolicy;
                    options.IdleTimeout = TimeSpan.FromHours(sessionTimeOutHours);
                })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString(loginPath);
                    options.ReturnUrlParameter = returnUrlParameter;
                    options.AccessDeniedPath = new PathString(accessDeniedPath ?? loginPath);

                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = securePolicy;
                    options.Cookie.Domain = cookieDomain;
                });

            return services;
        }

        /// <summary>
        /// 开启腾讯CDN加速请求头识别
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseQcloudForwardedHeaders(this IApplicationBuilder app) =>
              app.UseForwardedHeaders(new ForwardedHeadersOptions
              {
                  ForwardedForHeaderName = "X-Forwarded-For",
                  ForwardedProtoHeaderName = "X-Forwarded-Proto",
                  OriginalForHeaderName = "X-Original-For",
                  OriginalProtoHeaderName = "X-Original-Proto",
                  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
              });

        /// <summary>
        /// 注入所有扩展模块
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddExtensions(this IServiceCollection services)
        {
            services
                .AddSingleton<EmailSender>()
                .AddScoped<ViewRender>();
            return services;
        }

    }
}
