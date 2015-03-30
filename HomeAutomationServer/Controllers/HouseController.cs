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

        // GET api/house
        public IEnumerable<House> Get()
        {
            return houseRepository.GetAllHouses();
        }

        // GET api/house/id
        public JObject Get(string houseid)
        {
            return houseRepository.GetHouse(houseid);
        }


        // POST api/house
        public JToken Post(/*User user*/ string houseid, [FromBody] JToken model)                  // HTTP POST - posts a new user
        {
            return houseRepository.SaveHouse(houseid, model);
        }

        // PATCH api/house
        public HttpResponseMessage Patch(House house)
        {
            Exception ex = houseRepository.UpdateHouse(house);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);

            if (ex.Message == "updated")
                return Request.CreateResponse<House>(System.Net.HttpStatusCode.OK, house);
            else return response;
        }

        // PATCH api/house/id, name, userId
        public HttpResponseMessage Patch(int id, string name = "", int userId = -1)
        {
            Exception ex = houseRepository.UpdateHouse(id, name, userId);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);

            if (ex.Message == "updated")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            else return response;
        }

        // DELETE api/house/id
        public HttpResponseMessage Delete(int id)
        {
            Exception ex = houseRepository.DeleteHouse(id);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);

            if (ex.Message == "deleted")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            else return response;
        }
    }
}
