using System;
using System.Collections.Generic;
using System.Linq;

namespace CavemanTools
{
	public static class EnumUtils
	{
		/// <summary>
		/// Gets enum as enumerable values.
		/// </summary>
		/// <typeparam name="T">EnumType</typeparam>
		/// <returns></returns>
		public static IEnumerable<T> AsValues<T>()
		{
			return (T[])Enum.GetValues(typeof (T));
		}

		/// <summary>
		/// Gets enum as enumerable values, skipping the first (usually default,none) value
		/// </summary>
		/// <typeparam name="T">EnumType</typeparam>
		/// <returns></returns>
		public static IEnumerable<T> AsValuesWithoutFirst<T>()
		{
			
			var t = (T[]) Enum.GetValues(typeof (T)); 
			return t.Skip(1);
		}

		
		/// <summary>
		/// Gets enum as enumerable values, skipping the first (usually default,none) value
		/// </summary>
		/// <typeparam name="T">EnumType</typeparam>
		/// <returns></returns>
		public static IEnumerable<string> AsNamesWithoutFirst<T>()
		{
			return Enum.GetNames(typeof (T)).Skip(1);			
		}

		
		/// <summary>
		/// Returns a random enum constant
		/// </summary>
		/// <typeparam name="T">EnumType</typeparam>
		/// <returns></returns>
		public static T GetRandom<T>()
		{
			var values = Enum.GetValues(typeof (T));
			var cnt = values.Length;
			var item=new Random().Next(0, cnt);
			return (T)values.GetValue(item);
		}

		/// <summary>
		/// Returns a random enum name
		/// </summary>
		/// <typeparam name="T">EnumType</typeparam>
		/// <returns></returns>
		public static string GetRandomName<T>()
		{
			var r = new Random();
			return Enum.GetNames(typeof (T)).OrderBy(d => r.Next()).First();			
		}
	}
}
