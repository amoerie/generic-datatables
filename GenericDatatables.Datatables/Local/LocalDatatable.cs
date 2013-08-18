using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Local
{
    /// <summary>
    ///     Represents a data table of which all the data is loaded immediately.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LocalDatatable<TEntity> : Datatable<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Entities { get; set; }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Columns.SelectMany(c => c.Validate());
        }

        public override string ToString()
        {
            return string.Format("{0}, Entities: {1}", base.ToString(), Entities.Count());
        }
    }
}