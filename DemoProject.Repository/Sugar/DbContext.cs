using DataCenter.Common.Helper;
using DemoProject.Common.Config;
using SqlSugar;
using System;
using System.Linq;

namespace DemoProject.Repository.Sugar
{
    /// <summary>
    /// </summary>
    public class DbContext
    {
        private static string _connectionString;
        private static DbType _dbType;

        /// <summary>
        ///     数据库上下文实例（自动关闭连接） Blog.Core
        /// </summary>
        public static DbContext Context => new DbContext();

        /// <summary>
        ///     数据连接对象 Blog.Core
        /// </summary>
        public SqlSugarClient Db { get; }

        #region 构造方法

        /// <summary>
        ///     功能描述:构造函数 作　　者:Blog.Core
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        public DbContext(bool blnIsAutoCloseConnection = true)
        {
            // if (string.IsNullOrEmpty(_connectionString)) throw new ArgumentNullException("数据库连接字符串为空");
            Init(ConfigFile.ConnectionString);
            Db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = blnIsAutoCloseConnection,
                IsShardSameThread = false,
                ConfigureExternalServices = new ConfigureExternalServices(),
                MoreSettings = new ConnMoreSettings
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });

            //调试代码 用来打印SQL
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
#if DEBUG
                Log.WriteDebugLine($"sql脚本：{sql}\r\n{Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value))}");
                Console.WriteLine();
#endif
            };
            //执行SQL 错误事件
            Db.Aop.OnError = OnError;
        }

        /// <summary>
        ///     构造方法，使用动态的数据库连接串
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "指定数据库连接字符串为空");

            Db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                IsShardSameThread = false,
                ConfigureExternalServices = new ConfigureExternalServices(),
                MoreSettings = new ConnMoreSettings
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });

            //调试代码 用来打印SQL
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
#if DEBUG
                Log.WriteDebugLine($"sql脚本：{sql}\r\n{Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value))}");
                Console.WriteLine();
#endif
            };
            //执行SQL 错误事件
            Db.Aop.OnError = OnError;
        }

        #endregion 构造方法

        #region 实例方法

        /// <summary>
        ///     功能描述:获取数据库处理对象 作　　者:Blog.Core
        /// </summary>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDb<T>() where T : class, new() => new SimpleClient<T>(Db);

        /// <summary>
        ///     功能描述:获取数据库处理对象 作　　者:Blog.Core
        /// </summary>
        /// <param name="db">db</param>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDb<T>(SqlSugarClient db) where T : class, new() => new SimpleClient<T>(db);

        private void OnError(SqlSugarException exp)
        {
            try
            {
                //exp.sql exp.parameters 可以拿到参数和错误Sql
                Log.WriteDebugLine(
                    $"SQL OnError:{exp.Sql},{exp.Parametres},{exp.InnerException?.Message},{exp.InnerException?.StackTrace}");
            }
            catch (Exception ex)
            {
                Log.WriteErrorLine($"Sql Error:{exp.Sql} {ex.Message} {ex.StackTrace}");
            }
        }

        #endregion 实例方法

        #region 静态方法

        /// <summary>
        ///     功能描述:获得一个DbContext 作　　者:Blog.Core
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接（如果为false，则使用接受时需要手动关闭Db）</param>
        /// <returns>返回值</returns>
        public static DbContext GetDbContext(bool blnIsAutoCloseConnection = true) => new DbContext(blnIsAutoCloseConnection);

        /// <summary>
        ///     功能描述:设置初始化参数 作　　者:Blog.Core
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        /// <param name="enmDbType">数据库类型</param>
        public static void Init(string strConnectionString, DbType enmDbType = DbType.PostgreSQL)
        {
            _connectionString = strConnectionString;
            _dbType = enmDbType;
        }

        #endregion 静态方法
    }
}