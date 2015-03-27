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
    public class DeviceController : ApiController
    {

        private DeviceRepository deviceRepository;

        public DeviceController()
        {
            this.deviceRepository = new DeviceRepository();
        } 

        // GET api/Device
        public Device[] Get()
        {
            return deviceRepository.GetAllDevices();
        }

        // GET api/Device/id
        public Device Get(int id)
        {
            return null;
        }

        // GET api/Device/name
        public Device Get(string name)
        {
            return null;
        }

        // PATCH api/Device/id, name, type, roomId
        public HttpResponseMessage Patch( int id, string name = null, string type = null, int roomId = 0)
        {
            return null;
        }

        // POST api/Device
        public HttpResponseMessage Post(Device device)
        {
            this.deviceRepository.SaveDevice(device);

            var response = Request.CreateResponse<Device>(System.Net.HttpStatusCode.Created, device);

            return response;
        }

        // DELETE api/Device/id
        public HttpResponseMessage Delete(int id)
        {
            /*this.deviceRepository.DeleteDevice(device);

            var response = Request.CreateResponse<Device>(System.Net.HttpStatusCode.OK, device);

            return response;*/
            return null;
        }
    }
}
