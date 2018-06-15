using System;

namespace CavemanTools
{
    public struct TimeInterval
    {
        public DateTime StartsOn;

        public DateTime StartsOn1 => StartsOn;

        public DateTime EndsOn1 => EndsOn;

        public DateTime EndsOn;

        public TimeInterval(DateTime startsOn, DateTime endsOn)
        {
            StartsOn = startsOn;
            EndsOn = endsOn;
        }
    }
}