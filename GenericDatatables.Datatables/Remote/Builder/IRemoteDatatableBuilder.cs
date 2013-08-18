using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Remote.Builder
{
    /// <summary>
    /// Builder for a <see cref="RemoteDatatable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRemoteDatatableBuilder<TEntity> : IDatatableBuilder<TEntity> where TEntity : Entity
    {
        // adds a display column to the datatable 
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header);

        // adds a property column to the datatable
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, bool?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, int?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, double?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, decimal?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, long?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, short?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, string>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, DateTime?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column([NotNull] string header, [NotNull] Expression<Func<TEntity, TimeSpan?>> propertyExpression);
        IRemoteDatatableColumnBuilder<TEntity> Column<TProperty>([NotNull] string header, [NotNull] Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression);
    }
}