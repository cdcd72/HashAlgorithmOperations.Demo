using HashCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    /// <summary>
    /// 雜湊工具
    /// </summary>
    public class HashTool : BaseHashTool
    {
        private readonly IConfiguration _config;

        #region Constructor

        public HashTool(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region HMAC - SHA256

        /// <summary>
        /// HMAC - SHA256 加密
        /// </summary>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public string HMACSHA256(string message)
        {
            return base.HMACSHA256(message, _config.GetValue<string>("HMACSHA256:Key"));
        }

        /// <summary>
        /// HMAC - SHA256 校驗
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="hash">雜湊值</param>
        /// <returns></returns>
        public bool VerifyHMACSHA256(string message, string hash)
        {
            return base.VerifyHMACSHA256(message, _config.GetValue<string>("HMACSHA256:Key"), hash);
        }

        #endregion
    }
}
