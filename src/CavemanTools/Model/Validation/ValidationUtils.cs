using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace CavemanTools.Model.Validation
{
	/// <summary>
	/// Used to validate objects
	/// </summary>
	public static class ValidationUtils
	{
		
		 /// <summary>
		/// Validate object properties using Data Annotations, registering the error messages in process
		/// </summary>
		/// <typeparam name="T">Type decorated with Validation Attributes</typeparam>
		/// <param name="data">Object</param>		
		public static bool Validate<T>(this T data)
		 {
		 	return Validate(data, null);
		 }

		/// <summary>
		/// Validate object properties using Data Annotations, registering the error messages in process
		/// </summary>
		/// <typeparam name="T">Type decorated with Validation Attributes</typeparam>
		/// <param name="data">Object</param>
		/// <param name="errorBag">Error dictionary,can be null</param>
		public static bool Validate<T>(this T data, IValidationDictionary errorBag)
		  {
			//if (errorBag == null) throw new ArgumentNullException("errorBag");
			var flag = true;
			foreach (var pi in typeof(T).GetProperties())
			  {
				  var attr = pi.GetCustomAttributes<ValidationAttribute>(true);
				  if (attr.Length == 0) continue;
				  foreach (var a in attr)
				  {
					  if (!a.IsValid(pi.GetValue(data, null)))
					  {
						 if (errorBag!=null) errorBag.AddError(pi.Name, a.FormatErrorMessage(pi.Name));
					  	flag = false;
						  break;
					  }

				  }
			  }
			return flag;
		  }

	

		/// <summary>
		/// Validates a value according to validation attributes of a class, ignoring the errors
		/// </summary>
		/// <typeparam name="T">Type decorated with validation attributes</typeparam>
		/// <param name="value">Value to be validated</param>
		public static bool ValidateAs<T>(this object value)
		{
			return ValidateAs<T>(value, null);
		}
		
		/// <summary>
		/// Validates a value according to validation attributes of a class, registering error messages.
		/// </summary>
		/// <typeparam name="T">Type decorated with validation attributes</typeparam>
		/// <param name="value">Value to be validated</param>
		/// <param name="state">Error dictionary, can be null</param>
		public static bool ValidateAs<T>(this object value, IAddError state)
		{
			
			var attr = typeof(T).GetCustomAttributes<ValidationAttribute>(true);
			if (attr.Length == 0) return true;
			foreach (var a in attr)
			{
				if (a.IsValid(value)) continue;
				if (state!=null)state.AddError(a.FormatErrorMessage(state.Key));
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validates a value according to validation attributes of a property, ignoring the errors.
		/// </summary>
		/// <typeparam name="T">Type which contains property</typeparam>
		/// <param name="value">Value to be validated</param>
		/// <param name="property">Property decorated with validation attributes</param>
		public static bool ValidateFor<T>(this object value, Expression<Func<T, object>> property)
		{
			return ValidateFor(value, property, null);
		}

		/// <summary>
		/// Validates a value according to validation attributes of a property, registering the error messages.
		/// </summary>
		/// <typeparam name="T">Type which contains property</typeparam>
		/// <param name="value">Value to be validated</param>
		/// <param name="property">Property decorated with validation attributes</param>
		/// <param name="state">Error dictionary, can be null</param>
		public static bool ValidateFor<T>(this object value, Expression<Func<T, object>> property, IValidationDictionary state)
		{
			//if (value == null) throw new ArgumentNullException("value");
			//if (state == null) throw new ArgumentNullException("state");
			if (property == null) throw new ArgumentNullException("property");

			var prop = property.Body as MemberExpression;
			if (prop==null && property.Body is UnaryExpression)
			{
				prop = (property.Body as UnaryExpression).Operand as MemberExpression;
			}
			if (prop == null) throw new FormatException("Lambda should be a property");
			var attr = prop.Member.GetCustomAttributes<ValidationAttribute>(true);
			if (attr.Length == 0) return true;
			foreach (var a in attr)
			{
				if (!a.IsValid(value))
				{
					if (state!=null) state.AddError(prop.Member.Name, a.FormatErrorMessage(prop.Member.Name));
					return false;
				}

			}
			return true;
		}
	}
}