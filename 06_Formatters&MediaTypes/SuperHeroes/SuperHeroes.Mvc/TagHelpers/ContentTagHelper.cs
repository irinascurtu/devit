using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SuperHeroes.Mvc.TagHelpers
{
    public class ContentTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            output.PreElement.SetHtmlContent("<section>Pre Element</section>");
            output.PreContent.SetHtmlContent("<div>Pre Content</div>");
            output.Content.SetHtmlContent("<div>Actual content Content</div>");
            output.PostContent.SetHtmlContent("<div>Post Content</div>");
            output.PostElement.SetHtmlContent("<section>Post Element</section>");
        }
    }
}
