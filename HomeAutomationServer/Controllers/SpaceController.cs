using HomeAutomationServer.Models;
using HomeAutomationServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Space controller class

namespace HomeAutomationServerAPI.Controllers
{
    public class SpaceController : ApiController
    {
        private SpaceRepository spaceRepository;

        public SpaceController()
        {
            this.spaceRepository = new SpaceRepository();
        }

        // GET api/Space
        public IEnumerable<Space> Get()                             // HTTP GET - gets information about the space
        {
            return spaceRepository.GetAllSpaces();
        }

        // GET api/Space/id
        public Space Get(int id)
        {
            return spaceRepository.GetSpace(id);
        }

        // PATCH api/Space/id, name, type, housename
        public HttpResponseMessage Patch(int id, string name = "", int type = -1, int houseId = -1)                      // HTTP PATCH - updates information about the space
        {
            Exception ex = spaceRepository.UpdateSpace(id, name, type, houseId);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            if (ex.Message == "updated")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);

            return response;
        }

        // PATCH api/Space
        public HttpResponseMessage Patch(Space space)                      // HTTP PATCH - updates information about the space
        {
            Exception ex = spaceRepository.UpdateSpace(space);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            if (ex.Message == "updated")
                return Request.CreateResponse<Space>(System.Net.HttpStatusCode.OK, space);

            return response;
        }

        /*// PATCH api/Space/id, permission
        public HttpResponseMessage Patch(int id, bool permission)                      // HTTP PATCH - updates information about the space
        {
            /*Exception ex = this.spaceRepository.UpdateSpace();
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            if (ex.Message == "updated")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);

            return response;
            return null;
        }*/

        // POST api/Space
        public HttpResponseMessage Post(Space space)                  // HTTP POST - posts a new space
        {
            Exception ex = spaceRepository.SaveSpace(space);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            if (ex.Message == "saved")
                response = Request.CreateResponse<Space>(System.Net.HttpStatusCode.Created, space);

            return response;
        }

        // DELETE api/Space/id
        public HttpResponseMessage Delete(int id)                // HTTP DELETE - deletes a space
        {
            Exception ex = spaceRepository.DeleteSpace(id);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            if (ex.Message == "deleted")
                response = Request.CreateResponse(System.Net.HttpStatusCode.OK);

            return response;
        }
    }
}
