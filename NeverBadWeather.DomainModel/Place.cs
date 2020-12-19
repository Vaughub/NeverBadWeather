using System;
using System.Collections.Generic;
using System.Text;

namespace NeverBadWeather.DomainModel
{
    public class Place
    {
        public string Country { get; }
        public string Region { get; }
        public string City { get; }
        public string Name { get; }
        public Location Location { get; }

        public Place(string country, string region, string city, string name, Location location)
        {
            Country = country;
            Region = region;
            City = city;
            Name = name;
            Location = location;
        }

        public Place()
        {
        }

        public override string ToString()
        {
            const string separator = ", ";
            return Name + separator + City + separator + Region + separator + Country;
        }
    }
}
