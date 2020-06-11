using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Test
{
    /// <summary>
    /// This class allows us to use dependency injection even in unit tests
    /// </summary>
    public static class FakeStartup
    {
        public static IServiceCollection GetDefaultServices()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var services = new ServiceCollection();
            new Startup(config)
                .AddBusinessDependencies(services);
            return services;
        }
    }
}
