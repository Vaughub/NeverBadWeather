using System;
using System.Collections.Generic;
using System.Text;

namespace NeverBadWeather.DomainModel
{
    public class ClothingRecommendationRequest
    {
        public TimePeriod Time { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }

        public ClothingRecommendationRequest(TimePeriod time, Location location)
        {
            Time = time;
            Location = location;
        }
    }
}
