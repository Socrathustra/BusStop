using System.Collections.Generic;
using BusStop.Business.Helpers;
using BusStop.Business.Models;

namespace BusStop.Business.BusinessLogic
{
    public interface IBusStopTimeBusinessLogic
    {
        IBusinessResult<IList<RouteDto>, BusStopTimeResultCode> GetNextStopTimes(int busStopId, int numberOfTimes);
    }
}