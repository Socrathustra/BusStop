using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Models
{
    public class RouteDto
    {
        public string Name { get; set; }
        public IList<BusStopDto> BusStops { get; set; }
    }
}
