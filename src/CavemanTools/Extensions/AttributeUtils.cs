
namespace System.Reflection
{
    public static class AttributeUtils
	{

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




    }
}