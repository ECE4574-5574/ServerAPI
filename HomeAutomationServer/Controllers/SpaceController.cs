using HomeAutomationServerAPI.Models;
using HomeAutomationServerAPI.Services;
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

        public Space[] Get()                             // HTTP GET - gets information about the space
        {
            return spaceRepository.GetAllSpaces();
        }

        public Space Get(int id)
        {
            return null;
        }

        public Space Get(string name)
        {
            return null;
        }

        public HttpResponseMessage Patch(int id, string name = null, string type = null, string houseName = null)                      // HTTP PATCH - updates information about the space
        {
            /*Exception ex = this.spaceRepository.UpdateSpace();
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            if (ex.Message == "updated")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);

            return response;*/
            return null;
        }

        public HttpResponseMessage Patch(int id, bool permission)                      // HTTP PATCH - updates information about the space
        {
            /*Exception ex = this.spaceRepository.UpdateSpace();
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            if (ex.Message == "updated")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);

            return response;*/
            return null;
        }

        public HttpResponseMessage Post(Space space)                  // HTTP POST - posts a new space
        {
            Exception ex = this.spaceRepository.SaveSpace(space);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            if (ex.Message == "none")
                response = Request.CreateResponse<Space>(System.Net.HttpStatusCode.Created, space);

            return response;
        }

        public HttpResponseMessage Delete(int id)                // HTTP DELETE - deletes a space
        {
            /*Exception ex = this.spaceRepository.DeleteSpace(space);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            if (ex.Message == "none")
                response = Request.CreateResponse<Space>(System.Net.HttpStatusCode.OK, space);

            return response;*/
            return null;
        }
    }
}
