using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信签名
    /// </summary>
    public static partial class Signature
    {
        /// <summary>
        /// 创建微信加密签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string Create(string timestamp, string nonce, string token, string msg = null) =>
            string.Join("", new[] { token, timestamp, nonce, msg }.OrderBy(z => z).ToArray()).EncodeToSHA1();
        
    }
}
