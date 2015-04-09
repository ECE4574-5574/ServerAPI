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

namespace HomeAutomationServer.Controllers
{
    [RoutePrefix("api/app")]
    public class AppController : ApiController
    {
        private UserRepository userRepository = new UserRepository();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // User

        // POST api/storage/user/updateposition
        /// <summary>
        /// Updates the user position and location time stamp to the user, requires JSON object data and the username for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="model"></param>
        /// <returns>Returns true if updated, false if not.</returns>
        [Route("user/updateposition/{username}")]
        public bool UpdatePosition(string username, [FromBody] JObject model)
        {
            model["userId"] = username;
            DateTime currentTime;
            //currentTime = DateTime.Now;
            //model["locationTimeStamp"] = currentTime.ToString();
            return userRepository.OnUpdatePosition(model);
        }
    }
}