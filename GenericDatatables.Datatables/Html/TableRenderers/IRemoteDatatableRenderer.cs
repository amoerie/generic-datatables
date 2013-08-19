using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Remote;

namespace GenericDatatables.Datatables.Html.TableRenderers
{
    public interface IRemoteDatatableRenderer
    {
        IHtmlString Render<TEntity>(HtmlHelper htmlHelper, RemoteDatatable<TEntity> remoteDatatable, object htmlAttributes) where TEntity : Entity;
    }
}
