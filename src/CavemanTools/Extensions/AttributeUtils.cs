
namespace System.Reflection
{
    public static class AttributeUtils
	{
#if !COREFX

        /// <summary>
        /// Returns all custom attributes of specified type
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="provider">Custom attributes provider</param>
        /// <returns></returns>
        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
            return GetCustomAttributes<T>(provider, true);
		}

		/// <summary>
		/// Returns all custom attributes of specified type
		/// </summary>
		/// <typeparam name="T">Attribute</typeparam>
		/// <param name="provider">Custom attributes provider</param>
		/// <param name="inherit">When true, look up the hierarchy chain for custom attribute </param>
		/// <returns></returns>
		public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider provider, bool inherit) where T : Attribute
		{
			if (provider == null)
				throw new ArgumentNullException("provider");
			T[] attributes = provider.GetCustomAttributes(typeof(T), inherit) as T[];
			if (attributes == null)
			{
				return new T[0];
			}
			return attributes;
		}

		/// <summary>
		/// Gets a single or the first custom attribute of specified type
		/// </summary>
		/// <typeparam name="T">Attribute</typeparam>
		/// <param name="memberInfo">Custom Attribute provider</param>
		/// <param name="inherit">True to lookup the hierarchy chain for the attribute</param>
		/// <returns></returns>
		public static T GetSingleAttribute<T>(this ICustomAttributeProvider memberInfo,bool inherit=true) where T : Attribute
		{
            if (memberInfo == null) throw new ArgumentNullException("memberInfo");
			var list = memberInfo.GetCustomAttributes(typeof(T), inherit);
			if (list.Length > 0) return (T)list[0];
			return null;
		}	
	
		public static bool HasCustomAttribute<T>(this ICustomAttributeProvider mi,bool inherit=true) where T:Attribute
		{
			return mi.GetSingleAttribute<T>(inherit) != null;
		}

	    //public static IEnumerable<T> OrderByAttribute<T>(this IEnumerable<T> src) where T:class
	    //{
	    //    return src.OrderBy(d => d.GetType().GetAttributeValue<OrderAttribute, int>(a => a.Value));
	    //}
        public static V GetAttributeValue<T, V>(this ICustomAttributeProvider mi, Func<T, V> getValue,
	        V defaultValue = default(V), bool inherit = true) where T : Attribute
	    {
	        var attrib = GetSingleAttribute<T>(mi,inherit);
	        if (attrib == null) return defaultValue;
	        return getValue(attrib);
	    }

        public static bool HasCustomAttribute<T>(this ICustomAttributeProvider mi,Func<T,bool> condition,bool inherit=true) where T:Attribute
        {
            var a = mi.GetSingleAttribute<T>(inherit);
            if (a != null)
            {
                return condition(a);
            }
            return false;
        }
        
#else
	    public static T GetSingleAttribute<T>(this Type tp, bool inherit=true) where T : Attribute => tp.GetTypeInfo().GetCustomAttribute<T>(inherit);



        public static V GetAttributeValue<T, V>(this MemberInfo mi, Func<T, V> getValue,
                   V defaultValue = default(V), bool inherit = true) where T : Attribute
        {
            var attrib = mi.GetCustomAttribute<T>(inherit);
            if (attrib == null) return defaultValue;
            return getValue(attrib);
        }

        public static bool HasCustomAttribute<T>(this Type tp, Func<T, bool> condition = null, bool inherit = true) where T : Attribute
            =>tp.GetTypeInfo().HasCustomAttribute<T>(condition,inherit);

        public static bool HasCustomAttribute<T>(this MemberInfo mi, Func<T, bool> condition=null, bool inherit = true) where T : Attribute
        {
            condition=condition??(t=> true);
            var a = mi.GetCustomAttribute<T>(inherit);

            if (a != null)
            {
                return condition(a);
            }
            return false;
        }
#endif



    }
}