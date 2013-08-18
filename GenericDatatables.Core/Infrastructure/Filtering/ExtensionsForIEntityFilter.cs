using System;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Filtering
{
    /// <summary>
    ///     Extension methods for the <see cref="IEntityFilter{TEntity}" /> interface.
    /// </summary>
    public static class ExtensionsForIEntityFilter
    {
        /// <summary>
        ///     Returns a <see cref="IEntityFilter{TEntity}" /> that filters a sequence based on a predicate.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of the entity.
        /// </typeparam>
        /// <param name="baseFilter">
        ///     The base filter.
        /// </param>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntityFilter{TEntity}" />.
        /// </returns>
        public static IEntityFilter<TEntity> Where <TEntity>(
            this IEntityFilter<TEntity> baseFilter, Expression<Func<TEntity, bool>> predicate)
        {
            if (baseFilter == null)
            {
                throw new ArgumentNullException("baseFilter");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return new WhereEntityFilter<TEntity>(baseFilter, predicate);
        }
    }
}