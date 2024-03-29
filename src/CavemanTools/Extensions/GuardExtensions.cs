﻿using System.Collections.Generic;
using System.Linq;

using System.Reflection;

using System.Text.RegularExpressions;

namespace System
{
    public static class GuardExtensions
    {      
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
            
        public static void MustMatch(this string source,string regex,RegexOptions options=RegexOptions.None)
        {
            if (source.IsNullOrEmpty() || !Regex.IsMatch(source,regex,options)) throw new FormatException(string.Format("Argument doesn't match expression '{0}'",regex));
        }


        public static void MustBeNone<T>(this IEnumerable<T> data, Func<T,bool> predicate, Exception ex = null, string paramName = "")
        {
            if (data.Any(predicate)) throw ex ?? new ArgumentException($" All elements of {paramName??"argument"} must not satisfy he given condition");
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

                if (tp.GetTypeInfo().IsClass) return;


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

            if (!tp.GetTypeInfo().IsInterface) throw new ArgumentException("'{0}' is not an interface".ToFormat(tp));

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

        public static void MustBeAtLeast(this int value, int limit,string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  should be at least {limit}");
            if (value < limit) throw ex;
        }
        
        public static void MustBeBetween(this DateTime value, DateTime start, DateTime end,string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  should be between {start} and {end}");
            if (!(value >=start && value <=end)) throw ex;
        }
        
        public static void MustBeBetween(this DateTimeOffset value, DateTimeOffset start, DateTimeOffset end,string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  should be between {start} and {end}");
            if (!(value >=start && value <=end)) throw ex;
        }
        
        public static void MustBeAtLeast(this decimal value, decimal limit,string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  should be at least {limit}");
            if (value < limit) throw ex;
        }
        
        public static void MustBeAtLeast(this DateTime value, DateTime limit,string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  should be at least {limit}");
            if (value < limit) throw ex;
        }
        
        public static void MustBeAtLeast(this DateTimeOffset value, DateTimeOffset limit,string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  should be at least {limit}");
            if (value < limit) throw ex;
        }
        
        public static void MustBeGreaterThan0(this int value, string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  must be greater than 0");
            if (value <= 0) throw ex;
        }
        public static void MustBeGreaterThan0(this decimal value, string varName=null,Exception ex=null)
        {
            ex = ex ?? new ArgumentException($"Value of {varName ?? "argument"}  must be greater than 0");
            if (value <= 0) throw ex;
        }
        
        public static void MustBeGeneric(this Type type)
        {

            type.Must(t => t.GetTypeInfo().IsGenericType, "Type must be a generic type");

        }
    }
}