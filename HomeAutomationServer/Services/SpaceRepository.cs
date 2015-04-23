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
	public class SpaceRepository
	{

		// returns room object or null if error
		public JObject GetSpace (string houseID, string spaceid)
		{

			try {

				WebRequest request = WebRequest.Create (DeviceRepository.storageURL + "BR/" + houseID + "/" + spaceid);
				request.Method = "GET";

				try {

					using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
						if (response.StatusCode != HttpStatusCode.OK)
							throw new Exception (String.Format (
								"Server error (HTTP {0}: {1}).",
								response.StatusCode,
								response.StatusDescription));
						var stream = response.GetResponseStream ();
						var reader = new StreamReader (stream);

						string spaceString = reader.ReadToEnd ();
						return JObject.Parse (spaceString);
					}

				} catch (Exception ex) {
					LogFile.AddLog ("Decision -- Could not Get the data from the House System: " + ex.Message + "\n");
					return null;
				}

			} catch (SystemException ex) {
				LogFile.AddLog ("Storage -- Could not create the specified url with the data provided: " + ex.Message + "\n");
				return null;
			}

		}


		public UInt64 SaveSpace (JObject model)
		{
			UInt64 houseId, roomID;
#if DEBUG

			try
			{
				houseId = (UInt64)model["houseID"]; // houseID is the correct key and is type UInt64
			}
			catch (Exception ex)
			{ // catches the exception if any of the keys are missing      
				LogFile.AddLog("Device -- Keys are invalid or missing: " + ex.Message + "\n");
				return 0;
			}

			return 1;
#else
			try {
				houseId = (UInt64)model ["houseID"]; // houseID is the correct key and is type UInt64
			} catch (Exception ex) { // catches the exception if any of the keys are missing      
				LogFile.AddLog ("Device -- Keys are invalid or missing: " + ex.Message + "\n");
				return 0;
			}

			try {
				
				WebRequest request = WebRequest.Create (DeviceRepository.storageURL + "R/" + houseId);
				request.ContentType = "application/json";
				request.Method = "POST";

				try {

					using (var streamWriter = new StreamWriter (request.GetRequestStream ())) {
						streamWriter.Write (model.ToString ());
					using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
						if (response.StatusCode != HttpStatusCode.OK)
							throw new Exception (String.Format (
								"Server error (HTTP {0}: {1}).",
								response.StatusCode,
								response.StatusDescription));

						var stream = response.GetResponseStream ();
						var reader = new StreamReader (stream);

						roomID = UInt64.Parse (reader.ReadToEnd ());
					}
                    }
				} catch (Exception ex) {
					LogFile.AddLog ("House -- Could not post room to the server: " + ex.Message + "\n");
					return 0;
				}

			} catch (SystemException ex) {
				LogFile.AddLog ("Device -- Failed to send POST request with the URL provided: " + ex.Message + "\n");
				return 0;
			}


			return roomID;
#endif
		}

		public JObject DeleteSpace (string houseid, string spaceid)
		{
			/*WebRequest request = WebRequest.Create("http://54.152.190.217:8081/HI/" + houseid);
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
                JObject houseObject = JObject.Parse(spaceString);
            }
              
            int version;
            // Get version from JObject
              
            request = WebRequest.Create("http://54.152.190.217:8081/R/" + houseid + "/" + version + "/" + spaceid);
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

                string spaceString = reader.ReadToEnd();
                return JObject.Parse(spaceString);
            }*/
			return null;

		}

		/*public Exception UpdateSpace(int id, string name, int type, int houseId)
        {
            //Space space = new Space();
            //space = getSpace(id);                             // Persistent storage getSpace() method

            //if (space == null)
            //    return new Exception("Space with Space Id: " + id + " not found");

            //if (name != "")
            //    space.SpaceName = name;

            //if (type != -1)
            //    space.SpaceType = type;

            //if (houseId != -1)
            //    space.HouseId = houseId;

            //updateSpace(space);                                   // Persistent storage updateSpace() method
            return null;

            //return new Exception("updated");
        }

        public Exception UpdateSpace(Space space)
        {
            //if (getSpace(space.SpaceId) == null)                         // Persistent storage getSpace() method
            //    return new Exception("Space with Space Id: " + space.SpaceId + " not found");
            //else
            //{
            //    updateSpace(space);                                   // Persistent storage updateSpace() method
            //    return new Exception("updated");
            //}
            return null;

        }*/
	}
}
