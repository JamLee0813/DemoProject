using DemoProject.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoProject.Services.Base
{
    public class BaseServices<TEntity> where TEntity : class, new()
    {
        public BaseRepository<TEntity> BaseDal; //通过在子类的构造函数中注入，这里是基类，不用构造函数

        #region 增

        /// <summary>
        ///     写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> AddAsync(TEntity entity) => await BaseDal.AddAsync(entity);

        #endregion 增

        #region 删

        /// <summary>
        ///     删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(object id) => await BaseDal.DeleteByIdAsync(id);

        #endregion 删

        #region 改

        /// <summary>
        ///     更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity) => await BaseDal.UpdateAsync(entity);

        #endregion 改

        #region 查

        /// <summary>
        ///     查是否存在此数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, string tableName = null) => await BaseDal.AnyAsync(whereExpression, tableName);

        /// <summary>
        ///     功能描述:查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression) => await BaseDal.QueryAsync(whereExpression);

        #endregion 查
    }
}