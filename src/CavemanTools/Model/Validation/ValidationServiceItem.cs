using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CavemanTools.Model.Validation
{
	internal class ValidationServiceItem
	{
		
		public PropertyInfo ReflectionInfo { get; set; }

		private List<ValidationAttribute> _validators=new List<ValidationAttribute>();
		public List<ValidationAttribute> Validators
		{
			get { return _validators; }			
		}

		public bool Validate(object value,IValidationDictionary errorBag)
		{
			//if (errorBag == null) throw new ArgumentNullException("errorBag");
			
			foreach(var validator in _validators)
			{
				if (!validator.IsValid(value))
				{
					if (errorBag!=null) errorBag.AddError(ReflectionInfo.Name,validator.FormatErrorMessage(ReflectionInfo.Name));
					return false;
				}
			}
			return true;
		}
	}
}