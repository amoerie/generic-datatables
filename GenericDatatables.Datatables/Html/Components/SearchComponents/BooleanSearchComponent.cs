using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Extensions;

namespace GenericDatatables.Datatables.Html.SearchComponents
{
    public class BooleanSearchComponent: ISearchComponent
    {
        public IHtmlString ToHtml <TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class
        {
            return new TagBuilder("select").Attribute("name", column.Name)
                .Class("datatable-column-filter")
                .AppendHtml(new TagBuilder("option").Attribute("value", "").Html("All"))
                .AppendHtml(new TagBuilder("option").Attribute("value", "null").Html("Not specified"))
                .AppendHtml(new TagBuilder("option").Attribute("value", "true").Html("Yes"))
                .AppendHtml(new TagBuilder("option").Attribute("value", "false").Html("No"))
                .ToHtml();
        }
    }
}
