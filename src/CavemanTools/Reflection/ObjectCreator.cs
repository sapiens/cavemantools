using System.Reflection.Emit;

namespace System.Reflection
{
	/// <summary>
	/// Create an object from type using the parameterless constructor.
	/// Faster than Activator on average by 2x. Use it if you need to create many of objects of the same type at the same location
	/// </summary>
	public class ObjectCreator
	{
		delegate object MethodInvoker();
		MethodInvoker methodHandler = null;

		public ObjectCreator(Type type)
		{
			CreateMethod(type.GetConstructor(Type.EmptyTypes));
		}

		public ObjectCreator(ConstructorInfo target)
		{
			CreateMethod(target);
		}

		void CreateMethod(ConstructorInfo target)
		{
			DynamicMethod dynamic = new DynamicMethod(string.Empty,
						typeof(object),
						new Type[0],
						target.DeclaringType);
			ILGenerator il = dynamic.GetILGenerator();
            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Ret);

			methodHandler = (MethodInvoker)dynamic.CreateDelegate(typeof(MethodInvoker));
		}

		public object CreateInstance()
		{
			return methodHandler();
		}
	}
}