using System;
using System.Linq.Expressions;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     Represents one datatable property of TEntity
    ///     and contains information on how to generate the column for this property
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Type of entity
    /// </typeparam>
    public interface IDatatableProperty<TEntity>
        where TEntity : class
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the string to be used as column header
        /// </summary>
        string ColumnHeader { get; set; }

        /// <summary>
        ///     Gets or sets the display expression so the view can show the property in the html (is used to construct json)
        /// </summary>
        Expression<Func<TEntity, string>> Display { get; set; }

        /// <summary>
        ///     Gets or sets the sort by expression, which is usually a raw property expression.
        ///     It is used for example when you format dates, but want the columns to sort on the actual date and not the
        ///     string representation.
        /// </summary>
        LambdaExpression SortBy { get; set; }

        /// <summary>
        ///     Gets or sets the expression that is linq to entities compatible. Not to be read by humans.
        /// </summary>
        Expression<Func<TEntity, string>> ToSqlString { get; set; }

        #endregion
    }
}