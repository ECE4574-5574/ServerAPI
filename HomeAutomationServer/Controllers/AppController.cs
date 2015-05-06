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

using api;

using System.Threading;

namespace HomeAutomationServer.Controllers
{
    [RoutePrefix("api/app")]
    public class AppController : ApiController
    {
        private SimRepository simRepo = new SimRepository();
        private UserRepository userRepository = new UserRepository();

        private DecisionRepository decisionRepository = new DecisionRepository();
        private DeviceRepository deviceRepo = new DeviceRepository();

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
            return userRepository.OnUpdatePosition(model);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Device

        // POST api/app/device/
        /// <summary>
        /// Get pending JSON device data with the device ID provided.
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns>Returns pending JSON device data with the device ID provided.</returns>
        [Route("device/viaSimulation/{houseid}/{roomid}/{deviceid}")]
        public bool UpdateDeviceSim(UInt64 houseID, UInt64 roomID, UInt64 deviceID, [FromBody] JObject sendData)
        {
            return deviceRepo.updateSimulation(houseID, roomID, deviceID, sendData);
        }

        [Route("device/viaActual/{houseid}/{roomid}/{deviceid}")]
        public bool UpdateDeviceActual(UInt64 houseID, UInt64 roomID, UInt64 deviceID, [FromBody] JObject sendData)
        {
            return deviceRepo.updateActual(houseID, roomID, deviceID, sendData);
        }


        // GET api/app/device/{deviceid}
		/// <summary>
		/// Get pending JSON device data with the device ID provided.
		/// </summary>
		/// <param name="deviceid"></param>
		/// <returns>Returns pending JSON device data with the device ID provided.</returns>
		[Route ("device/{fullid}")]
		public JToken GetDevice (FullID fullID)
		{
			return AppCache.GetDeviceBlob (fullID);
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

        // GET api/app/device/enumeratedevices/{houseID}
        /// <summary>
        /// Get the list of devices from the house.
        /// </summary>
        /// <returns>Returns the list of unregistered devices in the house.</returns>
        [Route("device/enumeratedevices/{houseID}")]
        public JArray GetUnregisteredDevices(string houseID)
        {

            JArray test = new JArray();
            test = deviceRepo.SendUnregisteredDevice(houseID);
            return test;
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

        // GET api/logfile
        /// <summary>
        /// Gets the logfile as a string
        /// </summary>
        /// <returns></returns>
        [Route("logfile/getlog")]
        public string Get()
        {
            return LogFile.GetLog();
        }

        // Get api/logfile/count
        /// <summary>
        /// Gets the logfile count
        /// </summary>
        /// <returns></returns>
        [Route("logfile/count")]
        public int GetCount()
        {
            return LogFile.GetCount();
        }

        // Delete api/logfile
        /// <summary>
        /// Deletes the logile
        /// </summary>
        [Route("logfile/delete")]
        public void Delete()
        {
            LogFile.DeleteLog();
        }

        [Route("user/devicetoken/{username}/{pass}")]
        [HttpPost]
        public bool PostDeviceToken([FromBody] JObject model, string username, string pass)
        {
            #if DEBUG
            
                return true;
                
            #else
            
                string deviceToken = (string) model["deviceToken"];
                string retVal = userRepository.PostDeviceToken(username, pass, deviceToken);
                
                if (retVal == "true")
                    return true;
                else
                    return false;
                
            #endif
        }

        [Route("user/notify/{username}/{pass}")]
        [HttpPost]
        public bool SendNotification([FromBody] JObject model, string username, string pass)
        {
            #if DEBUG
            
                return true;
                
            #else
            
                string message = (string) model["message"];
                string retVal = userRepository.SendNotification(username, pass, message);
                
                if (retVal == "true")
                    return true;
                else
                    return false;
                
            #endif
        }

        [Route("user/userid/{username}/{pass}")]
        [HttpGet]
        public UInt64 GetUserId(string username, string pass)
        {
            #if DEBUG
            
                return 1;
                
            #else
            
                return Convert.ToUInt64(userRepository.GetUserId(username, pass));
                
            #endif
        }

        [Route("user/command")]
        [HttpPost]
        public bool PostCommand([FromBody] JObject model)
        {
            #if DEBUG
                
                return true;
                
            #else
            
                // Make a POST request on decisionURL
                return decisionRepository.PostCommand(model);
                
            #endif
        }
        
        [Route("user/delete/{userid}")]
        [HttpDelete]
        public bool DeleteUser(string userid)
        {
            #if DEBUG
                
                return true;
                
            #else
            
                if (userRepository.DeleteUser(userid) == "true")
                    return true;
                else
                    return false;
            
            #endif
        }
    }
}
