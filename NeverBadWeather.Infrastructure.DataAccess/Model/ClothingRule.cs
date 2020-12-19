using System;
using System.Collections.Generic;
using System.Text;

namespace NeverBadWeather.Infrastructure.DataAccess.Model
{
    public class ClothingRule
    {
        public Guid Id { get; set; }
        public bool? IsRaining { get; set; }
        public int FromTemperature { get; set; }
        public int ToTemperature { get; set; }
        public string Clothes { get; set; }
    }
}
