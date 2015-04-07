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

    [RoutePrefix("api/devicemgr")]
    public class DeviceMgrController : ApiController
    {
        private DeviceRepository repo;
        private ServerIdentityService identityService;
        private DeviceMgrRepository deviceMgrRepository;

        public DeviceMgrController()
        {
            repo = new DeviceRepository();
            identityService = new ServerIdentityService();
            this.deviceMgrRepository = new DeviceMgrRepository();
        }

        // GET api/devicemgr/state/deviceid
        /// <summary>
        /// Gets the status of the device with the device ID provided.
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns>Returns the status of the device and device information via JSON object data. Hardcoded for now.</returns>
        [Route("state/{deviceid}")]
        public JObject GetDeviceState(int deviceid)
        {
            JObject device = new JObject();
            device["DeviceId"] = deviceid;
            device["DeviceName"] = "Living Room Main Light";
            device["DeviceType"] = 2;
            device["State"] = true;
            return device;
        }

        // POST api/devicemgr/state
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
