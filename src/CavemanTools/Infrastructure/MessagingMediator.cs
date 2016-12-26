using System;
using System.Threading;
using System.Threading.Tasks;

namespace CavemanTools.Infrastructure
{
    public static class MessagingMediatorStatic
    {
        /// <summary>
        /// Specify the input of a request
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="med"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IHandlerResultFrom<TInput> For<TInput>(this IMediateMessages med, TInput input) =>new Builder<TInput>(med,input);

        /// <summary>
        /// Used as a very light in-memory command bus returning a <see cref="CommandResult"/>. 
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="med"></param>
        /// <param name="cmd">command</param>
        /// <returns></returns>
        public static CommandResult SendCommand<TCommand>(this IMediateMessages med,TCommand cmd) => med.For(cmd).Request<CommandResult>();

        /// <summary>
        /// Used as a very light in-memory command bus returning a <see cref="CommandResult"/>. 
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="med"></param>
        /// <param name="cmd">command</param>
        /// <param name="cancel">token</param>
        /// <returns></returns>
        public static Task<CommandResult> SendCommandAsync<TCommand>(this IMediateMessages med,TCommand cmd,CancellationToken cancel) => med.For(cmd).RequestAsync<CommandResult>(cancel);


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
            return handler.Handle((dynamic)input);
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

    public interface IHandlerResultFrom<TInput>
    {
        TResult Request<TResult>() where TResult: class;
        Task<TResult> RequestAsync<TResult>(CancellationToken token) where TResult: class;

    }


}