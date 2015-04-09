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
//using api;

namespace HomeAutomationServer.Services
{
    public class DecisionRepository
    {
        private string deviceApiHost = "http://localhost:8080/";
        private string appApiHost = "";

        public bool PostDeviceState(UInt64 houseid, UInt64 roomid, UInt64 deviceid, JObject data)
        {
            WebRequest request = WebRequest.Create(deviceApiHost + houseid + "/" + roomid + "/" + deviceid);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data.ToString());
                streamWriter.Flush();
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

            catch (WebException)
            {
                //return false;
            }

            /*request = WebRequest.Create(appApiHost);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data.ToString());
                streamWriter.Flush();
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

            catch (WebException)
            {
                //return false;
            }*/ 

            // PUSH NOTIFICATION TO APP

            return true;
        }
    }
}