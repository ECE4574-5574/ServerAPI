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
using api;
using Hats.Time;
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
                WebRequest request = WebRequest.Create(storageURL + "DD/" + houseid + "/" + spaceid + "/" + deviceid);
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
                    UInt64 id2 = (UInt64)(blobVal["ID"]);
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


        public bool updateSimulation(UInt64 houseID, UInt64 roomID, UInt64 deviceID, JObject sendData) 
        { 
           // UInt64 test = fullId.HouseID;
#if DEBUG
#else
            HouseRepository houseRepo = new HouseRepository();
            JObject houseObj = new JObject();
            JObject deviceObj = new JObject();
            String HouseString, DeviceString, commands;
            var id = new FullID();
            id.HouseID = houseID;
            id.DeviceID = deviceID;
            id.RoomID = roomID;
    
            WebRequest request = WebRequest.Create(storageURL + "UD/" + houseID + "/" + roomID +  "/"  + deviceID);
            request.ContentType = "application/json"; 
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(sendData.ToString());
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
            }

            houseObj = houseRepo.GetHouse(houseID); // queries PS for information
            deviceObj = GetDevice(houseID.ToString(), roomID.ToString(), deviceID.ToString());
            JToken blobVal;
            //String blob = (string)deviceArr["blob"];
            //blobVal = JToken.Parse(blob);
            String house_url = (string)(houseObj["house_url"]);
            UInt64 ID = (UInt64)(deviceObj["ID"]);
            String clas = (string)(deviceObj["Class"]);
            JObject send = new JObject();
            JObject send1 = new JObject();
            send["house_url"] = house_url;
            send1["ID"] = ID;
            send1["Class"] = clas;
            TimeFrame frame = new TimeFrame();
            HouseString = JsonConvert.SerializeObject(send);
            DeviceString = JsonConvert.SerializeObject(send1);
            commands = JsonConvert.SerializeObject(sendData);

            var dev_out = ServerSideAPI.CreateDevice(id, HouseString, DeviceString, frame);
            if (dev_out != null) //Good to go
            {
                //state changed means some value was changed, e.g. the command was not idempotent
                var state_changed = Interfaces.UpdateDevice(dev_out, commands);
            }
#endif
            return true;    
        }

        public bool updateActual(UInt64 houseID, UInt64 roomID, UInt64 deviceID, JObject sendData)
        {

#if DEBUG
#else
            updateSimulation(houseID,roomID,deviceID,sendData);

            WebRequest request = WebRequest.Create(decisionURL + "DeviceState/");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(sendData.ToString());
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
            }
#endif
            return true;
        
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
               throw new Exception ("Device -- Keys are invalid or missing: " + ex.Message + "\n");
               //return 0;
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
                    throw new Exception("Device -- Failed to send information to the Storage: " + ex.Message + "\n");
                    //return 0;
                }
            }

            catch (SystemException ex) 
            {
                LogFile.AddLog("Device -- Failed to send POST request with the URL provided: " + ex.Message + "\n");
                throw new Exception("Device -- Failed to send information to the Storage: " + ex.Message + "\n");;
                //return 0;
            }

            return deviceId;
#endif
        }

        public bool DeleteDevice(string houseID, string spaceID, string deviceID)
        {
#if DEBUG
#else
            WebRequest request = WebRequest.Create(storageURL + "D/" + houseID + "/" + spaceID +  "/"  + deviceID);
            request.Method = "DELETE";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
            }
#endif
          return true;
        }
    }
}
