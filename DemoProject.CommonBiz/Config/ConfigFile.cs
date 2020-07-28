using DemoProject.CommonBiz.Helper;

namespace DemoProject.CommonBiz.Config
{
    /// <summary>
    ///     配置信息读取模型
    /// </summary>
    public static class ConfigFile
    {
        /// <summary>
        ///     项目名
        /// </summary>
        public static string AppName => ConfigHelper.GetValue("AppSettings:AppName");

        #region 鉴权配置

        /// <summary>
        ///     密钥
        /// </summary>
        public static string AudienceSecret => ConfigHelper.GetValue("Audience:Secret");

        /// <summary>
        ///     密钥文件
        /// </summary>
        public static string AudienceSecretFile => ConfigHelper.GetValue("Audience:SecretFile");

        /// <summary>
        ///     发行人
        /// </summary>
        public static string AudienceIssuer => ConfigHelper.GetValue("Audience:Issuer");

        /// <summary>
        ///     订阅人
        /// </summary>
        public static string Audience => ConfigHelper.GetValue("Audience:Audience");

        /// <summary>
        ///     Token有效时长(小时)
        /// </summary>
        public static int TokenValidHours => ConfigHelper.GetValue<int>("Audience:TokenValidHours");

        #endregion 鉴权配置
    }
}