using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Base
{
    public abstract class DatatableColumn<TEntity>: IDatatableColumn<TEntity> where TEntity : class
    {
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

        protected string Description
        {
            get { return string.Format("Column with header '{0}' and name '{1}'", Header, Name); }
        }

        public IEnumerable<DatatableValidationResult> Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
                yield return new DatatableValidationResult(string.Format("Column with header '{0}' does not have a name!", Name));
            if(Searchable && SearchComponent == null)
                yield return new DatatableValidationResult(Description + " does not have a search component, even though it is configured to be searchable!");
            foreach (var internalValidationResult in InternalValidate())
                yield return internalValidationResult;
        }

        protected abstract IEnumerable<DatatableValidationResult> InternalValidate();

        public void SetAttributes(TagBuilder th)
        {
            th
                .Attribute("data-width", Width ?? string.Empty)
                .Attribute("data-searchable", Searchable.ToString(CultureInfo.InvariantCulture).ToLower())
                .Attribute("data-sortable", Sortable.ToString(CultureInfo.InvariantCulture).ToLower())
                .Attribute("data-visible", Visible.ToString(CultureInfo.InvariantCulture).ToLower())
                .Attribute("data-class", Class ?? string.Empty)
                .Attribute("data-default-content", DefaultContent ?? string.Empty);
        }

        public string Display(HtmlHelper htmlHelper, TEntity entity)
        {
            return DisplayFunction(entity);
        }

        public override string ToString()
        {
            return string.Format("Header: {0}, Name: {1}", Header, Name);
        }
    }
}
