using DemoProject.Common.Config;
using System;
using System.IO;

namespace DemoProject.AuthHelper
{
    /// <summary>
    ///     服务密钥配置
    /// </summary>
    public class AppSecretConfig
    {
        private static readonly string AudienceSecret = ConfigFile.AudienceSecret;
        private static readonly string AudienceSecretFile = ConfigFile.AudienceSecretFile;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static string GetAudience_Secret_String() => InitAudience_Secret();

        private static string InitAudience_Secret()
        {
            var securityString = DifDbConnOfSecurity(AudienceSecretFile);
            if (!string.IsNullOrEmpty(AudienceSecretFile) && !string.IsNullOrEmpty(securityString))
            {
                return securityString;
            }
            else
            {
                return AudienceSecret;
            }
        }

        private static string DifDbConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return conn[^1];
        }
    }
}