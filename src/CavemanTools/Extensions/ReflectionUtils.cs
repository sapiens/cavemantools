using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;
#if !COREFX
using System.Reflection.Emit;
#endif
namespace System.Reflection
{
    public static class ReflectionUtils
    {
#if COREFX
        public static bool IsClass(this Type type) => type.GetTypeInfo().IsClass;
        public static bool IsValueType(this Type type) => type.GetTypeInfo().IsValueType;            
#else
        public static bool IsClass(this Type type) => type.IsClass;
        public static bool IsValueType(this Type type) => type.IsValueType;
#endif

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
#if !COREFX
        private delegate void Setter(object dest, object value);

        private static ConcurrentDictionary<int, Setter> _cache;
        //static object setLock=new object();

        /// <summary>
        /// Fast setter. aprox 8x faster than simple Reflection
        /// </summary>
        /// <param name="a"></param>
        /// <param name="value"></param>
        public static void SetValueFast(this PropertyInfo p, object a, object value)
        {
            Setter inv = null;

            if (_cache == null)
            {
                _cache = new ConcurrentDictionary<int, Setter>();
            }
            var key = p.GetHashCode();

            if (!_cache.TryGetValue(key, out inv))
            {
                var mi = p.GetSetMethod();                

                

                DynamicMethod met = new DynamicMethod("set_" + key, typeof(void), new[] { typeof(object), typeof(object) }, typeof(ObjectExtend).Module, true);
                var il = met.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);//instance           
                il.Emit(OpCodes.Ldarg_1);//value
                if (p.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Unbox_Any, p.PropertyType);
                }
                il.Emit(OpCodes.Call, mi);
                il.Emit(OpCodes.Ret);
                inv = (Setter)met.CreateDelegate(typeof(Setter));

        _cache.TryAdd(key, inv);

            }


            inv(a, value);
        }

        /// <summary>
        /// Fast getter. aprox 5x faster than simple Reflection, aprox. 10x slower than manual get
        /// </summary>
        /// <param name="a"></param>	      
        public static object GetValueFast(this PropertyInfo p, object a)
        {
            Func<object, object> inv = null;
            if (_cacheGet == null)
            {
                _cacheGet = new ConcurrentDictionary<int, Func<object, object>>();
            }
            var key = p.GetHashCode();
            
            if (!_cacheGet.TryGetValue(key, out inv))
            {
                var mi = p.GetGetMethod();
                DynamicMethod met = new DynamicMethod("get_" + key, typeof(object), new[] { typeof(object) }, typeof(ObjectExtend).Module, true);
                var il = met.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);//instance           
                il.Emit(OpCodes.Call, mi);//call getter
                if (p.PropertyType.IsValueType) il.Emit(OpCodes.Box, p.PropertyType);
                il.Emit(OpCodes.Ret);
                inv = (Func<object, object>)met.CreateDelegate(Expression.GetFuncType(typeof(object), typeof(object)));
                _cacheGet.TryAdd(key, inv);

            }

            return inv(a);
        }
#endif
        /// <summary>
        /// Gets delegate to quickly create instances of type using public parameterless constructor.
        /// Use this only when you want to create LOTS of instances (dto scenario)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Func<object> GetFactory(this Type type)
        {
            type.MustNotBeNull("type");
            return TypeFactory.GetFactory(type);
        }

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
        /// Gets the value of a property
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
        /// Facade to set a value to a property or field
        /// </summary>
        /// <param name="member"></param>
        /// <param name="data"></param>
        /// <param name="value"></param>
        public static void SetValue(this MemberInfo member, object data,object value)
        {
            var prop = member as PropertyInfo;
            if (prop != null)
            {
                member.As<PropertyInfo>().SetValue(data, value);
                return;
            }
            var field = member as FieldInfo;
            if (field != null)
            {
                member.As<FieldInfo>().SetValue(data, value);
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

#if COREFX
            return pi.GetValue(@object);

#else
                  return pi.GetValueFast(@object);
#endif
        }
        private static ConcurrentDictionary<int, Func<object, object>> _cacheGet;

#if !COREFX
        /// <summary>
        /// Gets the file version of current executing assembly
        /// </summary>
        /// <returns></returns>
        public static string CurrentFileVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).FileVersion;
        }

        /// <summary>
        /// Gets the assembly version
        /// </summary>
        /// <returns></returns>
        public static Version CurrentAssemblyVersion()
        {
            return Assembly.GetCallingAssembly().GetName().Version;
        }

#endif
    }
}