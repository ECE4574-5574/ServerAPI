using HomeAutomationServerAPI.Models;
using HomeAutomationServerAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//This is our controller. It will respond to GET, POST, DELETE...

namespace HomeAutomationServerAPI.Controllers
{
    public class DeviceV2Controller : ApiController
    {

        private DeviceRepository deviceV2Repository;

        public DeviceV2Controller()
        {
            this.deviceV2Repository = new DeviceRepository();
        }

        public Device[] Get()
        {
            return deviceV2Repository.GetAllDevices();
        }

        public HttpResponseMessage Post(Device device)
        {
            this.deviceV2Repository.SaveDevice(device);

            var response = Request.CreateResponse<Device>(System.Net.HttpStatusCode.Created, device);

            return response;
        }

        public HttpResponseMessage Delete(Device device)
        {
            this.deviceV2Repository.DeleteDevice(device);

            var response = Request.CreateResponse<Device>(System.Net.HttpStatusCode.OK, device);

            return response;
        }
    }
}
