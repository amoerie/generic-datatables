using System.Collections.Generic;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Base
{
    /// <summary>
    ///     Represents a data table with columns
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Datatable<TEntity> where TEntity : class
    {
        private ICollection<IDatatableColumn<TEntity>> _columns;
        public string Id { get; set; }

        public virtual ICollection<IDatatableColumn<TEntity>> Columns
        {
            get { return _columns ?? (_columns = new List<IDatatableColumn<TEntity>>()); }
            set { _columns = value; }
        }

        public IEnumerable<DatatableValidationResult> Validate()
        {
            return InternalValidate();
        }

        protected abstract IEnumerable<DatatableValidationResult> InternalValidate();

        public override string ToString()
        {
            return string.Format("Type: {0}, Id: {1}, Columns: {2}", GetType(), Id, Columns);
        }
    }
}