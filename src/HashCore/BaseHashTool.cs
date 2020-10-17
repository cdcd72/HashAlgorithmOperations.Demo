namespace HashCore
{
    /// <summary>
    /// 雜湊工具(基底)
    /// </summary>
    public class BaseHashTool
    {
        private readonly MACAlgorithm.HMACSHA256 hmacsha256;

        #region Constructor

        public BaseHashTool()
        {
            hmacsha256 = new MACAlgorithm.HMACSHA256();
        }

        #endregion

        #region HMAC - SHA256

        /// <summary>
        /// HMAC - SHA256 加密
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="key">秘密金鑰</param>
        /// <returns></returns>
        public string HMACSHA256(string message, string key)
        {
            return hmacsha256.Hash(message, key);
        }

        /// <summary>
        /// HMAC - SHA256 校驗
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="key">秘密金鑰</param>
        /// <param name="hash">雜湊值</param>
        /// <returns></returns>
        public bool VerifyHMACSHA256(string message, string key, string hash)
        {
            return hmacsha256.Verify(message, key, hash);
        }

        #endregion
    }
}
