using System;

namespace CavemanTools.Testing
{
    public class BenchmarkResult
    {
        public TimeSpan Min { get; private set; }
        public TimeSpan Max { get; private set; }
        public TimeSpan Average { get; private set; }
        public TimeSpan Total { get; private set; }

        public void Reset()
        {
            Max = Average = Total = TimeSpan.Zero;
            Min = TimeSpan.MaxValue;
            i = 0;
            NotSupported = false;
        }

        public void SetNoSupported(string message)
        {
            NotSupported = true;
            _nsReason = message;
        }

        public bool NotSupported { get; set; }


        public string Name { get; set; }

        private int i = 0;
        private string _nsReason;

        public BenchmarkResult()
        {
            Min = TimeSpan.MaxValue;
        }

        public void AddIteration(TimeSpan ts)
        {
            if (Min>ts)
            {
                Min = ts;
            }

            if (Max<=ts)
            {
                Max = ts;
            }
            i++;
            Total = Total.Add(ts);
            Average = Total.Multiply(1/(double)i);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool allDetails=true)
        {
            if (NotSupported)
            {
                return string.Format("{0} doesn't support the action. {1}", Name,_nsReason);
            }
            var s1 = string.Format("{0} - {1} iterations executed in {2} ms", Name,i, Total.TotalMilliseconds);
            if (!allDetails) return s1;
            return s1 + string.Format("\n\t 1 iteration took around {0} ms (min: {1} ms, max: {2} ms)",Average.TotalMilliseconds,Min.TotalMilliseconds,Max.TotalMilliseconds);
        }
    }
}