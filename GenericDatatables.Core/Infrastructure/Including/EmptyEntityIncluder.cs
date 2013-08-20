using System.Diagnostics;
using System.Linq;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Including
{
    /// <summary>
    ///     Literally does nothing.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    [DebuggerDisplay("EntityIncluder ( Nothing Included )")]
    internal class EmptyEntityIncluder<TEntity>: IEntityIncluder<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> ExecuteInclusions(IQueryable<TEntity> entities)
        {
            return entities;
        }
    }
}
