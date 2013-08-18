using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Datatables.Remote.Filtering
{
    public interface IDatatablePropertyFilter<TEntity>
    {
        IEntityFilter<TEntity> Filter(IEntityFilter<TEntity> filter, string search);
    }
}
