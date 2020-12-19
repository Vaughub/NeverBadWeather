using System;
using System.Collections.Generic;
using System.Text;

namespace NeverBadWeather.DomainModel
{
    public class TimePeriod
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public TimePeriod(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
