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
    public class DeviceController : ApiController
    {

       /* private DeviceRepository deviceRepository = new DeviceRepository();

        // GET api/device/houseid/spaceid/deviceid
        /// <summary>
        /// Gets the device information with the specified house ID, space ID, and device ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <param name="deviceid"></param>
        /// <returns>Returns JSON object data of the device.</returns>
        [Route("api/device/{houseid}/{spaceid}/{deviceid:alpha}")]
        public JObject Get(string houseid, string spaceid, string deviceid)
        {
            return deviceRepository.GetDevice(houseid, spaceid, deviceid);
        }

        // GET api/device/houseid/spaceid
        /// <summary>
        /// Gets all of the devices information with the specified house ID and space ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <returns>Returns all devices in the space via JSON array data.</returns>
        [Route("api/device/{houseid}/{spaceid}")]
        public JArray Get(string houseid, string spaceid)
        {
            return deviceRepository.GetDevice(houseid, spaceid);
        }

        // GET api/device/houseid/spaceid/type
        /// <summary>
        /// Gets all of the devices in the space of the type specified, with the provided house ID and space ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <param name="type"></param>
        /// <returns>Returns all of the devices in the space of the type specified via JSON array data.</returns>
        [Route("api/device/{houseid}/{spaceid}/{type:int}")]
        public JArray Get(string houseid, string spaceid, int type)
        {
            return deviceRepository.GetDevice(houseid, spaceid, type);
        }

        // GET api/device/houseid
        /// <summary>
        /// Gets all of the devices in the house with the specified house ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>Returns all of the devices in the house specified via JSON array data.</returns>
        [Route("api/device/{houseid}")]
        public JArray Get(string houseid)
        {
            return deviceRepository.GetDevice(houseid);
        }

        // GET api/device/houseid/type
        /// <summary>
        /// Gets all of the devices in the house of the specified type, with the provided house ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="type"></param>
        /// <returns>Returns all of the devices in the specified house via JSON array data.</returns>
        [Route("api/device/{houseid}/{type}")]
        public JArray Get(string houseid, int type)
        {
            return deviceRepository.GetDevice(houseid, type);
        }

        // POST api/device
        /// <summary>
        /// Posts a device with the JSON object data given.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if posted, false if not.</returns>
        [Route("api/device")]
        public bool Post([FromBody] JObject model)
        {
            return true; //deviceRepository.SaveDevice(model);
        }

        // DELETE api/device/id
        /// <summary>
        /// Deletes a device with the specified house ID, space ID, and device ID provided.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <param name="deviceid"></param>
        /// <returns>Returns true if the device was deleted, false if not.</returns>
        [Route("api/device/{houseid}/{spaceid}/{deviceid}")]
        public bool Delete(string houseid, string spaceid, string deviceid)
        {
            return true; // deviceRepository.DeleteDevice(houseid, spaceid, deviceid);
        }*/
    }
}
