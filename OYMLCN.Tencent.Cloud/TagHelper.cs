#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using OYMLCN;

namespace Microsoft.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "cdn-src")]
    public class CDNImageHelper : TagHelper
    {
        string CDN_Url { get; set; }
        public CDNImageHelper(IConfiguration configuration) =>
            CDN_Url = configuration.GetValue<string>("TencentCloud:CDN")?.TrimEnd('/');

        /// <summary>
        /// 若要使用，请在 appsettings 配置文件中配置 string TencentCloud:CDN 参数 
        /// </summary>
        [HtmlAttributeName("cdn-src")]
        public string Attribute { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("src", $"{CDN_Url}/{Attribute.TrimStart('~', '/')}");
            base.Process(context, output);
        }
    }

    [HtmlTargetElement("img", Attributes = "cos-src")]
    public class COSImageTagHelper : TagHelper
    {
        string COS_Url { get; set; }
        public COSImageTagHelper(IConfiguration configuration) =>
            COS_Url = configuration.GetValue<string>("TencentCloud:COSImage")?.TrimEnd('/');
        IConfiguration Configuration { get; set; }

        /// <summary>
        /// 若要使用，请在 appsettings 配置文件中配置 string TencentCloud:COSImage 参数 
        /// </summary>
        [HtmlAttributeName("cos-src")]
        public string Attribute { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.RemoveAttribute("src");
            output.Attributes.Add("src", $"{COS_Url}/{Attribute.TrimStart('/')}");
            base.Process(context, output);
        }
    }
}
