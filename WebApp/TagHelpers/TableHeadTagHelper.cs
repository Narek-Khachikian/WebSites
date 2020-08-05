using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace WebApp.TagHelpers
{
    [HtmlTargetElement("tablehead")]
    public class TableHeadTagHelper : TagHelper
    {
        public string BgColor { get; set; } = "light";
        public string TextColor { get; set; } = "dark";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-{TextColor} text-center");
            string content = (await output.GetChildContentAsync()).GetContent();
            output.Content
            .SetHtmlContent($"<tr><th colspan=\"2\">{content}</th></tr>");
        }
    }
}
