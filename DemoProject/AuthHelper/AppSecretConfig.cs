using DemoProject.Common.Config;
using System.IO;

namespace DemoProject.AuthHelper
{
    /// <summary>
    ///     服务密钥配置
    /// </summary>
    public class AppSecretConfig
    {
        private static readonly string Audience_Secret = ConfigFile.AudienceSecret;
        private static readonly string Audience_Secret_File = ConfigFile.AudienceSecretFile;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static string GetAudience_Secret_String() => InitAudience_Secret();

        private static string InitAudience_Secret()
        {
            var securityString = DifDBConnOfSecurity(Audience_Secret_File);
            if (!string.IsNullOrEmpty(Audience_Secret_File) && !string.IsNullOrEmpty(securityString))
            {
                return securityString;
            }
            else
            {
                return Audience_Secret;
            }
        }

        private static string DifDBConnOfSecurity(params string[] conn)
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
                catch (System.Exception) { }
            }

            return conn[conn.Length - 1];
        }
    }
}