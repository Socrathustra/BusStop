using BusStop.Business.BusinessLogic;
using BusStop.Business.Helpers;
using BusStop.Business.Models;
using BusStop.Business.Test;
using BusStop.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace BusStop.Test
{
    public class BusStopControllerShould : TestingBase<BusStopController>
    {
        [Fact]
        public void ReturnOkWhenBusinessResultIsSuccessful()
        {
            // arrange
            var services = FakeStartup.GetDefaultServices();
            services.AddScoped<BusStopController>();
            var businessLogicSub = Substitute.For<IBusStopTimeBusinessLogic>();
            businessLogicSub.GetNextStopTimes(Arg.Any<int>(), Arg.Any<int>())
                .Returns(new BusinessResult<BusStopDto, BusStopTimeResultCode>(new BusStopDto(), BusStopTimeResultCode.Okay, string.Empty, true));
            services.AddScoped<IBusStopTimeBusinessLogic>(x => businessLogicSub);
            var sut = this.GetSut(services);
            // act
            var actual = sut.GetNextArrivals(1);
            // assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BusStopDto>((actual as OkObjectResult).Value);
            Assert.NotNull((actual as OkObjectResult).Value);
            Assert.Equal((int)HttpStatusCode.OK, (actual as OkObjectResult).StatusCode);
        }

        [Fact]
        public void ReturnNotFoundWhenResultCodeIsInvalidBusStop()
        {
            // arrange
            var services = FakeStartup.GetDefaultServices();
            services.AddScoped<BusStopController>();
            var businessLogicSub = Substitute.For<IBusStopTimeBusinessLogic>();
            businessLogicSub.GetNextStopTimes(Arg.Any<int>(), Arg.Any<int>())
                .Returns(new BusinessResult<BusStopDto, BusStopTimeResultCode>(new BusStopDto(), BusStopTimeResultCode.InvalidStop, string.Empty, false));
            services.AddScoped<IBusStopTimeBusinessLogic>(x => businessLogicSub);
            var sut = this.GetSut(services);
            // act
            var actual = sut.GetNextArrivals(1);
            // assert
            Assert.IsType<NotFoundObjectResult>(actual);
            Assert.Equal((int)HttpStatusCode.NotFound, (actual as NotFoundObjectResult).StatusCode);
        }

        [Fact]
        public void ReturnServerErrorWhenResultCodeIsUnknownError()
        {
            // arrange
            var services = FakeStartup.GetDefaultServices();
            services.AddScoped<BusStopController>();
            var businessLogicSub = Substitute.For<IBusStopTimeBusinessLogic>();
            businessLogicSub.GetNextStopTimes(Arg.Any<int>(), Arg.Any<int>())
                .Returns(new BusinessResult<BusStopDto, BusStopTimeResultCode>(new BusStopDto(), BusStopTimeResultCode.UnknownError, string.Empty, false));
            services.AddScoped<IBusStopTimeBusinessLogic>(x => businessLogicSub);
            var sut = this.GetSut(services);
            // act
            var actual = sut.GetNextArrivals(1);
            // assert
            Assert.IsType<ObjectResult>(actual);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (actual as ObjectResult).StatusCode);
        }
    }
}
