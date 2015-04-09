using HomeAutomationServer.Models;
using HomeAutomationServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

// Space controller class

namespace HomeAutomationServerAPI.Controllers
{
    public class SpaceController : ApiController
    {
        private SpaceRepository spaceRepository = new SpaceRepository();

        /*// GET api/space/houseid/spaceid
        /// <summary>
        /// Gets the space information with the houseid and spaceid provided.
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="spaceid"></param>
        /// <returns>Returns JSON object data of the space information found.</returns>
        [Route("api/space/{houseid}/{spaceid}")]
        public JObject Get(string houseid, string spaceid)                        // HTTP GET - gets devices in the space
        {
            return spaceRepository.GetSpace(houseid, spaceid);
        }

        // POST api/space
        /// <summary>
        /// Posts the space with the JSON object data information provided.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if posted, false if not.</returns>
        [Route("api/space")]
        public bool Post([FromBody] JObject model)                  // HTTP POST - posts a new space
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
        [Route("api/space/{houseid}/{spaceid}")]
        public bool Delete(string houseid, string spaceid)                // HTTP DELETE - deletes a space
        {
            return true; // spaceRepository.DeleteSpace(houseid, spaceid);
        }*/
    }
}
