using HomeAutomationServerAPI.Models;
using System;
using System.Collections.Generic;
using HomeAutomationServerAPI.Models;
using HomeAutomationServerAPI.Services;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// This is the house controller and will respond to GET, POST, PATCH, and DELETE

namespace HomeAutomationServerAPI.Controllers
{
    public class HouseController : ApiController
    {
        private HouseRepository houseRepository;

        // GET api/house
        public IEnumerable<House> Get()
        {
            return houseRepository.GetAllHouses();
        }

        // GET api/house/id
        public House Get(int id)
        {
            return houseRepository.GetHouse(id);
        }

        // POST api/house
        public HttpResponseMessage Post(House house)
        {
            Exception ex = houseRepository.SaveHouse(house);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            if (ex.Message == "saved")
                return Request.CreateResponse<House>(System.Net.HttpStatusCode.Created, house);
            else return response;
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
