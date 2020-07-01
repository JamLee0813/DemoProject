using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

namespace DemoProject.Common.Config
{
    /// <summary>
    ///     配置
    /// </summary>
    public class AppSettings
    {
        private static readonly IConfigurationSection ConfigurationSection;

        static AppSettings()
        {
            string Path = "appsettings.json";
            {
                //如果你把配置文件 是 根据环境变量来分开了，可以这样写
                Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            }

            //Configuration = new ConfigurationBuilder()
            //.Add(new JsonConfigurationSource { Path = Path, ReloadOnChange = true })//请注意要把当前appsetting.json 文件->右键->属性->复制到输出目录->始终复制
            //.Build();

            Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
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

        private static IConfiguration Configuration { get; set; }

        //static Appsettings()
        //{
        //    //ReloadOnChange = true 当appsettings.json被修改时重新加载
        //    Configuration = new ConfigurationBuilder()
        //    .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })//请注意要把当前appsetting.json 文件->右键->属性->复制到输出目录->始终复制
        //    .Build();
        //}

        /// <summary>
        ///     封装要操作的字符
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string App(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }

                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}