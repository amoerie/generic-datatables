using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Remote;

namespace GenericDatatables.Datatables.Html.Renderers.Table
{
    public class TwitterBootstrapRemoteDatatableRenderer: IRemoteDatatableRenderer
    {
        public IHtmlString Render <TEntity>(HtmlHelper htmlHelper, RemoteDatatable<TEntity> remoteDatatable, object htmlAttributes) where TEntity : Entity
        {
            // Build HTML

            // table
            var table = new TagBuilder("table").Attribute("id", remoteDatatable.Id)
                .Attribute("data-url", remoteDatatable.Url)
                .Class("table")
                .Class("table-striped");


            // thead
            var thead = new TagBuilder("thead");

            // column filters

            var trColumnFilters = new TagBuilder("tr").Class("datatable-column-filters");
            foreach (var column in remoteDatatable.Columns)
            {
                // don't render the search component if this column is not searchable
                if (!column.Searchable)
                {
                    trColumnFilters.AppendHtml(new TagBuilder("td"));
                }
                else
                {
                    var controls = new TagBuilder("div").Class("controls");
                    controls.Html(column.SearchComponent.ToHtml(htmlHelper, column));
                    trColumnFilters.AppendHtml(new TagBuilder("td").Html(controls));
                }
            }

            // column headers

            var trColumnHeaders = new TagBuilder("tr").Class("datatable-column-headers");
            foreach (var column in remoteDatatable.Columns)
            {
                var th = new TagBuilder("th")
                    .Attribute("data-property", column.Name)
                    .Html(column.Header);
                column.SetAttributes(th);
                trColumnHeaders.AppendHtml(th);
            }

            thead.AppendHtml(trColumnFilters);
            thead.AppendHtml(trColumnHeaders);


            //tbody
            var tbody = new TagBuilder("tbody");

            table.Merge(htmlAttributes)
                .AppendHtml(thead)
                .AppendHtml(tbody);

            return table.ToHtml();
        }
    }
}
