#if !NET461
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN
{
    public class TagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public bool IsEqualController(string controller) => controller.IsEqual(ViewContext.RouteData.Values["controller"]?.ToString(), StringComparison.OrdinalIgnoreCase);
        public bool IsEqualAction(string action) => action.IsEqual(ViewContext.RouteData.Values["action"]?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public static class TagHelperExtension
    {
        public static void AddClass(this TagHelperOutput output, string className)
        {
            var pre = output.Attributes.Where(d => d.Name.Equals("class", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (pre != null)
                output.Attributes.Remove(pre);
            output.Attributes.Add("class", $"{pre?.Value?.ToString()} {className}".Trim());
        }
    }
}
#endif