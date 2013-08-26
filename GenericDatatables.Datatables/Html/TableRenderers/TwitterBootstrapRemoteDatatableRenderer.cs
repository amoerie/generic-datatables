using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Remote;
using HtmlBuilders;

namespace GenericDatatables.Datatables.Html.TableRenderers
{
    public class TwitterBootstrapRemoteDatatableRenderer: IRemoteDatatableRenderer
    {
        public IHtmlString Render <TEntity>(HtmlHelper htmlHelper, RemoteDatatable<TEntity> remoteDatatable, object htmlAttributes) where TEntity : Entity
        {
            // Build HTML

            // table
            var table = new HtmlTag("table").Id(remoteDatatable.Id)
                .Data("url", remoteDatatable.Url)
                .Class("table table-striped");


            // thead
            var thead = new HtmlTag("thead");

            // column filters

            var trColumnFilters = new HtmlTag("tr").Class("datatable-column-filters");
            foreach (var column in remoteDatatable.Columns)
            {
                // don't render the search component if this column is not searchable
                if (!column.Searchable)
                {
                    trColumnFilters.Append(new HtmlTag("td"));
                }
                else
                {
                    var controls = new HtmlTag("div").Class("controls");
                    controls.Append(HtmlTag.ParseAll(column.SearchComponent.ToHtml(htmlHelper, column)));
                    trColumnFilters.Append(new HtmlTag("td").Append(controls));
                }
            }

            // column headers

            var trColumnHeaders = new HtmlTag("tr").Class("datatable-column-headers");
            foreach (var column in remoteDatatable.Columns)
            {
                var th = new HtmlTag("th")
                    .Data("property", column.Name)
                    .Append(column.Header)
                    .Merge(column.GetAttributes());
                trColumnHeaders.Append(th);
            }

            thead.Append(trColumnFilters);
            thead.Append(trColumnHeaders);


            //tbody
            var tbody = new HtmlTag("tbody");

            table.Merge(htmlAttributes)
                .Append(thead)
                .Append(tbody);

            return table.ToHtml();
        }
    }
}
