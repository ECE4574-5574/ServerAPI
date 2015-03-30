using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

// This class tells the controller how to process the HTTP commands

namespace HomeAutomationServer.Services
{
    public class UserRepository
    {

        public User GetUser(string username)
        {
            WebRequest request = WebRequest.Create("http://54.152.190.217:8080/UI/" + username);
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);

                User user = JsonConvert.DeserializeObject<User>(reader.ReadToEnd());
                return user;
            }
        }
        // The below code is good, but I commented it out because I could not build it.

        public HttpWebResponse SaveUser(/*User user*/string username, JToken model)
        {
            WebRequest request = WebRequest.Create("http://54.152.190.217:8080/U/" + username);
            request.ContentType = "text/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                /*string json = "{\"username\":\"" + user.UserName + "\"," +
                  "\"firstname\":\"" + user.FirstName + "\"," + "\"lastname\":\"" + user.LastName + "\"," + 
                  "\"houses\":\"" + user.MyHouses.ToString() + "\"}";*/

                streamWriter.Write(model.ToString());
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return response;
        }


        public User UpdateUserPosition(string username)
        {
            WebRequest request = WebRequest.Create("http://54.152.190.217:8080/UI/" + username);
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);

                User user = JsonConvert.DeserializeObject<User>(reader.ReadToEnd());
                return user;
            }
        }

        //public Exception UpdateUser(int id, string firstName, string lastName)
        //{
        //    User user = new User();
        //    user = getUser(id);                             // Persistent storage getUser() method

        //    if (user == null)
        //        return new Exception("User with User Id: " + id + " not found");

        //    if (firstName != "")
        //        user.FirstName = firstName;

        //    if (lastName != "")
        //        user.LastName = lastName;

        //    updateUser(user);                                   // Persistent storage updateUser() method
        //    return new Exception("updated");
        //}

        //public Exception UpdateUser(User user)
        //{
        //    if (getUser(user.UserId) == null)                         // Persistent storage getUser() method
        //        return new Exception("User with User Id: " + user.UserId + " not found");
        //    else
        //    {
        //        updateUser(user);                                   // Persistent storage updateUser() method
        //        return new Exception("updated");
        //    }
        //}

        //public Exception DeleteUser(int id)
        //{
        //    if (getUser(id) == null)                 // Persistent storage getUser() method
        //        return new Exception("User with User Id: " + id + " not found");
        //    else
        //    {
        //        removeUser(id);                      // Persistent storage removeUser() method
        //        return new Exception("deleted");
        //    }
        //}
    }
}

