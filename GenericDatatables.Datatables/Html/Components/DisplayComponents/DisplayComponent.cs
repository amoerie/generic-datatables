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
    public abstract class PropertyDisplayComponent<TProperty>: IDisplayComponent
    {
        public object PropertyValue { get; set; }

        protected abstract IHtmlString ToHtml<TEntity>(HtmlHelper htmlHelper,TEntity entity, TProperty property,IDatatableColumn<TEntity> column) where TEntity : class;

        public IHtmlString ToHtml <TEntity>(HtmlHelper htmlHelper, TEntity entity, IDatatableColumn<TEntity> column) where TEntity : class
        {
            return ToHtml(htmlHelper, entity, (TProperty) PropertyValue, column);
        }
    }
}
