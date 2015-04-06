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
       /* //private EventsRepository eventsRepository;

        public EventsController()
        {
            //this.eventsRepository = new EventsRepository();
        }

        // POST api/events/weather
        /// <summary>
        /// Updates the weather.
        /// </summary>
        /// <returns>Reutrns status OK.</returns>
        [Route("weather")]
        public object UpdateWeather()
        {
            return Ok();
        }

        // POST api/events/devicestate
        /// <summary>
        /// Updates devices state.
        /// </summary>
        /// <returns>Returns status OK.</returns>
        [Route("devicestate")]
        public object UpdateDeviceState()
        {
            return Ok();
        }

        // POST api/events/command
        /// <summary>
        /// Updates action/command.
        /// </summary>
        /// <returns>Returns status OK.</returns>
        [Route("command")]
        public object Command()
        {
            return Ok();
        }

        // POST api/events/locationchange/userid
        /// <summary>
        /// Updates the users position.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="model"></param>
        /// <returns>Returns JToken received.</returns>
        [Route("locationchange/{userid}")]
        public object UpdatePosition(int userid, [FromBody] JToken model)
        {

            //eventsRepository.OnUpdatePosition("stub");
            return Ok(model);
        }

        // POST api/events/localtime
        /// <summary>
        /// Update local time.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns status OK.</returns>
        [Route("localtime")]
        public object PostLocalTime([FromBody] JObject model)
        {
            return Ok();
        }*/

    }
}
