using System;
using System.Collections.Generic;
using System.Text;

namespace NeverBadWeather.DomainServices
{
    public interface IAppConfiguration
    {
        string ConnectionString { get; }
    }
}
