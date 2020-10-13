namespace HashCore
{
    /// <summary>
    /// 雜湊工具
    /// </summary>
    public static class HashTool
    {
        #region HMAC - SHA256

        /// <summary>
        /// HMAC - SHA256 加密
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="key">秘密金鑰</param>
        /// <returns></returns>
        public static string HMACSHA256(string message, string key)
        {
            return MACAlgorithm.HMACSHA256.Hash(message, key);
        }

        /// <summary>
        /// HMAC - SHA256 校驗
        /// </summary>
        /// <param name="hash">雜湊值</param>
        /// <param name="message">訊息</param>
        /// <param name="key">秘密金鑰</param>
        /// <returns></returns>
        public static bool VerifyHMACSHA256(string hash, string message, string key)
        {
            return MACAlgorithm.HMACSHA256.Verify(hash, message, key);
        }

        #endregion
    }
}
