using System;
using TestBlog.IService;
using TestBlog.IRepositoty;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestBlog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private IBaseRepositoty<TEntity> _baseRepositoty;

        public BaseService(IBaseRepositoty<TEntity> baseRepositoty)
        {
            _baseRepositoty = baseRepositoty;
        }

        public bool Add(TEntity tEntity)
        {
            _baseRepositoty.insert(tEntity);
            return _baseRepositoty.SaveChanges();
        }
        public bool Modify(TEntity tEntity)
        {
            _baseRepositoty.Update(tEntity);
            return _baseRepositoty.SaveChanges();
        }
        public bool Remov(TEntity tEntity)
        {
            _baseRepositoty.Delete(tEntity);
            return _baseRepositoty.SaveChanges();
        }
        public Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> whereLamebda)
        {
            return _baseRepositoty.QueryEntity(whereLamebda);
        }
        public Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> whereLamebda)
        {
            return _baseRepositoty.QueryEntities(whereLamebda);
        }
        public Task<IEnumerable<TEntity>> GetEntitiesByPpage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda)
        {
            return _baseRepositoty.GetEntitiesByuPage(pageSize, pageIndex, isAsc, whereLamebda, orderByLamebda);
        }
        public Task<int> GetCount(Expression<Func<TEntity, bool>> whereLamebda)
        {
            return _baseRepositoty.QueryCount(whereLamebda);
        }
        public Task<IEnumerable<TEntity>> QueryBySqlData(string SqlText, params object[] parameters)
        {
            return _baseRepositoty.QueryBySqlData(SqlText, parameters);
        }
        public Task<int> QueryBySql(string SqlText, params object[] parameters)
        {
            return _baseRepositoty.QueryBySql(SqlText, parameters);
        }
    }
}
