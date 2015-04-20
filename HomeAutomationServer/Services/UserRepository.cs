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
        private string path = "ServerAPILogFile\logfile.txt";
        // deviceRepo has static url
        private string dm_url = DeviceRepository.decisionURL;
		private string pss_url = DeviceRepository.storageURL;

        public JObject GetUser(string username)
        {
            #if DEBUG
            return new JObject.Parse('{"name": "user"}');
            #else
            try
            {
				WebRequest request = WebRequest.Create(pss_url + username);
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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory("ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Could not Get user information from Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nUpdateLocation -- Could not create the URL with the data provided: " + ex.Message);

                    return null;
                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory("ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("UserGet -- Could not create URL from data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nUpdateLocation -- Could not create the URL with the data provided: " + ex.Message);

                return null;
            }

            #endif
        }

        public bool SaveUser(string username, JToken model)
        {
			#if DEBUG
            try
            {
                string userID = (string)model["userID"]; // houseID is the correct key and is type UInt64
                string passWord = (string)model["Password"];   // roomID is the correct key and is type UInt64
                int[] houseIDs = (int[])model["houseIDs"]; // Type is the correct key and is type string
            }
            catch (Exception e){ // catches the exception if any of the keys are missing    
				Console.WriteLine(e.Source);
                return false;
            }
			return true;
			#else
            try
            {
				WebRequest request = WebRequest.Create(pss_url + username);
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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory("ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Could not post user information to the Storage: " + we.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nUpdateLocation -- Could not create the URL with the data provided: " + we.Message);

                    return false;
                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory("ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("UserPost -- Could not create URL from data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nUpdateLocation -- Could not create the URL with the data provided: " + ex.Message);


                return false;
            }

			#endif

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
			#if DEBUG
			try
			{
				string time = model["time"];
				double lat = model["lat"];
				double lon = model["long"];
				double alt = model["alt"];
				string userID = model["userID"];
			}
			catch (Exception e){ // catches the exception if any of the keys are missing      
				Console.WriteLine(e.Source);
				return false;
			}

			return true;
			#else
            try
            {
				WebRequest request = WebRequest.Create(dm_url +"LocationChange");
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
                        Directory.CreateDirectory("ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("UpdateLocation -- Could not post location change to Decision System: " + we.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nUpdateLocation -- Could not create the URL with the data provided: " + we.Message);

                    return false;

                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory("ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("UpdateLocation -- Could not create the URL with the data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nUpdateLocation -- Could not create the URL with the data provided: " + ex.Message);

                return false;
            }

			#endif

            return true;
        }

        //Sends an updated position to the decison system and needs the nearest object to be brightened that can be brightened
        public bool Brighten(JObject model)
        {
			#if DEBUG
			try
			{
				string time = model["time"];
				double lat = model["lat"];
				double lon = model["long"];
				double alt = model["alt"];
				string userID = model["userID"];
				string command = model["brightenNearMe"];
			}
			catch (Exception e){ // catches the exception if any of the keys are missing  
				Console.WriteLine(e.Source);
				return false;
			}
			return true;

			#else

            try
            {
				WebRequest request = WebRequest.Create(dm_url + "LocationChange");
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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory("ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Brigthen -- Could not post information to Decision System: " + we.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nBrighten -- Could not post information to Decision System: " + we.Message);

                    return false;

                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory("ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Brigthen -- Could not create the URL with the data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nBrighten -- Could not create the URL with the data provided: " + ex.Message);

                return false;
            }

			#endif

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

