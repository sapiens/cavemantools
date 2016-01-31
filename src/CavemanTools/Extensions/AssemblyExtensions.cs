using System.Collections.Generic;
using System.Linq;

namespace System.Reflection
{
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Returns public types derived from T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="asm"></param>
		/// <param name="instantiable">True to return only types that can be instantiated i.e no interfaces and no abstract classes</param>
		/// <returns></returns>
		public static IEnumerable<Type> GetTypesDerivedFrom<T>(this Assembly asm,bool instantiable=false)
		{
            if (asm == null) throw new ArgumentNullException("asm");
			var res= asm.GetExportedTypes().Where(tp => (typeof (T)).IsAssignableFrom(tp));
            if (instantiable)
            {
#if COREFX
                res = res.Where(t => !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsInterface);
#else
                res = res.Where(t => !t.IsAbstract && !t.IsInterface);
#endif
            }
		    return res;
		}

        /// <summary>
        /// Searches and instantiate types derived from T. Must contain a public parameterless constructor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asm"></param>
        /// <returns></returns>
	    public static IEnumerable<T> GetInstancesOfTypesDerivedFrom<T>(this Assembly asm) where T : class,new()
        {
            return asm.GetInstancesOfTypesDerivedFrom<T>(t => (T) Activator.CreateInstance(t));
        }
        public static IEnumerable<T> GetInstancesOfTypesDerivedFrom<T>(this Assembly asm,Func<Type,T> factory) where T : class,new()
	    {
	        factory.MustNotBeNull();
            return asm.GetTypesDerivedFrom<T>(true).Select(factory);
	    }

		public static IEnumerable<Type> GetTypesWithAttribute<T>(this Assembly asm) where T:Attribute
		{
			if (asm == null) throw new ArgumentNullException("asm");
#if COREFX
		    return asm.GetExportedTypes().Where(a => a.GetTypeInfo().HasCustomAttribute<T>());
#else
            return asm.GetExportedTypes().Where(a => a.HasCustomAttribute<T>());
#endif

		}

        public static IEnumerable<Type> GetPublicTypes(this Assembly asm, Func<Type, bool> filter)
	    {
#if COREFX
            return asm.GetTypes().Where(t => t.GetTypeInfo().IsPublic && filter(t));
#else
            return asm.GetTypes().Where(t => t.IsPublic && filter(t));
#endif
        }

	}
}