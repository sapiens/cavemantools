using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CavemanTools.Model.Validation.Attributes
{
	///<summary>
	/// Validates that a string doesn't contain unwanted words.
	///</summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field | AttributeTargets.Class,Inherited = true)]
	public class ForbidWordsAttribute:ValidationAttribute
	{
		private string _defaultError = "Field '{0}' contains forbidden words";

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <param name="words">Separate by comma</param>
		[Obsolete("Use the other constructor",true)]
		public ForbidWordsAttribute(string words)
		{
			if (string.IsNullOrEmpty(words)) throw new ArgumentNullException("words");
			Forbidden = words.Split(',').Select(d=>d.Trim());
		}

	    public ForbidWordsAttribute(params string[]words)
	    {
	        words.MustNotBeEmpty();
            Forbidden = words;
	    }

		/// <summary>
		///  Gets the forbidden words list
		/// </summary>
		public IEnumerable<string> Forbidden { get;private set; }


		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrEmpty(t)) return true;
			t = t.Trim().ToLowerInvariant();
			return !Forbidden.Contains(t);
		}

		public override string FormatErrorMessage(string name)
		{
			if (string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(ErrorMessageResourceName))
			{
				return string.Format(_defaultError, name);
			}
			return base.FormatErrorMessage(name);
		}
	}
}