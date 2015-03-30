using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

// User controller class

namespace HomeAutomationServer.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private UserRepository userRepository = new UserRepository();

        public UserController()
        {
            this.userRepository = new UserRepository();
        }

        //// GET api/User
        //public IEnumerable<User> Get()                             // HTTP GET - gets information about the user
        //{
        //    return userRepository.GetAllUsers();
        //}

        // GET api/User/id
        public JObject Get(string username)
        {
            return userRepository.GetUser(username);
        }

        // PATCH api/User/id, first name, last name 
        public HttpResponseMessage Patch(string username, string firstName = "", string lastName = "")                      // HTTP PATCH - updates information about the user
        {
            //Exception ex = userRepository.UpdateUser(id, firstName, lastName);
            //var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            //if (ex.Message == "updated")
            //    return Request.CreateResponse(System.Net.HttpStatusCode.OK);

            //return response;
            return null;

        }

        // PATCH api/User
        public HttpResponseMessage Patch(User user)                      // HTTP PATCH - updates information about the user
        {
            //Exception ex = userRepository.UpdateUser(user);
            //var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);
            //if (ex.Message == "updated")
            //    return Request.CreateResponse<User>(System.Net.HttpStatusCode.OK, user);

            //return response;
            return null;

        }

        // POST api/User
        public JToken Post(/*User user*/ string username, [FromBody] JToken model)                  // HTTP POST - posts a new user
        {
            return userRepository.SaveUser(username, model);
        }

        // DELETE api/User/id
        public HttpResponseMessage Delete(string username)                // HTTP DELETE - deletes a user
        {
            //Exception ex = userRepository.DeleteUser(id);
            //var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            //if (ex.Message == "deleted")
            //    response = Request.CreateResponse(System.Net.HttpStatusCode.OK);

            //return response;
            return null;

        }

        // POST api/User/updateposition
        [Route("updateposition/{userid}")]
        public object UpdatePosition(int userid, [FromBody] JToken model)
        {
            return Ok(model);
        }
    }
}

