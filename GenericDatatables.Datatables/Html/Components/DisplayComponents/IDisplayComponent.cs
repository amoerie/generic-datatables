using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Html.Components.DisplayComponents
{
    public interface IDisplayComponent
    {
        object PropertyValue { get; set; }
        IHtmlString ToHtml<TEntity>(HtmlHelper htmlHelper, TEntity entity, IDatatableColumn<TEntity> column) where TEntity : class;
    }
}
