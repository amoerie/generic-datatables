using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Base.Repositories;
using LinqKit;

namespace GenericDatatables.Default.Base.Repositories
{
    public abstract class Repository<TEntity, TContext>: IRepository<TEntity>
        where TEntity : class, IDeletable, IIdentifiable
        where TContext : DbContext

    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entitySet;
        private readonly IQueryable<TEntity> _entities;

        public Repository(TContext context)
        {
            _context = context;
            _entitySet = context.Set<TEntity>();
            _entities = _entitySet;
        }

        /// <summary>
        ///     Gets the DbContext
        /// </summary>
        protected virtual TContext Context
        {
            get { return _context; }
        }

        /// <summary>
        ///     Gets the entities
        /// </summary>
        protected virtual IQueryable<TEntity> Entities(IEntityFilter<TEntity> filter = null, IEntitySorter<TEntity> sorter = null, IEntityIncluder<TEntity> includer = null)
        {
            var entities = _entities;
            if (includer != null)
                entities = includer.AddInclusions(entities);
            entities = entities.AsExpandable().Where(e => !e.Deleted);
            if (filter != null)
                entities = filter.Filter(entities);
            if (sorter != null)
                entities = sorter.Sort(entities);
            return entities;
        }

        /// <summary>
        ///     Gets the editable dbset
        /// </summary>
        protected virtual DbSet<TEntity> EntitySet
        {
            get { return _entitySet; }
        }

        public IQueryable<TEntity> List(IEntityFilter<TEntity> filter = null,
            IEntitySorter<TEntity> sorter = null,
            int? page = null,
            int? pageSize = null,
            IEntityIncluder<TEntity> includer = null)
        {
            if ((page.HasValue || pageSize.HasValue) && sorter == null)
            {
                throw new ArgumentException("You have to define a sorting order if you specify a page or pageSize! (IEntitySorter was null)");
            }

            if (page.HasValue && !pageSize.HasValue)
            {
                throw new ArgumentException("You have to define a pageSize if you specify a page!");
            }

            var entities = Entities(filter, sorter, includer);

            if (page != null)
                entities = entities.Skip(pageSize.Value * page.Value);

            if (pageSize != null)
                entities = entities.Take(pageSize.Value);

            return entities;
        }

        public virtual int Count(IEntityFilter<TEntity> filter = null)
        {
            return Entities(filter).Count();
        }

        public bool Any(IEntityFilter<TEntity> filter = null)
        {
            return Entities(filter).Any();
        }

        public TEntity SingleOrDefault(IEntityFilter<TEntity> filter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, includer: includer).SingleOrDefault();
        }

        public TEntity Single(IEntityFilter<TEntity> filter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, includer: includer).Single();
        }

        public TEntity FirstOrDefault(IEntityFilter<TEntity> filter = null, IEntitySorter<TEntity> sorter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, sorter, includer).FirstOrDefault();
        }

        public TEntity First(IEntityFilter<TEntity> filter = null, IEntitySorter<TEntity> sorter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, sorter, includer).First();
        }

        public IEnumerable<TResult> Select <TResult>(Func<TEntity, TResult> selector,
            IEntityFilter<TEntity> filter = null,
            IEntitySorter<TEntity> sorter = null,
            IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, sorter, includer).Select(selector);
        }

        public virtual TEntity Find(int id)
        {
            return EntitySet.Find(id);
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            if (entity.Id == 0)
            {
                Add(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(int id)
        {
            TEntity entity = Find(id);
            if (entity != null)
                Delete(entity);
        }

        public virtual void HardDelete(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                EntitySet.Attach(entity);
                EntitySet.Remove(entity);
            }
        }

        public virtual void HardDelete(int id)
        {
            TEntity entity = Find(id);
            if (entity != null)
                HardDelete(entity);
        }

        public TResult Query<TResult>(Func<IQueryable<TEntity>, TResult> query)
        {
            return query(Entities());
        }

        protected virtual void Add(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                EntitySet.Add(entity);
            }
        }

        protected virtual void Update(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                EntitySet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }
    }
}
