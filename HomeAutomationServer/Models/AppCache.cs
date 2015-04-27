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
		private static Dictionary<string, Device> deviceBlobs = new Dictionary<string, Device>();
		// add any other JArrays containing blobs here

		////////////////////////////////////////////////////////////////////////////////////////
		//
		// deviceBlobs methods

		/*************** DEBUG MODE METHOD *************/

		static public bool AddDeviceBlob_DEBUG(JObject blob)
		{
			string blob_string = blob.ToString ();
			Device dev = Interfaces.DeserializeDevice(blob_string, null, null, new TimeFrame()); // to convert the JSON blob to an actual Device object.
			string devID = (string)blob["deviceID"];

			deviceBlobs.Add(devID, dev);

			if (deviceBlobs.Contains(dev)) {
				return true;
			}

			return false;
		}

		/*************** END DEBUG MODE METHOD *************/

		static public bool AddDeviceBlob(JObject blob)
		{
			string blob_string = blob.ToString ();
			Device dev = Interfaces.DeserializeDevice(blob_string, null, null, new TimeFrame()); // to convert the JSON blob to an actual Device object.
			string devID = (string)blob["deviceID"];

			deviceBlobs.Add(devID, dev);

			if (deviceBlobs.Contains(dev)) {
				try
				{
					AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient("AKIAJM2E3LGZHJYGFSQQ", "p3Qi8DAXj+XHAH+ny7HrlRyleBs5V5DJv77zKK3T", Amazon.RegionEndpoint.USEast1);
					snsClient.Publish("arn:aws:sns:us-east-1:336632281456:MyTopic", "New Device Updates");
				}

				catch (Exception ex)
				{
					LogFile.AddLog("AppCache -- Could not send Push Notification: " + ex.Message + "\n");
					return false;
				}

				return true;
			}

			return false;
		}

		static public JToken GetDeviceBlob(string deviceID)
		{
			Device dev = deviceBlobs[deviceID];
			deviceBlobs.Remove(deviceID);
			return dev.ToString; // implicit conversion
		}

		static public JArray GetAllBlobs()
		{
			List<JToken> blobs = new List<JToken>();
			foreach(Device dev in deviceBlobs)
			{
				JToken blob = dev.ToString;
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
