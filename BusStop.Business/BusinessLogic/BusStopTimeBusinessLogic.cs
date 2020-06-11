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

        public IBusinessResult<IList<RouteDto>, BusStopTimeResultCode> GetNextStopTimes(int busStopId, int numberOfTimes)
        {
            try
            {
                var routeStops = this.RouteStopDataService.GetByBusStop(busStopId, numberOfTimes);

                if (routeStops == null || routeStops.Count == 0)
                {
                    return this.GetErrorResult<IList<RouteDto>>(BusStopTimeResultCode.InvalidStop);
                }

                var dtos = routeStops.Select(x => new RouteDto
                {
                    Name = x.Route.Name,
                    BusStops = new List<BusStopDto>
                    {
                        new BusStopDto
                        {
                            ArrivalTimes = x.ArrivalTimes,
                            Name = x.BusStop.Name
                        }
                    }
                }).ToList();

                return this.GetBusinessResult<IList<RouteDto>>(dtos, BusStopTimeResultCode.Okay);
            }
            catch (Exception)
            {
                return this.GetErrorResult<IList<RouteDto>>(BusStopTimeResultCode.UnknownError);
            }
        }
    }
}
