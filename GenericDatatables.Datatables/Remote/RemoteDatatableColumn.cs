using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Remote.Filtering;
using GenericDatatables.Datatables.Remote.Sorting;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Remote
{
    /// <summary>
    /// Represents a column in an instance of <see cref="RemoteDatatable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public sealed class RemoteDatatableColumn<TEntity> : DatatableColumn<TEntity>, IRemoteDatatableColumn<TEntity> where TEntity : Entity
    {
        private ICollection<IDatatablePropertySorter<TEntity>> _propertySorters;

        public ICollection<IDatatablePropertySorter<TEntity>> PropertySorters
        {
            get { return _propertySorters ?? (_propertySorters = new List<IDatatablePropertySorter<TEntity>>()); }
            set { _propertySorters = value; }
        }

        public IDatatablePropertyFilter<TEntity> PropertyFilter { get; set; }

        public RemoteDatatableColumn()
        {
            Sortable = true;
            Searchable = true;
            Visible = true;
        }

        public IEntitySorter<TEntity> Sort(IEntitySorter<TEntity> sorter, SortDirection sortDirection)
        {
            return PropertySorters.Aggregate(sorter, (current, propertySorter) => propertySorter.Sort(current, sortDirection));
        }

        public IEntityFilter<TEntity> Filter(IEntityFilter<TEntity> filter, string search)
        {
            if (!Searchable)
                return filter;
            return PropertyFilter.Filter(filter, search);
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            if(Searchable && PropertyFilter == null)
                yield return new DatatableValidationResult(Description + " does not have a property filter, even though it is configured to be searchable (it's impossible to apply filtering to this column without a property filter)");
            if(Sortable && (PropertySorters == null || !PropertySorters.Any()))
                yield return new DatatableValidationResult(Description + " does not have any property sorters, even though it is configured to be sortable! (it's impossible to apply sorting to this column without at least 1 property sorter)");
        }

        public override string ToString()
        {
            return string.Format("{0}, PropertyFilter: {1}, PropertySorters: {2}", base.ToString(), PropertyFilter, PropertySorters);
        }
    }
}