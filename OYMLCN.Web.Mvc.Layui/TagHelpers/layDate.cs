using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.Web.Mvc.Layui.TagHelpers
{
    [HtmlTargetElement("input", Attributes = "layui-laydate-value")]
    public class LayDateTagHelper : Mvc.TagHelpers.TagHelper
    {
        /// <summary>
        /// layui-nav-itemed-controller
        /// 多个用任意分隔符分割
        /// </summary>
        [HtmlAttributeName("layui-laydate-value")]
        public DateTime? Value { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("value", Value?.ToString("yyyy-MM-dd hh:mm:ss"));
            base.Process(context, output);
        }
    }

}
