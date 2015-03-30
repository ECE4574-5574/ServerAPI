using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeAutomationServer.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        public EventsController()
        {

        }

        [Route("weather")]
        public object UpdateWeather()
        {
            return Ok();
        }

        [Route("devicestate")]
        public object UpdateDeviceState()
        {
            return Ok();
        }

        [Route("command")]
        public object Command()
        {
            return Ok();
        }

        public object LocalTime()
        {
            return Ok();
        }

    }
}
