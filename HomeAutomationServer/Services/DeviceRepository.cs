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
#if DEBUG
        public static string decisionURL = "http://localhost:8085/";
        public static string storageURL = "http://localhost:8080/";
#else
		public static string decisionURL = "http://52.5.26.12:8085/";
		public static string storageURL = "http://ec2-52-11-96-207.us-west-2.compute.amazonaws.com:8080/";
#endif
        public JObject GetDevice(string houseid, string spaceid, string deviceid)       // Return device
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "/DD" + houseid + "/" + spaceid + "/" + deviceid);
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

                        string deviceString = reader.ReadToEnd();
                        return JObject.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Device -- Failed to get data to the Storage: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Device -- Failed to create request with the URL provided: " + ex.Message + "\n");
                return null;
            }
        }

        public JArray GetDevice(string houseid, string spaceid)            // Return devices in space
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "RD/" + houseid + "/" + spaceid);
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

                        string deviceString = reader.ReadToEnd();
                        return JArray.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Device -- Failed to get data to the Storage: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Device -- Failed to create request with the URL provided: " + ex.Message + "\n");
                return null;
            }
        }

        public JArray GetDevice(string houseid, string spaceid, int type)      // Return devices in space by type
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "RT/" + houseid + "/" + spaceid + "/" + type);
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

                        string deviceString = reader.ReadToEnd();
                        return JArray.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Device -- Failed to get data to the Storage: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Device -- Failed to create request with the URL provided: " + ex.Message + "\n");
                return null;
            }
        }

        public JArray GetDevice(string houseid)            // Return devices in house
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "HD/" + houseid);
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

                        string deviceString = reader.ReadToEnd();
                        return JArray.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Device -- Failed to get data to the Storage: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Device -- Failed to create request with the URL provided: " + ex.Message + "\n");
                return null;
            }
        }

        public JArray GetDevice(string houseid, int type)          // Return devices in house by type
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "HT/" + houseid + "/" + type);
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

                        string deviceString = reader.ReadToEnd();
                        return JArray.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Device -- Failed to get data to the Storage: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Device -- Failed to send GET request with the URL provided: " + ex.Message + "\n");
                return null;
            }
        }

        public JArray SendUnregisteredDevice(string houseid)
        {
            JArray allDevices;
            JArray devicesInStorage;
            JArray unregisteredDevices = new JArray();
#if DEBUG
            JObject test = new JObject();
            test["Enabled"] = "false";
            test["Value"] = 0;
            test["ID"] = 0;
            test["LastUpdate"] = "2015-04-29T14:15:58.0088064";
            test["Name"] = "light";
            test["Class"] = "lightSwitch";
            unregisteredDevices.Add(test);
#else
            //first request all the devices from the house
            WebRequest request = WebRequest.Create("http://localhost:8081/api/device"); // this is local house uri
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
                allDevices =  JArray.Parse(deviceString);
            }
            devicesInStorage = GetDevice(houseid);
            for (int i = 0; i < allDevices.Count(); i++)
            {
                UInt64 id = (UInt64)(allDevices[i]["ID"]);
                if (devicesInStorage.Count() == 0)
                    return allDevices;
                for (int j = 0; j < devicesInStorage.Count(); j++)
                {
                    JToken blobVal;
                    String blob = (string)devicesInStorage[j]["blob"];
                    //if (blob.Contains("I"))
                    //{
                    //    int h;
                    //    h = 1;
                    //}
                    
                    blobVal = JToken.Parse(blob);
                    UInt64 id2 = (UInt64)(blobVal[j]["ID"]);
                    if (id != id2)
                    {
                        unregisteredDevices.Add(allDevices[i]);
                        break;
                    }
                }
            }
            
#endif
            return unregisteredDevices;
        }


        public UInt64 SaveDevice(JObject model)     // Returns the device ID from the Storage which is type UInt64
        {
            UInt64 houseId, roomId;
            string deviceType;
            UInt64 deviceId;
#if DEBUG
            try
            {
                houseId = (UInt64)model["houseID"]; // houseID is the correct key and is type UInt64
                roomId = (UInt64)model["roomID"];   // roomID is the correct key and is type UInt64
                deviceType = (string)model["Type"]; // Type is the correct key and is type string
            }
            catch (Exception ex)
            { // catches the exception if any of the keys are missing      
                LogFile.AddLog("Device -- Keys are invalid or missing: " + ex.Message + "\n");
                return 0;
            }

            return 1;
            //model.TryGetValue("houseID", houseId);
            //model.TryGetValue()
#else

            try {
            houseId = (UInt64)model["houseID"]; // houseID is the correct key and is type UInt64
            roomId = (UInt64)model["roomID"];   // roomID is the correct key and is type UInt64
            deviceType = (string)model["Type"]; // Type is the correct key and is type string
            }
            catch (Exception ex)
            {
               LogFile.AddLog("Device -- Keys are invalid or missing: " + ex.Message + "\n");
               return 0;
            }

            try
            {
                WebRequest request = WebRequest.Create(storageURL + "D/" + houseId + "/" + roomId + "/" + deviceType);
                request.Method = "POST";

                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(model.ToString());
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream);

                        deviceId = UInt64.Parse(reader.ReadToEnd());
                        //JObject houseObject = JObject.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Device -- Failed to send information to the Storage: " + ex.Message + "\n");
                    return 0;
                }
            }

            catch (SystemException ex) 
            {
                LogFile.AddLog("Device -- Failed to send POST request with the URL provided: " + ex.Message + "\n");
                return 0;
            }

            return deviceId;
#endif
        }

        public JObject DeleteDevice(string houseid, string spaceid, string deviceid)
        {
            /*WebRequest request = WebRequest.Create("http://54.152.190.217:8081/HD/" + houseid);
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
    }
}
