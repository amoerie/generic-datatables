using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Html.SearchComponents;
using HtmlBuilders;

namespace GenericDatatables.Datatables.Html.Components.SearchComponents
{
    public class DateTimeSearchComponent: ISearchComponent
    {
        public IHtmlString ToHtml <TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class
        {
            var inputPrepend = new HtmlTag("div").Class("input-prepend");
            var addon = new HtmlTag("span").Class("add-on");
            var icon = new HtmlTag("i").Class("icon-search");
            var input = new HtmlTag("input")
                .Class("datatable-column-filter")
                .Attribute("type", "datetime")
                .Attribute("name", column.Name.Replace(" ", "."))
                .Attribute("placeholder", column.Header);

            return inputPrepend
                .Append(addon.Append(icon))
                .Append(input)
                .ToHtml();
        }
    }
}
