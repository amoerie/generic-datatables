using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Extensions;

namespace GenericDatatables.Datatables.Html.SearchComponents
{
    public class TextSearchComponent: ISearchComponent
    {
        public IHtmlString ToHtml<TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class
        {
            var inputPrepend = new TagBuilder("div").Class("input-prepend");
            var addon = new TagBuilder("span").Class("add-on");
            var icon = new TagBuilder("i").Class("icon-search");
            var input = new TagBuilder("input")
                .Class("datatable-column-filter")
                .Attribute("type", "text")
                .Attribute("name", column.Name.Replace(" ", "."))
                .Attribute("placeholder", column.Header);

            return inputPrepend
                .AppendHtml(addon.Html(icon))
                .AppendHtml(input)
                .ToHtml();
        }
    }
}
