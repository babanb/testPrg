using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Interfaces;

namespace Repository.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            if (filter != null)
            {
                dbQuery = dbQuery.Where(filter);
            }

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            if (orderBy != null)
            {
                dbQuery = orderBy(dbQuery);
            }

            return dbQuery.AsNoTracking().ToList();
        }

        public virtual IEnumerable<TEntity> GetAllSingleTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            if (filter != null)
            {
                dbQuery = dbQuery.Where(filter);
            }

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            if (orderBy != null)
            {
                dbQuery = orderBy(dbQuery);
            }

            return dbQuery.ToList();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).AsNoTracking().ToList();
        }

        public void ExecuteSqlCommand(string sql, params object[] parameters)
        {
            context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            dbQuery = dbQuery.Where(where);

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            return dbQuery.AsNoTracking().SingleOrDefault();
        }

        public virtual TEntity GetSingleTracking(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            dbQuery = dbQuery.Where(where);

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            return dbQuery.SingleOrDefault();
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = dbSet;

            dbQuery = dbQuery.Where(where);

            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            return dbQuery.AsNoTracking().FirstOrDefault();
        }

        public virtual void Insert(TEntity item)
        {
            dbSet.Add(item);
        }

        public virtual void Update(TEntity item)
        {
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity item)
        {
            if (context.Entry(item).State == EntityState.Detached)
            {
                dbSet.Attach(item);
            }
            dbSet.Remove(item);
        }
    }
}
