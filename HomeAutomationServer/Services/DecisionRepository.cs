using HomeAutomationServer.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using api;

namespace HomeAutomationServer.Services
{
    public class DecisionRepository
    {
        private string houseApiHost = "";
        private string appApiHost = "";
        static Uri serverUri;
        private Interfaces deviceInterface = new Interfaces(serverUri);

        public bool StateUpdate(UInt64 deviceid, bool state)
        {
            string deviceString;

            WebRequest request = WebRequest.Create(houseApiHost);
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

                deviceString = reader.ReadToEnd();
            }

            Device myDevice = Interfaces.DeserializeDevice(deviceString, null, null);
            //myDevice.Enable = state;
            JObject device = JObject.Parse(myDevice.ToString());
            //send to APP cache
            // send to Storage

            return true;
        }

        public bool GetState(UInt64 deviceid)
        {
            JObject device = new JObject();

            WebRequest request = WebRequest.Create(houseApiHost);
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
                 device = JObject.Parse(deviceString);
            }

            return (bool)device["Enabled"];
        }
    }
}
