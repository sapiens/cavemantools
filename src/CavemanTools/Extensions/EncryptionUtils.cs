

namespace System
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    public static class EncryptionUtils
	{
	

        #region Encryption
        /// <summary>
        /// Encrypts using AES (Rijndael) standard
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Encrypt(this string data, byte[] key)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null || key.Length != 32) throw new ArgumentException("Key must have 32 length", "key");
			byte[] result;
			using (var c = new RijndaelManaged())
			{
				c.Key = key;
				c.GenerateIV();
				using (var ms = new MemoryStream())
				{
					ms.Write(c.IV, 0, c.IV.Length);
					using (var cs = new CryptoStream(ms, c.CreateEncryptor(), CryptoStreamMode.Write))
					{
						var cl = Encoding.Unicode.GetBytes(data);
						cs.Write(cl, 0, cl.Length);
						cs.FlushFinalBlock();
					}
					result = ms.ToArray();
				}
				c.Clear();
			}
			return result;
		}

		/// <summary>
		/// Returns encrypted data as Base64 string
		/// </summary>
		/// <param name="data"></param>
		/// <param name="salt">Salt of 16 chars</param>
		/// <returns></returns>
		public static string EncryptAsString(this string data, string salt)
		{
			salt = PadSecret(salt);
			var dt = data.Encrypt(salt.GenerateEncryptionKey());

			return Convert.ToBase64String(dt);
		}

		/// <summary>
		/// Ensures secret length of 16 chars
		/// </summary>
		/// <param name="salt"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <returns></returns>
		private static string PadSecret(string salt)
		{
			if (salt.Length > 16) throw new ArgumentOutOfRangeException("salt", "Secret length is greater than 16 chars");
			if (salt.Length < 16)
			{
				var sb = new StringBuilder(salt);
				for (var i = salt.Length; i < 16; i++) sb.Append(' ');
				salt = sb.ToString();
			}
			return salt;
		}

		/// <summary>
		/// Returns decrypted data from Base64 string
		/// </summary>
		/// <param name="data">Base 64 encoded encryption</param>
		/// <param name="salt">Salt of 16 chars</param>
		/// <exception cref="CryptographicException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <returns></returns>
		public static string DecryptAsString(this string data, string salt)
		{
			return Convert.FromBase64String(data).Decrypt(PadSecret(salt).GenerateEncryptionKey());
		}

		public static string Decrypt(this byte[] data, byte[] key)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null || key.Length != 32) throw new ArgumentException("Key must have 32 length", "key");
			string result;
			using (var c = new RijndaelManaged())
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
					result = Encoding.Unicode.GetString(ms.ToArray());
				}
				c.Clear();
			}
			return result;
		}


		/// <summary>
		/// Generates hex representation of bytes
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string ToHexString(this byte[] data)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (data.Length == 0) return string.Empty;
			var sb = new StringBuilder();
			foreach (byte bit in data)
			{
				sb.Append(bit.ToString("x2"));

			}
			return sb.ToString();
		}

		public static byte[] GenerateEncryptionKey()
		{
			string t = StringUtils.CreateRandomString(16);
			return Encoding.Unicode.GetBytes(t);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="salt">32 bytes size (16 unicode length)</param>
		/// <returns></returns>
		public static byte[] GenerateEncryptionKey(this string salt)
		{
			if (string.IsNullOrEmpty(salt)) throw new ArgumentNullException("salt");
			if (salt.Length > 16) throw new ArgumentOutOfRangeException("salt", "16 length max");
			return Encoding.Unicode.GetBytes(salt);
		}




		#endregion
	}
}