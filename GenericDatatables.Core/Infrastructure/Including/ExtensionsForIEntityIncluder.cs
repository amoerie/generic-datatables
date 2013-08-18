using System;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Including
{
    public static class ExtensionsForIEntityIncluder
    {
        /// <summary>
        ///     Adds another property to the list of properties that need to be included in the query. 
        ///     See http://msdn.microsoft.com/en-us/library/gg671236%28v=vs.103%29.aspx for more information.
        /// </summary>
        /// <example>
        /// <code>
        ///     //To include a single reference: 
        ///     Include(e => e.Level1Reference)
        /// 
        ///     //To include a single collection: 
        ///     Include(e => e.Level1Collection)
        /// 
        ///     //To include a reference and then a reference one level down: 
        ///     Include(e => e.Level1Reference.Level2Reference)
        /// 
        ///     //To include a reference and then a collection one level down: 
        ///     Include(e => e.Level1Reference.Level2Collection)
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection, a reference, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference))
        /// 
        ///     //To include a collection, a collection, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference)))
        /// </code>
        /// </example>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="includer">The includer containing the inclusions that are already made</param>
        /// <param name="property">The property to be included</param>
        /// <returns>An instance of <see cref="IEntityIncluder{TEntity}"/> containing the included properties</returns>
        public static IEntityIncluder<TEntity> And<TEntity, TProperty>(this IEntityIncluder<TEntity> includer, Expression<Func<TEntity, TProperty>> property) where TEntity : class
        {
            return new EntityIncluder<TEntity>(includer, new PropertyInclusionHolder<TEntity, TProperty>(property));
        }
    }
}
