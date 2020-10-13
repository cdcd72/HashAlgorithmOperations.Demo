using System;
using System.Text;
using Cryptography = System.Security.Cryptography;

namespace HashCore.MACAlgorithm
{
    /// <summary>
    /// HMAC - SHA256 加密
    /// </summary>
    public static class HMACSHA256
    {
        /// <summary>
        /// 雜湊
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="key">秘密金鑰</param>
        /// <returns></returns>
        public static string Hash(string message, string key)
        {
            // UTF8 not emit BOM, https://docs.microsoft.com/zh-tw/dotnet/api/system.text.utf8encoding?view=netcore-3.1
            UTF8Encoding utf8 = new UTF8Encoding();

            byte[] messageBytes = utf8.GetBytes(message);
            byte[] keyBytes = utf8.GetBytes(key);

            using (Cryptography.HMACSHA256 hmacsha256 = new Cryptography.HMACSHA256(keyBytes))
            {
                byte[] hashedBytes = hmacsha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashedBytes).Replace("-", string.Empty).ToLower();
            }
        }

        /// <summary>
        /// 校驗
        /// </summary>
        /// <param name="hash">雜湊值</param>
        /// <param name="message">訊息</param>
        /// <param name="key">秘密金鑰</param>
        public static bool Verify(string hash, string message, string key)
        {
            return hash == Hash(message, key);
        }
    }
}
