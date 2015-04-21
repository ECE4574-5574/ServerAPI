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
    public class SimRepository
    {
        // deviceRepo has static url
        private string dm_url = DeviceRepository.decisionURL;
        private string pss_url = DeviceRepository.storageURL;

        public bool sendConfigData(JObject model)
        {
#if DEBUG
            return true;
#else
            try
            {
                WebRequest request = WebRequest.Create(dm_url + "TimeConfig");
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
                    LogFile.AddLog("Sim -- Could not Post data to the Decision System: " + ex.Message + "\n");
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
                    LogFile.AddLog("Could not Post data to Storage: " + ex.Message + "\n"); 
                    return false;
                }*/
            }

            catch (SystemException ex)
            {
                LogFile.AddLog("Sim -- Failed to create URL with the provided information: " + ex.Message + "\n");
                return false;
            }

#endif

            return true;
        }
    }
}
