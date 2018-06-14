namespace System
{
    using System.Security.Cryptography;
    using System.Text;
    public static class Hashing
    {
        #region Hashes

        public static string Hmac256(this string data, string key)
        {
            key.MustNotBeEmpty();

            var bytes = Encoding.UTF8.GetBytes(key);
            using (var hasher = new HMACSHA256(bytes))
            {
                var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
                return hash.ToHexString();
            }
        }


        /// <summary>
        /// Hash a string using the SHA256 algorithm. 32 bytes (hex): 64 unicode chars, 128 bytes
        /// </summary>
        public static string Sha256(this string data) => HashAsString(data, SHA256.Create);
       

       

        /// <summary>
        /// Hash a string using the SHA512 algorithm. 128 unicode chars, 256 bytes
        /// </summary>
        public static string Sha512(this string data) => HashAsString(data, SHA512.Create);
       

        /// <summary>
        /// Creates MD5 sum from string, 16 bytes(hex): 32 unicode chars
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5(this string data) => HashAsString(data, Security.Cryptography.MD5.Create);
     
       
        static string HashAsString(string data, Func<HashAlgorithm> factory)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            using (var hasher = factory())
            {
                var by2 = hasher.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in by2)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static byte[] Hash(this string data, Func<HashAlgorithm> factory)
            => Encoding.Unicode.GetBytes(data).Hash(factory);

        public static byte[] Hash(this byte[] bytes, Func<HashAlgorithm> factory)
        {
            using (var hasher = factory())
            {
                 return hasher.ComputeHash(bytes);
            }
          
        }

        #endregion 
    }
}