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
    public class DeviceMgrRepository
    {
        private string deviceApiHost = "http://localhost:8080/";

        public bool PostDeviceState(string houseid, string roomid, string deviceid, JObject data)
        {
            WebRequest request = WebRequest.Create(deviceApiHost + houseid + "/" + roomid + "/" + deviceid);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = data.ToString();

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                return true;
            }
        }
    }
}