using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder
{
    internal class Duration
    {
        private int seconds = 0;

        public void CountSecond() { seconds++; }

        public override string ToString()
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);

            return string.Format("{0:00}:{1:00}:{2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
