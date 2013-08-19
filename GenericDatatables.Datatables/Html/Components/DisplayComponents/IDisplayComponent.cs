using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Html.Components.DisplayComponents
{
    public interface IDisplayComponent<TProperty>
    {
        IHtmlString ToHtml<TEntity>(TEntity entity, IDatatableColumn<TEntity, TProperty> datatableColumn) where TEntity : class;
    }
}
