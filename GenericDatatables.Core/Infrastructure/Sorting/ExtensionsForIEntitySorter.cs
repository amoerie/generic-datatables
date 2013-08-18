using System;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Sorting
{
    /// <summary>
    ///     Extension methods on <see cref="IEntitySorter{TEntity}" />.
    /// </summary>
    public static class ExtensionsForIEntitySorter
    {
        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that sorts  the elements of a sequence
        ///     in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of the entity.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key returned by the function that is represented by keySelector.
        /// </typeparam>
        /// <param name="sorter">
        ///     The sorter.
        /// </param>
        /// <param name="keySelector">
        ///     A function to extract a key from an element.
        /// </param>
        /// <returns>
        ///     An <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="sorter" /> or
        ///     <paramref name="keySelector" /> are null.
        /// </exception>
        public static IEntitySorter<TEntity> OrderBy <TEntity, TKey>(
            this IEntitySorter<TEntity> sorter, Expression<Func<TEntity, TKey>> keySelector)
        {
            if (sorter == null)
            {
                throw new ArgumentNullException("sorter");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            // Note: The sorter parameter is not used, because an OrderBy will invalidate all previous
            // OrderBy and ThenBy statements (as Enumerable.OrderBy and Queryable.OrderBy do).
            return EntitySorter<TEntity>.OrderBy(keySelector);
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that sorts the elements of a sequence
        ///     in descending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of the entity.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key returned by the function that is represented by keySelector.
        /// </typeparam>
        /// <param name="sorter">
        ///     The sorter.
        /// </param>
        /// <param name="keySelector">
        ///     A function to extract a key from an element.
        /// </param>
        /// <returns>
        ///     An <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="sorter" /> or
        ///     <paramref name="keySelector" /> are null.
        /// </exception>
        public static IEntitySorter<TEntity> OrderByDescending <TEntity, TKey>(
            this IEntitySorter<TEntity> sorter, Expression<Func<TEntity, TKey>> keySelector)
        {
            if (sorter == null)
            {
                throw new ArgumentNullException("sorter");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            // Note: The sorter parameter is not used, because an OrderBy will invalidate all previous
            // OrderBy and ThenBy statements (as Enumerable.OrderBy and Queryable.OrderBy do).
            return EntitySorter<TEntity>.OrderByDescending(keySelector);
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that performs a subsequent ordering of the
        ///     elements in in a collection of <typeparamref name="TEntity" /> objects in ascending order
        ///     according to a key.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of the entity.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key.
        /// </typeparam>
        /// <param name="sorter">
        ///     The sorter.
        /// </param>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <returns>
        ///     An <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="sorter" /> or
        ///     <paramref name="keySelector" /> are null.
        /// </exception>
        public static IEntitySorter<TEntity> ThenBy <TEntity, TKey>(
            this IEntitySorter<TEntity> sorter, Expression<Func<TEntity, TKey>> keySelector)
        {
            if (sorter == null)
            {
                throw new ArgumentNullException("sorter");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            // Wrap the original sorter in a new entity sorter to extend the sorting.
            return new ThenByEntitySorter<TEntity, TKey>(sorter, keySelector, SortDirection.Ascending);
        }

        /// <summary>
        ///     The then by descending.
        /// </summary>
        /// <param name="sorter">
        ///     The sorter.
        /// </param>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static IEntitySorter<TEntity> ThenByDescending <TEntity, TKey>(
            this IEntitySorter<TEntity> sorter, Expression<Func<TEntity, TKey>> keySelector)
        {
            if (sorter == null)
            {
                throw new ArgumentNullException("sorter");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            // Wrap the original sorter in a new entity sorter to extend the sorting.
            return new ThenByEntitySorter<TEntity, TKey>(sorter, keySelector, SortDirection.Descending);
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that performs a subsequent ordering of the
        ///     elements in in a collection of <typeparamref name="TEntity" /> objects in ascending order
        ///     by using the property, specified by it's <paramref name="propertyName" />.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of the entity.
        /// </typeparam>
        /// <param name="sorter">
        ///     The sorter.
        /// </param>
        /// <param name="propertyName">
        ///     Name of the property or a list of chained properties, separated by a dot.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when the specified <paramref name="propertyName" />
        ///     is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when the specified <paramref name="propertyName" /> is
        ///     empty or when the specified property could not be found on the <typeparamref name="TEntity" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="sorter" /> or
        ///     <paramref name="propertyName" /> are null.
        /// </exception>
        public static IEntitySorter<TEntity> ThenBy <TEntity>(this IEntitySorter<TEntity> sorter, string propertyName)
        {
            if (sorter == null)
            {
                throw new ArgumentNullException("sorter");
            }

            var builder = new EntitySorterBuilder<TEntity>(propertyName) {SortDirection = SortDirection.Ascending};

            return builder.BuildThenByEntitySorter(sorter);
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that performs a subsequent ordering of the
        ///     elements in in a collection of <typeparamref name="TEntity" /> objects in descending order
        ///     by using the property, specified by it's <paramref name="propertyName" />.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of the entity.
        /// </typeparam>
        /// <param name="sorter">
        ///     The sorter.
        /// </param>
        /// <param name="propertyName">
        ///     Name of the property or a list of chained properties, separated by a dot.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when the specified <paramref name="propertyName" />
        ///     is a null reference or when the specified <paramref name="sorter" /> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when the specified <paramref name="propertyName" /> is
        ///     empty or when the specified property could not be found on the <typeparamref name="TEntity" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="sorter" /> or
        ///     <paramref name="propertyName" /> are null.
        /// </exception>
        public static IEntitySorter<TEntity> ThenByDescending <TEntity>(
            this IEntitySorter<TEntity> sorter, string propertyName)
        {
            if (sorter == null)
            {
                throw new ArgumentNullException("sorter");
            }

            var builder = new EntitySorterBuilder<TEntity>(propertyName) {SortDirection = SortDirection.Descending};

            return builder.BuildThenByEntitySorter(sorter);
        }
    }
}