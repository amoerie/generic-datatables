using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Remote
{
    /// <summary>
    /// Represents a remote datatable, of which the data can be fetched through the <see cref="Url"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity for which this datatable was made</typeparam>
    public class RemoteDatatable<TEntity> : Datatable<TEntity> where TEntity : class
    {
        private ICollection<IRemoteDatatableColumn<TEntity>> _columns;

        /// <summary>
        /// Gets or sets the url where the data can be fetched from
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets the columns
        /// </summary>
        public new virtual ICollection<IRemoteDatatableColumn<TEntity>> Columns
        {
            get { return _columns ?? (_columns = new List<IRemoteDatatableColumn<TEntity>>()); }
            set { _columns = value; }
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Columns.SelectMany(column => column.Validate());
        }

        public override string ToString()
        {
            return string.Format("{0}, Url: {1}, Columns: {2}", base.ToString(), Url, Columns);
        }
    }
}
