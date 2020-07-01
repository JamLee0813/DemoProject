using Microsoft.Extensions.Configuration;
using System.IO;

namespace DemoProject.Common.Config
{
    /// <summary>
    ///     配置信息读取模型
    /// </summary>
    public static class ConfigFile
    {
        private static readonly IConfiguration Configuration;

        private static readonly IConfigurationSection ConfigurationSection;

        static ConfigFile()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            ConfigurationSection = Configuration.GetSection("AppSettings");
        }

        #region 共通

        /// <summary>
        ///     数据库连接字符串
        /// </summary>
        public static string ConnectionString => Configuration.GetSection("ConnectionStrings")["db"];

        /// <summary>
        ///     Token期满小时
        /// </summary>
        public static int TokenExpireHours => int.Parse(ConfigurationSection["TokenExpireHours"]);

        #endregion 共通
    }
}