using HomeAutomationServer.Services;
using Newtonsoft.Json.Linq;
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
        private EventsRepository eventsRepository;

        public EventsController()
        {
            this.eventsRepository = new EventsRepository();
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

        [Route("locationchange/{userid}")]
        public object UpdatePosition(int userid, [FromBody] JToken model)
        {

            eventsRepository.OnUpdatePosition("stub");
            return Ok(model);
        }

        [Route("locatltime")]
        public object PostLocalTime([FromBody] JObject model)
        {
            return Ok();
        }

    }
}
