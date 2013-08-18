using System;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Remote
{
    /// <summary>
    /// Provides a central storage space for datatables so they can be fetched again to build Json responses
    /// </summary>
    public static class DatatableStorage
    {
        /// <summary>
        /// Gets the <see cref="Datatable{TEntity}"/> instance that was saved using the given <paramref name="datatableId"/>.
        /// If no <see cref="Datatable{TEntity}"/> is found in the storage, the <paramref name="datatableRenderer"/> action is executed, and a second attempt is
        /// made to get the <see cref="Datatable{TEntity}"/> from the storage. 
        /// If the <see cref="Datatable{TEntity}"/> is still not found, an <see cref="ArgumentException"/> is thrown
        /// </summary>
        /// <typeparam name="TEntity">The type of the datatable</typeparam>
        /// <param name="datatableId">The id of the datatable</param>
        /// <param name="datatableRenderer">The action that stores the datatable to storage again. This method is only called if the first fetching attempt failed.</param>
        /// <returns>the <see cref="Datatable{TEntity}"/> instance that was saved using the given <paramref name="datatableId"/></returns>
        /// <exception cref="ArgumentException">If the datatable was not found even after calling the <paramref name="datatableRenderer"/></exception>
        public static RemoteDatatable<TEntity> Get<TEntity>(string datatableId, Action datatableRenderer) where TEntity : Entity
        {
            if (!Has(datatableId))
                datatableRenderer();
            if (!Has(datatableId))
                throw new ArgumentException(@"No such datatable in storage found! (" + datatableId + ") ", "datatableId");
            return (RemoteDatatable<TEntity>)HttpRuntime.Cache[datatableId];
        }

        /// <summary>
        /// Saves the <paramref name="datatable"/> in storage using its Id property
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="datatable">The <see cref="Datatable{TEntity}"/> to save in storage</param>
        public static void Put<TEntity>(RemoteDatatable<TEntity> datatable) where TEntity : Entity
        {
            HttpRuntime.Cache[datatable.Id] = datatable;
        }

        private static bool Has(string datatableId)
        {
            return HttpRuntime.Cache[datatableId] != null;
        }
    }
}
