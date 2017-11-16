using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OYMLCN.Web.Layui
{
    [HtmlTargetElement("li", Attributes = "layui-this-controller")]
    [HtmlTargetElement("dd", Attributes = "layui-this-controller,layui-this-action")]
    public class LayuiThisTagHelper : TagHelper
    {
        [HtmlAttributeName("layui-this-controller")]
        public string Controller { get; set; }
        [HtmlAttributeName("layui-this-action")]
        public string Action { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsEqualController(Controller) && (Action.IsNullOrEmpty() || IsEqualAction(Action)))
                output.AddClass("layui-this");
            base.Process(context, output);
        }

    }

    [HtmlTargetElement("a", Attributes = "layui-this-controller")]
    public class AnchorLayuiThisTagHelper : LayuiThisTagHelper
    {
        IHtmlGenerator Generator;
        public AnchorLayuiThisTagHelper(IHtmlGenerator generator) => Generator = generator;
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
