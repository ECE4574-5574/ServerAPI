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
    public class HouseRepository
    {
        public JObject GetHouse(string id)
        {
             WebRequest request = WebRequest.Create("http://54.152.190.217:8081/HI/" + id);
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

                string houseString = reader.ReadToEnd();
                return JObject.Parse(houseString);
            }
        }

        /*public JObject SaveHouse(string houseId, JToken model)
        {
            WebRequest request = WebRequest.Create("http://54.152.190.217:8081/H/" + houseId);
            request.ContentType = "application/json";
            request.Method = "POST";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(model.ToString());
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
            }

            request = WebRequest.Create("http://54.152.190.217:8081/HI/" + houseId);
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

                string houseString = reader.ReadToEnd();
                return JObject.Parse(houseString);
            }
            return null;
        }*/

        public JObject DeleteHouse(string houseid)
        {
            /*WebRequest request = WebRequest.Create("http://54.152.190.217:8081/H/" + houseid);
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

               string houseString = reader.ReadToEnd();
               return JObject.Parse(houseString);
           }*/

            return null;
        }

        /*public Exception UpdateHouse(int id, string name, int userId)
        {
            //House house = new House();
            //house = getHouse(id);                             // Persistent storage getHouse() method

            //if (house == null)
            //    return new Exception("House with House Id: " + id + " not found");

            //if (name != "")
            //    house.HouseName = name;

            //if (userId != -1)
            //    house.UserId = userId;

            //updateHouse(house);                                   // Persistent storage updateHouse() method
            //return new Exception("updated");
            return null;

        }

        public Exception UpdateHouse(House house)
        {
            //if (getHouse(house.HouseId) == null)                         // Persistent storage getHouse() method
            //    return new Exception("House with House Id: " + house.HouseId + " not found");
            //else
            //{
            //    updateHouse(house);                                   // Persistent storage updateHouse() method
            //    return new Exception("updated");
            //}
            return null;

        }*/
    }
}
