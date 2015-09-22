using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using CavemanTools.Model.ValueObjects;


namespace System
{
	public static class ObjectExtend
	{
	    private static ConcurrentDictionary<Type, TypeInfo> _typeDicts;
		/// <summary>
		/// Creates dictionary from object properties.
		/// </summary>
		/// <param name="value">Object</param>
		/// <returns></returns>
		public static IDictionary<string,object> ToDictionary(this object value)
		{
            value.MustNotBeNull();
            if (value is IDictionary<string, object>)
            {
                return (IDictionary<string, object>)value;
            }

            if (_typeDicts==null)
			{
			    _typeDicts= new ConcurrentDictionary<Type, TypeInfo>();
			}

		    TypeInfo info;
		    var tp = value.GetType();
           
            
            if(!_typeDicts.TryGetValue(tp,out info))
            {
                var allp = tp.GetProperties(BindingFlags.Instance|BindingFlags.Public|BindingFlags.GetProperty);
                
                //lambda
                var dict = Expression.Parameter(typeof (IDictionary<string, object>),"dict");
                var inst = Expression.Parameter(typeof(object),"obj");
              
                var lblock=new List<Expression>(allp.Length);
                
                
                for(int i=0;i<allp.Length;i++)
                {
                    var prop = allp[i];
                    var indexer = Expression.Property(dict, "Item",Expression.Constant(prop.Name));
                    lblock.Add(
                        Expression.Assign(indexer,
                            Expression.ConvertChecked(
                                Expression.Property(
                                   Expression.ConvertChecked(inst, tp), prop.Name), typeof(object))
                            ));
                }
                var body = Expression.Block(lblock);
                var lambda = Expression.Lambda(Expression.GetActionType(typeof(IDictionary<string,object>),typeof(object)),body, dict, inst);
                
                info=new TypeInfo(allp.Length,lambda.Compile());
                _typeDicts.TryAdd(tp, info);
            }

            return info.Update(value.ConvertTo(tp));		    
		}
		
        class TypeInfo
        {
            private readonly int _size;
            private readonly Delegate _del;
         
            public TypeInfo(int size,Delegate del)
            {
                _size = size;
                _del = del;
            }

            public Dictionary<string,object> Update(object o)
            {
                var d = new Dictionary<string, object>(_size);
                (_del as Action<IDictionary<string,object>,object>)(d, o);
                return d;
            }
        }

        /* http://stackoverflow.com/questions/1616144/how-can-a-large-array-be-split-into-smaller-arrays */
       public static T[][] Split<T>(this T[] arrayIn, int length)
        {
            bool even = arrayIn.Length % length == 0;
            int totalLength = arrayIn.Length / length;
            if (!even)
                totalLength++;

            T[][] newArray = new T[totalLength][];
            for (int i = 0; i < totalLength; ++i)
            {
                int allocLength = length;
                if (!even && i == totalLength - 1)
                    allocLength = arrayIn.Length % length;

                newArray[i] = new T[allocLength];
                Array.Copy(arrayIn, i * length, newArray[i], 0, allocLength);
            }
            return newArray;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="agg"></param>
        /// <param name="handler">Returns true if exception is handled. Any unhandled exceptions will be rethrown</param>
	   public static void Handle<T>(this AggregateException agg, Func<T,bool> handler) where T:Exception
	    {
	        if (agg == null) return;
	       List<Exception> unhandled=null;
	       foreach (var x in agg.Flatten().InnerExceptions.OfType<T>())
	       {
	           if (!handler(x))
	           {
	               if (unhandled == null)
	               {
	                   unhandled = new List<Exception>();
	               }
                   unhandled.Add(x);
	           };
	       }
	       if (unhandled.Count > 0)
	       {
	           throw new AggregateException(agg.Message,unhandled);
	       }
	    }

	    /// <summary>
	    /// 
	    /// If object is not null invokes function else returns default of type
	    /// </summary>
	    /// <typeparam name="V"></typeparam>
	    /// <typeparam name="T"></typeparam>
	    /// <param name="item"></param>
	    /// <param name="invoke"></param>
	    /// <returns></returns>
        public static T ReturnDefaultOrResult<V, T>(this V item, Func<V, T> invoke,T defaultValue=default(T)) where V : class
	    {
	        if (item == null) return defaultValue;
            return invoke(item);
        }
        
        /// <summary>
        /// Executes the 'process' function if the source is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="data"></param>
        /// <param name="process"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static R IfNotNullApply<T, R>(this T data, Func<T, R> process, R valueIfNull = default(R)) where T : class
	        => (data == null) ? valueIfNull : process(data);
	    
            

	    //public static void DoIf<T>(this T item, Func<T, bool> condition, Action<T> action)
	    //{
	    //    if (condition(item)) action(item);
  
	    //}

	    public static T GetValue<T>(this AbstractValueObject<T> d, T defaultValue = default(T))
	    {
	        if (d == null) return defaultValue;
	        return d.Value;
	    }

       
		/// <summary>
		/// Converts object to type.
		/// Supports conversion to Enum, DateTime,TimeSpan and CultureInfo
		/// </summary>
		/// <exception cref="InvalidCastException"></exception>
		/// <param name="data">Object to be converted</param>
		/// <param name="tp">Type to convert to</param>
		/// <returns></returns>
		public static object ConvertTo(this object data, Type tp)
		{
		   if (data==null)
		   {
		       if (tp.IsValueType && !tp.IsNullable())
		       {
		           throw new InvalidCastException("Cant convert null to a value type");
		       }
		       return null;
		   }

           var otp = data.GetType();
           if (otp.Equals(tp)) return data;
           if (tp.IsEnum)
           {
               if (data is string)
               {
                   return Enum.Parse(tp, data.ToString());
               }
               var o = Enum.ToObject(tp, data);
               return o;
           }

           if (tp.IsValueType)
           {
               if (tp == typeof(TimeSpan))
               {
                   return TimeSpan.Parse(data.ToString());
               }

               if (tp == typeof(DateTime))
               {
                   if (data is DateTimeOffset)
                   {
                       return data.Cast<DateTimeOffset>().DateTime;
                   }
                   return DateTime.Parse(data.ToString());
               }

               if (tp == typeof(DateTimeOffset))
               {
                   if (data is DateTime)
                   {
                       var dt = (DateTime)data;
                       return new DateTimeOffset(dt);
                   }
                   return DateTimeOffset.Parse(data.ToString());
               }

               if (tp.IsNullable())
               {
                   var under = Nullable.GetUnderlyingType(tp);
                   return data.ConvertTo(under);
               }
           }
           else if (tp == typeof(CultureInfo)) return new CultureInfo(data.ToString());

           return System.Convert.ChangeType(data, tp);
		}

		/// <summary>
		///	Tries to convert the object to type.
		/// </summary>
		/// <exception cref="InvalidCastException"></exception>
		/// <exception cref="FormatException"></exception>
		/// <typeparam name="T">Type to convert to</typeparam>
		/// <param name="data">Object</param>
		/// <returns></returns>
		public static T ConvertTo<T>(this object data)
		{
			var tp = typeof(T);			
			var temp = (T)ConvertTo(data, tp);
			return temp;			
		}

		

		/// <summary>
		///	Tries to convert the object to type.
		/// If it fails it returns the specified default value.
		/// </summary>
		/// <typeparam name="T">Type to convert to</typeparam>
		/// <param name="data">Object</param>
		/// <param name="defaultValue">IF not set , the default for T is used</param>
		/// <returns></returns>
		public static T SilentConvertTo<T>(this object data,T defaultValue=default(T))
		{
			var tp = typeof (T);  
			try
			{
				var temp = (T) ConvertTo(data, tp);
				return temp;
		    }
			catch (InvalidCastException)
			{
			    return defaultValue;
			}			
		}

        
        /// <summary>
        /// Shorthand for lazy people to cast an object to a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
       public static T As<T>(this object o) where T:class 
        {
            return o as T ;
		}

        /// <summary>
        /// Shorthand for lazy people to cast an object to a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static T Cast<T>(this object o)
        {
            return (T)o;
        }

        /// <summary>
        /// Shortcut for 'object is type'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool Is<T>(this object o)
        {
            if (o is Type)
            {
                return (Type) o == typeof (T);
            }
            return o is T;
        }

	    public static bool IsNull<T>(this T inst) where T : class
	    {
	        return inst == null;
	    }

	    public static bool IsNotNull<T>(this T inst) where T : class
	    {
	        return inst != null;
	    }


        public static bool IsNot<T>(this object o)
        {
            return !Is<T>(o);
        }

	    public static T CreateIfNull<T>(this T instance, Action<T> config = null) where T : new()
	    {
	        if (instance != null)
	        {
	            return instance;
	        }
            instance=new T();
	        if (config != null)
	        {
	            config(instance);
	        }
	        return instance;
	    }
	}

    
}

namespace CavemanTools.Testing
{
//public static class ObjectExtensions
//{
//    /// <summary>
//    /// Generates a string containing the properties and values of the object
//    /// </summary>
//    /// <param name="val"></param>
//    /// <returns></returns>
//    public static string ToDebugString(this object val, StringBuilder sb = null)
//    {
//        if (val == null) return string.Empty;
//        if (sb == null)
//        {
//            sb = new StringBuilder();
//        }
//        var dict = val.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
//            .Where(p => p.GetIndexParameters().Length == 0)
//            .ToDictionary(p => string.Format("({1}) {0}", p.Name, p.PropertyType.Name), p => p.GetValue(val, null));
//        sb.AppendFormat("({0})\n", val.GetType());
//        foreach (var kv in dict)
//        {
//            sb.AppendFormat("{0} =", kv.Key);
//            if (kv.Value != null)
//            {
//                var tp = kv.Value.GetType();
//                if (tp.Implements<IEnumerable>())
//                {
//                    var en = kv.Value as IEnumerable;
//                    var i = 0;
//                    sb.AppendFormat("\n\t");
//                    foreach (var item in en)
//                    {
//                        sb.AppendFormat("[{0}]={1}\n", i, item.ToDebugString());
//                        i++;
//                    }

//                }
//                else
//                {
//                    if (!tp.IsPrimitive)
//                    {
//                        sb.Append("\n\t");
//                        sb.Append(kv.Value.ToDebugString());
//                    }
//                    else
//                    {
//                        sb.Append(kv.Value);
//                    }
//                }
//            }
//            else
//            {
//                sb.Append("null");
//            }
//            sb.AppendLine();
//        }

//        return sb.ToString();
//    }

//}

}							 