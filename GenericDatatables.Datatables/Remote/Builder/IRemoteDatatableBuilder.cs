using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Local.Builder;
using GenericDatatables.Datatables.Validation;

namespace GenericDatatables.Datatables.Remote.Builder
{
    /// <summary>
    /// Builder for a <see cref="RemoteDatatable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <seealso cref="RemoteDatatableBuilder{TEntity}"/>
    /// <seealso cref="ILocalDatatableBuilder{TEntity}"/>
    /// <seealso cref="LocalDatatableBuilder{TEntity}"/>
    public interface IRemoteDatatableBuilder<TEntity> : IDatatableBuilder where TEntity : Entity
    {
        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        ///     At the very minimum, you should configure a display function for this column, or the datatable validation will fail.
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        /// <seealso cref="DatatableValidationResult"/>
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, bool?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, bool?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, int?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, int?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, double?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, double?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, decimal?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, decimal?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, long?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, long?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, short?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, short?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, string> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, string>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, DateTime?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, DateTime?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TimeSpan?> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, TimeSpan?>> propertyExpression);

        /// <summary>
        ///     Adds a <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> to this <see cref="RemoteDatatable{TEntity}"/>
        /// </summary>
        /// <param name="header">The header that should appear at the top of the column</param>
        /// <param name="propertyExpression">The collection property that should be used for this column.</param>
        /// <returns>this <see cref="IRemoteDatatableBuilder{TEntity}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, ICollection<TProperty>> Column<TProperty>([NotNull] string header, [NotNull] Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression);
    }
}