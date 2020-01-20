using BlogModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestBlog.IRepositoty;

namespace TestBlog.Repositoty
{
    public class BaseRepositoty<TEntity> : IBaseRepositoty<TEntity> where TEntity : class, new()
    {
        private readonly BlogContext _dbContext = new BlogContext();
        private DbSet<TEntity> _dbSet;
        public BaseRepositoty()
        {
            _dbSet = _dbContext.Set<TEntity>();
        }
        public void insert(TEntity tEntity)
        {
            _dbSet.Add(tEntity);
        }
        public void Update(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbContext.Entry(tEntity).State = EntityState.Modified;
        }
        public void Delete(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbSet.Remove(tEntity);
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesByuPage<TType>(int pageSize, int pageIndex, bool isAsc, Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda)
        {
            //查询
            var result = _dbSet.Where(whereLamebda);
            //排序
            result = isAsc ? result.OrderBy(orderByLamebda) : result.OrderByDescending(orderByLamebda);
            //分页
            var offset = (pageIndex - 1) * pageSize;
            result = result.Skip(offset).Take(pageSize);
            return await result.ToListAsync();
        }
        public async Task<int> QueryCount(Expression<Func<TEntity, bool>> whereLamebda)
        {
            return await _dbSet.CountAsync();
        }
        public async Task<IEnumerable<TEntity>> QueryEntities(Expression<Func<TEntity, bool>> whereLamebda)
        {
            return await _dbSet.Where(whereLamebda).ToListAsync();
        }
        public async Task<TEntity> QueryEntity(Expression<Func<TEntity, bool>> whereLamebda)
        {
            return await _dbSet.FirstOrDefaultAsync(whereLamebda);
        }
        public bool SaveChanges()
        {
            return _dbContext.SaveChanges()>0;
        }
        public async Task<IEnumerable<TEntity>> QueryBySqlData(string SqlText, params object[] parameters)
        {
            if (parameters == null)
                return await _dbSet.FromSqlRaw(SqlText).ToListAsync();
            else
                return await _dbSet.FromSqlRaw(SqlText, parameters).ToListAsync();
        }
        public async Task<int> QueryBySql(string SqlText, params object[] parameters)
        {
            if(parameters == null)
                return await _dbContext.Database.ExecuteSqlRawAsync(SqlText);
            else
                return await _dbContext.Database.ExecuteSqlRawAsync(SqlText, parameters);
        }
    }
}
