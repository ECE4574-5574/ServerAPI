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

//This class serves as the data storage space. It is just a list of Device items that are regenerated from the cache

namespace HomeAutomationServer.Services
{
    public class DeviceRepository
    {
        public static string decisionURL = "http://52.5.152.139:8085/";
        public static string storageURL = "http://ec2-52-11-96-207.us-west-2.compute.amazonaws.com:8080/";

        private string path = @"C:\ServerAPILogFile\logfile.txt";

        public JObject GetDevice(string houseid, string spaceid, string deviceid)       // Return device
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "/DD" + houseid + "/" + spaceid + "/" + deviceid);
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
                        return JObject.Parse(deviceString);
                    }
                }

                catch (Exception ex) 
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to get data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDevice -- Failed to get data to the Storage: " + ex.Message);
                    return null;
                }
            }

            catch(SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to create request with the URL provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDevice -- Failed to create request with the URL provided: " + ex.Message);
                return null;
            }
        }

        public JArray GetDevice(string houseid, string spaceid)            // Return devices in space
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "RD/" + houseid + "/" + spaceid);
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
                        return JArray.Parse(deviceString);
                    }
                }
                
                catch (Exception ex) 
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to get data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDevice -- Failed to get data to the Storage: " + ex.Message);
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
                        Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to create request with the URL provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDevice -- Failed to create request with the URL provided: " + ex.Message);
                return null;
            }
        }

        public JArray GetDevice(string houseid, string spaceid, int type)      // Return devices in space by type
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "RT/" + houseid + "/" + spaceid + "/" + type);
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
                        return JArray.Parse(deviceString);
                    }
                }

                catch (Exception ex) 
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to get data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDevice -- Failed to get data to the Storage: " + ex.Message);
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
                        Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to create request with the URL provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDevice -- Failed to create request with the URL provided: " + ex.Message);
                return null;
            }
        }

        public JArray GetDevice(string houseid)            // Return devices in house
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "HD/" + houseid);
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
                        return JArray.Parse(deviceString);
                    }
                }
                
                catch (Exception ex) 
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to get data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDevice -- Failed to get data to the Storage: " + ex.Message);
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
                        Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to create request with the URL provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDevice -- Failed to create request with the URL provided: " + ex.Message);
                return null;
            }
        }

        public JArray GetDevice(string houseid, int type)          // Return devices in house by type
        {
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "HT/" + houseid + "/" + type);
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
                        return JArray.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to get data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDevice -- Failed to get data to the Storage: " + ex.Message);
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
                        Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to send GET request with the URL provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDevice -- Failed to send GET request with the URL provided: " + ex.Message);
                return null;
            }
        }
        
        public UInt64 SaveDevice(JObject model)     // Returns the device ID from the Storage which is type UInt64
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
            catch (Exception ex){ // catches the exception if any of the keys are missing                
                return 0;
            }

            return 1;
            //model.TryGetValue("houseID", houseId);
            //model.TryGetValue()
#else
            houseId = (UInt64)model["houseID"]; // houseID is the correct key and is type UInt64
            roomId = (UInt64)model["roomID"];   // roomID is the correct key and is type UInt64
            deviceType = (string)model["Type"]; // Type is the correct key and is type string
            try
            {
                WebRequest request = WebRequest.Create(storageURL + "D/" + houseId + "/" + roomId + "/" + deviceType);
                request.Method = "POST";

                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(model.ToString());
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream);

                        deviceId = UInt64.Parse(reader.ReadToEnd());
                        //JObject houseObject = JObject.Parse(deviceString);
                    }
                }

                catch (Exception ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to send information to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nDevice -- Failed to send information to the Storage: " + ex.Message);
                    return 0;
                }
            }

            catch (SystemException ex) 
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Device -- Failed to send POST request with the URL provided: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else File.AppendAllText(path, "\nDevice -- Failed to send POST request with the URL provided: " + ex.Message);
                return 0;
            }

            return deviceId;
#endif
        }

        public JObject DeleteDevice(string houseid, string spaceid, string deviceid)
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
                JObject houseObject = JObject.Parse(deviceString);
            }
              
            int version;
            // Get version from JObject
             
            request = WebRequest.Create("http://54.152.190.217:8081/D/" + houseid + "/" + version + "/" + spaceid + "/" + deviceid);
            request.ContentType = "application/json";
            request.Method = "DELETE";

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
            }*/

            return null;
        }
    }
}
