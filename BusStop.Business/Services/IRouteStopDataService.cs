using BusStop.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Services
{
    public interface IRouteStopDataService
    {
        IList<RouteStop> GetByBusStop(int busStopId, int numberOfTimes);
    }
}
