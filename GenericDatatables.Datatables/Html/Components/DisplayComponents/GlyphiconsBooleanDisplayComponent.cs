using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Extensions;

namespace GenericDatatables.Datatables.Html.Components.DisplayComponents
{
    public class GlyphiconsBooleanDisplayComponent: IDisplayComponent<bool?>
    {
        public IHtmlString ToHtml <TEntity>(TEntity entity, IDatatableColumn<TEntity, bool?> datatableColumn) where TEntity : class
        {
            var value = datatableColumn.PropertyExpression.Compile().Invoke(entity);
            switch (value)
            {
                case null:
                    return MvcHtmlString.Create(string.Empty);
                case false:
                    return new TagBuilder("i").Class("icon-check-empty").ToHtml();
                default:
                    return new TagBuilder("i").Class("icon-check").ToHtml();
            }
        }
    }
}
