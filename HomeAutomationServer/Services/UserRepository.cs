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
		
        // deviceRepo has static url
        private string dm_url = DeviceRepository.decisionURL;
		private string pss_url = DeviceRepository.storageURL;
        
        private NotificationManager notificationManager;
        
        public UserRepository()
        {
            // of the array is one line of the file. 
            string[] lines = System.IO.File.ReadAllLines(@"C:\keys.txt");
            string accK = lines[0];
            string secK = lines[1];
            notificationManager = new NotificationManager(accK, secK, Amazon.RegionEndpoint.USEast1);
            notificationManager.init();
        }
        
        public string DeleteUser(string userid)
        {
            WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "BU/" + userid);
            request.ContentType = "application/json";
            request.Method = "GET";
            
            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "false";
                    }
                    
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string value = reader.ReadToEnd();
                    JObject j = JObject.Parse(value);
                    string platformAppArn = (string) j["platformAppArn"];
                    notificationManager.DeletePlatformApplication(platformAppArn);
                    //return "true";
                }
            }

            catch (WebException we)
            {
                return "false";
            }
            
            request = WebRequest.Create(DeviceRepository.storageURL + "A/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "false";
                    }
                }
            }

            catch (WebException we)
            {
                return "false";
            }
            
            return "true";
        }
        
        public string GetUserId(string username, string pass)
        {
            WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "IU/" + username + "/" + pass);
            request.ContentType = "application/json";
            request.Method = "GET";

            string userId = "";

            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }
                    
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userId = reader.ReadToEnd();
                    return userId;
                }
            }

            catch (WebException we)
            {
                throw we;
            }
        }
        
        public string PostDeviceToken(string username, string pass, string deviceToken)
        {
            string[] arn = new string[2]{"", ""};
            try
            {
                arn = notificationManager.createPlatformApplication(deviceToken, username);
            }
            
            catch (Exception e)
            {
                return "false";
            }
            
            WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "IU/" + username + "/" + pass);
            request.ContentType = "application/json";
            request.Method = "GET";

            string userId = "";

            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "false";
                    }
                    
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userId = reader.ReadToEnd();
                }
            }

            catch (WebException we)
            {
                return "false";
            }
            
            // Now POST the topic ARN here
            
            request = WebRequest.Create(DeviceRepository.storageURL + "UU/" + userId);
            request.ContentType = "application/json";
            request.Method = "POST";
            
            JObject jobject = new JObject();
            jobject["platformAppArn"] = arn[0];
            jobject["endPointArn"] = arn[1];
            string json = jobject.ToString();
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }
            
            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "false";
                    }
                }
            }

            catch (WebException we)
            {
                return "false";
            }
            
            return "true";
        }
        
        public string SendNotification(string username, string pass, string message)
        {
            WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "IU/" + username + "/" + pass);
            request.ContentType = "application/json";
            request.Method = "GET";

            string userId = "";

            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "false";
                    }
                    
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userId = reader.ReadToEnd();
                }
            }

            catch (WebException we)
            {
                return "false";
            }
            
            
            // Now Get the Json blob
            
            request = WebRequest.Create(DeviceRepository.storageURL + "BU/" + userId);
            request.ContentType = "application/json";
            request.Method = "GET";
            
            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "false";
                    }
                    
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string value = reader.ReadToEnd();
                    JObject j = JObject.Parse(value);
                    string endPointArn = (string) j["endPointArn"];
                    notificationManager.PublishNotification(endPointArn, message);
                    return "true";
                }
            }

            catch (WebException we)
            {
                return "false";
            }
        }

        public JObject GetUser(string username)
        {
            #if DEBUG
            JObject val = new JObject();
            val["test"] = "test1";
            return val;
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
                    LogFile.AddLog("UpdateLocation -- Could not create the URL with the data provided: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("UserGet -- Could not create URL from data provided: " + ex.Message + "\n");
                return null;
            }

            #endif
        }

        public string SaveUser(JObject model)
        {
			#if DEBUG
            try
            {
                string userID = (string)model["userID"]; // houseID is the correct key and is type UInt64
                string passWord = (string)model["Password"];   // roomID is the correct key and is type UInt64
                //int[] houseIDs = (int[])model["houseIDs"]; // Type is the correct key and is type string
                return "1";
            }
            catch (Exception e){ // catches the exception if any of the keys are missing    
				Console.WriteLine(e.Source);
                return "false";
            }
			#else
            try
            {
                //string userID = (string)model["userID"]; // houseID is the correct key and is type UInt64
                //string passWord = (string)model["Password"];   // roomID is the correct key and is type UInt64
				WebRequest request = WebRequest.Create(pss_url + "U");
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(model.ToString());
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
                            
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream);

                        return reader.ReadToEnd();
                    }
                }

                catch (Exception we)
                {
                    LogFile.AddLog("Could not post user information to the Storage: " + we.Message + "\n");
                    return "false";
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("UpdateLocation -- Could not create the URL with the data provided: " + ex.Message + "\n");
                return "false";
            }
            return "true";
#endif
        }

        //Sends an updated position to the decison system
        public bool OnUpdatePosition(JObject model)
        {

			#if DEBUG
			try
			{
				string time = model["time"].ToString();
				string lat = model["lat"].ToString();
				string lon = model["long"].ToString();
				string alt = model["alt"].ToString();
				string userID = model["userID"].ToString();
			}
			catch (Exception e){ // catches the exception if any of the keys are missing      
				Console.WriteLine(e.Source);
				return false;
			}

			return true;
			#else
            //string time = model["time"].ToString();
            
            //string userID = model["userID"].ToString();
            try
            {
                double lat = (double)model["lat"];
                double lon = (double)model["lon"];
                double alt = (double)model["alt"];
                model["lat"] = lat;
                model["lon"] = lon;
                model["alt"] = alt;
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
#endif
        }

        //Sends an updated position to the decison system and needs the nearest object to be brightened that can be brightened
        public bool Brighten(JObject model)
        {
			#if DEBUG
			try
			{
				string time = model["time"].ToString();
				double lat = (double) model["lat"];
				double lon = (double) model["long"];
				double alt = (double) model["alt"];
				string userID = model["userID"].ToString();
				string command = model["brightenNearMe"].ToString();
			}
			catch (Exception e){ // catches the exception if any of the keys are missing  
				Console.WriteLine(e.Source);
				return false;
			}
			return true;

			#else

            try
            {
                string time = model["time"].ToString();
				double lat = (double) model["lat"];
				double lon = (double) model["lon"];
				double alt = (double) model["alt"];
				string userID = model["userID"].ToString();
				string command = model["command-string"].ToString();
		        WebRequest request = WebRequest.Create(dm_url + "CommandsFromApp");
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
                LogFile.AddLog("Brigthen -- Could not create the URL with the data provided: " + ex.Message + "\n");
                return false;
            }
            return true;
#endif
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

