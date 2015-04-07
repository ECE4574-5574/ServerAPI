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
		public JObject GetDeviceState(int deviceid)
		{
			return null;
		}

		public bool PostDeviceState(JObject model)
		{
			return true;
		}
    }
}