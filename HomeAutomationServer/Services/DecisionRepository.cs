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

namespace HomeAutomationServer.Services
{
    public class DecisionRepository
    {
        private string houseApiHost = "http://house_address:house_port/device/";
        private string storageUrl = "";
        private AppCache appCache = new AppCache();

        public bool StateUpdate(JObject model)
        {
            try
            {
                WebRequest request = WebRequest.Create(houseApiHost + "/" + (UInt64)model["HouseID"] + "/" + (UInt64)model["RoomID"] + "/" + (UInt64)model["DeviceID"]);
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

                catch (WebException ex)
                {
                    File.AppendAllText("HomeAutomationServer/logfile.txt", "House POST device request: " + ex.Message);
                    return false;
                }

                request = WebRequest.Create(storageUrl + "UD/" + (UInt64)model["HouseID"] + "/" + (UInt64)model["RoomID"] + "/" + (UInt64)model["DeviceID"]);
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

                catch (WebException ex)
                {
                    File.AppendAllText("HomeAutomationServer/logfile.txt", "Storage POST device request: " + ex.Message);
                    return false;
                }

                try
                {
                    if (!appCache.AddDeviceBlob(model))
                        throw new Exception("AppCache add device failed when adding: " + model.ToString());
                }

                catch (Exception ex)
                {
                    File.AppendAllText("HomeAutomationServer/logfile.txt", ex.Message);
                    return false;
                }

            }

            catch(SystemException ex)
            {
                File.AppendAllText("HomeAutomationServer/logfile.txt", "Could not parse the JSON data with the appropriate keys: " + ex.Message);
                return false;
            }

            return true;
        }

        public bool GetState(JObject model)
        {
            JObject device = new JObject();

            try
            {
                WebRequest request = WebRequest.Create(houseApiHost + (UInt64)model["HouseID"] + (UInt64)model["RoomID"] + (UInt64)model["DeviceID"]);
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
                        device = JObject.Parse(deviceString);
                        return (bool)device["Enabled"];
                    }
                }

                catch (WebException ex)
                {
                    File.AppendAllText("HomeAutomationServer/logfile.txt", "House GET device request: " + ex.Message);
                    return (bool)model["Enabled"];
                }
            }

            catch (SystemException ex)
            {
                File.AppendAllText("HomeAutomationServer/logfile.txt", "Could not parse the JSON data with the appropriate keys: " + ex.Message);
                return (bool)model["Enabled"];
            }
        }
    }
}
