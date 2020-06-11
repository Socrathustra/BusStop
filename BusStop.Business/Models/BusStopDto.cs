using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Models
{
    // note: this isn't properly RESTful since there are no URLs to get resources, but since we only have one endpoint, that seemed like overkill
    public class BusStopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteDto> Routes { get; set; }
    }
}
