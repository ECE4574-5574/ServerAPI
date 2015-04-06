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

        public DeviceMgrController()
        {
            repo = new DeviceRepository();
            identityService = new ServerIdentityService();
        }

        // GET api/devicemgr/state/deviceid
        /// <summary>
        /// Gets the status of the device with the device ID provided.
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns>Status of the device and device information via JObject. Hardcoded for now.</returns>
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

        // POST api/devicemgr/state/deviceid
        /// <summary>
        /// Post a device status to a device with device ID specified. Takes a JObject.
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="model"></param>
        /// <returns>Returns the information posted via JObject.</returns>
        [Route("state/{deviceid}")]
        public JObject PostDeviceState(int deviceid, [FromBody] JObject model)
        {
            return model;
        }

    }
}
