using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeverBadWeather.UserInterfaceApi.Model
{
    public class ClothingRule 
    {
        public string Id { get; set; }
        public int? FromTemperature { get; set; }
        public int? ToTemperature { get; set; }
        public bool? IsRaining { get; set; }
        public string Clothes { get; set; }

        public DomainModel.ClothingRule GetAsDomainModel()
        {
            var guid = Id == null ? (Guid?)null : new Guid(Id);
            return new DomainModel.ClothingRule(
                FromTemperature ?? 0,
                ToTemperature ?? 0,
                IsRaining,
                Clothes,
                guid
            );
        }

        public static ClothingRule GetAsViewModel(DomainModel.ClothingRule rule)
        {
            return new ClothingRule
            {
                Id = rule.Id.ToString(),
                IsRaining = rule.IsRaining,
                Clothes = rule.Clothes,
                ToTemperature = rule.ToTemperature,
                FromTemperature = rule.FromTemperature
            };
        }
    }
}
