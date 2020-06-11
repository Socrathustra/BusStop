using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusStop.Business.BusinessLogic;
using BusStop.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusStop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusStopController : ControllerBase
    {
        private IBusStopTimeBusinessLogic BusStopTimeBusinessLogic { get; set; }

        public BusStopController(IBusStopTimeBusinessLogic busStopTimeBusinessLogic)
        {
            this.BusStopTimeBusinessLogic = busStopTimeBusinessLogic;
        }

        [HttpGet("{busStopId}")]
        public IActionResult GetNextArrivals(int busStopId)
        {
            // hard-coding the number of arrival times for now
            var result = this.BusStopTimeBusinessLogic.GetNextStopTimes(busStopId, 2);

            if (result.IsSuccessful == true)
            {
                return this.Ok(result.Result);
            }

            switch (result.ResultCode)
            {
                case BusStopTimeResultCode.InvalidStop:
                    return this.NotFound(result.ResultMessage);
                case BusStopTimeResultCode.UnknownError:
                default:
                    return this.StatusCode((int)HttpStatusCode.InternalServerError, result.ResultMessage);
            }
        }
    }
}
