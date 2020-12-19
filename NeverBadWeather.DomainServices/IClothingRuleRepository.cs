using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeverBadWeather.DomainModel;

namespace NeverBadWeather.DomainServices
{
    public interface IClothingRuleRepository
    {
        Task<IEnumerable<ClothingRule>> GetRulesByUser(Guid? userId);
        Task<int> Create(ClothingRule rule);
        Task<int> Update(ClothingRule rule);
        Task<int> Delete(ClothingRule rule);
    }
}
