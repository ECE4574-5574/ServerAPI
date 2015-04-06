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
    public class SpaceRepository
    {
        public JObject GetSpace(string houseid, string spaceid)
        {
            /*WebRequest request = WebRequest.Create("http://54.152.190.217:8081/RI/" + houseid + "/" + spaceid);
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

                string spaceString = reader.ReadToEnd();
                return JObject.Parse(spaceString);
            }*/

            return null;
        }

        public JObject SaveSpace(string houseid, string spaceid, JToken model)
        {
            /*WebRequest request = WebRequest.Create("http://54.152.190.217:8081/HI/" + houseid);
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
                JObject houseObject = JObject.Parse(spaceString);
            }
              
            int version;
            // Get version from JObject
             
            request = WebRequest.Create("http://54.152.190.217:8081/R/" + houseid + "/" + version + "/" + spaceid);
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

            request = WebRequest.Create("http://54.152.190.217:8081/RI/" + houseid + "/" + version + "/" + spaceid);
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

                string spaceString = reader.ReadToEnd();
                return JObject.Parse(spaceString);
            }*/
            return null;
        }

        public JObject DeleteSpace(string houseid, string spaceid)
        {
            /*WebRequest request = WebRequest.Create("http://54.152.190.217:8081/HI/" + houseid);
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
                JObject houseObject = JObject.Parse(spaceString);
            }
              
            int version;
            // Get version from JObject
              
            request = WebRequest.Create("http://54.152.190.217:8081/R/" + houseid + "/" + version + "/" + spaceid);
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

                string spaceString = reader.ReadToEnd();
                return JObject.Parse(spaceString);
            }*/
            return null;

        }

        /*public Exception UpdateSpace(int id, string name, int type, int houseId)
        {
            //Space space = new Space();
            //space = getSpace(id);                             // Persistent storage getSpace() method

            //if (space == null)
            //    return new Exception("Space with Space Id: " + id + " not found");

            //if (name != "")
            //    space.SpaceName = name;

            //if (type != -1)
            //    space.SpaceType = type;

            //if (houseId != -1)
            //    space.HouseId = houseId;

            //updateSpace(space);                                   // Persistent storage updateSpace() method
            return null;

            //return new Exception("updated");
        }

        public Exception UpdateSpace(Space space)
        {
            //if (getSpace(space.SpaceId) == null)                         // Persistent storage getSpace() method
            //    return new Exception("Space with Space Id: " + space.SpaceId + " not found");
            //else
            //{
            //    updateSpace(space);                                   // Persistent storage updateSpace() method
            //    return new Exception("updated");
            //}
            return null;

        }*/
    }
}
