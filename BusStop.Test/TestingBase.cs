using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Test
{
    public abstract class TestingBase<T> where T : class
    {

        // Use if not needing to customize dependencies
        protected T GetSut()
        {
            var services = FakeStartup.GetDefaultServices();
            return this.GetSut(services);
        }

        protected T GetSut(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<T>();
        }
    }
}
