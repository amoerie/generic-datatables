using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Base
{
    public interface IDatatableColumn<TEntity> where TEntity: class
    {
        /// <summary>
        ///     Gets or sets the header
        /// </summary>
        string Header { get; set; }

        /// <summary>
        ///     Gets or sets the name
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        ///     Gets or sets the function 
        /// </summary>
        IDisplayComponent DisplayComponent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this column is sortable or not
        /// </summary>
        bool Sortable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this column is searchable or not
        /// </summary>
        bool Searchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this column is visible or not
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the width of this column. You can use every value that would also be valid for the CSS 'width' property
        /// </summary>
        string Width { get; set; }

        /// <summary>
        /// Gets or sets the class of this column. Use this to add extra classes to columns
        /// </summary>
        string Class { get; set; }

        /// <summary>
        /// Gets or sets the default content of this column. If this value is not null, it will be used to replace empty values.
        /// Common use cases include showing "None" or "Does not apply here"
        /// </summary>
        string DefaultContent { get; set; }

        /// <summary>
        /// Gets or sets the search element that will be used for property specific searching. 
        /// </summary>
        ISearchComponent SearchComponent { get; set; }

        IEnumerable<DatatableValidationResult> Validate();

        void SetAttributes(TagBuilder th);

    }
}
