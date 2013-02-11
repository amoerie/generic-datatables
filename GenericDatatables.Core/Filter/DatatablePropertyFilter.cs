using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericDatatables.Core.Utilities;
using LinqKit;

namespace GenericDatatables.Core.Filter
{
    /// <summary>
    ///     Handles all property specific searches. This is done in a separate class mainly for the separation of concerns.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type
    /// </typeparam>
    internal class DatatablePropertyFilter<TEntity>
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
        ///     Initializes a new instance of the <see cref="DatatablePropertyFilter{TEntity}" /> class.
        /// </summary>
        /// <param name="param">
        ///     The param.
        /// </param>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        public DatatablePropertyFilter(DatatableParam param, IDatatableProperty<TEntity>[] properties)
        {
            _param = param;
            _properties = properties;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Filters the entities
        /// </summary>
        /// <param name="entities">
        ///     The entities.
        /// </param>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> Filter(IQueryable<TEntity> entities)
        {
            var expressions = new List<Expression<Func<TEntity, bool>>>();
            for (int i = 0; i < _param.Search.Length; i++)
            {
                string search = _param.Search[i];
                if (_param.Searchable[i] && !string.IsNullOrEmpty(search))
                {
                    IDatatableProperty<TEntity> property = _properties[i];
                    /**
                     * Use the ToUpper method to perform a case-ignorant contains operation
                     * I prefer the ToUpper method over its ToLower competitor because Microsoft 
                     */
                    MethodCallExpression searchExpr = Expression.Call(
                        Expression.Constant(search.Escape()), typeof (string).GetMethod("ToLower", new Type[0]));
                    MethodCallExpression valueToLowerExpression = Expression.Call(
                        property.ToSqlString.Body, typeof (string).GetMethod("ToLower", new Type[0]));
                    MethodCallExpression containsCall = Expression.Call(
                        valueToLowerExpression, typeof (string).GetMethod("Contains"), new Expression[] {searchExpr});
                    LambdaExpression resultCall = Expression.Lambda(containsCall, property.ToSqlString.Parameters);
                    expressions.Add(resultCall as Expression<Func<TEntity, bool>>);
                }
            }

            if (expressions.Count != 0)
            {
                // glue all these expressions into one expression, combined with the AND operator
                Expression<Func<TEntity, bool>> resultLambda = expressions.Aggregate((expr1, expr2) => expr1.And(expr2));
                return entities.Where(resultLambda.Expand());
            }

            return entities;
        }

        #endregion
    }
}