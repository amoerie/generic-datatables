using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericDatatables.Core.Utilities;
using LinqKit;

namespace GenericDatatables.Core.Filter
{
    /// <summary>
    ///     Class responsible for the global filter
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Entity type
    /// </typeparam>
    internal class DatatableFilter<TEntity>
        where TEntity : class
    {
        #region Fields

        /// <summary>
        ///     The _param.
        /// </summary>
        private readonly DatatableParam _param;

        /// <summary>
        ///     The _properties.
        /// </summary>
        private readonly IDatatableProperty<TEntity>[] _properties;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableFilter{TEntity}" /> class.
        /// </summary>
        /// <param name="param">
        ///     The param.
        /// </param>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        public DatatableFilter(DatatableParam param, IDatatableProperty<TEntity>[] properties)
        {
            _param = param;
            _properties = properties;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The filter.
        /// </summary>
        /// <param name="entities">
        ///     The entities.
        /// </param>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> Filter(IQueryable<TEntity> entities)
        {
            // No need for a global filter if the parameter value is null or empty
            if (string.IsNullOrEmpty(_param.GlobalSearch))
            {
                return entities;
            }

            // Escape special characters and make a constant expression
            ConstantExpression escapedExpression = Expression.Constant(_param.GlobalSearch.Escape());

            // call ToLower to support case-agnostic filters
            MethodCallExpression searchExpr = Expression.Call(
                escapedExpression, typeof (string).GetMethod("ToLower", new Type[0]));

            // iterate through properties and build a property.ToSqlString.ToLower.Contains expression
            var expressions = new List<Expression<Func<TEntity, bool>>>();

            // i kept the foreach (could have been a linq query) for readability
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var property in _properties)
            {
                MethodCallExpression stringToLowerExpression = Expression.Call(
                    property.ToSqlString.Body, typeof (string).GetMethod("ToLower", new Type[0]));
                MethodCallExpression containsExpression = Expression.Call(
                    stringToLowerExpression, typeof (string).GetMethod("Contains"), new Expression[] {searchExpr});
                LambdaExpression lambdaExpression = Expression.Lambda(
                    containsExpression, property.ToSqlString.Parameters);
                expressions.Add(lambdaExpression as Expression<Func<TEntity, bool>>);
            }

            // ReSharper restore LoopCanBeConvertedToQuery

            /*
             * Now we combine all these expressions
             * into an OR group.
             * This 'resultLambda' should result in true if one expression in 'expressions' is true.
             */
            Expression<Func<TEntity, bool>> resultLambda = expressions.Aggregate((expr1, expr2) => expr1.Or(expr2));
            return entities.Where(resultLambda.Expand());
        }

        #endregion
    }
}