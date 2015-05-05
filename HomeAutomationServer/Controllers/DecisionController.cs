using HomeAutomationServer.Filters;
using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
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

        // Post api/decision/device
        /// <summary>
        /// Sends the devices decision made to the App System. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the decision has been send. Returns false if not.</returns>
        [Route("device")]
        [HttpPost]
        public bool SendDevDecision([FromBody] JObject model)
        {
            return AppCache.AddDeviceInfo(model);
        }

        /*// Get api/decision/state
        /// <summary>
        /// Get the state of the device from the device JSON data provided, which is either true or false.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return the state of the device. True if enabled, false if not.</returns>
        [Route("state")]
        public bool GetDeviceState([FromBody] JObject model)
        {
            return decisionRepo.GetState(model);
        }*/
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// 
		// App
		
		// Post api/decision/app
		/// <summary>
		/// Sends information that the user needs to know about the backend processing
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Returns true if the information was sent. Returns false if not.</returns>
		[Route("app"), HttpPost]
		public bool Information([FromBody] JObject model)
		{
			return AppCache.AddUserInfo(model);
		}
    }
}
