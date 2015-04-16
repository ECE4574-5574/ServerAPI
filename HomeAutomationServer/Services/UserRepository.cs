using HomeAutomationServer.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;

// This class tells the controller how to process the HTTP commands

namespace HomeAutomationServer.Services
{
    public class UserRepository
    {
        private string path = @"C:\ServerAPILogFile\logfile.txt";

        public JObject GetUser(string username)
        {
            WebRequest request = WebRequest.Create("http://172.31.26.85:8080/UI/" + username);
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

                string userString = reader.ReadToEnd();
                return JObject.Parse(userString);
            }
        }

        public bool SaveUser(string username, JToken model)
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

            catch (WebException we)
            {
                if (!File.Exists(path))
                {
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(we.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else
                {
                    using (FileStream fstream = File.OpenWrite(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(we.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                return false;
            }

            //return null;
            return true;
            //stubbed, will send an updated positio*/
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
                    if (!File.Exists(path))
                    {
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(we.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else
                    {
                        using (FileStream fstream = File.OpenWrite(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(we.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    return false;
                }

            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else
                {
                    using (FileStream fstream = File.OpenWrite(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
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

