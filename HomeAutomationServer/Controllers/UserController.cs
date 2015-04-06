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

// User controller class

namespace HomeAutomationServer.Controllers
{
    //[RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private UserRepository userRepository = new UserRepository();

        public UserController()
        {
            this.userRepository = new UserRepository();
        }

        // GET api/user/username
        [Route("api/user/{username}")]
        public JObject Get(string username)
        {
            return userRepository.GetUser(username);
        }

        /*// PATCH api/User/username, first name, last name 
        [Route("{username}")]
        public JObject Patch(string username, string firstName = "", string lastName = "")                      // HTTP PATCH - updates information about the user
        {
            return null;

        }*/

        // POST api/user/username
        [Route("api/user/{username}")]
        public JObject Post(/*User user*/ string username, [FromBody] JToken model)                  // HTTP POST - posts a new user
        {
            return userRepository.SaveUser(username, model);
        }

        // DELETE api/user/username
        [Route("api/user/{username}")]
        public JObject Delete(string username)                // HTTP DELETE - deletes a user
        {
            return userRepository.DeleteUser(username);
        }

        // POST api/user/updateposition
        [Route("api/user/updateposition/{username}")]
        public object UpdatePosition(string username, [FromBody] JToken model)
        {
            JToken child = model.First();
            child.AddBeforeSelf(new JProperty("usersID", username));
            DateTime currentTime;
            currentTime = DateTime.Now;
            JToken child2 = model.Last();
            child2.AddAfterSelf(new JProperty("locationTimeStamp", currentTime.ToString()));
            //model.AddBeforeSelf(new JProperty("user",username));
            userRepository.OnUpdatePosition(model);
            return Ok(model);
        }
    }
}

