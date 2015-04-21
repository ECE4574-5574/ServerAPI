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
        public JObject GetUser(string username)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://172.31.26.85:8080/UI/" + username);
                request.Method = "GET";

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream);

                        string userString = reader.ReadToEnd();
                        return JObject.Parse(userString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("UpdateLocation -- Could not create the URL with the data provided: " + ex.Message + "\n")
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("UserGet -- Could not create URL from data provided: " + ex.Message + "\n");
                return null;
            }
        }

        public bool SaveUser(string username, JToken model)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://54.152.190.217:8081/U/" + username);
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(model.ToString());
                    streamWriter.Close();
                }
                // request = WebRequest.Create("http://localhost:8081");

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }
                }

                catch (Exception we)
                {
                    LogFile.AddLog("Could not post user information to the Storage: " + we.Message + "\n");
                    return false;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("UpdateLocation -- Could not create the URL with the data provided: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        public JObject DeleteUser(string username)
        {
            /*WebRequest request = WebRequest.Create("http://54.152.190.217:8080/A/USER/" + username);
            request.Method = "DELETE";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);

                string userString = reader.ReadToEnd();
                return JObject.Parse(userString);
            }*/
            return null;
        }

        //Sends an updated position to the decison system
        public bool OnUpdatePosition(JObject model)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://54.152.190.217:8085/LocationChange");
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(model.ToString());
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }
                }
                catch (WebException we)
                {
                    LogFile.AddLog("UpdateLocation -- Could not create the URL with the data provided: " + we.Message + "\n");
                    return false;

                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("UpdateLocation -- Could not create the URL with the data provided: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        //Sends an updated position to the decison system and needs the nearest object to be brightened that can be brightened
        public bool Brighten(JObject model)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://54.152.190.217:8085/LocationChange");
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(model.ToString());
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }
                }
                catch (Exception we)
                {
                    LogFile.AddLog("Brighten -- Could not post information to Decision System: " + we.Message + "\n");
                    return false;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Brigthen -- Could not create the URL with the data provided: " + ex.Message + "\n")
                return false;
            }

            return true;
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

