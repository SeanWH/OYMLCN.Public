using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.Tencent.Cloud.TagHelpers
{
    /// <summary>
    /// COSImageTagHelper
    /// </summary>
    [HtmlTargetElement("img", Attributes = "cos-src")]
    public class COSImageTagHelper : TagHelper
    {
        string COS_Url { get; set; }
        /// <summary>
        /// COSImageTagHelper
        /// </summary>
        /// <param name="configuration"></param>
        public COSImageTagHelper(IConfiguration configuration) =>
            COS_Url = configuration.GetValue<string>("TencentCloud:COSImage")?.TrimEnd('/');
        IConfiguration Configuration { get; set; }

        /// <summary>
        /// 若要使用，请在 appsettings 配置文件中配置 string TencentCloud:COSImage 参数 
        /// </summary>
        [HtmlAttributeName("cos-src")]
        public string Attribute { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.RemoveAttribute("src");
            output.Attributes.Add("src", $"{COS_Url}/{Attribute.TrimStart('/')}");
            base.Process(context, output);
        }
    }
}
