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
    [RoutePrefix("api/app")]
    public class AppController : ApiController
    {
        private SimRepository simRepo = new SimRepository();
        private UserRepository userRepository = new UserRepository();
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // User

        // POST api/app/user/updateposition
        /// <summary>
        /// Updates the user position and location time stamp to the user, requires JSON object data and the username for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="model"></param>
        /// <returns>Returns true if updated, false if not.</returns>
        [Route("user/updateposition/{username}")]
        public bool UpdatePosition(string username, [FromBody] JObject model)
        {
            model["userId"] = username;
            //DateTime currentTime;
            //currentTime = DateTime.Now;
            //model["locationTimeStamp"] = currentTime.ToString();
            return userRepository.OnUpdatePosition(model);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Device

        // GET api/app/device/{deviceid}
        /// <summary>
        /// Get pending JSON device data with the device ID provided.
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns>Returns pending JSON device data with the device ID provided.</returns>
        [Route("device/{deviceid}")]
        public JToken GetDevice(string deviceid)
        {
            return AppCache.GetDeviceBlob(deviceid);
        }

        // GET api/app/device
        /// <summary>
        /// Get all the pending JSON device data as an array of JSON data.
        /// </summary>
        /// <returns>Returns an array of JSON data of the devices.</returns>
        [Route("device")]
        public JArray GetDevices()
        {
            return AppCache.GetAllBlobs();
        }

        // GET api/app/device/count
        /// <summary>
        /// Get the pending JSON device data count.
        /// </summary>
        /// <returns>Returns the number of pending JSON device data.</returns>
        [Route("device/count")]
        public int GetDeviceCount()
        {
            return AppCache.GetBlobCount();
        }

        // POST api/app/user/brighten
        /// <summary>
        /// A request from the App system to make something brighter near their location. Information provided 
        /// in a JSON. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true or false, depeding if the information got sent or not.</returns>
        [Route("user/brighten")]
        public bool MakeBrighter([FromBody] JObject model)
        {
            return userRepository.Brighten(model);
        }
        
        // GET api/app/logfile
        /// <summary>
        /// Returns a string version of the logfile from the server.
        /// </summary>
        /// <returns></returns>
        [Route("logfile")]
        public string GetLogFile()
        {
            return simRepo.GetLog();
        }
    }
}
