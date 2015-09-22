using System;

namespace CavemanTools.Model.Validation
{
	/// <summary>
	/// Default implementation to collect validation errors for the same item
	/// </summary>
	public class ValidationItem : IAddError
	{
		private string _key;
		private IValidationDictionary _erbag;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key">Error Key</param>
		/// <param name="erbag">Error Bag</param>
		public ValidationItem(string key,IValidationDictionary erbag)
		{
			if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
			if (erbag==null) throw new ArgumentNullException("erbag");
			_erbag = erbag;
			_key = key;
		}

		public string Key
		{
			get
			{
				return _key;
			}
		}

		public void AddError(string error)
		{
			_erbag.AddError(_key,error);
		}
	
	}
}