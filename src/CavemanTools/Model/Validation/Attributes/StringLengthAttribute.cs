using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CavemanTools.Model.Validation.Attributes
{
	/// <summary>
	/// Validates if the value's length is between minimum and maximum length
	/// </summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field | AttributeTargets.Class,Inherited = true)]
	public class StringLengthAttribute:ValidationAttribute
	{
		//private string _defaultError = "Invalid length for the field '{0}'";

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Maximum Length is negative</exception>
		/// <param name="max">Maximum length</param>
		public StringLengthAttribute(int max)
		{
			if (max < 0) throw new ArgumentOutOfRangeException("max");
			MaxLength = max;
		}
		/// <summary>
		/// Maximum length for string
		/// </summary>
		public int MaxLength { get; private set; }

		/// <summary>
		/// Minimum lenght for string
		/// </summary>
		public int MinLength { get; set; }

		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrEmpty(t)) return true;
			t = t.Trim();
			return t.Length >= MinLength && t.Length<=MaxLength;
		}

		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, MaxLength);
		}
	}
}