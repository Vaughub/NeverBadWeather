using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeverBadWeather.DomainServices;

namespace NeverBadWeather.UserInterfaceApi.Model
{
    public class AppConfiguration : IAppConfiguration
    {
        public string ConnectionString { get; }

        public AppConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
