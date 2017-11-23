using Microsoft.Extensions.DependencyInjection;
using OYMLCN.Tencent.Cloud;
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
        /// 注入腾讯云服务SDK
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTencentCloudSingletons(this IServiceCollection services)
        {
            services
                .AddSingleton<CosCloud>();
            return services;
        }
    }
}
