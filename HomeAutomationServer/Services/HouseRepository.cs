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

        public JObject GetHouse(UInt64 id)
        {
#if DEBUG
            JObject value = new JObject();
            value["houseID"] = 12;
            value["deviceID"] = 12;
            return value;

#else
            try
            {
                WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "/BH/" + id);
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

                        string houseString = reader.ReadToEnd();
                        return JObject.Parse(houseString);
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Failed to Get house data from Storage: " + ex.Message + "\n");
                    return null;
                }
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("GetHouse -- Failed to create URL with the provided data: " + ex.Message = "\n");
                return null;
            }
#endif
        }

        public bool PostState(JObject deviceBlob)
        {

            //POST UD/HOUSEID/ROOMID/DEVICEID

            UInt64 houseID;
            UInt64 roomID;
            UInt64 deviceID;
#if DEBUG
            try
            {
                houseID = (UInt64)deviceBlob["houseID"];
                roomID = (UInt64)deviceBlob["roomID"];
                deviceID = (UInt64)deviceBlob["deviceID"];
            }
            catch (WebException ex)
            {
                LogFile.AddLog("House -- Could not access correct keys: " + ex.Message + "\n");
                return false;
            }
            return true;
#else
	    houseID = (UInt64)deviceBlob["houseID"];
	    roomID = (UInt64)deviceBlob["roomID"];
	    deviceID = (UInt64)deviceBlob["deviceID"];
        

            try
            {
                WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "UD/" + houseID + "/" + roomID + "/" + deviceID);
                request.ContentType = "application/json";
                request.Method = "POST";

                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(deviceBlob.ToString());
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception(String.Format(
                                "Server error (HTTP {0}: {1}).",
                                response.StatusCode,
                                response.StatusDescription));
                        }
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("House -- Failed to Post state change to Storage: " + ex.Message + "\n");
                    return false;
                }

		try 
		{
#if DEBUG
			if(!AppCache.AddDeviceBlob_DEBUG(deviceBlob))
				throw new Exception("AppCache add device failed when adding: " + deviceBlob.ToString());
#else
		    if(!AppCache.AddDeviceBlob(deviceBlob))
		        throw new Exception("AppCache add device failed when adding: " + deviceBlob.ToString());
#endif
		}		
		catch (Exception ex)
		{
            LogFile.AddLog("House -- " + ex.Message + "\n");
                    return false;
		}
	    }

            catch (SystemException ex)
            {
                LogFile.AddLog("DeviceStateHouse -- Failed to create URL from data provided: " + ex.Message + "\n");
                return false;
            }

	    return true;
#endif
        }

        public UInt64 SaveHouse(JToken model)
        {
            UInt64 houseId;
            WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "H");
            request.ContentType = "application/json";
            request.Method = "POST";

#if DEBUG
            return 1;
#else
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

                    return UInt64.Parse(reader.ReadToEnd());
                }
            }

            catch (Exception ex)
            {
                LogFile.AddLog("House -- Could not post house to the server: " + ex.Message + "\n");
                return 0;
            }
#endif
        }

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
