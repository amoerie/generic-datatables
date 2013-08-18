using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Remote.Builder;

namespace GenericDatatables.Datatables.Local.Builder
{
    /// <summary>
    ///     Builder for a <see cref="LocalDatatable{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ILocalDatatableBuilder<TEntity> : IDatatableBuilder<TEntity> where TEntity : class
    {
        // adds a display column to the datatable 
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header);

        // adds a property column to the datatable
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, bool?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, int?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, double?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, decimal?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, long?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, short?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, string>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, DateTime?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, TimeSpan?>> propertyExpression);
        ILocalDatatableColumnBuilder<TEntity> Column<TProperty>([NotNull] string header, [NotNull] Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression);
    }
}