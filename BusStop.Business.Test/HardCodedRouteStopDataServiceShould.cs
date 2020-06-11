using BusStop.Business.Helpers;
using BusStop.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BusStop.Business.Test
{
    // Note: these tests would probably be moved into a scheduler which creates stop-times within the database in a real application, but it's not worth trying to figure out how to do that for a fake application
    public class HardCodedRouteStopDataServiceShould : TestingBase<IRouteStopDataService>
    {
        [Fact]
        public void ReturnTheCorrectTimes()
        {
            // arrange
            var services = FakeStartup.GetDefaultServices();
            var nowSubstitute = Substitute.For<IDateTimeNow>();
            nowSubstitute.Now.Returns(x => new DateTime(2020, 6, 10, 8, 12, 0));
            services.AddScoped<IDateTimeNow>(x => nowSubstitute);
            var sut = this.GetSut(services);
            // act
            const int numberOfArrivalTimes = 2;
            const int busStopId = 8;
            var actual = sut.GetByBusStop(busStopId, numberOfArrivalTimes);
            // assert
            Assert.Equal(3, actual.Count);
            // make sure the calculations are correct
            foreach (var route in actual)
            {
                var arrivalTimes = route.ArrivalTimes;
                Assert.Equal(numberOfArrivalTimes, arrivalTimes.Count);
                var first = arrivalTimes[0];
                var second = arrivalTimes[1];
                switch (route.RouteId)
                {
                    case 1:
                        Assert.True(first.Day == 10 && first.Month == 6 && first.Year == 2020 && first.Minute == 14);
                        Assert.True(second.Day == 10 && second.Month == 6 && second.Year == 2020 && second.Minute == 29);
                        break;
                    case 2:
                        Assert.True(first.Day == 10 && first.Month == 6 && first.Year == 2020 && first.Minute == 16);
                        Assert.True(second.Day == 10 && second.Month == 6 && second.Year == 2020 && second.Minute == 31);
                        break;
                    case 3:
                        Assert.True(first.Day == 10 && first.Month == 6 && first.Year == 2020 && first.Minute == 18);
                        Assert.True(second.Day == 10 && second.Month == 6 && second.Year == 2020 && second.Minute == 33);
                        break;
                }
            }
        }
    }
}
