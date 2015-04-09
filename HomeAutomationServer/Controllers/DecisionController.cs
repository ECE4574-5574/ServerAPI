using HomeAutomationServer.Filters;
using HomeAutomationServer.Models;
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

    [RoutePrefix("api/decision")]
    public class DecisionController : ApiController
    {
        private DeviceRepository repo;
        private ServerIdentityService identityService;
        private DecisionRepository deviceMgrRepository;

        public DecisionController()
        {
            repo = new DeviceRepository();
            identityService = new ServerIdentityService();
            this.deviceMgrRepository = new DecisionRepository();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Device

        // POST api/decision/state
        /// <summary>
        /// Update a device state to a device. Requires JSON object data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the information was posted, false if not.</returns>
        [Route("state")]
        public bool PostDeviceState([FromBody] JObject model)
        {
            deviceMgrRepository.PostDeviceState((UInt64)model["houseId"], (UInt64)model["roomId"], (UInt64)model["deviceId"], model);
            return true;
        }
    }
}
