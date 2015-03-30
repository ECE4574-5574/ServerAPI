using HomeAutomationServer.Filters;
using HomeAutomationServer.Models;
using HomeAutomationServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

//This is the device controller and will respond to GET, POST, PATCH, and DELETE

namespace HomeAutomationServerAPI.Controllers
{
    [LearningAuthorizeAttribute]
    public class DeviceController : ApiController
    {

        private DeviceRepository deviceRepository = new DeviceRepository();

        // GET api/device/houseid/spaceid/deviceid
        [Route("api/device/{houseid}/{spaceid}/{deviceid}")]
        public JObject Get(string houseid, string spaceid, string deviceid)
        {
            return null;
        }

        // GET api/device/houseid/spaceid
        [Route("api/device/{houseid}/{spaceid}")]
        public JObject Get(string houseid, string spaceid)
        {
            return null;
        }

        // GET api/device/houseid/spaceid/type
        [Route("api/device/{houseid}/{spaceid}/{type}")]
        public JObject Get(string houseid, string spaceid, int type)
        {
            return null;
        }

        // GET api/device/houseid
        [Route("api/device/{houseid}")]
        public JObject Get(string houseid)
        {
            return null;
        }

        // GET api/device/houseid/type
        [Route("api/device/{houseid}/{type}")]
        public JObject Get(string houseid, int type)
        {
            return null;
        }

       /* // PATCH api/Device/housid, spaceid, deviceid, name, type, newSpaceId
        public JObject Patch(string houseid, string spaceid, string deviceid, string name = "", int type = -1, string newSpaceId = "")
        {
            return null;
        }*/

        // POST api/device/houseid, spaceid, deviceid, model
        [Route("api/device/{houseid}/{spaceid}/{deviceid}")]
        public JToken Post(string houseid, string spaceid, string deviceid, [FromBody] JToken model)
        {
            return null;
        }

        // DELETE api/device/id
        [Route("api/device/{houseid}/{spaceid}/{deviceid}")]
        public JObject Delete(string houseid, string spaceid, string deviceid)
        {
            return null;
        }
    }
}
