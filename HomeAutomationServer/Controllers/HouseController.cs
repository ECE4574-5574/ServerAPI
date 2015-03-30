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

        // GET api/house/houseid
        [Route("api/house/{houseid}")]
        public JObject Get(string houseid)
        {
            return houseRepository.GetHouse(houseid);
        }


        // POST api/house/houseid
        [Route("api/house/{houseid}")]
        public JToken Post(/*User user*/ string houseid, [FromBody] JToken model)                  // HTTP POST - posts a new user
        {
            return houseRepository.SaveHouse(houseid, model);
        }

        /*// PATCH api/House/
        [Route("{houseid}")]
        public JObject Patch(string houseid, string housename = "", string username = "")
        {
            return null;
        }*/

        // DELETE api/house/houseid
        [Route("api/house/{houseid}")]
        public JObject Delete(string houseid)
        {
            return null;
        }
    }
}
