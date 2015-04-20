using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Web;

namespace HomeAutomationServer.Services
{
    public class SimRepository
    {
        private string path = @"ServerAPILogFile\logfile.txt";

        public bool sendConfigData(JObject model)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://54.152.190.217:8085/TimeConfig");
                request.ContentType = "application/json";
                request.Method = "POST";

                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(model.ToString());
                        streamWriter.Close();
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
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
                            Byte[] info = new UTF8Encoding(true).GetBytes("Sim -- Failed to Post dat to the Decision System: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nSim -- Could not Post data to the Decision System: " + ex.Message);
                    return false;
                }

                /*request = WebRequest.Create("");    // Send to storage
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(model.ToString());
                    streamWriter.Close();
                }

                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
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
                 * if (!File.Exists(path))
                   {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                          Byte[] info = new UTF8Encoding(true).GetBytes("Sim -- Failed to Post data to Storage: " + ex.Message);
                          fstream.Write(info, 0, info.Length);
                        }
                    }
                    else File.AppendAllText(path, "\nCould not Post data to Storage: " + ex.Message);
                    return false;
                }*/
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Sim -- Failed to create URL with the provided information: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                File.AppendAllText(path, "\nSim -- Failed to create URL with the provided information: " + ex.Message);
                return false;
            }

            return true;
        }

        public string GetLog()
        {
            try
            {
                if (!File.Exists(path))
                {
                    return "LogFile does not exist";
                }
                else
                {
                    return File.ReadAllText(path);
                }
            }

            catch (Exception ex)
            {
                return "Trying to find file gave this exception: " + ex.Message;
            }
        }
    }
}
