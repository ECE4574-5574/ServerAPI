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

namespace HomeAutomationServer.Services
{
    public class SimRepository
    {
        private string path = @"C:\ServerAPILogFile\logfile.txt";

        public bool sendConfigData(JObject model)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://54.152.190.217:8085/TimeConfig");
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
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("SimConfig -- Failed to Post Sim config info to Decision System: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else
                    {
                        using (FileStream fstream = File.OpenWrite(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("SimConfig -- Failed to Post Sim config info to Decision System: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    return false;
                }

                request = WebRequest.Create("http://172.31.26.85:8080/U/SimHarness");    // Send to storage
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

                catch (WebException ex)
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(@"C:\ServerAPILogFile");
                        using (FileStream fstream = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("SimConfig -- Failed to send data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    else
                    {
                        using (FileStream fstream = File.OpenWrite(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes("SimConfig -- Failed to send data to the Storage: " + ex.Message);
                            fstream.Write(info, 0, info.Length);
                        }
                    }
                    return false;
                }
            }

            catch (SystemException ex)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(@"C:\ServerAPILogFile");
                    using (FileStream fstream = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("SimConfig -- Failed to parse model with appropriate keys: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                else
                {
                    using (FileStream fstream = File.OpenWrite(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("SimConfig -- Failed to parse model with appropriate keys: " + ex.Message);
                        fstream.Write(info, 0, info.Length);
                    }
                }
                return false;
            }

            return true;
        }
    }
}
