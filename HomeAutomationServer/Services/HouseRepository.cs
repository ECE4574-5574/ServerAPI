using HomeAutomationServer.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

using System.Text;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

// This class tells the controller how to process the HTTP commands

namespace HomeAutomationServer.Services
{
	public class HouseRepository
	{
        private string path = @"C:\ServerAPILogFile\logfile.txt";

		public JObject GetHouse (string id)
		{
            try
            {
                WebRequest request = WebRequest.Create(DeviceRepository.storageURL + "/HI/" + id);
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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Failed to Get house data from Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nFailed to Get house data from Storage: " + ex.Message);
                    return null;
                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("GetHouse -- Failed to create URL with the provided data: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nGetHouse -- Failed to create URL with the provided data: " + ex.Message);
                return null;
            }
		}

		public bool postState(JObject deviceBlob)
		{

			//POST UD/HOUSEID/ROOMID/DEVICEID

			UInt64 houseID;
			UInt64 roomID;
			UInt64 deviceID;

			houseID = (UInt64)deviceBlob["HouseID"];
			roomID = (UInt64)deviceBlob["RoomID"];
			deviceID = (UInt64)deviceBlob["DeviceID"];

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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("House -- Failed to Post state change to Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nHouse -- Failed to Post state change to Storage: " + ex.Message);
                    return false;
                }

				if(!AppCache.AddDeviceBlob(deviceBlob))
					throw new Exception("AppCache add device failed when adding: " + deviceBlob.ToString());
				// ping app here
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("DeviceStateHouse -- Failed to create URL from data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDeviceStateHouse -- Failed to create URL from data provided: " + ex.Message);
                return false;
            }

			return true;
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
