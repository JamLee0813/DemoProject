using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace DemoProject.Common.Helper
{
    /// <summary>
    ///     配置访问工具类 bob 2019-07-08
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        ///     初始化系统配置（默认添加 appsettings.json 文件配置、系统环境变量配置、阿波罗统一配置管理系统） bob 2019-07-08
        /// </summary>
        static ConfigHelper()
        {
            ConfigBuilder = new ConfigurationBuilder();
            ConfigBuilder.SetBasePath(BasePath);
            //添加对系统环境变量配置的支持   bob   2019-08-06
            ConfigBuilder.AddEnvironmentVariables();
            //添加appsettings.json文件配置   bob   2019-07-13
            AddJsonFile("appsettings.json", true);
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrWhiteSpace(env)) AddJsonFile($"appsettings.{env}.json", true);
        }

        /// <summary>
        /// </summary>
        public static ConfigurationBuilder ConfigBuilder { get; set; }

        /// <summary>
        /// </summary>
        public static ConfigurationRoot ConfigRoot { get; set; }

        /// <summary>
        /// </summary>
        public static string BasePath { get; } = Directory.GetCurrentDirectory();

        /// <summary>
        ///     添加自定义的json文件配置到系统配置，PS：因是后期加入，取值优先级较高 bob 2019-07-13
        /// </summary>
        /// <param name="jsonFileName">相对当前应用工作路径的json配置文件地址</param>
        /// <param name="optional">该配置文件是否可选：false = 必需，但找不到该文件时会抛出异常；true = 可选，该配置文件不是必需的</param>
        /// <returns></returns>
        public static ConfigurationBuilder AddJsonFile(string jsonFileName, bool optional = false)
        {
            ConfigBuilder.AddJsonFile(jsonFileName, optional, true);
            ConfigRoot = (ConfigurationRoot)ConfigBuilder.Build();
            return ConfigBuilder;
        }

        /// <summary>
        ///     添加内存数据到系统配置中，PS：因是后期加入，取值优先级高。 典型应用场景如：在测试项目中，需要指定不同的配置值来做值覆盖测试 bob 2019-07-13
        /// </summary>
        /// <param name="key">配置key</param>
        /// <param name="value">配置value</param>
        /// <returns></returns>
        public static ConfigurationBuilder AddInMemoryCollection(string key, string value)
        {
            return AddInMemoryCollection(new Dictionary<string, string> { { key, value } });
        }

        /// <summary>
        ///     添加内存数据到系统配置中，PS：因是后期加入，取值优先级高。 典型应用场景如：在测试项目中，需要指定不同的配置值来做值覆盖测试 bob 2019-07-13
        /// </summary>
        /// <param name="dic">配置数据</param>
        /// <returns></returns>
        public static ConfigurationBuilder AddInMemoryCollection(Dictionary<string, string> dic)
        {
            ConfigBuilder.AddInMemoryCollection(dic);
            ConfigRoot = (ConfigurationRoot)ConfigBuilder.Build();
            return ConfigBuilder;
        }

        /// <summary>
        ///     Extracts the value with the specified key and converts it to type T. bob 2019-07-08
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <returns>The converted value.</returns>
        public static T GetValue<T>(string key) => ConfigRoot.GetValue<T>(key);

        /// <summary>
        ///     Extracts the value with the specified key and converts it to type string. bob 2019-07-08
        /// </summary>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <returns>The converted value.</returns>
        public static string GetValue(string key) => ConfigRoot.GetValue<string>(key);

        /// <summary>
        ///     Extracts the value with the specified key and converts it to type T. bob 2019-07-08
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="defaultValue">The default value to use if no value is found.</param>
        /// <returns>The converted value.</returns>
        public static T GetValue<T>(string key, T defaultValue) => ConfigRoot == null ? defaultValue : ConfigRoot.GetValue(key, defaultValue);
    }
}