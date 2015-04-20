using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Filters;
using System.Text;
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

namespace HomeAutomationServer.Models
{
    static public class AppCache  // A cache to temporarily store app information while waiting on 
    {                      // information request.

        private static string path = @"C:\ServerAPILogFile\logfile.txt";
        // A JSON array of device blobs
        private static JArray deviceBlobs = new JArray();
        // add any other JArrays containing blobs here
        
        ////////////////////////////////////////////////////////////////////////////////////////
        //
        // deviceBlobs methods

		/*************** DEBUG MODE METHOD *************/

		static public bool AddDeviceBlob_DEBUG(JObject blob)
		{
			deviceBlobs[(string)blob["deviceID"]] = blob;

			if (deviceBlobs [(string)blob ["deviceID"]] == blob) {
				return true;
			}

			return false;
		}

		/*************** END DEBUG MODE METHOD *************/

        static public bool AddDeviceBlob(JObject blob)
        {
            deviceBlobs[(string)blob["deviceID"]] = blob;

            if (deviceBlobs[(string)blob["deviceID"]] == blob)
            {
                try
                {
                    AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient("AKIAJM2E3LGZHJYGFSQQ", "p3Qi8DAXj+XHAH+ny7HrlRyleBs5V5DJv77zKK3T", Amazon.RegionEndpoint.USEast1);
                    snsClient.Publish("arn:aws:sns:us-east-1:336632281456:MyTopic", "New Device Updates");
                }

                catch (Exception ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("AppCache -- Failed to send Push Notification: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nAppCache -- Could not send Push Notification: " + ex.Message);
                    return false;
                }

                return true;
            }

            return false;
        }

        static public JToken GetDeviceBlob(string deviceID)
        {
            JToken blob = deviceBlobs[deviceID];
            deviceBlobs[deviceID].Remove();
            return blob;
        }

        static public JArray GetAllBlobs()
        {
            JArray blobs = deviceBlobs;
            deviceBlobs.RemoveAll();
            return blobs;
        }

        static public int GetBlobCount()
        {
            return deviceBlobs.Count;
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        //
        // other blob methods
    }
}
