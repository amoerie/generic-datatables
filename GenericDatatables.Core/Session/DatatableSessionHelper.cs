using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace GenericDatatables.Core.Session
{
    /// <summary>
    ///     Helper class that provides utility methods to store and retrieve datatable specific information
    ///     in the current session
    /// </summary>
    public static class DatatableSessionHelper
    {
        #region Constants

        /// <summary>
        ///     The session key that is used to identify the proper datatable session object
        /// </summary>
        private const string Key = "datatables";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add datatable properties.
        /// </summary>
        /// <param name="session">
        ///     The session.
        /// </param>
        /// <param name="datatableId">
        ///     The datatable id.
        /// </param>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        public static void AddDatatableProperties<TEntity>(
            this HttpSessionStateBase session, string datatableId, DatatableSessionObject<TEntity> properties)
            where TEntity : class
        {
            IDictionary<string, DatatableSessionObject<TEntity>> dictionary =
                session.GetOrMakeDatatablesDictionary<TEntity>();
            dictionary[datatableId] = properties;
            session[Key] = dictionary;
        }

        /// <summary>
        ///     The get datatable properties.
        /// </summary>
        /// <param name="session">
        ///     The session.
        /// </param>
        /// <param name="datatableId">
        ///     The datatable id.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DatatableSessionObject" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     If there is no datatables dictionary in the session
        /// </exception>
        public static DatatableSessionObject<TEntity> GetDatatableProperties<TEntity>(
            this HttpSessionStateBase session, string datatableId) where TEntity : class
        {
            IDictionary<string, DatatableSessionObject<TEntity>> dictionary =
                session.GetOrMakeDatatablesDictionary<TEntity>();
            if (dictionary.ContainsKey(datatableId))
            {
                return dictionary[datatableId];
            }
            throw new ArgumentException("No datatable properties found in session!", "datatableId");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The get datatables dictionary.
        /// </summary>
        /// <param name="session">
        ///     The session.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IDictionary" />.
        /// </returns>
        private static IDictionary<string, DatatableSessionObject<TEntity>> GetOrMakeDatatablesDictionary<TEntity>(
            this HttpSessionStateBase session) where TEntity : class
        {
            object dictionaryObject = session[Key] ?? new Dictionary<string, DatatableSessionObject<TEntity>>();
            var dictionary = dictionaryObject as IDictionary<string, DatatableSessionObject<TEntity>>;

            // If dictionary is null at this point, the universe might collapse so start running.
            Debug.Assert(dictionary != null, "Strap your developer gloves on, this is going to be one hell of a bug.");
            return dictionary;
        }

        #endregion
    }
}