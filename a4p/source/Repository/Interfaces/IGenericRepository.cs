using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        IEnumerable<TEntity> GetAllSingleTracking(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           params Expression<Func<TEntity, object>>[] navigationProperties);

        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        void ExecuteSqlCommand(string query, params object[] parameters);

        TEntity GetSingle(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity GetSingleTracking(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity GetFirst(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        void Insert(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);
    }
}
