using DemoProject.Model.Dto;
using DemoProject.Repository.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoProject.Repository.Base
{
    public class BaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository()
        {
            //DbContext.Init(ConfigFile.ConnectionString);
            Context = DbContext.GetDbContext();
            Db = Context.Db;
            EntityDb = Context.GetEntityDb<TEntity>(Db);
        }

        /// <summary>
        ///     构造方法，使用动态的数据库连接串
        /// </summary>
        /// <param name="connectionString"></param>
        public BaseRepository(string connectionString)
        {
            Context = new DbContext(connectionString);
            Db = Context.Db;
            EntityDb = Context.GetEntityDb<TEntity>(Db);
        }

        public DbContext Context { get; set; }
        internal SqlSugarClient Db { get; }
        internal SimpleClient<TEntity> EntityDb { get; }

        #region 增或改

        /// <summary>
        ///     插入或更新一条数据(同步)
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <param name="insertExpression">插入字段表达式</param>
        /// <param name="insertIgnoreExpression">忽略插入字段表达式</param>
        /// <param name="updateExpression">更新字段表达式</param>
        /// <param name="updateIgnoreExpression">忽略更新字段表达式</param>
        /// <param name="updateWhereExpression">更新条件表达式</param>
        /// <returns></returns>
        public TEntity SaveSync(TEntity entity,
            Expression<Func<TEntity, object>> insertExpression = null,
            Expression<Func<TEntity, object>> insertIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateExpression = null,
            Expression<Func<TEntity, object>> updateIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateWhereExpression = null)
        {
            var command = Db.Saveable(entity);
            if (insertExpression != null) command.InsertColumns(insertExpression);
            if (insertIgnoreExpression != null) command.InsertIgnoreColumns(insertIgnoreExpression);
            if (updateExpression != null) command.UpdateColumns(updateExpression);
            if (updateIgnoreExpression != null) command.UpdateIgnoreColumns(updateIgnoreExpression);
            if (updateWhereExpression != null) command.UpdateWhereColumns(updateWhereExpression);

            //这种方式会以主键为条件
            return command.ExecuteReturnEntity();
        }

        /// <summary>
        ///     批量插入或更新(同步)
        /// </summary>
        /// <param name="entities">实体数据</param>
        /// <param name="insertExpression">插入字段表达式</param>
        /// <param name="insertIgnoreExpression">忽略插入字段表达式</param>
        /// <param name="updateExpression">更新字段表达式</param>
        /// <param name="updateIgnoreExpression">忽略更新字段表达式</param>
        /// <param name="updateWhereExpression">更新条件表达式</param>
        /// <returns></returns>
        public List<TEntity> SaveSync(List<TEntity> entities,
            Expression<Func<TEntity, object>> insertExpression = null,
            Expression<Func<TEntity, object>> insertIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateExpression = null,
            Expression<Func<TEntity, object>> updateIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateWhereExpression = null)
        {
            var command = Db.Saveable(entities);
            if (insertExpression != null) command.InsertColumns(insertExpression);
            if (insertIgnoreExpression != null) command.InsertIgnoreColumns(insertIgnoreExpression);
            if (updateExpression != null) command.UpdateColumns(updateExpression);
            if (updateIgnoreExpression != null) command.UpdateIgnoreColumns(updateIgnoreExpression);
            if (updateWhereExpression != null) command.UpdateWhereColumns(updateWhereExpression);

            //这种方式会以主键为条件
            return command.ExecuteReturnList();
        }

        /// <summary>
        ///     插入或更新一条数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <param name="insertExpression">插入字段表达式</param>
        /// <param name="insertIgnoreExpression">忽略插入字段表达式</param>
        /// <param name="updateExpression">更新字段表达式</param>
        /// <param name="updateIgnoreExpression">忽略更新字段表达式</param>
        /// <param name="updateWhereExpression">更新条件表达式</param>
        /// <returns></returns>
        public async Task<TEntity> Save(TEntity entity,
            Expression<Func<TEntity, object>> insertExpression = null,
            Expression<Func<TEntity, object>> insertIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateExpression = null,
            Expression<Func<TEntity, object>> updateIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateWhereExpression = null)
        {
            var command = Db.Saveable(entity);
            if (insertExpression != null) command.InsertColumns(insertExpression);
            if (insertIgnoreExpression != null) command.InsertIgnoreColumns(insertIgnoreExpression);
            if (updateExpression != null) command.UpdateColumns(updateExpression);
            if (updateIgnoreExpression != null) command.UpdateIgnoreColumns(updateIgnoreExpression);
            if (updateWhereExpression != null) command.UpdateWhereColumns(updateWhereExpression);

            //这种方式会以主键为条件
            return await command.ExecuteReturnEntityAsync();
        }

        /// <summary>
        ///     批量插入或更新
        /// </summary>
        /// <param name="entities">实体数据</param>
        /// <param name="insertExpression">插入字段表达式</param>
        /// <param name="insertIgnoreExpression">忽略插入字段表达式</param>
        /// <param name="updateExpression">更新字段表达式</param>
        /// <param name="updateIgnoreExpression">忽略更新字段表达式</param>
        /// <param name="updateWhereExpression">更新条件表达式</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Save(List<TEntity> entities,
            Expression<Func<TEntity, object>> insertExpression = null,
            Expression<Func<TEntity, object>> insertIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateExpression = null,
            Expression<Func<TEntity, object>> updateIgnoreExpression = null,
            Expression<Func<TEntity, object>> updateWhereExpression = null)
        {
            var command = Db.Saveable(entities);
            if (insertExpression != null) command.InsertColumns(insertExpression);
            if (insertIgnoreExpression != null) command.InsertIgnoreColumns(insertIgnoreExpression);
            if (updateExpression != null) command.UpdateColumns(updateExpression);
            if (updateIgnoreExpression != null) command.UpdateIgnoreColumns(updateIgnoreExpression);
            if (updateWhereExpression != null) command.UpdateWhereColumns(updateWhereExpression);

            //这种方式会以主键为条件
            return await command.ExecuteReturnListAsync();
        }

        #endregion 增或改

        #region 增

        /// <summary>
        ///     写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            //var i = await Task.Run(() => _db.Insertable(entity).ExecuteReturnBigIdentity());
            ////返回的i是long类型,这里你可以根据你的业务需要进行处理
            //return (int)i;

            var insert = Db.Insertable(entity);
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        ///     写入实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lstColumns"></param>
        /// <param name="lstIgnoreColumns"></param>
        /// <param name="tableName">写入与实体名字不一样的表名</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null,
            string tableName = null)
        {
            var insert = string.IsNullOrWhiteSpace(tableName)
                ? Db.Insertable(entity)
                : Db.Insertable(entity).AS(tableName);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                insert = insert.IgnoreColumns(lstIgnoreColumns.ToArray());
            if (lstColumns != null && lstColumns.Count > 0) insert = insert.InsertColumns(lstColumns.ToArray());
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        ///     写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumns">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        public async Task<int> Add(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null)
        {
            var insert = Db.Insertable(entity);
            if (insertColumns == null)
                return await insert.ExecuteReturnIdentityAsync();
            return await insert.InsertColumns(insertColumns).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        ///     批量插入实体(速度快)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> Add(List<TEntity> listEntity) => await Db.Insertable(listEntity).ExecuteCommandAsync();

        /// <summary>
        ///     写入实体数据(同步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddSync(TEntity entity) => Db.Insertable(entity).ExecuteReturnIdentity();

        /// <summary>
        ///     批量插入实体(速度快)(同步)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public int AddSync(List<TEntity> listEntity) => Db.Insertable(listEntity).ExecuteCommand();

        #endregion 增

        #region 删

        /// <summary>
        ///     根据表达式删除一条数据(同步)
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<TEntity, bool>> whereExpression, string tableName = null)
        {
            var delete = string.IsNullOrWhiteSpace(tableName)
                ? Db.Deleteable(whereExpression)
                : Db.Deleteable(whereExpression).AS(tableName);

            return delete.ExecuteCommandHasChange();
        }

        /// <summary>
        ///     根据表达式删除一条数据(同步)
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression, string tableName = null)
        {
            var delete = string.IsNullOrWhiteSpace(tableName)
                ? Db.Deleteable(whereExpression)
                : Db.Deleteable(whereExpression).AS(tableName);

            return await delete.ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        ///     根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await Db.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        ///     删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            //var i = await Task.Run(() => _db.Deleteable<TEntity>(id).ExecuteCommand());
            //return i > 0;
            return await Db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        ///     删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids) =>
            await Db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();

        /// <summary>
        ///     逻辑删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> LogicalDeleteById(object id)
        {
            return await Db.Updateable<TEntity>(new { IsDeleted = true, Id = id }).ExecuteCommandHasChangeAsync();
        }

        #endregion 删

        #region 改

        /// <summary>
        ///     批量更新实体数据(同步)
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreColumnsExpression"></param>
        /// <param name="updateColumnsExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int Update(List<TEntity> entities,
            Expression<Func<TEntity, object>> ignoreColumnsExpression = null,
            Expression<Func<TEntity, object>> updateColumnsExpression = null,
            string tableName = null
        )
        {
            var update = string.IsNullOrWhiteSpace(tableName)
                ? Db.Updateable(entities)
                : Db.Updateable(entities).AS(tableName);
            if (ignoreColumnsExpression != null) update = update.IgnoreColumns(ignoreColumnsExpression);
            if (updateColumnsExpression != null) update = update.UpdateColumns(updateColumnsExpression);
            return update.ExecuteCommand();
        }

        /// <summary>
        ///     批量更新实体数据(异步)
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreColumnsExpression"></param>
        /// <param name="updateColumnsExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(List<TEntity> entities,
            Expression<Func<TEntity, object>> ignoreColumnsExpression = null,
            Expression<Func<TEntity, object>> updateColumnsExpression = null,
            string tableName = null
        )
        {
            var update = string.IsNullOrWhiteSpace(tableName)
                ? Db.Updateable(entities)
                : Db.Updateable(entities).AS(tableName);
            if (ignoreColumnsExpression != null) update = update.IgnoreColumns(ignoreColumnsExpression);
            if (updateColumnsExpression != null) update = update.UpdateColumns(updateColumnsExpression);
            return await update.ExecuteCommandAsync();
        }

        /// <summary>
        ///     更新实体数据(同步)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreColumnsExpression"></param>
        /// <param name="updateColumnsExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool Update(TEntity entity,
            Expression<Func<TEntity, object>> ignoreColumnsExpression = null,
            Expression<Func<TEntity, object>> updateColumnsExpression = null,
            string tableName = null
        )
        {
            var update = string.IsNullOrWhiteSpace(tableName)
                ? Db.Updateable(entity)
                : Db.Updateable(entity).AS(tableName);
            if (ignoreColumnsExpression != null) update = update.IgnoreColumns(ignoreColumnsExpression);
            if (updateColumnsExpression != null) update = update.UpdateColumns(updateColumnsExpression);
            return update.ExecuteCommandHasChange();
        }

        /// <summary>
        ///     更新实体数据(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreColumnsExpression"></param>
        /// <param name="updateColumnsExpression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity,
            Expression<Func<TEntity, object>> ignoreColumnsExpression = null,
            Expression<Func<TEntity, object>> updateColumnsExpression = null,
            string tableName = null
        )
        {
            var update = string.IsNullOrWhiteSpace(tableName)
                ? Db.Updateable(entity)
                : Db.Updateable(entity).AS(tableName);
            if (ignoreColumnsExpression != null) update = update.IgnoreColumns(ignoreColumnsExpression);
            if (updateColumnsExpression != null) update = update.UpdateColumns(updateColumnsExpression);

            return await update.ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        ///     更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            //这种方式会以主键为条件
            return await Db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
        {
            return await Db.Ado.ExecuteCommandAsync(strSql, parameters) > 0;
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            //return await Task.Run(() => _db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
            return await Db.Updateable(entity).Where(strWhere).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(
            TEntity entity,
            List<string> lstColumns = null,
            List<string> lstIgnoreColumns = null,
            string strWhere = "",
            string tableName = null
        )
        {
            var up = string.IsNullOrWhiteSpace(tableName) ? Db.Updateable(entity) : Db.Updateable(entity).AS(tableName);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
            if (lstColumns != null && lstColumns.Count > 0) up = up.UpdateColumns(lstColumns.ToArray());
            if (!string.IsNullOrEmpty(strWhere)) up = up.Where(strWhere);
            return await up.ExecuteCommandHasChangeAsync();
        }

        #endregion 改

        #region 查

        public bool Any(Expression<Func<TEntity, bool>> whereExpression,
            string tableName = null)
        {
            var query = string.IsNullOrWhiteSpace(tableName)
                ? Db.Queryable<TEntity>()
                : Db.Queryable<TEntity>().AS(tableName);
            return query.WhereIF(whereExpression != null, whereExpression).Any();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression,
            string tableName = null)
        {
            var query = string.IsNullOrWhiteSpace(tableName)
                ? Db.Queryable<TEntity>()
                : Db.Queryable<TEntity>().AS(tableName);
            return await query.WhereIF(whereExpression != null, whereExpression).AnyAsync();
        }

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public async Task<int> Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).CountAsync();
        }

        public async Task<TEntity> QueryById(int objId) => await Db.Queryable<TEntity>().In(objId).SingleAsync();

        public TEntity QueryByIdSync(object objId) => Db.Queryable<TEntity>().In(objId).Single();

        /// <summary>
        ///     功能描述:根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(int objId, bool blnUseCache = false)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
            return await Db.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).SingleAsync();
        }

        /// <summary>
        ///     功能描述:根据ID查询数据
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(int[] lstIds)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().In(lstIds).ToList());
            return await Db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        /// <summary>
        ///     根据ID查询一条未被逻辑删除的数据
        /// </summary>
        /// <param name="objId">唯一主键ID</param>
        /// <returns></returns>
        public async Task<TEntity> QueryByIdNotDeleted(int objId)
        {
            return await Db.Queryable<TEntity>().In(objId).Where("isdeleted = 'f'").SingleAsync();
        }

        /// <summary>
        ///     根据ID查询未被逻辑删除的数据
        /// </summary>
        /// <param name="lstIds">唯一主键ID列表</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryByIdsNotDeleted(int[] lstIds)
        {
            return await Db.Queryable<TEntity>().In(lstIds).Where("isdeleted = 'f'").ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            //return await Task.Run(() => _entityDb.GetList());
            return await Db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await Db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <param name="tableName">指定表名</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFields,
            string tableName = null)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(whereExpression != null, whereExpression).ToList());
            if (!string.IsNullOrWhiteSpace(tableName))
                return await Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).AS(tableName)
                    .OrderByIF(strOrderByFields != null, strOrderByFields).ToListAsync();
            return await Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(strOrderByFields != null, strOrderByFields).ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList());
            return await Db.Queryable<TEntity>()
                .OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc)
                .WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFields)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
            return await Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields)
                .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询数据列表(同步)
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="tableName"></param>
        /// <returns>数据列表</returns>
        public List<TEntity> QuerySync(Expression<Func<TEntity, bool>> whereExpression = null,
            Expression<Func<TEntity, object>> orderByExpression = null,
            string tableName = null)
        {
            var query = string.IsNullOrWhiteSpace(tableName)
                ? Db.Queryable<TEntity>()
                : Db.Queryable<TEntity>().AS(tableName);
            return query.WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(orderByExpression != null, orderByExpression)
                .ToList();
        }

        /// <summary>
        ///     功能描述:查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFields)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList());
            return await Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields)
                .WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
        }

        /// <summary>
        ///     功能描述:查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFields)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList());
            return await Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields)
                .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToListAsync();
        }

        /// <summary>
        ///     功能描述:分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFields)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize));
            return await Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields)
                .WhereIF(whereExpression != null, whereExpression).ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        ///     功能描述:分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intPageIndex,
            int intPageSize,
            string strOrderByFields)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
            return await Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields)
                .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        ///     分页查询[使用版本，其他分页未测试]
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFields">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex = 1, int intPageSize = 20, string strOrderByFields = null)
        {
            RefAsync<int> totalCount = 0;
            var list = await Db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            var pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<TEntity>
            {
                DataCount = totalCount,
                PageCount = pageCount,
                Page = intPageIndex,
                PageSize = intPageSize,
                Data = list
            };
        }

        /// <summary>
        ///     查询-多表查询
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) =&gt; new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">
        ///     返回表达式 (s1, s2) =&gt; new { Id =s1.UserNo, Id1 = s2.UserNo}
        /// </param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =&gt;w1.UserNo == "")</param>
        /// <returns>值</returns>
        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            if (whereLambda == null) return await Db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
            return await Db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
        }

        public async Task<List<TResult>> QueryMuch<T1, T2, TResult>(
            Expression<Func<T1, T2, object[]>> joinExpression,
            Expression<Func<T1, T2, TResult>> selectExpression,
            Expression<Func<T1, T2, bool>> whereLambda = null) where T1 : class, new()
        {
            if (whereLambda == null) return await Db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
            return await Db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
        }

        #endregion 查
    }
}