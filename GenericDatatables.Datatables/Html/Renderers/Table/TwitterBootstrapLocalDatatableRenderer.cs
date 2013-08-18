using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Local;

namespace GenericDatatables.Datatables.Html.Renderers.Table
{
    public class TwitterBootstrapLocalDatatableRenderer: ILocalDatatableRenderer
    {
        public IHtmlString Render <TEntity>(HtmlHelper htmlHelper, LocalDatatable<TEntity> localDatatable, object htmlAttributes) where TEntity : class
        {
            // Build HTML

            // table
            var table = new TagBuilder("table").Attribute("id", localDatatable.Id)
                .Class("table")
                .Class("table-striped");


            // thead
            var thead = new TagBuilder("thead");

            // column filters

            var trColumnFilters = new TagBuilder("tr").Class("datatable-column-filters");
            foreach (var column in localDatatable.Columns)
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
            foreach (var column in localDatatable.Columns)
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

            foreach (var entity in localDatatable.Entities)
            {
                var tbodyrow = new TagBuilder("tr");
                foreach (var column in localDatatable.Columns)
                {
                    tbodyrow.AppendHtml(new TagBuilder("td").Html(column.DisplayComponent.ToHtml(htmlHelper, entity, column)));
                }
                tbody.AppendHtml(tbodyrow);
            }

            table.Merge(htmlAttributes)
                .AppendHtml(thead)
                .AppendHtml(tbody);

            return table.ToHtml();
        }
    }
}
