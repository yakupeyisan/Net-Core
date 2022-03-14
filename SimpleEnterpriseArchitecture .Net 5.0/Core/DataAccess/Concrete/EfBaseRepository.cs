using Core.DataAccess.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public abstract class EfBaseRepository<TEntity,TContext> : IBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext: DbContext,new()
    {
        public virtual bool Add(TEntity entity)
        {
            using (var context= new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return true;
            }
        }

        public virtual bool Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
        }

        public virtual TEntity Get(Expression<Func<TEntity>> filter)
        {
            return null;
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity>> filter = null)
        {
            return new List<TEntity>();
        }

        public virtual bool Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
    }
}
