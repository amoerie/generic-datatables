using System;
using System.Linq.Expressions;
using System.Web;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     The DatatableBuilder interface.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    public interface IDatatableBuilder<TEntity>
        where TEntity : class
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Constructs the HTML of the datatable.
        /// </summary>
        /// <returns>The datatable as HTML</returns>
        IHtmlString Finish();

        /// <summary>
        ///     Constructs the HTML of the datatable and also adds
        ///     a last column to the datatable in which you are free to put HTML
        ///     as you like.
        /// </summary>
        /// <param name="lastColumnHeader">
        ///     The text that should appear in the header of the last column
        /// </param>
        /// <param name="lastColumnHtml">
        ///     A lambda function that takes an instance of TEntity and uses it to generate some HTML
        /// </param>
        /// <returns>
        ///     The datatable as HTML.
        /// </returns>
        IHtmlString LastColumn(string lastColumnHeader, Func<TEntity, IHtmlString> lastColumnHtml);

        /// <summary>
        ///     Adds an int property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, int?>> propertyExpression);

        /// <summary>
        ///     Adds a decimal property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, decimal?>> propertyExpression);

        /// <summary>
        ///     Adds a double property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, double?>> propertyExpression);

        /// <summary>
        ///     Adds a string property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, string>> propertyExpression);

        /// <summary>
        ///     Adds a datetime property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <param name="dateFormat">The format with which to format the date</param>
        /// <returns>This datatablebuilder</returns>
        IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, DateTime?>> propertyExpression, string dateFormat);

        /// <summary>
        ///     Adds a timespan property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <param name="timeFormat">The format with which to format the timespan</param>
        /// <returns>This datatablebuilder</returns>
        IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, TimeSpan?>> propertyExpression, string timeFormat);

        #endregion
    }
}