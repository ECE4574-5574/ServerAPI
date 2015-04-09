using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

// This is the house controller and will respond to GET, POST, PATCH, and DELETE

namespace HomeAutomationServerAPI.Controllers
{
    public class HouseController : ApiController
    {
        private HouseRepository houseRepository = new HouseRepository();

        /*// GET api/house/houseid
        /// <summary>
        /// Gets the houses information with the specified houseid.
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>Returns the houses information via JSON object data.</returns>
        [Route("api/storage/house/{houseid}")]
        public JObject Get(string houseid)
        {
            return houseRepository.GetHouse(houseid);
        }


        // POST api/house
        /// <summary>
        /// Posts the house with the JSON object information provided.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the house was posted, false if not.</returns>
        [Route("api/storage/house")]
        public bool Post([FromBody] JObject model)                  // HTTP POST - posts a new user
        {
            return true; // houseRepository.SaveHouse(houseid, model);
        }

        // PATCH api/House/
        [Route("{houseid}")]
        public JObject Patch(string houseid, string housename = "", string username = "")
        {
            return null;
        }

        // DELETE api/house/houseid
        /// <summary>
        /// Deletes the house with the specified houseid.
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>Returns true if the house was deleted, false if not.</returns>
        [Route("api/storage/house/{houseid}")]
        public JObject Delete(string houseid)
        {
            return null;
        }*/
    }
}
