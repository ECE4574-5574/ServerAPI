using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Filters;

namespace HomeAutomationServer.Controllers
{
    [RoutePrefix("api/sim")]
    public class SimController : ApiController
    {
        private SimRepository simRepo = new SimRepository();

        // POST api/sim/timeframe
        /// <summary>
        /// Post the information via JSON data regarding the SimHarneses time frame configurations to the Decision Making System.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if posted, false if not.</returns>
        [Route("timeframe")]
        public bool Post([FromBody] JObject model)
        {
            return simRepo.sendTimeFrame(model);
        }

    }
}
