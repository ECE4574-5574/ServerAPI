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

//This class serves as the data storage space. It is just a list of Device items that are regenerated from the cache

namespace HomeAutomationServer.Services
{
    public class DeviceRepository
    {
        public JObject GetDevice(string houseid, string spaceid, int id)
        {
            //return getDevice(id);                       // Persistent storage getDevice() method
            return null;

        }

        public Exception SaveDevice(Device device)
        {
            //if (getDevice(device.DeviceId) != null)          // Persistent storage getDevice() method
            //    return new Exception("Device with Device ID: " + device.DeviceId + " already exists");
            
            //addDevice(device);                              // Persistent storage addDevice() method
            //return new Exception("saved");
            return null;

        }

        public Exception UpdateDevice(int id, string name, int type, int spaceId)
        {
            //Device device = new Device();
            //device = getDevice(id);                             // Persistent storage getDevice() method
            
            //if (device == null)
            //    return new Exception("Device with Device Id: " + id + " not found");

            //if (name != "")
            //    device.DeviceName = name;

            //if (type != -1)
            //    device.DeviceType = type;

            //if (spaceId != -1)
            //    device.SpaceId = spaceId;

            //updateDevice(device);                                   // Persistent storage updateDevice() method
            //return new Exception("updated");
            return null;

        }

        public Exception UpdateDevice(Device device)
        {
            //if (getDevice(device.DeviceId) == null)                         // Persistent storage getDevice() method
            //    return new Exception("Device with Device Id: " + device.DeviceId + " not found");
            //else
            //{
            //    updateDevice(device);                                   // Persistent storage updateDevice() method
            //    return new Exception("updated");
            //}
            return null;

        }

        public Exception DeleteDevice(int id)
        {
            //if (getDevice(id) == null)                 // Persistent storage getDevice() method
            //    return new Exception("Device with Device Id: " + id + " not found");
            //else
            //{
            //    removeDevice(id);                      // Persistent storage removeDevice() method
            //    return new Exception("deleted");
            //}
            return null;
        }
    }
}