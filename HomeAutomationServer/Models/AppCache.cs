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
	public static class AppCache  // A cache to temporarily store app information while waiting on 
	{                      // information request.
		// A JSON array of device blobs
		private static JArray deviceBlobs = new JArray();
		// add any other JArrays containing blobs here

		////////////////////////////////////////////////////////////////////////////////////////
		//
		// deviceBlobs methods

		public static bool AddDeviceBlob(JObject blob)
		{
			deviceBlobs[(string)blob["deviceID"]] = blob;

			if (deviceBlobs[(string)blob["deviceID"]] == blob)
				return true;

			return false;
		}

		public static JToken GetDeviceBlob(string deviceID)
		{
			JToken blob = deviceBlobs[deviceID];
			deviceBlobs[deviceID].Remove();
			return blob;
		}

		public static JArray GetAllBlobs()
		{
			JArray blobs = deviceBlobs;
			deviceBlobs.RemoveAll();
			return blobs;
		}

		public static int GetBlobCount()
		{
			return deviceBlobs.Count;
		}

		////////////////////////////////////////////////////////////////////////////////////////
		//
		// other blob methods
	}
}
