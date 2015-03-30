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
        public IEnumerable<House> GetAllHouses()
        {
            //IEnumerable<House> houseEnumerable;
            //houseEnumerable = getAllHouses();             // Persistent storage getAllHouses() method
            //return houseEnumerable;
            return null;

        }

        public JObject GetHouse(string id)
        {
             WebRequest request = WebRequest.Create("http://54.152.190.217:8080/HI/" + id);
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

                string houseString = JsonConvert.DeserializeObject<string>(reader.ReadToEnd());
                return JObject.Parse(@"{user: 'test', password: 'bla'}");
            }
        }

        public JToken SaveHouse(/*House house*/ string houseId, JToken model)
        {
            WebRequest request = WebRequest.Create("http://54.152.190.217:8080/H/" + houseId);
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
            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception(String.Format(
                "Server error (HTTP {0}: {1}).",
                response.StatusCode,
                response.StatusDescription));

            return model;
        }

        public Exception UpdateHouse(int id, string name, int userId)
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

        }

        public Exception DeleteHouse(int id)
        {
            //if (getHouse(id) == null)                 // Persistent storage getHouse() method
            //    return new Exception("House with House Id: " + id + " not found");
            //else
            //{
            //    removeHouse(id);                      // Persistent storage removeHouse() method
            //    return new Exception("deleted");
            //}
            return null;

        }
    }
}
