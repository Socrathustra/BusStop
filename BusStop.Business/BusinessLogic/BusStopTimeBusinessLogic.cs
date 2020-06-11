using BusStop.Business.Helpers;
using BusStop.Business.Models;
using BusStop.Business.Services;
using BusStop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusStop.Business.BusinessLogic
{
    public enum BusStopTimeResultCode
    {
        Okay = 1,
        InvalidStop = 2,
        UnknownError = 3
    }

    public class BusStopTimeBusinessLogic : BusinessLogicBase<BusStopTimeResultCode>, IBusStopTimeBusinessLogic
    {
        protected override IDictionary<BusStopTimeResultCode, string> ResultMessages { get; set; } = new Dictionary<BusStopTimeResultCode, string> { { BusStopTimeResultCode.Okay, string.Empty }, { BusStopTimeResultCode.InvalidStop, "Stop was not valid" }, { BusStopTimeResultCode.UnknownError, "An unknown error has occurred." } };

        private IRouteStopDataService RouteStopDataService { get; set; }

        public BusStopTimeBusinessLogic(IRouteStopDataService routeStopDataService, IDateTimeNow dateTimeNow)
        {
            this.RouteStopDataService = routeStopDataService;
        }

        public IBusinessResult<BusStopDto, BusStopTimeResultCode> GetNextStopTimes(int busStopId, int numberOfTimes)
        {
            try
            {
                var routeStops = this.RouteStopDataService.GetByBusStop(busStopId, numberOfTimes);

                if (routeStops == null || routeStops.Count == 0)
                {
                    return this.GetErrorResult<BusStopDto>(BusStopTimeResultCode.InvalidStop);
                }

                var dto = new BusStopDto
                {
                    Name = routeStops.First().BusStop.Name,
                    Routes = routeStops.Select(x => new RouteDto
                    {
                        Name = x.Route.Name,
                        ArrivalTimes = x.ArrivalTimes
                    }).ToList()
                };

                return this.GetBusinessResult<BusStopDto>(dto, BusStopTimeResultCode.Okay);
            }
            catch (Exception)
            {
                return this.GetErrorResult<BusStopDto>(BusStopTimeResultCode.UnknownError);
            }
        }
    }
}
