using System;

namespace CavemanTools.Testing
{
    public class StubTimer : ITimer
    {
        private Action<object> _action;
        private object _state;

        public void Dispose()
        {
            
        }

        public void Start(object initialState = null, TimeSpan? delay = null)
        {
            IsRunning = true;
            _state = initialState;
        }

        public void InvokeHandler(object state = null)
        {
            _action(state ?? _state);
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }
        public void SetHandler(Action<object> action)
        {
            _action = action;
        }

        public TimeSpan Interval { get; set; }
    }
}