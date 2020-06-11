using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Core
{
    public class RouteStop
    {
        public int BusStopId { get; set; }
        public BusStopData BusStop { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public IList<DateTime> ArrivalTimes { get; set; } = new List<DateTime>();
    }
}
