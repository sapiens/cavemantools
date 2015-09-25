using System;
using System.Threading;
using System.Threading.Tasks;

namespace CavemanTools.Infrastructure
{
    public static class MessagingMediatorStatic
    {
        /// <summary>
        /// It should return instance or null
        /// </summary>
//        public static Func<Type, object> Resolver = t => { throw new InvalidOperationException("There is no resolver set for the MessagingMediator. Use 'MessageMediator.Resolver=type=>{/* call container to return instance */}'");};


        

//        public static ValidatorResult Validate<TInput>(this TInput input) where TInput : class
//        {
//            input.MustNotBeNull();
//            var handlerType = typeof (IValidateInput<>).MakeGenericType(typeof (TInput));
//            var handler = (IValidateInput<TInput>) Resolver(handlerType);
//            if (handler == null) throw new InvalidOperationException("There's no handler implementing 'IValidateInputy<{0}>' registered with the DI Container".ToFormat(typeof(TInput).GetType().Name));
//            return handler.Validate(input);
//        }
       
      
       
//        /// <summary>
//        /// Invokes the query handler which will take the specified argument as the input model
//        /// </summary>
//        /// <typeparam name="TOut">View model</typeparam>
//        /// <param name="model">Input model</param>
//        /// <returns></returns>
//        public static TOut QueryTo<TOut>(this object model) where TOut : class
//        {
//            return model.QueryTo(typeof (TOut)) as TOut;          
//        }
        
//        /// <summary>
//        /// Invokes the query handler which will take the specified argument as the input model
//        /// </summary>
    
//        /// <param name="model">Input model</param>
//        /// <returns></returns>
//        public static object QueryTo(this object model,Type resultType)
//        {
//            model.MustNotBeNull();
//            var handlerType = typeof(IHandleQuery<,>).MakeGenericType(model.GetType(), resultType);
//            var handler = (dynamic)Resolver(handlerType);
//            if (handler == null) throw new InvalidOperationException("There's no handler implementing 'IHandleQuery<{0},{1}>' registered with the DI Container".ToFormat(model.GetType().Name, resultType.Name));
//            return handler.Handle((dynamic)model);
//        }
//#if !Net4
//        /// <summary>
//        /// Invokes the async query handler which will take the specified argument as the input model
//        /// </summary>
//        /// <typeparam name="TOut">View model</typeparam>
//        /// <param name="model">Input model</param>
//        /// <returns></returns>
//        public static async Task<TOut> QueryAsyncTo<TOut>(this object model,CancellationToken cancel)  where TOut : class
//        {
//            var r= await model.QueryAsyncTo(typeof (TOut),cancel);
//            return r as TOut;
//        }

//        /// <summary>
//        /// Invokes the async query handler which will take the specified argument as the input model
//        /// </summary>
//        /// <param name="model">Input model</param>
//        /// <param name="resultType"></param>
//        /// <param name="cancel"></param>
//        /// <returns></returns>
//        public static async Task<object> QueryAsyncTo(this object model, Type resultType, CancellationToken cancel)
//        {
//            model.MustNotBeNull();
//            var handlerType = typeof(IHandleQueryAsync<,>).MakeGenericType(model.GetType(), resultType);
//            var handler= (dynamic)Resolver(handlerType);
//            if (handler==null) throw new InvalidOperationException("There's no handler implementing 'IHandleQueryAsync<{0},{1}>' registered with the DI Container".ToFormat(model.GetType().Name, resultType.Name));
//            var t=await handler.HandleAsync((dynamic)model,cancel);
//            return t;
//        }
//#endif
//        /// <summary>
//        /// Invokes a request (command) taking the specified argument as the input model and returns its result
//        /// </summary>
//        /// <typeparam name="TResult">Output model</typeparam>
//        /// <param name="input">Input model</param>
//        /// <returns></returns>
//        public static TResult ExecuteAndReturn<TResult>(this object input) where TResult : class
//        {
//            return input.ExecuteAndReturn(typeof (TResult)) as TResult;
            
//        }
        
//        /// <summary>
//        /// Invokes a request (command) taking the specified argument as the input model and returns its result
//        /// </summary>
        
//        /// <param name="input">Input model</param>
//        /// <returns></returns>
//        public static object ExecuteAndReturn(this object input,Type resultType)
//        {
//            input.MustNotBeNull();
//            var handlerType = typeof(IHandleCommand<,>).MakeGenericType(input.GetType(), resultType);
//            var handler = (dynamic)Resolver(handlerType);
//            if (handler == null) throw new InvalidOperationException("There's no handler implementing 'IHandleCommand<{0},{1}>' registered with the DI Container".ToFormat(input.GetType().Name, resultType.Name));
//            return handler.Handle((dynamic)input);
//        }


       
//        /// <summary>
//        /// Invokes a request (command) taking the specified argument as the input model and returns its result
//        /// </summary>
//        /// <typeparam name="TResult">Output model</typeparam>
//        /// <param name="input">Input model</param>
//        /// <returns></returns>
//        public static async Task<TResult> ExecuteAsyncAndReturn<TResult>(this object input,CancellationToken cancel) where TResult : class
//        {
//            var r = await input.ExecuteAsyncAndReturn(typeof (TResult),cancel);
//            return r as TResult;
            
//        }

//        /// <summary>
//        /// Invokes a request (command) taking the specified argument as the input model and returns its result
//        /// </summary>
//        /// <param name="input">Input model</param>
//        /// <param name="resultType"></param>
//        /// <param name="cancel"></param>
//        /// <returns></returns>
//        public static async Task<object> ExecuteAsyncAndReturn(this object input, Type resultType, CancellationToken cancel)
//        {
//            input.MustNotBeNull();
//            var handlerType = typeof(IHandleCommandAsync<,>).MakeGenericType(input.GetType(), resultType);
//            var handler = (dynamic)Resolver(handlerType);
//            if (handler == null) throw new InvalidOperationException("There's no handler implementing 'IHandleCommandAsync<{0},{1}>' registered with the DI Container".ToFormat(input.GetType().Name, resultType.Name));
//            var t=await handler.HandleAsync((dynamic)input,cancel);
//            return t;
//        }


        public static IHandlerResultFrom<TInput> For<TInput>(this IMediateMessages med, TInput input) =>new Builder<TInput>(med,input);
        

        class Builder<T>:IHandlerResultFrom<T>
        {
            private readonly IMediateMessages _med;
            private readonly T _data;

            public Builder(IMediateMessages med,T data)
            {
                _med = med;
                _data = data;
            }

            public TResult Request<TResult>() where TResult : class => _med.Request(_data,typeof(TResult)) as TResult;
            

            public async Task<TResult> RequestAsync<TResult>(CancellationToken token) where TResult : class
            {
                var result=await _med.RequestAsync(_data, typeof (TResult), token).ConfigureAwait(false);
                return result as TResult;
            }
        }
    }
    
    /// <summary>
    ///  Should be singleton
    /// </summary>
    public class MessagingMediator:IMediateMessages
    {
        private readonly Func<Type, object> _resolve;

        public MessagingMediator(Func<Type, object> resolve)
        {
            resolve.MustNotBeNull();
            _resolve = resolve;
        }

      
        public object Request(object input, Type result)
        {
            var handlerType = typeof(IHandleRequest<,>).MakeGenericType(input.GetType(), result);
            var handler = (dynamic)_resolve(handlerType);
            if (handler == null) throw new InvalidOperationException("There's no handler implementing 'IHandleRequest<{0},{1}>' registered with the DI Container".ToFormat(input.GetType().Name, result.Name));
            return handler.HandleAsync((dynamic)input);
        }

        public async Task<object> RequestAsync(object input, Type result, CancellationToken token)
        {
            var handlerType = typeof(IHandleRequestAsync<,>).MakeGenericType(input.GetType(), result);
            var handler = (dynamic)_resolve(handlerType);
            if (handler == null) throw new InvalidOperationException("There's no handler implementing 'IHandleRequestAsync<{0},{1}>' registered with the DI Container".ToFormat(input.GetType().Name, result.Name));
            var rez=await handler.HandleAsync((dynamic)input,token).ConfigureAwait(false);
            return rez;
        }
    }

    public interface IMediateMessages
    {
        object Request(object input, Type result);
        Task<object> RequestAsync(object input, Type result, CancellationToken token);
        
    }

    public interface IHandlerResultFrom<T>
    {
        TResult Request<TResult>() where TResult: class;
        Task<TResult> RequestAsync<TResult>(CancellationToken token) where TResult: class;

    }

}