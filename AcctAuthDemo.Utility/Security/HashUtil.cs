using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AcctAuthDemo.Utility.Security
{
    /// <summary>
    /// 哈希
    /// </summary>
    public static class HashUtil
    {
        /// <summary>
        /// 创建HMAC SHA256哈希字符串（Base64）
        /// </summary>
        /// <param name="message">原始信息</param>
        /// <param name="secret">干扰项，盐</param>
        /// <returns>哈希字符串（Base64）</returns>
        public static string CreateHmacSha256Hash(byte[] message, string secret)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("secret");
            }
            var encoding = new System.Text.ASCIIEncoding();
            var keyBytes = encoding.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(message);
                return Convert.ToBase64String(hashmessage);
            }
        }

        /// <summary>
        /// 创建HMAC SHA256哈希字符串（Base64）
        /// </summary>
        /// <param name="message">原始信息</param>
        /// <param name="secret">干扰项，盐</param>
        /// <returns>哈希字符串（Base64）</returns>
        public static string CreateHmacSha256Hash(string message, string secret)
        {
            var encoding = new System.Text.ASCIIEncoding();
            var messageBytes = encoding.GetBytes(message);

            return CreateHmacSha256Hash(messageBytes, secret);
        }

        /// <summary>
        /// 创建SHA265哈希字符串（Base64）
        /// </summary>
        /// <param name="message">原始信息</param>
        /// <param name="salt">干扰项，盐</param>
        /// <returns></returns>
        public static string CreateSha256Hash(string message, string salt = "")
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(message + salt);
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] retVal = sha256.ComputeHash(bytValue);
            return Convert.ToBase64String(retVal);
        }

        /// <summary>
        /// 创建SHA-512哈希字符串（base64）
        /// </summary>
        /// <param name="message">字符串</param>
        /// <param name="toUpper">返回哈希值格式 true：英文大写，false：英文小写</param>
        /// <returns></returns>
        public static string CreateSha512Hash(string message, bool toUpper = true)
        {
            var sha512csp = new SHA512CryptoServiceProvider();
            var bytValue = Encoding.UTF8.GetBytes(message);
            var bytHash = sha512csp.ComputeHash(bytValue);
            sha512csp.Clear();
            return Convert.ToBase64String(bytHash);
        }

        /// <summary>
        /// 创建SHA1哈希字符串（Base64）
        /// </summary>
        /// <param name="message">原始信息</param>
        /// <returns></returns>
        public static string CreateSha1Hash(string message)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(message);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            return Convert.ToBase64String(bytes_sha1_out);
        }

        /// <summary>
        /// 获取MD5哈希
        /// </summary>
        /// <param name="message">字符串</param>
        /// <returns></returns>
        public static string CreateMD5(this string message)
        {
            var md5 = CreateMD5(Encoding.UTF8.GetBytes(message));
            return md5;
        }

        /// <summary>
        /// 获取MD5哈希
        /// </summary>
        /// <param name="byteGroup">二进制数据</param>
        /// <returns></returns>
        public static string CreateMD5(byte[] byteGroup)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var resultBytes = md5.ComputeHash(byteGroup);

                var sb = new StringBuilder();

                for (int i = 0; i < resultBytes.Length; i++)
                {
                    sb.Append(resultBytes[i].ToString("x").PadLeft(2, '0'));
                }

                return sb.ToString();
            }
        }
    }
}
