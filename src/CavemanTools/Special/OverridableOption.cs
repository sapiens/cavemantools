using System;
using System.Collections.Generic;

namespace CavemanTools.Special
{
	/// <summary>
	/// Agregates values from different sources for an option.
	/// THe priority strategy selects the final value
	/// </summary>
	/// <typeparam name="TSource">Source Type</typeparam>
	/// <typeparam name="TValue">Value Type</typeparam>
	public class OverridableOption<TSource,TValue> where TValue : class
	{
		
		protected Dictionary<TSource, TValue> Values = new Dictionary<TSource, TValue>();
		/// <summary>
		/// Init with default source and default value
		/// </summary>
		/// <param name="defaultSource">key for default value</param>
		/// <param name="defaultValue">default value</param>
		protected OverridableOption(TSource defaultSource,TValue defaultValue)
		{
			if (defaultValue == null) throw new ArgumentNullException("defaultValue");
			Values[defaultSource] = defaultValue;			
		}
		
		/// <summary>
		/// Init with default source and default value specifying priority strategy
		/// </summary>
		/// <param name="defaultSource">key for default value</param>
		/// <param name="defaultValue">default value</param>
		/// <param name="strategy">Priority sorting strategy</param>
		public OverridableOption(TSource defaultSource,TValue defaultValue,Func<IDictionary<TSource,TValue>,TValue> strategy)
		{
			if (strategy == null) throw new ArgumentNullException("strategy");
			if (defaultValue == null) throw new ArgumentNullException("defaultValue");
			Values[defaultSource] = defaultValue;
			Strategy = strategy;
		}

	
		/// <summary>
		/// Adds value from a source
		/// </summary>
		/// <param name="src">Source id</param>
		/// <param name="value">Option Value</param>
		public void FromSource(TSource src, TValue value)
		{
			if (!IsValid(value)) return;
			Values[src] = value;
			_value = null;
		}
		
		/// <summary>
		/// Checks if a value is valid.
		/// Default checks if it's null
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		protected virtual bool IsValid(TValue value)
		{
			return value != null;
		}

		///<summary>
		/// Priority sorting strategy
		///</summary>
		protected Func<IDictionary<TSource, TValue>, TValue> Strategy;

		private TValue _value;
		
		/// <summary>
		/// Gets option's final value
		/// </summary>
		public TValue Value
		{
			get
			{
				if (_value == null) _value = Strategy(Values);
				return _value;
			}
		}

	}
}