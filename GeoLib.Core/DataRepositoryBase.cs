using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GeoLib.Core
{
    public abstract class DataRepositoryBase<T, TU> : IDataRepository<T>
        where T : class, IIdentifiableEntity, new()
        where TU : DbContext, new()
    {
        protected abstract DbSet<T> DbSet(TU entityContext);
        protected abstract Expression<Func<T, bool>> IdentifierPredicate(TU entityContext, int id);

        private T AddEntity(TU entityContext, T entity)
        {
            return DbSet(entityContext).Add(entity);
        }

        private IEnumerable<T> GetEntities(TU entityContext)
        {
            return DbSet(entityContext).ToList();
        }

        private T GetEntity(TU entityContext, int id)
        {
            return DbSet(entityContext).Where(IdentifierPredicate(entityContext, id)).FirstOrDefault();
        }

        private T UpdateEntity(TU entityContext, T entity)
        {
            IQueryable<T> query = DbSet(entityContext).Where(IdentifierPredicate(entityContext, entity.EntityId));
            return query.FirstOrDefault();
        }

        public virtual T Add(T entity)
        {
            using (TU entityContext = new TU())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public virtual void Remove(T entity)
        {
            using (TU entityContext = new TU())
            {
                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual void Remove(int id)
        {
            using (TU entityContext = new TU())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual T Update(T entity)
        {
            using (TU entityContext = new TU())
            {
                T existingEntity = UpdateEntity(entityContext, entity);

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public virtual IEnumerable<T> Get()
        {
            using (TU entityContext = new TU())
            {
                return GetEntities(entityContext).ToArray().ToList();
            }
        }

        public virtual T Get(int id)
        {
            using (TU entityContext = new TU())
            {
                return GetEntity(entityContext, id);
            }
        }
    }
}
