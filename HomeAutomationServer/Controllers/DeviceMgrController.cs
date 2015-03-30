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

        [Route("state/{deviceid}")]
        public object GetDeviceState(int deviceid)
        {
            return new Device
            {
                DeviceId = deviceid,
                DeviceName = "Living Room Main Light",
                DeviceType =  2,
                State = true
            };
        }

        [Route("state/{deviceid}")]
        public object PostDeviceState(int deviceid, [FromBody] JToken model)
        {
            return Ok(model);
        }

    }
}
