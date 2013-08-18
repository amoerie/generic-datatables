using System;
using System.Linq;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Including
{
    public class EntityIncluder<TEntity> : IEntityIncluder<TEntity> where TEntity : class
    {
        private readonly IEntityIncluder<TEntity> _baseIncluder;
        private readonly IPropertyInclusionHolder<TEntity> _propertyInclusionHolder; 

        /// <summary>
        ///     Indicates that the <paramref name="property"/> should be immediately included when fetching the data for this query.
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
        ///     .Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
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
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityIncluder{TEntity}"/> containing the property inclusion</returns>
        public static IEntityIncluder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            return new EntityIncluder<TEntity>(null, new PropertyInclusionHolder<TEntity, TProperty>(property));
        }

        /// <summary>
        ///     Returns a <see cref="IEntityIncluder{TEntity}" /> instance that allows construction of
        ///     <see cref="IEntityIncluder{TEntity}" /> objects though the use of LINQ syntax.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEntityIncluder{TEntity}" /> instance.
        /// </returns>
        public static IEntityIncluder<TEntity> AsQueryable()
        {
            return new EntityIncluder<TEntity>(null, new DummyPropertyInclusionHolder<TEntity>());
        }

        internal EntityIncluder(IEntityIncluder<TEntity> baseIncluder, IPropertyInclusionHolder<TEntity> propertyInclusionHolder)
        {
            _baseIncluder = baseIncluder;
            _propertyInclusionHolder = propertyInclusionHolder;
        }

        /// <summary>
        ///     Performs the inclusions on the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities on which to perform the inclusions</param>
        /// <returns>The entities that are now marked with the properties that need to be eagerly loaded</returns>
        public IQueryable<TEntity> AddInclusions(IQueryable<TEntity> entities)
        {
            if (_baseIncluder != null)
            {
                entities = _baseIncluder.AddInclusions(entities);
            }
            return _propertyInclusionHolder.AddPropertyInclusion(entities);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            if(_baseIncluder != null)
                return string.Format("{0}, {1}", _baseIncluder, _propertyInclusionHolder);
            return string.Format("{0}", _propertyInclusionHolder);
        }
    }
}