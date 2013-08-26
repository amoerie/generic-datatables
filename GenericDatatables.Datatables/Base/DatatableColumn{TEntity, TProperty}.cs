using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Base
{
    public abstract class DatatableColumn<TEntity, TProperty> : IDatatableColumn<TEntity, TProperty> where TEntity : class
    {
        private Expression<Func<TEntity, TProperty>> _propertyExpression;
        private Func<TEntity, TProperty> _propertyFunction;
        public string Header { get; set; }
        public string Name { get; set; }
        public Func<TEntity, string> DisplayFunction { get; set; }
        public bool Sortable { get; set; }
        public bool Searchable { get; set; }
        public bool Visible { get; set; }
        public string Width { get; set; }
        public string Class { get; set; }
        public string DefaultContent { get; set; }
        public ISearchComponent SearchComponent { get; set; }

        public Expression<Func<TEntity, TProperty>> PropertyExpression
        {
            get { return _propertyExpression; }
            set
            {
                _propertyExpression = value;
                _propertyFunction = value.Compile();
            }
        }

        public TProperty GetProperty(TEntity entity)
        {
            if (_propertyFunction == null)
                throw new InvalidOperationException(Description + " cannot get property, no property function defined");
            return _propertyFunction(entity);
        }

        protected string Description
        {
            get { return string.Format("Column with header '{0}' and name '{1}'", Header, Name); }
        }

        public override string ToString()
        {
            return string.Format("Header: {0}, Name: {1}, PropertyExpression: {2}", Header, Name, PropertyExpression);
        }

        public IEnumerable<DatatableValidationResult> Validate()
        {
            if (PropertyExpression == null)
                yield return new DatatableValidationResult(Description + " does not have a property expression. Did you pass null?");
            if (DisplayFunction == null)
                yield return new DatatableValidationResult(Description + " does not have a display function.");
        }

        protected abstract IEnumerable<DatatableValidationResult> InternalValidate();

        public IDictionary<string, string> GetAttributes()
        {
            return new Dictionary<string, string>
            {
                {"data-width", Width ?? string.Empty },
                {"data-searchable", Searchable.ToString(CultureInfo.InvariantCulture).ToLower() },
                {"data-sortable", Sortable.ToString(CultureInfo.InvariantCulture).ToLower() },
                {"data-visible", Visible.ToString(CultureInfo.InvariantCulture).ToLower() },
                {"data-class", Class ?? string.Empty },
                {"data-default-content", DefaultContent ?? string.Empty }
            };
        }
    }
}
