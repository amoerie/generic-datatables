using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Extensions;
using HtmlBuilders;

namespace GenericDatatables.Datatables.Html.SearchComponents
{
    public class BooleanSearchComponent: ISearchComponent
    {
        public IHtmlString ToHtml <TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class
        {
            return new HtmlTag("select").Attribute("name", column.Name)
                .Class("datatable-column-filter")
                .Append(new HtmlTag("option").Attribute("value", "").Append("All"))
                .Append(new HtmlTag("option").Attribute("value", "null").Append("Not specified"))
                .Append(new HtmlTag("option").Attribute("value", "true").Append("Yes"))
                .Append(new HtmlTag("option").Attribute("value", "false").Append("No"))
                .ToHtml();
        }
    }
}
