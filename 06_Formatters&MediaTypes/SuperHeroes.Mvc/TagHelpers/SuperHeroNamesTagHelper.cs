using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SuperHeroese.Data;

namespace SuperHeroes.Mvc.TagHelpers
{
    ///<summary>
    /// Renders a select with superheroes
    /// </summary>
    [HtmlTargetElement("super-hero", ParentTag = "li", Attributes = "[first-name='clark'], last-name")]
    public class SuperHeroNamesTagHelper : TagHelper
    {
        private readonly ISuperHeroesRepository superHeroesRepository;

        public SuperHeroNamesTagHelper(ISuperHeroesRepository superHeroesRepository)
        {
            this.superHeroesRepository = superHeroesRepository;
        }

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            output.TagName = "select";
            output.Attributes.SetAttribute("class", "awesome-select");

            var names = superHeroesRepository
                        .GetHeroNamesAndIds()
                        .OrderBy(x => x.Nickname);

            foreach (var name in names)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };
                option.Attributes.Add("value", name.Id.ToString());
                option.InnerHtml.Append(name.Nickname);
                output.Content.AppendHtml(option);
            }

        }

    }
}
