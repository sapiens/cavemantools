using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using System.Runtime.CompilerServices;

using CavemanTools;


namespace System
{
    public static class TypeExtensions
    {


                
        /// <summary>
        /// Orders an enumerable using [Order] or specified ordering function.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="other">Optional ascending ordering function</param>
        /// <returns></returns>
        public static IEnumerable<Type> OrderByAttribute(this IEnumerable<Type> types,Func<Type,int> other=null)
        {           
            return types.OrderBy(t =>
            {
                var attrib = t.GetSingleAttribute<OrderAttribute>();
                if (attrib != null)
                {
                    return attrib.Value;
                }
                if (other != null)
                {
                    return other(t);
                }
                return Int32.MaxValue;
            });
        }

        /// <summary>
        /// Usually get properties returns first the properties declared on the type, then properties declared on the base types.
        /// This method will return properties as you expect it 
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesInOrder(this Type tp)
        {
            return GetPropertiesInOrder(tp, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        }
        
        /// <summary>
        /// Usually get properties returns first the properties declared on the type, then properties declared on the base types.
        /// This method will return properties as you expect it 
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesInOrder(this Type tp,BindingFlags flags)
        {
            var props = new List<PropertyInfo>();
            var baseTP = tp;
            while (baseTP != null)
            {
                props.AddRange(baseTP.GetProperties(flags).Select(d => tp.GetProperties().First(f => f.Name == d.Name)).Reverse());
                baseTP = baseTP.GetTypeInfo().BaseType;
            }
            props.Reverse();
            return props.Distinct();
        }

        /// <summary>
        /// Orders an enumerable using [Order] 
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="defValue">Default value if the attribute is missing</param>
        /// <returns></returns>
        public static IEnumerable<T> OrderByAttribute<T>(this IEnumerable<T> objects,int defValue=0)
        {
            return objects.OrderBy(t =>
            {
                var tp=(t is Type)?t as Type:t.GetType();
                var attrib = tp.GetSingleAttribute<OrderAttribute>();
                if (attrib != null)
                {
                    return attrib.Value;
                }               
                return defValue;
            });
        }


        /// <summary>
		/// Used for checking if a class implements an interface
		/// </summary>
		/// <typeparam name="T">Interface</typeparam>
		/// <param name="type">Class Implementing the interface</param>
		/// <returns></returns>
		public static bool Implements<T>(this Type type)
		{
			type.MustNotBeNull();
		    return type.Implements(typeof (T));
		}

        /// <summary>
        /// Creates a new instance of type using a public parameterless constructor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(this Type type)
        {
            type.MustNotBeNull();
            return type.GetFactory()();
        }

        /// <summary>
        /// Used for checking if a class implements an interface
        /// </summary>
        /// <param name="type">Class Implementing the interface</param>
        /// <param name="interfaceType">Type of an interface</param>
        /// <returns></returns>
        public static bool Implements(this Type type,Type interfaceType)
        {
            type.MustNotBeNull();
            interfaceType.MustNotBeNull();

            if (!interfaceType.GetTypeInfo().IsInterface) throw new ArgumentException("The generic type '{0}' is not an interface".ToFormat(interfaceType));

            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// True if the type implements of extends T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool DerivesFrom<T>(this Type type)
        {
            return type.DerivesFrom(typeof (T));
        }

        /// <summary>
        /// True if the type implements of extends parent. 
        /// Doesn't work with generics
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool DerivesFrom(this Type type, Type parent)
        {
            type.MustNotBeNull();
            parent.MustNotBeNull();
            return parent.IsAssignableFrom(type);
        }


        public static bool CheckIfAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            var info = type.GetTypeInfo();
            return info.HasCustomAttribute<CompilerGeneratedAttribute>()
                && info.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (info.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }


        public static bool ImplementsGenericInterfaceOf<T>(this Type type,
            params Type[] genericArgs)
        {
            return type.ImplementsGenericInterface(typeof (T), genericArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericInterfaceType">It can be an open or close generics interface</param>
        /// <returns></returns>
        public static bool ImplementsGenericInterface(this Type type, Type genericInterfaceType,params Type[] genericArgs)
        {
         
            var interf = type.GetInterfaces().Where(t => t.Name == genericInterfaceType.Name).ToArray();
            if (interf.IsNullOrEmpty()) return false;

            return interf.Any(i =>
            {
            var comparer = genericInterfaceType.GenericTypeArguments().Any()
                            ? genericInterfaceType.GenericTypeArguments()
                            : genericArgs;
            
                        if (comparer.Length > 0)
                        {
                            return i.GenericTypeArguments().HasTheSameElementsAs(comparer);
                        }
          
                        return true;
            });

            
        }

        public static bool InheritsGenericType(this Type tp, Type genericType, params Type[] genericArgs)
        {
            tp.MustNotBeNull();
            var info = tp.GetTypeInfo();

            genericType.GetTypeInfo().Must(t => t.IsGenericType, "Type must be a generic type");
            if (info.BaseType == null) return false;

            var baseType = info.BaseType;
            var baseInfo = info.BaseType.GetTypeInfo();
            bool found = false;
            if (baseInfo.IsGenericType)
            {
                if (baseInfo.Name == genericType.Name)
                {
                    var comparer = genericType.GenericTypeArguments().Any()
               ? genericType.GenericTypeArguments()
               : genericArgs;
                    if (comparer.Length > 0)
                    {
                        found = baseType.GenericTypeArguments().HasTheSameElementsAs(comparer);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            if (!found) return baseType.InheritsGenericType(genericType);

            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tp">Generic type</param>
        /// <param name="index">0 based index of the generic argument</param>
        /// <exception cref="InvalidOperationException">When the type is not generic</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static Type GetGenericArgument(this Type tp, int index = 0)
        {
            tp.MustNotBeNull();

            if (!tp.GetTypeInfo().IsGenericType) throw new InvalidOperationException("Provided type is not generic");


            return tp.GenericTypeArguments()[index];            
        }

        static Type[] GenericTypeArguments(this Type type)
        {
            type.MustBeGeneric();

            return type.GenericTypeArguments;


        }

        /// <summary>
        /// Checks if type is a reference but also not
        ///  a string, array, Nullable, enum
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsUserDefinedClass(this Type type)
        {
            type.MustNotBeNull();
            var info = type.GetTypeInfo();
            if (!info.IsClass) return false;
            
            if (type.GetTypeCode() != TypeCode.Object) return false;
            if (type.IsArray) return false;
            if (type.IsNullable()) return false;
           return true;
        }

        public static TypeCode GetTypeCode(this Type type)
        {
            if (type == null)
                return TypeCode.Empty;
            if (type == typeof(bool))
                return TypeCode.Boolean;
            if (type == typeof(char))
                return TypeCode.Char;
            if (type == typeof(sbyte))
                return TypeCode.SByte;
            if (type == typeof(byte))
                return TypeCode.Byte;
            if (type == typeof(short))
                return TypeCode.Int16;
            if (type == typeof(ushort))
                return TypeCode.UInt16;
            if (type == typeof(int))
                return TypeCode.Int32;
            if (type == typeof(uint))
                return TypeCode.UInt32;
            if (type == typeof(long))
                return TypeCode.Int64;
            if (type == typeof(ulong))
                return TypeCode.UInt64;
            if (type == typeof(float))
                return TypeCode.Single;
            if (type == typeof(double))
                return TypeCode.Double;
            if (type == typeof(decimal))
                return TypeCode.Decimal;
            if (type == typeof(DateTime))
                return TypeCode.DateTime;
            if (type == typeof(string))
                return TypeCode.String;
            if (type.GetTypeInfo().IsEnum)
                return GetTypeCode(Enum.GetUnderlyingType(type));
            return TypeCode.Object;
        }

        /// <summary>
        /// This always returns false if the type is taken from an instance.
        /// That is always use typeof
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            type.MustNotBeNull();
            return (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>));
        }

        public static bool IsNullableOf(this Type type, Type other)
        {
            type.MustNotBeNull();
            other.MustNotBeNull();
            return type.IsNullable() && type.GetGenericArguments()[0].Equals(other);
        }

        public static bool IsNullableOf<T>(this Type type)
        {
            return type.IsNullableOf(typeof (T));
        }

        public static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            return CanBeCastTo(type, typeof(T));
        }

        public static bool CanBeInstantiated(this Type type)
        {
            var info = type.GetTypeInfo();
            return !info.IsAbstract && !info.IsInterface;
        }
           

        public static bool CanBeCastTo(this Type type, Type other)
        {
            if (type == null) return false;
            if (type == other) return true;
            return other.IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns the version of assembly containing type
        /// </summary>
        /// <returns></returns>
        public static Version AssemblyVersion(this Type tp) =>

            tp.GetTypeInfo().Assembly.Version();


        /// <summary>
        /// Returns the full name of type, including assembly but not version, public key etc, i.e: namespace.type, assembly
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns></returns>
        public static string GetFullTypeName(this Type t)
        {
            if (t == null) throw new ArgumentNullException("t");

            return $"{t.FullName}, {t.Assembly.GetName().Name}";


        }

        public static object GetDefault(this Type type)
        {
            if (type.GetTypeInfo().IsValueType) return Activator.CreateInstance(type);

            return null;
        }

        /// <summary>
        /// Returns namespace without the assembly name
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string StripNamespaceAssemblyName(this Type type)
        {
            var asmName = type.GetTypeInfo().Assembly.GetName().Name;
            return type.Namespace.Substring(asmName.Length + 1);
        }
	}
}



