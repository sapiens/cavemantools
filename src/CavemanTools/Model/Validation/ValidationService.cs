using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace CavemanTools.Model.Validation
{
	/// <summary>
	/// Provides functionality to validate ValidationAttributes both by declaration and instantiation
	/// </summary>
	/// <typeparam name="T">Type containing declarated validation attributes</typeparam>
	public class ValidationService<T> where T:class
	{
		
		List<ValidationServiceItem> _validators=new List<ValidationServiceItem>();
		
		List<ValidationAttribute> _classValidators;

		public ValidationService()
		{
			_classValidators = new List<ValidationAttribute>(AttributeUtils.GetCustomAttributes<ValidationAttribute>(typeof(T)));

			foreach (var pi in typeof(T).GetProperties())
			{
				var attr = pi.GetCustomAttributes<ValidationAttribute>(true);
				if (attr.Length == 0) continue;
				foreach(var at in attr) AddToCollection(pi,at);
			}
		}

		#region Get Validators
		/// <summary>
		/// Gets the Validation Attributes decorating the class
		/// </summary>
		public IList<ValidationAttribute> GetClassValidators
		{
			get { return _classValidators; }
		}

		/// <summary>
		/// Gets all the class validation attributes matching type
		/// </summary>
		/// <typeparam name="TA">Implements ValidationAttribute</typeparam>
		/// <returns></returns>
		public IEnumerable<TA> GetClassValidator<TA>() where TA:ValidationAttribute
		{
			return _classValidators.Where(d => d.IsExactlyType<TA>()).Cast<TA>();
		}

		
		/// <summary>
		/// Gets all the validation attributes matching type for the property
		/// </summary>
		/// <typeparam name="TA">ValidationAttribute inheritor</typeparam>
		/// <param name="property">Expression Property</param>
		/// <returns></returns>
		 public IEnumerable<TA> GetValidators<TA>(Expression<Func<T,object>> property) where TA:ValidationAttribute
		 {
		 	if (property == null) throw new ArgumentNullException("property");
			var mi = property.GetPropertyInfo();
			var item = _validators.Find(d => d.ReflectionInfo.Name.Equals(mi.Name, StringComparison.InvariantCultureIgnoreCase));
			if (item == null) return new TA[0];
		 	return item.Validators.Where(d=> d.IsExactlyType<TA>()).Cast<TA>();
		 }

		/// <summary>
		/// Gets all defiend validation attributes for property
		/// </summary>
		/// <param name="property">Expression property</param>
		/// <returns></returns>
		public IEnumerable<ValidationAttribute> GetValidators(Expression<Func<T,object>> property)
		{
			if (property == null) throw new ArgumentNullException("property");
			var mi = property.GetPropertyInfo();
			var item = _validators.Find(d => d.ReflectionInfo.Name.Equals(mi.Name, StringComparison.InvariantCultureIgnoreCase));
			if (item == null) return new ValidationAttribute[0];
			return item.Validators;
		}

		#endregion

		#region Add validators
		/// <summary>
		/// Registers validator for a property
		/// </summary>
		/// <param name="property">Property expression</param>
		/// <param name="validator">Validator</param>
		public void AddValidation(Expression<Func<T,object>> property,ValidationAttribute validator)
		{
			if (validator == null) throw new ArgumentNullException("validator");
			var mi = property.GetPropertyInfo();
			AddToCollection(mi,validator);
		}

		 void AddToCollection(MemberInfo info,ValidationAttribute attr)
		 {
		 	var pi = info as PropertyInfo;
			 if (pi==null) throw new InvalidOperationException("You can define validators only for a property");
		 	var item = _validators.Find(d => d.ReflectionInfo.Name.Equals(pi.Name, StringComparison.InvariantCultureIgnoreCase));
			 
			 if (item==null) //nothing is set for the property
			 {
			 	item = new ValidationServiceItem() {ReflectionInfo = pi};
				
				 _validators.Add(item);
			 }

			 item.Validators.Add(attr);
		 }
		#endregion

		#region Validation

		 /// <summary>
		 /// Validates object of type, calling the validators for each property
		 /// It calls both validators defined in this service as well as the declared attributes if any
		 /// </summary>
		 /// <param name="object">Object to validate</param>
		 /// <returns></returns>
		 public bool ValidateObject(T @object)
		 {
		 	return ValidateObject(@object, null);
		 }

		/// <summary>
		/// Validates object of type, calling the validators for each property
		/// It calls both validators defined in this service as well as the declared attributes if any
		/// </summary>
		/// <typeparam name="T">type of object</typeparam>
		/// <param name="object">Object to validate</param>
		/// <param name="errorBag">Validation error bag, can be null if you don't need errors</param>
		/// <returns></returns>
		public bool ValidateObject(T @object, IValidationDictionary errorBag)
		{
			var valid = true;
			foreach(var item in _validators)
			{
				var value = @object.GetPropertyValue(item.ReflectionInfo.Name);
				if (!item.Validate(value, errorBag)) valid = false;
			}
			return valid;
		}

		/// <summary>
		/// Validates the value according to property validators
		/// </summary>
		/// <param name="property">Property expression</param>
		/// <param name="value">value to validate</param>
		/// <param name="errorBag">Error bag, can be null</param>
		/// <returns></returns>
		public bool ValidateValueFor(Expression<Func<T,object>> property,object value,IValidationDictionary errorBag)
		{
			if (property == null) throw new ArgumentNullException("property");
			var mi = property.GetPropertyInfo();
			var item=_validators.Find(d => d.ReflectionInfo.Name.Equals(mi.Name, StringComparison.InvariantCultureIgnoreCase));
			if (item == null) return true;
			return item.Validate(value, errorBag);
		}

		/// <summary>
		/// Validates value with the attributes decorating the class T
		/// </summary>
		/// <param name="value">Value</param>
		/// <param name="errorBag">Error bag</param>
		/// <returns></returns>
		public bool ValidateValueForClass(object value,IAddError errorBag)
		{
			foreach(var validator in _classValidators)
			{
				if (!validator.IsValid(value))
				{
					 if (errorBag!=null) errorBag.AddError(validator.FormatErrorMessage(errorBag.Key));
					return false;
				}
			}
			return true;
		}
		 #endregion
	}
}