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
    [RoutePrefix("api/storage")]
    public class StorageController : ApiController
    {
        private UserRepository userRepository = new UserRepository();
        private HouseRepository houseRepository = new HouseRepository();
        private SpaceRepository spaceRepository = new SpaceRepository();
        private DeviceRepository deviceRepository = new DeviceRepository();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Devices

        // GET api/device/houseid/spaceid/deviceid
        /// <summary>
        /// Gets the device information with the specified house ID, space ID, and device ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <param name="deviceid"></param>
        /// <returns>Returns JSON object data of the device.</returns>
        [Route("device/{houseid}/{spaceid}/{deviceid:alpha}")]
        public JObject GetDev(string houseid, string spaceid, string deviceid)
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
        [Route("device/{houseid}/{spaceid}")]
        public JArray GetDevSpace(string houseid, string spaceid)
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
        [Route("device/{houseid}/{spaceid}/{type:int}")]
        public JArray GetDevSpaceType(string houseid, string spaceid, int type)
        {
            return deviceRepository.GetDevice(houseid, spaceid, type);
        }

        // GET api/device/houseid
        /// <summary>
        /// Gets all of the devices in the house with the specified house ID.
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>Returns all of the devices in the house specified via JSON array data.</returns>
        [Route("device/{houseid}")]
        public JArray GetDevHouse(string houseid)
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
        [Route("device/{houseid}/{type}")]
        public JArray GetDevHouseType(string houseid, int type)
        {
            return deviceRepository.GetDevice(houseid, type);
        }

        // POST api/device
        /// <summary>
        /// Posts a device with the JSON object data given.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if posted, false if not.</returns>
        [Route("device")]
        public bool PostDevice([FromBody] JObject model)
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
        [Route("device/{houseid}/{spaceid}/{deviceid}")]
        public bool DeleteDevice(string houseid, string spaceid, string deviceid)
        {
            return true; // deviceRepository.DeleteDevice(houseid, spaceid, deviceid);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Space

        // GET api/space/houseid/spaceid
        /// <summary>
        /// Gets the space information with the houseid and spaceid provided.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <returns>Returns JSON object data of the space information found.</returns>
        [Route("space/{houseid}/{spaceid}")]
        public JObject GetSpace(string houseid, string spaceid)                        // HTTP GET - gets devices in the space
        {
            return spaceRepository.GetSpace(houseid, spaceid);
        }

        // POST api/space
        /// <summary>
        /// Posts the space with the JSON object data information provided.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if posted, false if not.</returns>
        [Route("space")]
        public bool PostSpace([FromBody] JObject model)                  // HTTP POST - posts a new space
        {
            return true; // spaceRepository.SaveSpace(houseid, spaceid, model);
        }

        // DELETE api/space/houseid/spaceid
        /// <summary>
        /// Deletes the space specified by the houseid and spaceid
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <returns>Returns true if deleted, false if not.</returns>
        [Route("space/{houseid}/{spaceid}")]
        public bool DeleteSpace(string houseid, string spaceid)                // HTTP DELETE - deletes a space
        {
            return true; // spaceRepository.DeleteSpace(houseid, spaceid);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 
        // House

        // GET api/house/houseid
        /// <summary>
        /// Gets the houses information with the specified houseid.
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>Returns the houses information via JSON object data.</returns>
        [Route("house/{houseid}")]
        public JObject GetHouse(string houseid)
        {
            return houseRepository.GetHouse(houseid);
        }


        // POST api/house
        /// <summary>
        /// Posts the house with the JSON object information provided.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the house was posted, false if not.</returns>
        [Route("house")]
        public bool PostHouse([FromBody] JObject model)                  // HTTP POST - posts a new user
        {
            return true; // houseRepository.SaveHouse(houseid, model);
        }

        /*// PATCH api/House/
        [Route("{houseid}")]
        public JObject Patch(string houseid, string housename = "", string username = "")
        {
            return null;
        }*/

        // DELETE api/house/houseid
        /// <summary>
        /// Deletes the house with the specified houseid.
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>Returns true if the house was deleted, false if not.</returns>
        [Route("house/{houseid}")]
        public bool DeleteHouse(string houseid)
        {
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // User

        // GET api/user/username
        /// <summary>
        /// Gets the users information by the username provided via JSON object data.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns the users information via JSON object data.</returns>
        [Route("user/{username}")]
        public JObject GetUser(string username)
        {
            return userRepository.GetUser(username);
        }

        // POST api/user
        /// <summary>
        /// Posts the users information provided by JSON object data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the information was posted, false if not.</returns>
        [Route("user")]
        public bool PostUser([FromBody] JObject model)                  // HTTP POST - posts a new user
        {
            return true; //userRepository.SaveUser(model);
        }

        // DELETE api/user/username
        /// <summary>
        /// Deletes the user specified by the username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns true if deleted, false if not.</returns>
        [Route("api/stroage/user/{username}")]
        public bool DeleteUser(string username)                // HTTP DELETE - deletes a user
        {
            return true; // userRepository.DeleteUser(username);
        }
    }
}