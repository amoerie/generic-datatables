using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Remote;

namespace GenericDatatables.Datatables.Html.Renderers.Table
{
    public interface IRemoteDatatableRenderer
    {
        IHtmlString Render<TEntity>(HtmlHelper htmlHelper, RemoteDatatable<TEntity> remoteDatatable, object htmlAttributes) where TEntity : Entity;
    }
}
