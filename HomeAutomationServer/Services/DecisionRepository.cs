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
using System.Text;

//using HomeAutomationServer.Models;

namespace HomeAutomationServer.Services
{
    public class DecisionRepository
    {
        private string houseApiHost = "http://house_address:house_port/device/";
        private string path = @"C:\ServerAPILogFile\logfile.txt";

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

                catch (Exception ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Decision -- Failed to send data to the House System: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDecision -- Failed to send data to the House System: " + ex.Message);
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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Decision -- Failed to send data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDecision -- Failed to send data to the Storage: " + ex.Message);
                    return false;
                }

                try
                {
                    if (!AppCache.AddDeviceBlob(model))
                        throw new Exception("Decision -- AppCache add device failed when adding: " + model.ToString());
                }

                catch (Exception ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\n" + ex.Message);
                    return false;
                }
            }

            catch(SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Decision -- Could not create the specified url with the data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDecision -- Could not create the specified url with the data provided: " + ex.Message);
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

                catch (Exception ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Decision -- Could not Get the data from the House System: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDecision -- Could not Get the data from the House System: " + ex.Message);
                    return (bool)model["Enabled"];
                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Decision -- Could not create the specified url with the data provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDecision -- Could not create the specified url with the data provided: " + ex.Message);
                return (bool)model["Enabled"];
            }
        }
    }
}
