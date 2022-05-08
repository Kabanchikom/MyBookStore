using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.TagHelpers;

[HtmlTargetElement("pager", TagStructure = TagStructure.NormalOrSelfClosing)]
public class PageLinkTagHelper : TagHelper
{
    private IUrlHelperFactory _urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, object> PageUrlValues { get; set; } = new();

    public PageModel PageModel { get; set; }
    public string PageAction { get; set; }
    public int LinksCount { get; set; }
    public string FirstPageText { get; set; }
    public string LastPageText { get; set; }
    public bool PageClassesEnabled { get; set; } = false;
    public string PageClass { get; set; }
    public string PageClassNormal { get; set; }
    public string PageClassSelected { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (PageModel.PagesCount == 0)
        {
            return;
        }

        var result = new TagBuilder("div");
        var links = CreateLinks(LinksCount);
        links.ForEach(t => result.InnerHtml.AppendHtml(t));
        output.Content.AppendHtml(result.InnerHtml);
    }

    public TagBuilder CreateLink(int pageNumber, string text)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

        var tag = new TagBuilder("a");

        if (PageClassesEnabled)
        {
            tag.AddCssClass(PageClass);
            tag.AddCssClass(pageNumber == PageModel.CurrentPage
                            && !(text == FirstPageText || text == LastPageText)
                ? PageClassSelected
                : PageClassNormal);
        }

        PageUrlValues["pageNumber"] = pageNumber;

        var url = new UrlActionContext
        {
            Action = PageAction,
            Values = PageUrlValues
        };

        tag.Attributes["href"] = urlHelper.Action(url);
        tag.InnerHtml.Append(text);

        return tag;
    }

    public List<TagBuilder> CreateLinks(int maxCount)
    {
        var result = new List<TagBuilder>();

        var startIndex = Math.Max(PageModel.CurrentPage - 5, 1);
        var finishIndex = Math.Min(PageModel.CurrentPage + 5, PageModel.PagesCount);

        result.Add(CreateLink(1, FirstPageText));

        for (var i = startIndex; i <= finishIndex; i++)
        {
            result.Add(CreateLink(i, i.ToString()));
        }

        result.Add(CreateLink(PageModel.PagesCount, LastPageText));

        return result;
    }
}