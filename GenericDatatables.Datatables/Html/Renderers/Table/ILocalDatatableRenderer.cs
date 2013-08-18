using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Local;

namespace GenericDatatables.Datatables.Html.Renderers.Table
{
    public interface ILocalDatatableRenderer
    {
        IHtmlString Render<TEntity>(HtmlHelper htmlHelper, LocalDatatable<TEntity> datatable, object htmlAttributes) where TEntity : class;
    }
}
