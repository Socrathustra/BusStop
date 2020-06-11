using System;
using System.Collections.Generic;
using System.Text;
using BusStop.Business.Helpers;
using BusStop.Core;

namespace BusStop.Business.Services
{
    public class HardCodedRouteStopDataService : IRouteStopDataService
    {
        private IDateTimeNow DateTimeNow { get; set; }

        public HardCodedRouteStopDataService(IDateTimeNow dateTimeNow)
        {
            this.DateTimeNow = dateTimeNow;
        }

        public IList<RouteStop> GetByBusStop(int busStopId, int numberOfTimes)
        {
            if (busStopId > 0 && busStopId <= 10)
            {
                var stop = new BusStopData
                {
                    Id = busStopId,
                    Name = $"Bus Stop {busStopId}",
                    RouteStops = new List<RouteStop>()
                };

                for (var routeId = 1; routeId <= 3; routeId++)
                {
                    var routeStop = new RouteStop
                    {
                        BusStopId = busStopId,
                        BusStop = stop,
                        RouteId = routeId,
                        Route = new Route
                        {
                            Id = routeId,
                            Name = $"Route {routeId}",
                            RouteStops = stop.RouteStops
                        },
                        ArrivalTimes = new List<DateTime>()
                    };

                    // Each route starts running 2 minutes after the previous one
                    var timeOffset = (routeStop.RouteId - 1) * 2;
                    // Each stop is 2 minutes away from the previous one
                    timeOffset += (routeStop.BusStopId - 1) * 2;
                    // Each stop is serviced every 15 minutes per route
                    timeOffset = timeOffset % 15;
                    var nearestMultiple = this.FindNearestMultipleOfFifteen();
                    var nextStop = this.FindNearestTimeWithOffset(nearestMultiple, timeOffset);

                    for (var num = 0; num < numberOfTimes; num++)
                    {
                        routeStop.ArrivalTimes.Add(nextStop);
                        nextStop = nextStop.AddMinutes(15);
                    }

                    stop.RouteStops.Add(routeStop);
                }

                return stop.RouteStops;
            }
            else
            {
                return new List<RouteStop>();
            }
        }


        private DateTime FindNearestMultipleOfFifteen()
        {
            var now = this.DateTimeNow.Now;
            var minutes = now.Minute;
            var minutesToNextMultiple = 15 - (minutes % 15);
            var next = now.AddMinutes(minutesToNextMultiple);
            // trim seconds/milliseconds
            next = next.AddSeconds(-next.Second);
            next = next.AddMilliseconds(-next.Millisecond);
            return next;
        }

        private DateTime FindNearestTimeWithOffset(DateTime basis, int offsetInMinutes)
        {
            // if true, this means the next stop actually occurs prior to the basis
            if (this.DateTimeNow.Now.Minute < (basis.Minute - 15) + offsetInMinutes)
            {
                return basis.AddMinutes(offsetInMinutes - 15);
            }
            else
            {
                return basis.AddMinutes(offsetInMinutes);
            }
        }
    }
}
