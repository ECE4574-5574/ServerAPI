using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Filters;

namespace HomeAutomationServer.Models
{
    public class AppCache  // A cache to temporarily store app information while waiting on 
    {                      // information request.
        // A JSON array of device blobs
        private JArray deviceBlobs = new JArray();
        // add any other JArrays containing blobs here
        
        ////////////////////////////////////////////////////////////////////////////////////////
        //
        // deviceBlobs methods

        public bool AddDeviceBlob(JObject blob)
        {
            deviceBlobs[(string)blob["deviceID"]] = blob;
            
            if (deviceBlobs[(string)blob["deviceID"]] == blob)
                return true;

            return false;
        }

        public JToken GetDeviceBlob(string deviceID)
        {
            return deviceBlobs[deviceID];
        }

        public JArray GetAllBlobs()
        {
            return deviceBlobs;
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        //
        // other blob methods
    }
}
