using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeverBadWeather.DomainModel;

namespace NeverBadWeather.UserInterfaceApi.Model
{
    public class ClothingRecommendationRequest
    {
        public byte HourFrom { get; set; }
        public byte HourTo { get; set; }
        public float Latitude{ get; set; }
        public float Longitude { get; set; }

        public DomainModel.ClothingRecommendationRequest ToDomainModel()
        {
            var now = DateTime.Now;
            var timeFrom = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            if (HourFrom < timeFrom.Hour) timeFrom = timeFrom.AddDays(1);
            var timeTo = timeFrom.AddHours(HourTo - timeFrom.Hour);
            if (timeTo < timeFrom) timeTo = timeTo.AddDays(1);
            return new DomainModel.ClothingRecommendationRequest(
                new TimePeriod(timeFrom, timeTo), new Location(Latitude, Longitude) );
        }
    }
}
