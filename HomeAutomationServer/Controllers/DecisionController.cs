using HomeAutomationServer.Filters;
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
        private DeviceRepository deviceRepo;
        private DecisionRepository decisionRepo;
        private ServerIdentityService identityService;
        private DecisionRepository deviceMgrRepository;

        public DecisionController()
        {
            deviceRepo = new DeviceRepository();
            decisionRepo = new DecisionRepository();
            identityService = new ServerIdentityService();
            this.deviceMgrRepository = new DecisionRepository();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Device


        // Patch api/decision/state
        /// <summary>
        /// Updates a devices state with the given device JSON data. The new device state should be reflected in the data. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the devices state has been updated. Returns false if not.</returns>
        [Route("state")]
        [HttpPatch]
        public bool UpdateState([FromBody] JObject model)
        {
            return decisionRepo.StateUpdate(model);
        }

        // Get api/decision/state
        /// <summary>
        /// Get the state of the device from the device JSON data provided, which is either true or false.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return the state of the device. True if enabled, false if not.</returns>
		[Route("state")]
		[HttpGet]
		public bool GetDeviceState([FromBody] JObject model)
		{
			return decisionRepo.GetState(model);
		}
    }
}
