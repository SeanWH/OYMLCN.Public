using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace OYMLCN
{
    /// <summary>
    /// CryptographyExtension
    /// </summary>
    public static class CryptographyExtension
    {
        private static string Encoder<T>(this T encryptor, string str) where T: HashAlgorithm
        {
            var sha1bytes = Encoding.UTF8.GetBytes(str);
            byte[] resultHash = encryptor.ComputeHash(sha1bytes);
            string sha1String = BitConverter.ToString(resultHash).ToLower();
            sha1String = sha1String.Replace("-", "");
            return sha1String;
        }

        /// <summary>
        /// 转换字符串为SHA1加密值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeToSHA1(this string str) => SHA1.Create().Encoder(str);
        /// <summary>
        /// 转换字符串为SHA256加密值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeToSHA256(this string str) => SHA256.Create().Encoder(str);
        /// <summary>
        /// 转换字符串为SHA384加密值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeToSHA384(this string str) => SHA384.Create().Encoder(str);
        /// <summary>
        /// 转换字符串为SHA512加密值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeToSHA512(this string str) => SHA512.Create().Encoder(str);

        /// <summary>
        /// 转换字符串为MD5加密值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeToMD5(this string str) => MD5.Create().Encoder(str);



        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoder">编码方式（默认为UTF8）</param>
        /// <returns></returns>
        public static string EncodeToBase64(this string str, Encoding encoder = null) => 
            Convert.ToBase64String((encoder ?? Encoding.UTF8).GetBytes(str));
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoder">编码方式（默认为UTF8）</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeFromBase64(this string str, Encoding encoder = null) =>
            (encoder ?? Encoding.UTF8).GetString(Convert.FromBase64String(str));


        /// <summary>
        /// 将明文字符串转成二进制字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToBitString(this string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str);
            StringBuilder result = new StringBuilder(data.Length * 8);
            foreach (byte b in data)
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            return result.ToString();
        }
        /// <summary>
        /// 将二进制字符串转成明文字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string BitStringToString(this string str)
        {
            CaptureCollection cs =
                Regex.Match(str, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
                data[i] = Convert.ToByte(cs[i].Value, 2);
            return Encoding.Unicode.GetString(data, 0, data.Length);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">明文</param>
        /// <param name="encodingAesKey">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(this string str, string encodingAesKey)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
            var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(encodingAesKey.EncodeToMD5());
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = aes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">密文</param>
        /// <param name="encodingAesKey">密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(this string str, string encodingAesKey)
        {
            byte[] toEncryptArray = Convert.FromBase64String(str);
            var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(encodingAesKey.EncodeToMD5());
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }



        /// <summary> 
        /// DES加密
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string DESEncrypt(this string str, string key = "12345678")
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var des = TripleDES.Create();
                var inputByteArray = Encoding.UTF8.GetBytes(str);
                var bKey = Encoding.ASCII.GetBytes(key.EncodeToMD5().Substring(0, 24));
                des.Key = bKey;
                des.IV = bKey.Take(8).ToArray();
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                    ret.AppendFormat("{0:X2}", b);
                return ret.ToString();
            }
        }
        /// <summary> 
        /// DES解密 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string DESDecrypt(this string str, string key = "12345678")
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var des = TripleDES.Create();
                var len = str.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x;
                for (x = 0; x < len; x++)
                {
                    var i = Convert.ToInt32(str.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                var bKey = Encoding.ASCII.GetBytes(key.EncodeToMD5().Substring(0, 24));
                des.Key = bKey;
                des.IV = bKey.Take(8).ToArray();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
