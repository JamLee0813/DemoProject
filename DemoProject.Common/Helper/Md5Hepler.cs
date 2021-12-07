using System;
using System.Security.Cryptography;
using System.Text;

namespace DemoProject.Common.Helper
{
    /// <summary>
    ///     MD5加密帮助类
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        ///     32位MD5加密
        /// </summary>
        /// <param name="input">待加密文本</param>
        /// <returns></returns>
        public static string ToMd5_32(this string input)
        {
            using var md5 = MD5.Create();//32位大写
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var strResult = BitConverter.ToString(result);
            var result32 = strResult.Replace("-", "");
            return result32;
        }
    }
}