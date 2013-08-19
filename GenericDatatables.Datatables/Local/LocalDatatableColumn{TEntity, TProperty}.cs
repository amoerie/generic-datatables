using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Config;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Local
{
    public class LocalDatatableColumn<TEntity, TProperty>: DatatableColumn<TEntity, TProperty>, ILocalDatatableColumn<TEntity, TProperty> where TEntity : class
    {
        public LocalDatatableColumn(string header, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            Searchable = true;
            Sortable = true;
            Visible = true;
            PropertyExpression = propertyExpression;
            Header = header;
            Name = ExpressionHelper.GetExpressionText(propertyExpression);
            var displayComponent = DatatableConfiguration.Components.DisplayComponents.Lookup<TProperty>();
            if (displayComponent == null)
            {
                var propertyFunction = propertyExpression.Compile();
                DisplayFunction = entity => Convert.ToString(propertyFunction(entity));
            }
            else
            {
                DisplayFunction = entity => displayComponent.ToHtml(entity, this).ToHtmlString();
            }
            SearchComponent = DatatableConfiguration.Components.SearchComponents.Lookup<TProperty>();
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Enumerable.Empty<DatatableValidationResult>();
        }
    }
}
