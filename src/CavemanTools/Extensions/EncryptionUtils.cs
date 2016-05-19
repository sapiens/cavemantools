

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class EncryptionUtils
	{
        /// <summary>
        /// Encrypts using AES (Rijndael) standard using a 256 bit key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Encrypt(this byte[] data, byte[] key)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null || key.Length != 32) throw new ArgumentException("Key must have 32 length", "key");
			byte[] result;
            
            using (var c = Aes.Create())
			{
				c.Key = key;
				c.GenerateIV();
				using (var ms = new MemoryStream())
				{
					ms.Write(c.IV, 0, c.IV.Length);
					using (var cs = new CryptoStream(ms, c.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(data, 0, data.Length);
						cs.FlushFinalBlock();
					}
					result = ms.ToArray();
				}				
			}
			return result;
		}

        /// <summary>
        /// Aes encryption
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string data, string key)
            => Encrypt(data.ToByteArray(), key.ToByteArray().Hash(SHA256.Create)).ToBase64();

		///// <summary>
		///// Encrypts string using AES and returns encrypted data as Base64 string
		///// </summary>
		///// <param name="data"></param>
		///// <param name="key">Key</param>
		///// <returns></returns>
		//public static string EncryptAsString(this string data, string key)
		//{
		//	key = PadSecret(key);
		//	var dt = data.Encrypt(key.GenerateEncryptionKey());

		//	return Convert.ToBase64String(dt);
		//}

        
		/// <summary>
		/// Returns decrypted data from Base64 string
		/// </summary>
		/// <param name="data">Base 64 encoded encryption</param>
		/// <param name="key">Salt of 16 chars</param>
		/// <exception cref="CryptographicException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <returns></returns>
		public static string DecryptAsString(this string data, string key)
		{
		    var bytes = Convert.FromBase64String(data).Decrypt(key.ToByteArray().Hash(SHA256.Create));
		    return Encoding.Unicode.GetString(bytes,0,bytes.Length);
		}


        /// <summary>
        /// Decrypts bytes encrypted with AES 256
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">256bit key</param>
        /// <returns></returns>
		public static byte[] Decrypt(this byte[] data, byte[] key)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null || key.Length != 32) throw new ArgumentException("Key must have 32 length", "key");
            
		
			using (var c = Aes.Create())
			{
				c.Key = key;
				using (var ms = new MemoryStream())
				{
					int ReadPos = 0;
					byte[] IV = new byte[c.IV.Length];
					Array.Copy(data, IV, IV.Length);
					c.IV = IV;
					ReadPos += c.IV.Length;
					using (var cs = new CryptoStream(ms, c.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(data, ReadPos, data.Length - ReadPos);
						cs.FlushFinalBlock();
					}
					return ms.ToArray();
				}
				
			}
			
		}

        
	}
}