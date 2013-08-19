using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Remote
{
    public interface IRemoteDatatableColumn<TEntity, TProperty>: IRemoteDatatableColumn<TEntity>, IDatatableColumn<TEntity, TProperty> where TEntity : class
    {
    }
}
