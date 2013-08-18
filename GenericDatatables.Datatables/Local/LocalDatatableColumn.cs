using System;
using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Remote;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Local
{
    /// <summary>
    ///     Represents one property (column) in a datatable
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LocalDatatableColumn<TEntity> : DatatableColumn<TEntity>, ILocalDatatableColumn<TEntity> where TEntity : class
    {
        public LocalDatatableColumn()
        {
            Searchable = true;
            Sortable = true;
            Visible = true;
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Enumerable.Empty<DatatableValidationResult>();
        }
    }
}