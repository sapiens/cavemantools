using System;
using System.Threading;

namespace CavemanTools
{
    public interface ITimer:IDisposable
    {
        /// <summary>
        /// You must set the handler and the interval before calling this
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="delay"></param>
        void Start(object initialState=null,TimeSpan? delay=null);
        void Stop();
        bool IsRunning { get; }
        void SetHandler(Action<object> action);
        TimeSpan Interval { get; set; }
    }

    public class DefaultTimer : ITimer
    {
        private Timer _timer;
        private TimerCallback _action;
        private TimeSpan _interval = -1.ToMiliseconds();


        public void Dispose()
        {
           Stop();
        }

        public void Start(object initialState = null, TimeSpan? delay = null)
        {
            if (IsRunning) throw new InvalidOperationException("Timer already started");
            _timer =new Timer(_action,initialState,delay??0.ToMiliseconds(),Interval);
            IsRunning = true;
        }

        public void Stop()
        {
            _timer?.Dispose();
            _timer = null;
            IsRunning = false;
        }

        public TimeSpan Interval
        {
            get { return _interval; }
            set
            {
                if (IsRunning) throw new InvalidOperationException("You have to stop the timer first");
                _interval = value;
            }
        }

        public void SetHandler(Action<object> action)
        {
            if (IsRunning) throw new InvalidOperationException("You have to Stop the timer, then change the handler, then start it again");
            action.MustNotBeNull();
            _action = s => action(s);            
                       
        }

        public bool IsRunning { get; private set; }
    }
}