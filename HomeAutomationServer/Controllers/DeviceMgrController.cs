using HomeAutomationServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeAutomationServer.Controllers
{
    public class DeviceMgrController : ApiController
    {
        private DeviceRepository repo;
        private ServerIdentityService identityService;

        public DeviceMgrController()
        {
            repo = new DeviceRepository();
            identityService = new ServerIdentityService();
        }

        public object GetDeviceState()
        {
            return null;
        }

    }
}
