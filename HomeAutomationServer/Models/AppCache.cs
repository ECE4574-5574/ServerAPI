using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Filters;
using System.IO;

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
	{                      // information request.

		// A JSON array of device blobs
		private static Dictionary<FullID, Device> deviceBlobs = new Dictionary<FullID, Device>();
		// add any other JArrays containing blobs here

		////////////////////////////////////////////////////////////////////////////////////////
		//
		// deviceBlobs methods

		/*************** DEBUG MODE METHOD *************/

		static public bool AddDeviceBlob_DEBUG(JObject blob)
		{
			string blob_string = blob.ToString ();
			Device dev = Interfaces.CreateDevices(blob_string, new TimeFrame()); // to convert the JSON blob to an actual Device object.

			ulong devID = (ulong)blob["deviceID"];
			ulong roomID = (ulong)blob["roomID"];
			ulong houseID = (ulong)blob["houseID"];

			FullID fullID = new FullID();
			fullID.DeviceID = devID;
			fullID.RoomID = roomID;
			fullID.HouseID = houseID;

			deviceBlobs.Add(fullID, dev);

			if (deviceBlobs.Contains(dev)) {
				return true;
			}

			return false;
		}

		/*************** END DEBUG MODE METHOD *************/

		static public bool AddDeviceBlob(JObject blob)
		{
			string blob_string = blob.ToString ();
			Device dev = Interfaces.CreateDevices(blob_string, new TimeFrame()); // to convert the JSON blob to an actual Device object.

			ulong devID = (ulong)blob["deviceID"];
			ulong roomID = (ulong)blob["roomID"];
			ulong houseID = (ulong)blob["houseID"];

			FullID fullID = new FullID();
			fullID.DeviceID = devID;
			fullID.RoomID = roomID;
			fullID.HouseID = houseID;

			deviceBlobs.Add(fullID, dev);

			if (deviceBlobs.Contains(dev)) {

				// Arjun -- add push notification code here

				return true;
			}

			return false;
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
			foreach(Device dev in deviceBlobs)
			{
				JToken blob = dev.ToString();
				blobs.Add(blob);
			}
			deviceBlobs.Clear();
			return blobs.ToArray; 
		}

		static public int GetBlobCount()
		{
			return deviceBlobs.Count;
		}

	}
}
