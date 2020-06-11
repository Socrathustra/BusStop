using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Models
{
    public class BusStopDto
    {
        public string Name { get; set; }
        public IList<RouteDto> Routes { get; set; }
    }
}
