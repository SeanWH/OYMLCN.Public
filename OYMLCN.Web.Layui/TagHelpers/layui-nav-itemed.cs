using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OYMLCN.Web.Layui
{
    [HtmlTargetElement("li", Attributes = "layui-nav-itemed-controller")]
    public class LayuiNavItemedTagHelper : TagHelper
    {
        [HtmlAttributeName("layui-nav-itemed-controller")]
        public string Controller { get; set; }
        [HtmlAttributeName("layui-nav-itemed-action")]
        public string Action { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsEqualController(Controller) && (Action.IsNullOrEmpty() || IsEqualAction(Action)))
                output.AddClass("layui-nav-itemed");
            base.Process(context, output);
        }
    }
}
