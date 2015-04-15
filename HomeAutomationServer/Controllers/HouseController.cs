// 	IMPORTANT: Add content to 'HouseController.cs' and delte file later

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Services;

namespace HomeAutomationServer.Controllers
{
	[RoutePrefix("api/house")]
	public class HouseControllerNew : Controller
	{
		private HouseRepository house = new HouseRepository();

		// the house posts an updated device blob to the system (persistent storage and state queue)
		[Route("device/state")]	
		public bool postState(JObject deviceBlob)
		{
			return house.postState(deviceBlob);
		}

	}
}