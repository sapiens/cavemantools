#if !COREFX
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CavemanTools.Model.Validation
{
	/// <summary>
	/// Collects error messages from validation
	/// </summary>
	public class DefaultValidationWrapper:IValidationDictionary, IDataErrorInfo
	{
		private IDictionary<string, string> err = new Dictionary<string, string>();

		/// <summary>
		/// Adds validation error message
		/// </summary>
		/// <param name="key">Key</param>
		/// <param name="errorMessage">Text</param>
		public void AddError(string key, string errorMessage)
		{
		    if (key == null) throw new ArgumentNullException("key");
            err.Add(key,errorMessage);
		}

		public bool HasErrors => !IsValid;

	    /// <summary>
		/// Returns true if there are no error messages
		/// </summary>
		public bool IsValid => err.Count == 0;

	    /// <summary>
        /// copies errors to another dictionary
        /// </summary>
        /// <param name="other"></param>
	    public void CopyTo(IValidationDictionary other)
	    {
	        if (other == null) throw new ArgumentNullException("other");
            foreach(var kv in err) other.AddError(kv.Key,kv.Value);
	    }


	    public string this[string columnName]
		{
			get { 
				if (err.ContainsKey(columnName)) return err[columnName];
				return string.Empty;
			}
		}

		public string Error
		{
			get { return string.Empty; }
		}

		public void Clear()
		{
			err.Clear();
		}
		
		public string ShowErrors()
		{
			var sb = new StringBuilder();
			foreach(var kv in err)
			{
				sb.AppendFormat("{0}: {1}\n", kv.Key, kv.Value);
			}
			return sb.ToString();
		}
	}
}
#endif