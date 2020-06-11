using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Core
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteStop> RouteStops { get; set; }
    }
}
