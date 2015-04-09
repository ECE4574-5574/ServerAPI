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

        /*// GET api/user/username
        /// <summary>
        /// Gets the users information by the username provided via JSON object data.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns the users information via JSON object data.</returns>
        [Route("api/storage/user/{username}")]
        public JObject Get(string username)
        {
            return userRepository.GetUser(username);
        }

        // POST api/user
        /// <summary>
        /// Posts the users information provided by JSON object data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns true if the information was posted, false if not.</returns>
        [Route("api/storage/user")]
        public bool Post([FromBody] JObject model)                  // HTTP POST - posts a new user
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
        public bool Delete(string username)                // HTTP DELETE - deletes a user
        {
            return true; // userRepository.DeleteUser(username);
        }

        // POST api/user/updateposition
        /// <summary>
        /// Updates the user position and location time stamp to the user, requires JSON object data and the username for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="model"></param>
        /// <returns>Returns true if updated, false if not.</returns>
        [Route("api/app/user/updateposition/{username}")]
        public bool UpdatePosition(string username,[FromBody] JObject model)
        {
            model["userId"] = username;
            DateTime currentTime;
            //currentTime = DateTime.Now;
            //model["locationTimeStamp"] = currentTime.ToString();
            return userRepository.OnUpdatePosition(model);
        }*/
    }
}

