using System;
using System.Collections.Generic;

namespace BusStop.Core
{
    public class BusStopData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteStop> RouteStops { get; set; }
    }
}
