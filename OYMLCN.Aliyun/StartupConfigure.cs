using Microsoft.Extensions.DependencyInjection;
using OYMLCN.Aliyun;
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
        /// 注入阿里云服务SDK
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAliyunSingletons(this IServiceCollection services)
        {
            services
                .AddSingleton<Alidayu>();
            return services;
        }
    }
}
