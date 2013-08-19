using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Local;

namespace GenericDatatables.Datatables.Html.TableRenderers
{
    public interface ILocalDatatableRenderer
    {
        IHtmlString Render<TEntity>(HtmlHelper htmlHelper, LocalDatatable<TEntity> datatable, object htmlAttributes) where TEntity : class;
    }
}
