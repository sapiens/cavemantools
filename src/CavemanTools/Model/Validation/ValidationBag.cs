using System.Collections.Generic;
using System.Linq;

namespace CavemanTools.Model.Validation
{
	/// <summary>
	/// Used to centralize multiple validations
	/// </summary>
	public class ValidationBag
	{
		/// <summary>
		/// DEfault validation state is invalid
		/// </summary>
		public ValidationBag():this(false)
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="init">State of bag: valid or invalid</param>
		public ValidationBag(bool init)
		{
			_init = init;
		}

		List<bool> _values= new List<bool>(5);
		private bool _init;

		/// <summary>
		/// Adds validation result
		/// </summary>
		/// <param name="value"></param>
		public void Add(bool value)
		{
			_values.Add(value);
		}

		/// <summary>
		/// Gets if all the validations were successful
		/// </summary>
		public bool IsValid
		{ 
			get
			{
				if (_values.Count == 0) return _init;
				return _values.TrueForAll(d => d);
			}
		}

        public bool AtLeastOneIs(bool value)
        {
            return _values.Any(d => d == value);
        }

	}
}