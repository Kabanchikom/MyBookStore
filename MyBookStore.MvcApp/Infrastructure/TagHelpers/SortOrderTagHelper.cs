using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Data.SqlClient;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.TagHelpers;

[HtmlTargetElement("sorter", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SortOrderTagHelper : TagHelper
{
    private IUrlHelperFactory _urlHelperFactory;

    public SortOrderTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, object> UrlValues { get; set; } = new();

    public SortModel SortModel { get; set; }

    public string Action { get; set; }
    public string ButtonClass { get; set; }
    public string AscIconClass { get; set; }
    public string DescIconClass { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var result = new TagBuilder("div");

        var asc = CreateButton(SortOrder.Ascending);
        var desc = CreateButton(SortOrder.Descending);

        result.InnerHtml.AppendHtml(asc);
        result.InnerHtml.AppendHtml(desc);

        output.Content.AppendHtml(result.InnerHtml);
    }

    private TagBuilder CreateButton(SortOrder sortOrder)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
        var tag = new TagBuilder("a");

        UrlValues["sortOrder"] = sortOrder;

        var url = new UrlActionContext
        {
            Action = Action,
            Values = UrlValues
        };

        tag.Attributes["href"] = urlHelper.Action(url);
        tag.AddCssClass(ButtonClass);

        if (SortModel.SortOrder == sortOrder)
        {
            tag.AddCssClass("active");
        }

        if (sortOrder == SortOrder.Ascending)
        {
            var i = new TagBuilder("i");
            i.AddCssClass(AscIconClass);
            tag.InnerHtml.AppendHtml(i);
        }

        if (sortOrder == SortOrder.Descending)
        {
            var i = new TagBuilder("i");
            i.AddCssClass(DescIconClass);
            tag.InnerHtml.AppendHtml(i);
        }

        return tag;
    }
}