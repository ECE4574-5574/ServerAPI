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

//This class serves as the data storage space. It is just a list of Device items that are regenerated from the cache

namespace HomeAutomationServer.Services
{
    public class DeviceRepository
    {
        string decisionURL = "http://LocalHost:8081/";
        string storageURL = "http://54.152.190.217:8080/";

        public JObject GetDevice(string houseid, string spaceid, string deviceid)       // Return device
        {
           WebRequest request = WebRequest.Create(storageURL + "/DD" + houseid + "/" + spaceid + "/" + deviceid);
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

               string deviceString = reader.ReadToEnd();
               return JObject.Parse(deviceString);
           }
        }

        public JArray GetDevice(string houseid, string spaceid)            // Return devices in space
        {
            WebRequest request = WebRequest.Create(storageURL + "RD/" + houseid + "/" + spaceid);
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

               string deviceString = reader.ReadToEnd();
               return JArray.Parse(deviceString);
           }
        }

        public JArray GetDevice(string houseid, string spaceid, int type)      // Return devices in space by type
        {
            WebRequest request = WebRequest.Create(storageURL + "RT/" + houseid + "/" + spaceid + "/" + type);
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

                string deviceString = reader.ReadToEnd();
                return JArray.Parse(deviceString);
            }
        }

        public JArray GetDevice(string houseid)            // Return devices in house
        {
            WebRequest request = WebRequest.Create(storageURL + "HD/" + houseid);
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

                string deviceString = reader.ReadToEnd();
                return JArray.Parse(deviceString);
            }
        }

        public JArray GetDevice(string houseid, int type)          // Return devices in house by type
        {
            WebRequest request = WebRequest.Create(storageURL + "HT/" + houseid + "/" + type);
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

                string deviceString = reader.ReadToEnd();
                return JArray.Parse(deviceString);
            }
        }

        public bool saveDevice(JObject model)
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
                JObject houseObject = JObject.Parse(deviceString);
            }
              
            int version;
            // Get version from JObject
            WebRequest request = WebRequest.Create(http://someurldecisionteam); //"http://54.152.190.217:8080/D/" + houseid + "/" + version + "/" + spaceid + "/" + deviceid);
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

            request = WebRequest.Create("http://54.152.190.217:8081/DI/" + houseid + "/" + version + "/" + spaceid + "/" + deviceid);
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

                string deviceString = reader.ReadToEnd();
                return JObject.Parse(deviceString);
            }*/
            return true;
        }

        /*public bool sendObject(JObject model)
        {
            WebRequest request = WebRequest.Create(decisionURL + );
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

                string deviceString = reader.ReadToEnd();
                JObject houseObject = JObject.Parse(deviceString);
            }
        }*/

        public JObject DeleteDevice(string houseid, string spaceid, string deviceid)
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
                JObject houseObject = JObject.Parse(deviceString);
            }
              
            int version;
            // Get version from JObject
             
            request = WebRequest.Create("http://54.152.190.217:8081/D/" + houseid + "/" + version + "/" + spaceid + "/" + deviceid);
            request.ContentType = "application/json";
            request.Method = "DELETE";

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
            }*/

            return null;
        }

        /*public Exception UpdateDevice(int id, string name, int type, int spaceId)
        {
            //Device device = new Device();
            //device = getDevice(id);                             // Persistent storage getDevice() method

            //if (device == null)
            //    return new Exception("Device with Device Id: " + id + " not found");

            //if (name != "")
            //    device.DeviceName = name;

            //if (type != -1)
            //    device.DeviceType = type;

            //if (spaceId != -1)
            //    device.SpaceId = spaceId;

            //updateDevice(device);                                   // Persistent storage updateDevice() method
            //return new Exception("updated");
            return null;

        }

        public Exception UpdateDevice(Device device)
        {
            //if (getDevice(device.DeviceId) == null)                         // Persistent storage getDevice() method
            //    return new Exception("Device with Device Id: " + device.DeviceId + " not found");
            //else
            //{
            //    updateDevice(device);                                   // Persistent storage updateDevice() method
            //    return new Exception("updated");
            //}
            return null;

        }*/
    }
}