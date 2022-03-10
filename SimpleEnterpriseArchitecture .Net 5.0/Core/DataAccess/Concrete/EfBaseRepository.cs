using Core.DataAccess.Abstract;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public abstract class EfBaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public virtual bool Add(TEntity entity)
        {
            return true;
        }

        public virtual bool Delete(TEntity entity)
        {
            return false;
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
            return true;
        }
    }
}
