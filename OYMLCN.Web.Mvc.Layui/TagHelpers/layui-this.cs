using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OYMLCN.Web.Mvc.Layui
{
    /// <summary>
    /// layui-this
    /// </summary>
    [HtmlTargetElement("li", Attributes = "layui-this-controller")]
    [HtmlTargetElement("dd", Attributes = "layui-this-controller,layui-this-action")]
    public class LayuiThisTagHelper : TagHelper
    {
        /// <summary>
        /// layui-this-controller
        /// </summary>
        [HtmlAttributeName("layui-this-controller")]
        public string Controller { get; set; }
        /// <summary>
        /// layui-this-action
        /// </summary>
        [HtmlAttributeName("layui-this-action")]
        public string Action { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsEqualController(Controller) && (Action.IsNullOrEmpty() || IsEqualAction(Action)))
                output.AddClass("layui-this");
            base.Process(context, output);
        }

    }

    /// <summary>
    /// layui-this 添加 href 地址
    /// </summary>
    [HtmlTargetElement("a", Attributes = "layui-this-controller")]
    public class AnchorLayuiThisTagHelper : LayuiThisTagHelper
    {
        IHtmlGenerator Generator;
        /// <summary>
        /// AnchorLayuiThisTagHelper
        /// </summary>
        /// <param name="generator"></param>
        public AnchorLayuiThisTagHelper(IHtmlGenerator generator) => Generator = generator;

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var aHelper = new AnchorTagHelper(Generator);
            aHelper.Action = Action;
            aHelper.Controller = Controller;
            aHelper.ViewContext = ViewContext;
            aHelper.Process(context, output);
            base.Process(context, output);
        }
    }
}
