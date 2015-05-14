using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC5
{
    class Stat
    {
        private long minTime = 0;
        private long maxTime = 0;
        private long countOfRuns = 0;
        private long totalTime = 0;

        public void ClearStat()
        {
            minTime = 0;
            maxTime = 0;
            countOfRuns = 0;
            totalTime = 0;
        }

        public void AddRun(long time)
        {
            totalTime += time;
            countOfRuns++;
            if (minTime == 0 || minTime > time)
                minTime = time;
            if (maxTime < time)
                maxTime = time;
        }

        public decimal average{
            get { 
                if (countOfRuns == 0)
                    return 0;
                return (totalTime/countOfRuns); 
            }
            set {}
        }

        public long shortestRun
        {
            get { return minTime; }
            set { }
        }

        public long longestRun 
        { 
            get { return maxTime; } 
            set { } 
        }

        public long numberOfRuns
        {
            get { return countOfRuns; }
            set { }
        }

        public long totalTimeProcessing
        {
            get { return totalTime; }
            set { }
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\n", average, minTime, maxTime,(minTime * 1000 /System.Diagnostics.Stopwatch.Frequency));
        }

        public string ToStringHeader()
        {
            return "Ave\tMin\tMax\tFasted in ms\n";
        }
    }
}
