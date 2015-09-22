using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CavemanTools.Infrastructure
{
    public class CommandResultMediator : ICommandResultMediator
    {

        public CommandResultMediator()
        {
            ResultCheckPeriod = 100;
        }
        public void AddResult<T>(Guid cmdId, T result) where T :class 
        {
            Listener l = null;
            if (_items.TryRemove(cmdId,out l))
            {
                l.Result = result;                
            }
            
        }

        /// <summary>
        /// How often to check if a result has arrived, in ms.
        /// Default is 100 ms
        /// </summary>
        public int ResultCheckPeriod { get; set; }

        void Remove(Guid cmdId)
        {
             Listener l = null;
            _items.TryRemove(cmdId, out l);
        }

        public int ActiveListeners
        {
            get { return _items.Count; }
        }

        ConcurrentDictionary<Guid,Listener> _items=new ConcurrentDictionary<Guid, Listener>();
      
        public IResultListener GetListener(Guid cmdId,TimeSpan? timeout=null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(5);
            var listener=new Listener(timeout.Value,cmdId,this);
            listener.UpdatePeriod = ResultCheckPeriod;
            _items.TryAdd(cmdId, listener);
            return listener;
        }


        class Listener:IResultListener
        {
            private readonly Guid _cmdId;
            private readonly CommandResultMediator _parent;

            public Listener(TimeSpan timeout, Guid cmdId, CommandResultMediator parent)
            {
                _cmdId = cmdId;
                _parent = parent;
                TimeoutTime = DateTime.Now.Add(timeout);
            }

            private DateTime TimeoutTime { get; set; }

            public object Result { get; set; }

            public int UpdatePeriod=100;

            public async Task<T> GetResult<T>(CancellationToken cancel=default(CancellationToken)) where T : class
            {
                while(Result==null)
                {
                    if (DateTime.Now >= TimeoutTime)
                    {
                        _parent.Remove(_cmdId);
                        throw new TimeoutException();
                    }
                    if (cancel.IsCancellationRequested)
                    {
                        _parent.Remove(_cmdId);
                        throw new OperationCanceledException();                        
                    }
                    await Task.Delay(UpdatePeriod,cancel).ConfigureAwait(false);
                }
                return Result as T;
            }
        }
    }
    
}