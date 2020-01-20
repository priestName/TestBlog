using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestBlog.IService
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="TEntity">添加内容Model</param>
        /// <returns>bool</returns>
        bool Add(TEntity TEntity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="TEntity">删除条件n => true(查全部) n => n.?=?</param>
        /// <returns>bool</returns>
        bool Remov(TEntity TEntity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="TEntity">条件n => true(查全部) n => n.?=?</param>
        /// <returns>bool</returns>
        bool Modify(TEntity TEntity);
        /// <summary>
        /// 查询行数
        /// </summary>
        /// <param name="whereLamebda">条件n => true(查全部) n => n.?=?</param>
        /// <returns>Int32</returns>
        Task<int> GetCount(Expression<Func<TEntity, bool>> whereLamebda);
        /// <summary>
        /// 查询(Model)
        /// </summary>
        /// <param name="whereLamebda">条件n => true(查全部) n => n.?=?</param>
        /// <returns>TEntity</returns>
        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> whereLamebda);
        /// <summary>
        /// 查询(IEnumerable<Model>)
        /// </summary>
        /// <param name="whereLamebda">条件n => true(查全部) n => n.?=?</param>
        /// <returns>IEnumerable</returns>
        Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> whereLamebda);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="pageSize">每页行数</param>
        /// <param name="pageIndex">起始项</param>
        /// <param name="isAsc">排序方式</param>
        /// <param name="whereLamebda">条件n => true(查全部) n => n.?=?</param>
        /// <param name="orderByLamebda">排序方法</param>
        /// <returns>IEnumerable</returns>
        Task<IEnumerable<TEntity>> GetEntitiesByPpage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda);
        /// <summary>
        /// 原生sql语句查询返回IEnumerable
        /// </summary>
        /// <param name="SqlText">sql语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>IEnumerable</returns>
        Task<IEnumerable<TEntity>> QueryBySqlData(string SqlText, params object[] parameters);
        /// <summary>
        /// 原生sql语句查询返回int
        /// </summary>
        /// <param name="SqlText">sql语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>int</returns>
        Task<int> QueryBySql(string SqlText, params object[] parameters);
    }
}
