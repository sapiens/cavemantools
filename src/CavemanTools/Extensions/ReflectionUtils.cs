
namespace System.Reflection
{
    public static class ReflectionUtils
    {


     

        /// <summary>
        /// Returns the assembly version
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static Version Version(this Assembly asm)
        {
            return asm.GetName().Version;
        }

        
        /// <summary>
        /// Used for resource localizing
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="ArgumentException">If property was not found</exception>
        /// <param name="propertyName">Public static property name</param>
        /// <returns></returns>
        public static T GetStaticProperty<T>(this object type, string propertyName)
        {
            var tp = type.GetType().GetProperty(propertyName, BindingFlags.Static);
            if (tp == null) throw new ArgumentException("Property doesn't exist.", "propertyName");
            return tp.GetValue(null, null).ConvertTo<T>();
        }

        private delegate void Setter(object dest, object value);

    

        /// <summary>
        /// Returns true if object is specifically of type. 
        /// Use "is" operator to check if an object is an instance of a type that derives from type.
        /// Returns false is T is nullable
        /// </summary>
        /// <typeparam name="T">any not nullable Type</typeparam>
        /// <param name="o">Object</param>
        /// <returns></returns>
        public static bool IsExactlyType<T>(this object o)
        {
            return (o.GetType() == typeof(T));
        }

        public static bool IsProperty(this MemberInfo info) => info is PropertyInfo;
        public static bool IsField(this MemberInfo info) => info is FieldInfo;
        public static bool IsMethod(this MemberInfo info) => info is MethodInfo;

        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            var info = memberInfo as MethodInfo;
            if (info != null)
                return info.ReturnType;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.PropertyType;
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
                return fieldInfo.FieldType;
            return null;
        }

        /// <summary>
        /// Gets the value of a property via reflection
        /// </summary>
        /// <typeparam name="T">Type of property value</typeparam>
        /// <param name="object">Object to get value from</param>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object @object, string propertyName)
        {
            return GetPropertyValue(@object, propertyName).ConvertTo<T>();
        }

        /// <summary>
        /// Facade to set a value to a property or field via reflection
        /// </summary>
        /// <param name="member"></param>
        /// <param name="data"></param>
        /// <param name="value"></param>
        public static void SetValue(this MemberInfo member, object data,object value)
        {
            var prop = member as PropertyInfo;
            if (prop != null)
            {
                member.CastAs<PropertyInfo>().SetValue(data, value);
                return;
            }
            var field = member as FieldInfo;
            if (field != null)
            {
                member.CastAs<FieldInfo>().SetValue(data, value);
                return;
            }
         
            throw new NotSupportedException("Only fields and non indexed properties are supported");
        }

        /// <summary>
        /// Gets the value of a public property
        /// </summary>
        /// <param name="object">Object to get value from</param>
        /// <param name="property">Public property name</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object @object, string property)
        {
            if (@object == null) return null;
            property.MustNotBeEmpty();
            var tp = @object.GetType();

            var pi = tp.GetProperty(property,
                                    BindingFlags.Instance | BindingFlags.Public);
            if (pi == null) throw new ArgumentException("Property doesn't exist.", "property");


            return pi.GetValue(@object);


        }
  
    }
}