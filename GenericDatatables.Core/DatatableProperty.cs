using System;
using System.Linq.Expressions;
using GenericDatatables.Core.Helper;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     contains useful info about 1 entity property
    /// </summary>
    /// <typeparam name="TEntity">
    ///     the entity type
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     the property type
    /// </typeparam>
    public class DatatableProperty<TEntity, TProperty> : IDatatableProperty<TEntity>
        where TEntity : class
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        public DatatableProperty(string columnHeader, Expression<Func<TEntity, string>> display)
        {
            ColumnHeader = columnHeader;
            Display = display;
            ToSqlString = display;
            SortBy = display;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        /// <param name="sortBy">
        ///     The sort by.
        /// </param>
        public DatatableProperty(
            string columnHeader, Expression<Func<TEntity, string>> display, Expression<Func<TEntity, TProperty>> sortBy)
        {
            Display = display;
            ToSqlString = Display;
            ColumnHeader = columnHeader;
            SortBy = sortBy;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        public DatatableProperty(string columnHeader, Expression<Func<TEntity, int?>> display)
        {
            Display = display.ConvertToStringExpression();
            ToSqlString = display.ConvertToSqlCompatibleExpression();
            ColumnHeader = columnHeader;
            SortBy = display;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        public DatatableProperty(string columnHeader, Expression<Func<TEntity, double?>> display)
        {
            Display = display.ConvertToStringExpression();
            ToSqlString = display.ConvertToSqlCompatibleExpression();
            ColumnHeader = columnHeader;
            SortBy = display;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        public DatatableProperty(string columnHeader, Expression<Func<TEntity, decimal?>> display)
        {
            Display = display.ConvertToStringExpression();
            ToSqlString = display.ConvertToSqlCompatibleExpression();
            ColumnHeader = columnHeader;
            SortBy = display;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        ///     Note that you are obligated to pass a dateformat.
        ///     Currently the following formats are supported:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>key</term>
        ///             <description>meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>dd</term>
        ///             <description>day</description>
        ///         </item>
        ///         <item>
        ///             <term>MM</term>
        ///             <description>month</description>
        ///         </item>
        ///         <item>
        ///             <term>yyyy</term>
        ///             <description>year</description>
        ///         </item>
        ///         <item>
        ///             <term>ss</term>
        ///             <description>second</description>
        ///         </item>
        ///         <item>
        ///             <term>mm</term>
        ///             <description>minute</description>
        ///         </item>
        ///         <item>
        ///             <term>hh</term>
        ///             <description>hour</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        /// <param name="timeFormat">
        ///     The date format.
        /// </param>
        public DatatableProperty(string columnHeader, Expression<Func<TEntity, DateTime?>> display, string timeFormat)
        {
            Display = display.ConvertToStringExpression(timeFormat);
            ToSqlString = display.ConvertToSqlCompatibleExpression(timeFormat);
            ColumnHeader = columnHeader;
            SortBy = display;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableProperty{TEntity,TProperty}" /> class.
        ///     Note that you have to pass a time format
        ///     <list type="table">
        ///         <listheader>
        ///             <term>key</term>
        ///             <description>meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>dd</term>
        ///             <description>day</description>
        ///         </item>
        ///         <item>
        ///             <term>MM</term>
        ///             <description>month</description>
        ///         </item>
        ///         <item>
        ///             <term>yyyy</term>
        ///             <description>year</description>
        ///         </item>
        ///         <item>
        ///             <term>ss</term>
        ///             <description>second</description>
        ///         </item>
        ///         <item>
        ///             <term>mm</term>
        ///             <description>minute</description>
        ///         </item>
        ///         <item>
        ///             <term>hh</term>
        ///             <description>hour</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <param name="columnHeader">
        ///     The column header.
        /// </param>
        /// <param name="display">
        ///     The display.
        /// </param>
        /// <param name="timeFormat">
        ///     The date format.
        /// </param>
        public DatatableProperty(string columnHeader, Expression<Func<TEntity, TimeSpan?>> display, string timeFormat)
        {
            Display = display.ConvertToStringExpression(timeFormat);
            ToSqlString = display.ConvertToSqlCompatibleExpression(timeFormat);
            ColumnHeader = columnHeader;
            SortBy = display;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the string to be used as column header
        /// </summary>
        public string ColumnHeader { get; set; }

        /// <summary>
        ///     Gets or sets the display expression so the view can show the property in the html (is used to construct json)
        /// </summary>
        public Expression<Func<TEntity, string>> Display { get; set; }

        /// <summary>
        ///     Gets or sets the the sort by expression, which is usually a raw property expression.
        ///     It is used for example when you format dates, but want the columns to sort on the actual date and not the
        ///     string representation.
        /// </summary>
        public LambdaExpression SortBy { get; set; }

        /// <summary>
        ///     Gets or sets the expression that is linq to entities compatible. Not to be read by humans.
        ///     If it's a DateTime or TimeSpan, don't even try. Seriously. You probably have better things to do anyway.
        /// </summary>
        public Expression<Func<TEntity, string>> ToSqlString { get; set; }

        #endregion
    }
}