using BusStop.Business.BusinessLogic;
using BusStop.Business.Helpers;
using BusStop.Business.Services;
using BusStop.Core;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusStop.Business.Test
{
    public class BusStopTimeBusinessLogicShould : TestingBase<IBusStopTimeBusinessLogic>
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 100)]
        [InlineData(5, 3)]
        public void ReturnAsManyTimesAsRequested(int busStopId, int numberOfTimes)
        {
            // arrange
            var sut = this.GetSut();
            // act
            var actual = sut.GetNextStopTimes(busStopId, numberOfTimes);
            // assert
            foreach (var route in actual.Result.Routes)
            {
                // should only have a single bus stop
                Assert.Equal(numberOfTimes, route.ArrivalTimes.Count);
            }
        }

        [Fact]
        public void IndicateAnInvalidStopWhenThereAreNoResults()
        {
            // arrange
            var services = FakeStartup.GetDefaultServices();
            var subDataService = Substitute.For<IRouteStopDataService>();
            subDataService.GetByBusStop(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<RouteStop>());
            services.AddScoped<IRouteStopDataService>(x => subDataService);
            var sut = this.GetSut(services);
            // act
            var actual = sut.GetNextStopTimes(1, 1);
            // assert
            Assert.Equal(BusStopTimeResultCode.InvalidStop, actual.ResultCode);
        }

        [Fact]
        public void IndicateAnUnknownErrorDuringUnhandledExceptions()
        {
            // arrange
            var services = FakeStartup.GetDefaultServices();
            var subDataService = Substitute.For<IRouteStopDataService>();
            subDataService.GetByBusStop(Arg.Any<int>(), Arg.Any<int>()).Throws(new Exception("test"));
            services.AddScoped<IRouteStopDataService>(x => subDataService);
            var sut = this.GetSut(services);
            // act
            var actual = sut.GetNextStopTimes(1, 1);
            // assert
            Assert.Equal(BusStopTimeResultCode.UnknownError, actual.ResultCode);
        }
    }
}
