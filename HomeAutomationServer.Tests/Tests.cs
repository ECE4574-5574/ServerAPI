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

namespace HomeAutomationServer.Tests
{
    class Tests
    {
        public int main()
        {
            string uri = "server:port/api/";

            WebRequest request = WebRequest.Create(uri + "sim/timeframe");
            request.ContentType = "application/json";
            request.Method = "POST";

            string json = "json string";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
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

            catch (WebException we)
            {
                // always catches this exception even when the Jtoken is sent properly. 
                // Gets an error saying Connection was closed.
            }

            return 0;
        }
    }
}
