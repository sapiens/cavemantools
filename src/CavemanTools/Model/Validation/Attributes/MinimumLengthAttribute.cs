using System;
using System.ComponentModel.DataAnnotations;

namespace CavemanTools.Model.Validation.Attributes
{
	/// <summary>
	/// Validates that a string has mnimum length
	/// </summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field|AttributeTargets.Class,Inherited = true)]
	public class MinimumLengthAttribute:ValidationAttribute
	{
		private string _defaultError = "Invalid minimum length for the field '{0}'";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="length">Minimum length</param>
		public MinimumLengthAttribute(int length)
		{
			Length = length;
		}
		/// <summary>
		/// Gets minimum length
		/// </summary>
		public int Length { get; private set; }

		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrEmpty(t)) return true;
			return t.Length >= Length;
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