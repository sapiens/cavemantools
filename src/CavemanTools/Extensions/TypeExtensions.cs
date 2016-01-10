using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CavemanTools;

namespace System
{
    using Reflection;
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
                baseTP = baseTP.BaseType;
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
            return Activator.CreateInstance(type);
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
            if (!interfaceType.IsInterface) throw new ArgumentException("The generic type '{0}' is not an interface".ToFormat(interfaceType));
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
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="o"></param>
        ///// <param name="interfaceName">The intuitive interface name</param>
        ///// <param name="genericType">Interface's generic arguments types</param>
        ///// <returns></returns>
        //public static bool ImplementsGenericInterface(this object o, string interfaceName, params Type[] genericType)
        //{
        //    Type tp = o.GetType();
        //    if (o is Type)
        //    {
        //        tp = (Type)o;
        //    }
        //    var interfaces = tp.GetInterfaces().Where(i => i.IsGenericType && i.Name.StartsWith(interfaceName));
        //    if (genericType.Length == 0)
        //    {
        //        return interfaces.Any();
        //    }

        //    return interfaces.Any(
        //        i =>
        //        {
        //            var args = i.GetGenericArguments();
        //            return args.HasTheSameElementsAs(genericType);
        //        });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericInterfaceType">It can be an open or close generics interface</param>
        /// <returns></returns>
        public static bool ImplementsGenericInterface(this Type type, Type genericInterfaceType,params Type[] genericArgs)
        {
            genericInterfaceType.MustComplyWith(t=>t.IsGenericType,"Type must be of a generic interface");
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

        public static bool InheritsGenericType(this Type tp, Type genericType,params Type[] genericArgs)
        {
            tp.MustNotBeNull();
            genericType.MustComplyWith(t => t.IsGenericType, "Type must be a generic type");
            if (tp.BaseType == null) return false;
            
            var baseType = tp.BaseType;
            bool found = false;
            if (baseType.IsGenericType)
            {
                if (baseType.Name == genericType.Name)
                {
                    var comparer = genericType.GenericTypeArguments().Any()
               ? genericType.GenericTypeArguments()
               : genericArgs;
                    if (comparer.Length>0)
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
            if (!tp.IsGenericType) throw new InvalidOperationException("Provided type is not generic");
            return tp.GenericTypeArguments()[index];            
        }

        static Type[] GenericTypeArguments(this Type type)
        {
            type.MustBeGeneric();
#if Net4
            return type.GetGenericArguments();
#else
            return type.GenericTypeArguments;

#endif
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
            if (!type.IsClass) return false;
            if (Type.GetTypeCode(type) != TypeCode.Object) return false;
            if (type.IsArray) return false;
            if (type.IsNullable()) return false;
           return true;
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
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>));
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
            return !type.IsAbstract && !type.IsInterface;
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
        public static Version AssemblyVersion(this Type tp)
        {
            return Assembly.GetAssembly(tp).Version();
        }
        /// <summary>
        /// Returns the full name of type, including assembly but not version, public key etc, i.e: namespace.type, assembly
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns></returns>
        public static string GetFullTypeName(this Type t)
        {
            if (t == null) throw new ArgumentNullException("t");
            return String.Format("{0}, {1}", t.FullName, Assembly.GetAssembly(t).GetName().Name);
        }

        public static object GetDefault(this Type type)
        {
#if COREFX
            if (type.GetTypeInfo().IsValueType) return Activator.CreateInstance(type);
#else
             if (type.IsValueType) return Activator.CreateInstance(type);
#endif            
            return null;
        }

        /// <summary>
        /// Returns namespace without the assembly name
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string StripNamespaceAssemblyName(this Type type)
        {
            var asmName = type.Assembly.GetName().Name;
            return type.Namespace.Substring(asmName.Length + 1);
        }
	}
}

