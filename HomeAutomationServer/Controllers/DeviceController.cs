using HomeAutomationServer.Filters;
using HomeAutomationServer.Models;
using HomeAutomationServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//This is the device controller and will respond to GET, POST, PATCH, and DELETE

namespace HomeAutomationServerAPI.Controllers
{
    [LearningAuthorizeAttribute]
    public class DeviceController : ApiController
    {

        private DeviceRepository deviceRepository = new DeviceRepository();

        public DeviceController()
        {
            deviceRepository = new DeviceRepository();
        } 

        // GET api/Device
        public IEnumerable<Device> Get()
        {
            return deviceRepository.GetAllDevices();
        }

        // GET api/Device/id
        public Device Get(int id)
        {
            return deviceRepository.GetDevice(id);
        }

        // PATCH api/Device/id, name, type, roomId
        public HttpResponseMessage Patch( int id, string name = "", int type = -1, int spaceId = -1)
        {
            Exception ex = deviceRepository.UpdateDevice(id, name, type, spaceId);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);

            if (ex.Message == "updated")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            else return response;
        }

        // PATCH api/Device
        public HttpResponseMessage Patch(Device device)
        {
            Exception ex = deviceRepository.UpdateDevice(device);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);

            if (ex.Message == "updated")
                return Request.CreateResponse<Device>(System.Net.HttpStatusCode.OK, device);
            else return response;
        }

        // POST api/Device
        public HttpResponseMessage Post(Device device)
        {
            Exception ex = deviceRepository.SaveDevice(device);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, ex);

            if (ex.Message == "saved")
                return Request.CreateResponse<Device>(System.Net.HttpStatusCode.Created, device);
            else return response;
        }

        // DELETE api/Device/id
        public HttpResponseMessage Delete(int id)
        {
            Exception ex = deviceRepository.DeleteDevice(id);
            var response = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotModified, ex);

            if (ex.Message == "deleted")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            else return response;
        }
    }
}
