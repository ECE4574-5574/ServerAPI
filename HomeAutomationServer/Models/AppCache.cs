using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Filters;
using System.IO;
using HomeAutomationServer.Services;
using Hats.Time;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using api;

namespace HomeAutomationServer.Models
{
    static public class AppCache  // A cache to temporarily store app information while waiting on 
    {
        // information request.

        // A JSON array of device blobs
        private static Dictionary<FullID, Device> deviceBlobs = new Dictionary<FullID, Device>();
        // add any other JArrays containing blobs here

        ////////////////////////////////////////////////////////////////////////////////////////
        //
        // deviceBlobs methods

        /*************** DEBUG MODE METHOD *************/

        static public bool AddDeviceBlob_DEBUG(JObject blob)
        {
            Uri storageURL = new Uri(DeviceRepository.storageURL);
            Interfaces check = new Interfaces(storageURL);
            string blob_string = blob.ToString();
            Device dev = check.CreateDevice(blob_string, new TimeFrame());
            ulong devID = (ulong)blob["deviceID"];
            ulong roomID = (ulong)blob["roomID"];
            ulong houseID = (ulong)blob["houseID"];

            FullID fullID = new FullID();
            fullID.DeviceID = devID;
            fullID.RoomID = roomID;
            fullID.HouseID = houseID;

            deviceBlobs.Add(fullID, dev);
            //if (deviceBlobs.Contains(fullID, dev))
            //{
            //    return true;
            //}

            return true;
        }

        /*************** END DEBUG MODE METHOD *************/

        static public bool AddDeviceBlob(JObject blob)
        {
            Uri storageURL = new Uri(DeviceRepository.storageURL);
            Interfaces check = new Interfaces(storageURL);
            string blob_string = blob.ToString();
            Device dev = check.CreateDevice(blob_string, new TimeFrame());
            ulong devID = (ulong)blob["deviceID"];
            ulong roomID = (ulong)blob["roomID"];
            ulong houseID = (ulong)blob["houseID"];

            FullID fullID = new FullID();
            fullID.DeviceID = devID;
            fullID.RoomID = roomID;
            fullID.HouseID = houseID;

            bool notify = false;
            deviceBlobs.Add(fullID, dev);
            //if (deviceBlobs.Contains(fullID, dev))
            //{
            //    notify = true;
            //}

            if (!notify)
                return true;

            WebRequest request = WebRequest.Create(DeviceRepository.decisionURL + "BH/" + houseID);
            request.Method = "GET";
            string userid = "";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JObject jobject = JObject.Parse(str);
                    userid = (string) jobject["userID"];
                }
            }

            catch (WebException we)
            {
                return true;
            }

            request = WebRequest.Create(DeviceRepository.decisionURL + "BU/" + userid);
            request.Method = "GET";
            string endpoint = "";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JObject jobject = JObject.Parse(str);
                    endpoint = (string)jobject["endPointArn"];

                    // of the array is one line of the file. 
                    //string[] lines = System.IO.File.ReadAllLines(@"C:\keys.txt");
                    string accK = "";
                    string secK = "";
                    NotificationManager notificationManager = new NotificationManager(accK, secK, Amazon.RegionEndpoint.USEast1);
                    notificationManager.init();
                    notificationManager.PublishNotification(endpoint, "Device Updated");
                }
            }

            catch (WebException we)
            {
                return true;
            }


            return true;
        }

        static public JToken GetDeviceBlob(FullID fullID)
        {
            Device dev = deviceBlobs[fullID];
            deviceBlobs.Remove(fullID);
            return dev.ToString(); // implicit conversion
        }

        static public JArray GetAllBlobs()
        {
            List<JToken> blobs = new List<JToken>();
            foreach (Device dev in deviceBlobs.Values)
            {
                JToken blob = dev.ToString();
                blobs.Add(blob);
            }
            deviceBlobs.Clear();
            return JArray.Parse(blobs.ToString());
        }

        static public int GetBlobCount()
        {
            return deviceBlobs.Count;
        }

    }
}
