using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAPI.Models;

//This class serves as the data storage space. It is just a list of Device items that are regenerated from the cache

namespace HomeAPI.Services
{
    public class DeviceRepository
    {
        private const string CacheKey = "DeviceStore";

        public DeviceRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var devices = new Device[]{}; //Load the devices from the back end data structure??

            ctx.Cache[CacheKey] = devices;
                }
            }
        }

        public Device[] GetAllDevices()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Device[])ctx.Cache[CacheKey];
            }

            return new Device[]
            {
            new Device
                {
                    DeviceId = 0,
                    DeviceName = "Placeholder",
                    RoomId = 0
                }
            };
        }

        public bool SaveDevice(Device device)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Device[])ctx.Cache[CacheKey]).ToList(); //get a list of the current data
                    currentData.Add(device);                                    //add the new device
                    ctx.Cache[CacheKey] = currentData.ToArray();                //recache the array

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        public bool DeleteDevice(Device device)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Device[])ctx.Cache[CacheKey]).ToList();
                    for (int i = 0; i < currentData.Count; i++)
                        if (device.DeviceName == currentData.ElementAt(i).DeviceName && device.DeviceId == currentData.ElementAt(i).DeviceId) //search for the matching device to delete
                            currentData.RemoveAt(i);
                    for (int i = 0; i < currentData.Count; i++)
                        System.Diagnostics.Debug.WriteLine(currentData.ElementAt(i).DeviceName);  //this serves as a check to see if the item was deleted
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}