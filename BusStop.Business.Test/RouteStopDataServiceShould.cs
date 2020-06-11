using BusStop.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BusStop.Business.Test
{
    public class RouteStopDataServiceShould : TestingBase<IRouteStopDataService>
    {
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(100)]
        public void ReturnListOfRouteStopsForABusStop(int numberOfTimes)
        {
            // arrange
            // todo: this would break after adding a database layer, but it could be fixed by adding a mock database to the default test service collection
            var sut = this.GetSut();
            // act
            var actual = sut.GetByBusStop(1, numberOfTimes);
            // assert
            Assert.True(actual.All(x => x.ArrivalTimes.Count == numberOfTimes));
            Assert.True(actual.All(x => x.BusStopId == 1));
        }
    }
}
