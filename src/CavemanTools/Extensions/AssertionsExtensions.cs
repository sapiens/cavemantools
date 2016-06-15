using System.Collections.Generic;
using System.Linq;
#if COREFX
using System.Reflection;
#endif
using System.Text.RegularExpressions;

namespace System
{
    public static class AssertionsExtensions
    {
        /// <exception cref="Exception"></exception>
        public static void MustNotBeDefault<T>(this T value,string message="",Exception ex=null)
        {
            if (value.Equals(default(T))) throw ex??new ArgumentException(message??$"Argument must not be {default(T)}");
        }
            
         public static void MustNotBeNull<T>(this T param,string message=null, Exception ex = null) where T:class
         {
             if (param == null) throw ex??new ArgumentNullException("parameter",message??"");
         }
         

        public static void MustNotBeEmpty(this string arg,string msg=null,Exception ex=null)
        {
            if (string.IsNullOrWhiteSpace(arg)) throw ex??new FormatException(msg??"Argument must not be null, empty or whitespaces");
        }

        public static void MustNotBeEmpty<T>(this IEnumerable<T> list, string msg = null, Exception ex = null)
        {
            if (list.IsNullOrEmpty()) throw ex??new ArgumentException(msg??"The collection must contain at least one element");
        }
            
        public static void MustRegex(this string source,string regex,RegexOptions options=RegexOptions.None)
        {
            if (source.IsNullOrEmpty() || !Regex.IsMatch(source,regex,options)) throw new FormatException(string.Format("Argument doesn't match expression '{0}'",regex));
        }

       

        /// <summary>
        /// Throws if the value can't be used as-is in an url
        /// </summary>
        /// <param name="source"></param>
        public static void MustBeUrlFriendly(this string source)
        {
            if (!source.IsNullOrEmpty())
            {
                if (source.MakeSlug().Equals(source,StringComparison.OrdinalIgnoreCase)) return;
            }
            throw new FormatException("Provided string value must be url friendly");
        }


        /// <summary>
        /// Value type must be of specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static void MustBeOfType<T>(this object value)
        {
            var tp = typeof (T);
            bool ex = false;
            if (value == null)
            {
#if COREFX
                if (tp.GetTypeInfo().IsClass) return;
#else
              if (tp.IsClass) return;
#endif

                ex = true;
            }
            if (ex || (value.GetType()!=tp)) throw new ArgumentException($"Argument must be of type '{tp}'");
        }


        public static void MustBe<T>(this T arg, T value,string msg="",Exception ex=null) where T:IEquatable<T> 
            => arg.Must(d=>d.Equals(value),msg,ex);
        public static void MustNotBe<T>(this T arg, T value,string msg="",Exception ex=null) where T:IEquatable<T> 
            => arg.Must(d=>!d.Equals(value),msg,ex);

        public static void Must<T>(this T arg, Func<T, bool> condition, string msg=null,Exception ex=null)
        {
            condition.MustNotBeNull();
            if (!condition(arg))
            {
                throw ex??new ArgumentException(msg??"Argument doesn't meet the specified condition");
            }
        }

        /// <summary>
        /// Arugment must implement interface T
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <param name="value"></param>
        public static void MustImplement<T>(this object value)
        {
            value.MustNotBeNull("value");
            var tp = typeof (T);            
#if COREFX
            if (!tp.GetTypeInfo().IsInterface) throw new ArgumentException("'{0}' is not an interface".ToFormat(tp));
#else
            if (!tp.IsInterface) throw new ArgumentException("'{0}' is not an interface".ToFormat(tp));
#endif
            var otype = value.GetType();
            
            if (value is Type)
            {
                otype= value as Type;
            }

            if (!otype.Implements(tp)) throw new ArgumentException("Argument must implement '{0}'".ToFormat(tp));
        }

        /// <summary>
        /// Argument must be an implementation or subclass of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static void MustDeriveFrom<T>(this object value)
        {
            value.MustNotBeNull("value");
            var tp = typeof(T);
            var otype = value.GetType();

            if (value is Type)
            {
                otype = value as Type;
            }

            if (!otype.DerivesFrom(tp)) throw new ArgumentException("Argument must derive from '{0}'".ToFormat(tp)); 
        }

        /// <summary>
        /// List must not be empty and must have non-null values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="throwWhenNullValues"></param>
        public static void MustHaveValues<T>(this IEnumerable<T> list,bool throwWhenNullValues=true) where T : class
        {
            list.MustNotBeEmpty();
            
            if (throwWhenNullValues)
            {
                if (list.Any(v => v == null))
                {
                    throw new ArgumentException("The collection is null, empty or it contains null values");
                }
            }            
        }

        public static void MustBeGeneric(this Type type)
        {
#if COREFX
            type.Must(t => t.GetTypeInfo().IsGenericType, "Type must be a generic type");
#else
            type.Must(t => t.IsGenericType, "Type must be a generic type");
#endif
        }
    }
}