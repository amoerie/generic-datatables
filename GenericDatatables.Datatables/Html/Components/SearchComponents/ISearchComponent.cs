using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Html.SearchComponents
{
    public interface ISearchComponent
    {
        IHtmlString ToHtml<TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class;
    }
}
