using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestBlog.IRepositoty
{
    public interface IBaseRepositoty<TEntity> where TEntity : class, new()
    {
        void insert(TEntity TEntity);
        void Delete(TEntity TEntity);
        void Update(TEntity TEntity);
        bool SaveChanges();
        Task<int> QueryCount(Expression<Func<TEntity, bool>> whereLamebda);
        Task<TEntity> QueryEntity(Expression<Func<TEntity, bool>> whereLamebda);
        Task<IEnumerable<TEntity>> QueryEntities(Expression<Func<TEntity, bool>> whereLamebda);
        Task<IEnumerable<TEntity>> GetEntitiesByuPage<TType>(int pageSize, int pageIndex, bool isAsc,
        Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda);
        //object QueryBySql(string SqlText);
        Task<IEnumerable<TEntity>> QueryBySqlData(string SqlText, params object[] parameters);
        Task<int> QueryBySql(string SqlText, params object[] parameters);
    }
}
