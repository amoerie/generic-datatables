using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Local;
using HtmlBuilders;

namespace GenericDatatables.Datatables.Html.TableRenderers
{
    public class TwitterBootstrapLocalDatatableRenderer: ILocalDatatableRenderer
    {
        public IHtmlString Render <TEntity>(HtmlHelper htmlHelper, LocalDatatable<TEntity> localDatatable, object htmlAttributes) where TEntity : class
        {
            // Build HTML

            // table
            var table = new HtmlTag("table").Id(localDatatable.Id)
                .Class("table")
                .Class("table-striped");


            // thead
            var thead = new HtmlTag("thead");

            // column filters

            var trColumnFilters = new HtmlTag("tr").Class("datatable-column-filters");
            foreach (var column in localDatatable.Columns)
            {
                // don't render the search component if this column is not searchable
                if (!column.Searchable)
                {
                    trColumnFilters.Append(new HtmlTag("td"));
                }
                else
                {
                    var controls = new HtmlTag("div").Class("controls");
                    controls.Append(HtmlTag.ParseAll(column.SearchComponent.ToHtml(htmlHelper, column).ToHtmlString()));
                    trColumnFilters.Append(new HtmlTag("td").Append(controls));
                }
            }

            // column headers

            var trColumnHeaders = new HtmlTag("tr").Class("datatable-column-headers");
            foreach (var column in localDatatable.Columns)
            {
                var th = new HtmlTag("th")
                    .Attribute("data-property", column.Name)
                    .Append(column.Header)
                    .Merge(column.GetAttributes());
                trColumnHeaders.Append(th);
            }

            thead.Append(trColumnFilters);
            thead.Append(trColumnHeaders);


            //tbody
            var tbody = new HtmlTag("tbody");

            foreach (var entity in localDatatable.Entities)
            {
                var tbodyrow = new HtmlTag("tr");
                foreach (var column in localDatatable.Columns)
                {
                    tbodyrow.Append(new HtmlTag("td").Append(column.DisplayFunction(entity)));
                }
                tbody.Append(tbodyrow);
            }

            table.Merge(htmlAttributes)
                .Append(thead)
                .Append(tbody);

            return table.ToHtml();
        }
    }
}
