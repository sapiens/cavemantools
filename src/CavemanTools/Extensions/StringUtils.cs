using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
#if !COREFX
using System.IO.Compression;
#endif
using System.Linq;
#if COREFX
using System.Reflection;
#endif
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CavemanTools.Hashing;

namespace System
{
	public static class StringUtils
	{
	    public static byte[] MurmurHash(this string data)
	    {
	        var hasher = new Murmur3();
	        return hasher.ComputeHash(Encoding.Unicode.GetBytes(data));            
	    }

	    /// <summary>
	    /// Get bytes from Unicode string
	    /// </summary>
	    /// <param name="data"></param>
	    /// <returns></returns>
	    public static byte[] ToByteArray(this string data) => Encoding.Unicode.GetBytes(data);
#if !COREFX

        /// <summary>
        /// Compress using GZip
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Zip(this string str)
        {
         if (str.IsNullOrEmpty()) return new byte[0];
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {

                using (var gs = new GZipStream(mso, CompressionLevel.Optimal))

                {
                   msi.CopyTo(gs);                   
                }

                return mso.ToArray();
            }
        }

        /// <summary>
        /// Unzip a compressed string using GZip
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Unzip(this byte[] bytes)
        {
            if (bytes.IsNullOrEmpty()) return "";
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);                    
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
#endif
        public static string AddSlashes(this string data,bool singleQuotes=true,bool doubleQuotes=true)
	    {
	        if (singleQuotes)
	        {
	            data = data.Replace("'", "\\'");
	        }
	        if (doubleQuotes)
	        {
	            data = data.Replace("\"", @"\""");
	        }
            return data;
	    }
        
        /// <summary>
	    /// Creates url friendly slug of a string
	    /// </summary>
	    /// <param name="text"></param>
	    /// <returns></returns>
	    public static string MakeSlug(this string text)
	    {
	        if (String.IsNullOrEmpty(text)) return String.Empty;

	        // to lowercase, trim extra spaces
	        text = text.ToLower().Trim();

	        var len = text.Length;
	        var sb = new StringBuilder(len);
	        bool prevdash = false;
	        char c;

	        for (int i = 0; i < text.Length; i++)
	        {
	            c = text[i];
	            if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-')
	            {
	                if (!prevdash)
	                {
	                    sb.Append('-');
	                    prevdash = true;
	                }
	            }
	            else if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
	            {
	                sb.Append(c);
	                prevdash = false;
	            }
	            if (i == 80) break;
	        }

	        text = sb.ToString();
	        // remove trailing dash, if there is one
	        if (text.EndsWith("-"))
	            text = text.Substring(0, text.Length - 1);
	        return text;
	    }

        /// <summary>
        /// Gets part of the string before the specified part
        /// </summary>
        /// <param name="src"></param>
        /// <param name="startPos">Position to start looking for the part</param>
        /// <param name="end"></param>
        /// <returns></returns>
	    public static string SubstringUntil(this string src, string end, int startPos=0)
        {
            var idx = src.IndexOf(end,startPos);
            if (idx == -1) return src;
            return src.Substring(startPos, idx);
        }

	    /// <summary>
		/// Parses string to culture. Returns null if unsuccessful.
		/// </summary>
		/// <param name="lang"></param>
		/// <returns></returns>
		public static CultureInfo ToCulture(this string lang)
		{
			CultureInfo c = null;
			try
			{
				c = new CultureInfo(lang); 
			}
			catch(ArgumentNullException)
			{
				
			}			
			catch(ArgumentException)
			{
				
			}
			
			return c;
		}
		


		/// <summary>
		/// Cuts the string to the specified length
		/// </summary>
		/// <param name="value">string</param>
		/// <param name="length">maximum length</param>
		/// <returns></returns>
		public static string Cut(this string value,int length)
		{
			if (string.IsNullOrEmpty(value)) return "";	
			var l = value.Length > length ? length : value.Length;
			return value.Substring(0, l);
		}

        public static string RemoveLastChar(this string value) => value.RemoveLastChars(1);

	    public static string RemoveLastChars(this string value,int length)
        {
            value.MustNotBeNull();
            if (value.Length == 0) return value;
            length.Must(l=> value.Length>length,"Can't remove more chars than the string's length");
           return value.Remove(value.Length - length, length);
        }

	    public static string StringJoin<T>(this IEnumerable<T> items, string separator = ",") => string.Join(separator, items.Select(t => t.ToString()));

	    public static bool IsNotEmpty(this string value) => !string.IsNullOrWhiteSpace(value);


	    /// <summary>
        /// Returns true if the string is empty 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="checkBlancs">trim blancs</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string data,bool checkBlancs=false)
        {
            if (data == null) return true;
            if (checkBlancs)
            {
                data = data.Trim();
            }
            return String.IsNullOrEmpty(data);
        }

		/// <summary>
		/// Converts strings form unicode to specified encoding
		/// </summary>
		/// <param name="s">String</param>
		/// <param name="encoding">Encoding</param>
		/// <returns></returns>
		public static string ConvertToEncoding(this string s, Encoding encoding)
		{
			var bytes = Encoding.Unicode.GetBytes(s);
			bytes = Encoding.Convert(Encoding.Unicode, encoding, bytes);
			return Encoding.ASCII.GetString(bytes);
		}
		
		/// <summary>
		/// Capitalizes the first letter from string.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Capitalize(this string s)
		{
			if (String.IsNullOrEmpty(s)) return String.Empty;
			return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1, s.Length - 1).ToLowerInvariant();
		}

		

		/// <summary>
		/// Reads the Stream as an UTF8 String
		/// </summary>
		/// <param name="data">Stream</param>
		/// <returns></returns>
		public static string ReadAsString(this Stream data)
		{
			using (var r = new StreamReader(data, Encoding.UTF8))
				return r.ReadToEnd();
		}

		/// <summary>
		/// Returns true if teh string is a valid email format.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsEmail(this string text)
		{
			return Regex.IsMatch(text, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" 
    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$");
		}

        /// <summary>
        /// Returns the first line from a multilined string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetFirstLine(this string source)
        {
            if (string.IsNullOrEmpty(source)) return "";
            return source.Split('\n').FirstOrDefault();
        }
		
	/// <summary>
	/// Generates a random string of the specified length starting with prefix
	/// </summary>
	/// <param name="prefix"></param>
	/// <param name="length"></param>
	/// <returns></returns>
        public static string GenerateRandomString(this string prefix, int length)
	{
	    if (prefix == null) prefix = "";
	    if (length <= prefix.Length) return prefix;
        return prefix+CreateRandomString(length-prefix.Length);
        }

		/// <summary>
		/// Generates a random string (letters and numbers) of the specified length
		/// </summary>
		/// <param name="length">Maximum string length</param>
		/// <returns></returns>
		public static string CreateRandomString(int length)
		{
            var buff = new byte[length];
            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(buff);
            }
            
		    return buff.ToHexString().Substring(0,length);            
		}


	    public static string ToBase64(this byte[] data) => Convert.ToBase64String(data);

	    public static string ToFormat(this string pattern, params object[] args)
        {
            pattern.MustNotBeEmpty();
            return String.Format(pattern, args);
        }

#if !COREFX
        public static void ToConsole(this object text)
	    {
	        Console.WriteLine(text.ToString());
	    }
        public static void ToConsole(this string text)
	    {
	        Console.WriteLine(text);
	    }
#endif

        public static T ToEnum<T>(this string value)
        {
#if COREFX
            if (!(typeof(T).GetTypeInfo().IsEnum)) throw new ArgumentException("Type '{0}' is not an enum".ToFormat(typeof(T)));
#else
            if (!typeof(T).IsEnum) throw new ArgumentException("Type '{0}' is not an enum".ToFormat(typeof(T)));
#endif
            return (T)Enum.Parse(typeof(T), value, true);
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
	}
}

namespace System.Text
{
    public static class TextUtils
    {
        /// <summary>
        /// Removes last character from builder
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static StringBuilder RemoveLast(this StringBuilder sb)
        {
            sb.MustNotBeNull();
            return sb.Remove(sb.Length - 1, 1);
        }

        /// <summary>
        /// Remove last char if matches the provided value
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value">value to check</param>
        /// <returns></returns>
        public static StringBuilder RemoveLastIfEquals(this StringBuilder sb,char value)
        {
            var last = sb[sb.Length - 1];
            if (value==last)
            {
                sb.RemoveLast();
            }
            return sb;
        }

        /// <summary>
        /// Remove last char if matches the provided value
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value">value to check</param>
        /// <returns></returns>
        public static StringBuilder RemoveLastIfEquals(this StringBuilder sb, string value)
        {
            //if (value.IsNullOrEmpty()) return sb;
            //if (sb.ToString().EndsWith(value))
            //{
            //    sb.Remove(sb.Length - value.Length, value.Length);
            //}
            
            bool eq = true;
            int pos = sb.Length - value.Length;
            if (pos < 0) return sb;
            foreach(var ch in value)
            {
                if (sb[pos]!=ch)
                {
                    eq = false;
                    break;
                }
                pos++;
            }
            if (!eq) return sb;

            sb.Remove(sb.Length - value.Length, value.Length);            
            return sb;
        }           
            
    }

}