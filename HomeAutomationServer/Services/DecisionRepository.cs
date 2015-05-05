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

//using HomeAutomationServer.Models;

namespace HomeAutomationServer.Services
{
    public class DecisionRepository
    {
        private string houseApiHost = "http://house_address:house_port/device/";

        public bool PostCommand(JObject model)
        {
            WebRequest request = WebRequest.Create(DeviceRepository.decisionURL + "/CommandsFromApp");
            request.ContentType = "application/json";
            request.Method = "POST";

            string json = model.ToString();

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Close();
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        return true;
                    else
                        return false;
                }
            }

            catch (WebException we)
            {
                return false;
            }
        }

        public bool StateUpdate(JObject model)
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
                LogFile.AddLog("Decision -- Invalid Keys: " + ex.Message + "\n");
                return false;
            }

#else
            
            try
            {
                houseId = (UInt64)model["houseID"]; // houseID is the correct key and is type UInt64
                roomId = (UInt64)model["roomID"];   // roomID is the correct key and is type UInt64
                deviceType = (string)model["Type"]; // Type is the correct key and is type string
				WebRequest request = WebRequest.Create(houseApiHost + "/" + houseId + "/" + roomId + "/" + deviceType);
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
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Decision -- Failed to send data to the House System: " + ex.Message + "\n");
                    return false;
                }

                request = WebRequest.Create(DeviceRepository.storageURL + "UD/" + (UInt64)model["HouseID"] + "/" + (UInt64)model["RoomID"] + "/" + (UInt64)model["DeviceID"]);
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
                    }
                }

                catch (Exception ex)
                {
                    LogFile.AddLog("Decision -- Failed to send data to the Storage: " + ex.Message + "\n");
                    return false;
                }

                try
                {
#if DEBUG
					if (!AppCache.AddDeviceBlob_DEBUG(model))
						throw new Exception("Decision -- AppCache add device failed when adding: " + model.ToString());
#else
                    if (!AppCache.AddDeviceBlob(model))
                        throw new Exception("Decision -- AppCache add device failed when adding: " + model.ToString());	
#endif
                }

                catch (Exception ex)
                {
                    LogFile.AddLog(ex.Message + "\n");
                    return false;
                }
            }

            catch(SystemException ex)
            {
                LogFile.AddLog("Decision -- Could not create the specified url with the data provided: " + ex.Message + "\n");
                return false;
            }
#endif

            return true;
        }

        public bool GetState(JObject model)
        {
#if DEBUG
            return true;
#else
			// parse the JSON
			UInt64 houseID = (UInt64)model["houseID"];
			UInt64 roomID = (UInt64)model["roomID"];
			UInt64 deviceID = (UInt64)model["deviceID"];
			string deviceClass = (string)model["deviceClass"];
			string houseURL = (string)model["houseURL"];
			// done parsing JSON

			TimeFrame frame = new TimeFrame();

			var id = new FullID();
			id.HouseID = houseID;
			id.RoomID = roomID;
			id.DeviceID = deviceID;

			JObject jHouseString = new JObject();
			jHouseString["houseURL"] = houseURL;
			string HouseString = jHouseString.ToString();

			JObject jDeviceString = new JObject();
			jDeviceString["ID"] = Convert.ToString(deviceID);
			jDeviceString["Class"] = deviceClass;
			string DeviceString = jDeviceString.ToString();
			
			var dev_out = ServerSideAPI.CreateDevice(id, HouseString, DeviceString, frame);

			if(dev_out != null) //Good to go
			{
			//state changed means some value was changed, e.g. the command was not idempotent
			var ok = dev_out.update();
			var json = JsonConvert.SerializeObject(dev_out); //this JSON blob should be sent to whomever wants updates
			AppCache.AddDeviceBlob(JObject.Parse(json));
			}

			return true; 

#endif
        }
    }
}
