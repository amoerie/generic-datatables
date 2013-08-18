using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Datatables.Remote.Sorting
{
    public interface IDatatablePropertySorter<TEntity>
    {
        IEntitySorter<TEntity> Sort(IEntitySorter<TEntity> sorter, SortDirection sortDirection);
    }
}
