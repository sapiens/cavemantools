using System.Linq.Expressions;

namespace System.Reflection.Emit

{
    public static class LCGUtils
    {
        /// <summary>
        /// IL corresponding to push Type on stack
        /// </summary>
        /// <param name="il"></param>
        /// <param name="tp"></param>
        public static void EmitPushType(this ILGenerator il, Type tp)
        {
            il.Emit(OpCodes.Ldtoken, tp);
            il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }));
        }

        /// <summary>
        /// IL to unbox values, also handles Nullable
        /// </summary>
        /// <param name="il"></param>
        /// <param name="type">value type</param>
        public static void EmitUnboxValue(this ILGenerator il,Type type)
        {
            if (type.IsNullable())
            {
                il.Emit(OpCodes.Unbox_Any, Nullable.GetUnderlyingType(type));
                il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { Nullable.GetUnderlyingType(type) }));
            }
            else
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
        }

        //public static void EmitIf(this ILGenerator il,bool condition,Action<ILGenerator> then)
        //{
        //    var endThen = il.DefineLabel();
        //    if(!condition)il.Emit(OpCodes.Brtrue,endThen);
        //    else
        //    {
        //        il.Emit(OpCodes.Brfalse, endThen);
        //    }
        //    then(il);
        //    il.MarkLabel(endThen);
            
        //}

        //public static void EmitFor(this ILGenerator il, int start,int max,int increment, Action<ILGenerator,LocalBuilder,Label> block)
        //{
          
        //    var beginLoop = il.DefineLabel();
        //    var checkLoop = il.DefineLabel();
        //    var endLoop = il.DefineLabel();
        //    il.BeginScope();
        //    var i = il.DeclareLocal(typeof(int));
        //    il.Emit(OpCodes.Ldc_I4,start);
        //    il.Emit(OpCodes.Stloc_S,i);
        //    il.Emit(OpCodes.Br_S,checkLoop);
        //    il.MarkLabel(beginLoop);
        //    block(il, i,endLoop);
        //    il.Emit(OpCodes.Ldloc_S,i);
        //    il.Emit(OpCodes.Add,increment);
        //    il.Emit(OpCodes.Stloc_S, i);
        //    il.MarkLabel(checkLoop);
        //    il.Emit(OpCodes.Ldloc_S,i);
        //    il.Emit(OpCodes.Ldc_I4,max);
        //    il.Emit(OpCodes.Clt);
        //    il.Emit(OpCodes.Brtrue_S,beginLoop);
            
        //    il.MarkLabel(endLoop);
        //    il.EndScope();
                        
        //}

        public static void EmitNewEmptyNullable(this ILGenerator il, Type type,out LocalBuilder def)
        {
            def = il.DeclareLocal(type);
            il.Emit(OpCodes.Ldloca_S,def);
            il.Emit(OpCodes.Initobj, type);                        
        }

        public static void EmitThrowException<T>(this ILGenerator il,string s=null) where T:Exception
        {
            var invalidcast = typeof(T);
            il.Emit(OpCodes.Ldstr,s??"message");
            il.Emit(OpCodes.Newobj, invalidcast.GetConstructor(new[]{typeof(string)}));
            il.ThrowException(invalidcast);
        }

        /// <summary>
        /// Console.WriteLine for the value on the stack
        /// </summary>
        /// <param name="il"></param>
        public static void EmitConsoleWriteLine(this ILGenerator il)
        {
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(object) }));
        }

        /// <summary>
        /// Shows message followed by current object from the top of the stack
        /// </summary>
        /// <param name="il"></param>
        /// <param name="msg"></param>
        public static void EmitDebugWriteLineForCurrentObject(this ILGenerator il,string msg)
        {
            il.EmitWriteLine(msg);
            il.Emit(OpCodes.Dup);
            il.EmitConsoleWriteLine();
        }
          
  
        public static void EmitDup(this ILGenerator il)
        {
          il.Emit(OpCodes.Dup);  
        }

        public static void EmitLoadLocal(this ILGenerator il,int pos)
        {
            il.Emit(OpCodes.Ldloc,pos);
        }

        public static void EmitStoreLocal(this ILGenerator il, int pos)
        {
            il.Emit(OpCodes.Stloc, pos);
        }

        public static void EmitLoadMethodArgument(this ILGenerator il, int pos)
        {
            il.Emit(OpCodes.Ldarg, pos);
        }

        public static void EmitCall<T>(this ILGenerator il,Expression<Action<T>> expression)
        {
            expression.MustNotBeNull();
            var meth = expression.Body as MethodCallExpression;
            if (meth == null) throw new ArgumentException("Not a method");
            il.Emit(OpCodes.Call,meth.Method);            
        }
        
        public static void EmitCallVirtual<T>(this ILGenerator il,Expression<Action<T>> expression)
        {
            expression.MustNotBeNull();
            var meth = expression.Body as MethodCallExpression;
            if (meth == null) throw new ArgumentException("Not a method");
            il.Emit(OpCodes.Callvirt,meth.Method);            
        }

        public static void EmitPropertyGetter<T>(this ILGenerator il, Expression<Action<T>> expression)
        {
            expression.MustNotBeNull();
            var meth = expression.Body as MemberExpression;
            if (meth==null) throw new ArgumentException("Not a property");
            var pr = (PropertyInfo) meth.Member;
            il.Emit(OpCodes.Callvirt,pr.GetGetMethod());
        }

        public static void EmitPropertySetter<T>(this ILGenerator il, Expression<Action<T>> expression)
        {
            expression.MustNotBeNull();
            var meth = expression.Body as MemberExpression;
            if (meth == null) throw new ArgumentException("Not a property");
            var pr = (PropertyInfo)meth.Member;
            il.Emit(OpCodes.Callvirt, pr.GetSetMethod());
        }
    }
}