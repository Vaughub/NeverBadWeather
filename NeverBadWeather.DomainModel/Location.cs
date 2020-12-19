using System;
using System.Globalization;

namespace NeverBadWeather.DomainModel
{
    public class Location
    {
	    private readonly float _latitude;
	    private readonly float _longitude;

	    public float Latitude => (float) Math.Round(_latitude, 4);
	    
	    public float Longitude => (float) Math.Round(_longitude, 4);

	    public Location(string latitude, string longitude)
        : this(ToFloat(latitude), ToFloat(longitude))
        {}

        public Location(float latitude, float longitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }

        private static float ToFloat(string longitude)
        {
            return Convert.ToSingle(longitude, CultureInfo.InvariantCulture);
        }


        public double GetDistanceFrom(Location location)
        {
            var deltaLat = location.Latitude - Latitude;
            var deltaLon = location.Longitude - Longitude;
            return Math.Sqrt(deltaLon * deltaLon + deltaLat * deltaLat);
        }

        public Location CreateWithDelta(float deltaLat, float deltaLon)
        {
            return new Location(Latitude + deltaLat, Longitude + deltaLon);
        }

        public bool IsWithin(Location min, Location max)
        {
            return Latitude >= min.Latitude && Latitude <= max.Latitude 
                && Longitude >= min.Longitude && Longitude <= max.Longitude;
        }
    }
}
