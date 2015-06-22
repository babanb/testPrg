using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;

namespace Repository.Implementations
{
    public class SoftDeleteRepository<TEntity> : GenericRepository<TEntity> where TEntity : class, ISoftDelete
    {
        public SoftDeleteRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            if (filter != null)
            {
                dbQuery = dbQuery.Where(filter);
            }

            dbQuery = dbQuery.Where(e => !e.IsDeleted);

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            if (orderBy != null)
            {
                dbQuery = orderBy(dbQuery);
            }

            return dbQuery.AsNoTracking().ToList();
        }

        public override TEntity GetSingle(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            dbQuery = dbQuery.Where(where);
            dbQuery = dbQuery.Where(e => !e.IsDeleted);

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            return dbQuery.AsNoTracking().SingleOrDefault();
        }

        public override TEntity GetSingleTracking(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            dbQuery = dbQuery.Where(where);
            dbQuery = dbQuery.Where(e => !e.IsDeleted);

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            return dbQuery.SingleOrDefault();
        }
    }
}
