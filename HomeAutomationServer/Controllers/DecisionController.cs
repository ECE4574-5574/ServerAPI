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

        
        // Patch api/decision/state/{deviceid}/{state}
        /// <summary>
        /// Updates a devices state with the given device ID to the state specified. 
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="state"></param>
        /// <returns>Returns true if the devices state has been updated. Returns false if not.</returns>
        [Route("api/decision/state/{deviceid}/{state}")]
        public bool UpdateState(UInt64 deviceid, bool state)
        {
            return true;
        }

        // Get api/decision/state/{deviceid}
        /// <summary>
        /// Get the state of the device, which is either true or false.
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns>Return the state of the device. True if enabled, false if not.</returns>
        [Route("state/{deviceid}")]
        public bool GetDeviceState(UInt64 deviceid)
        {
            return true;
        }
    }
}
