using System;
using System.Collections.Specialized;
using System.Text;

namespace CavemanTools.Strings
{
	public static class Serializers
	{
		/// <summary>
		/// Unserialize a base64 encoded string into name values
		/// </summary>
		/// <param name="value">Base64 encoded string</param>
		/// <returns></returns>
		public static NameValueCollection Base64Unserialize(this string value)
		{
			if (value == null) throw new ArgumentNullException("value");
			var nv = new NameValueCollection();

			foreach (var token in value.Split(new[] { ';' }))
			{
				var kv = token.Split(new[] { ':' });
				nv[kv[0]] = Encoding.Unicode.GetString(Convert.FromBase64String(kv[1]));
			}
			return nv;
		}


		/// <summary>
		/// Serialize a name value collecting to base64 encoded string
		/// </summary>
		/// <param name="value">Base64 encoded string</param>
		/// <returns></returns>
		public static string Base64Serialize(this NameValueCollection col)
		{
			var sb = new StringBuilder();
			foreach (string k in col.Keys)
			{
				sb.Append(k);
				sb.Append(":");
				sb.Append(Convert.ToBase64String(Encoding.Unicode.GetBytes(col[k])));
				sb.Append(";");
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}
		
	}
}