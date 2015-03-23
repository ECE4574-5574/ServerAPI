using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAPI.Models;

// This class tells the controller how to process the HTTP commands

namespace HomeAPI.Services
{
    public class SpaceRepository
    {
        private const string CacheKey = "SpaceStore";

        public SpaceRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var spaces = new Space[] { }; 

                    ctx.Cache[CacheKey] = spaces;
                }
            }
        }

        public Space[] GetAllSpaces()             // Gets all the spaces from the cache
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Space[])ctx.Cache[CacheKey];
            }

            return new Space[]
            {
            new Space
                {
                    SpaceName = "PlaceHolder"
                }
            };
        }

        public Exception UpdateSpace()         // Updates the list of devices to add new devices that should be in list or delete ones that do not exist
        {
            var ctx = HttpContext.Current;

            /*if (ctx != null)
            {
                var currentData = ((Space[])ctx.Cache[CacheKey]).ToList(); //get a list of the current data
                bool found = false;
                bool match = false;
                bool noMatch = false;
                DeviceRepository deviceRepo = new DeviceRepository();
                List<Device> deviceList = new List<Device>();
                deviceList = deviceRepo.GetAllDevices().ToList();
                for (int i = 0; i < currentData.Count; i++)
                {
                    for (int j = 0; j < deviceList.Count; j++)
                    {
                        if (currentData.ElementAt(i).SpaceId == deviceList[j].SpaceId)           // Check if the device belongs to that space
                        {
                            if (currentData.ElementAt(i).MyDevices.Count > 0)
                            {
                                for (int k = 0; k < currentData.ElementAt(i).MyDevices.Count; k++)
                                {
                                    if (currentData.ElementAt(i).MyDevices[k].DeviceId == deviceList[j].DeviceId)   // If it already exist, update it
                                    {
                                        found = true;
                                        currentData.ElementAt(i).MyDevices[k] = deviceList[j];
                                    }
                                }
                                if (!found)                                                         // If not add it
                                    currentData.ElementAt(i).MyDevices.Add(deviceList[j]);
                            }
                            else
                            {
                                currentData.ElementAt(i).MyDevices.Add(deviceList[j]);
                            }
                        }

                        for (int k = 0; k < currentData.ElementAt(i).MyDevices.Count; k++)
                        {
                            if (currentData.ElementAt(i).MyDevices[k].DeviceId == deviceList[j].DeviceId)
                                match = true;

                            if (!match && (k >= currentData.ElementAt(i).MyDevices.Count))
                            {
                                noMatch = true;
                                currentData.ElementAt(i).MyDevices[k] = null;           // If the device doesn't exist anymore, remove it from the list of devices
                            }
                        }
                    }

                    if ((deviceList.Count == 0) && currentData.ElementAt(i).MyDevices.Count > 0)    // If there are no devices in cache but devices in list, delete them
                        for (int k = 0; k < currentData.ElementAt(i).MyDevices.Count; k++)
                            currentData.ElementAt(i).MyDevices[k] = null;
                }
                ctx.Cache[CacheKey] = currentData.ToArray();
                if (noMatch || found || !found)*/
                    return new Exception("updated");
           /* }
            return new Exception("No devices found for the spaces available");*/
        }

        public Exception SaveSpace(Space space)            // Creates and saves new space
        {
            /*var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Space[])ctx.Cache[CacheKey]).ToList(); //get a list of the current data
                    bool added = false;
                    HouseRepository tempRepo = new HouseRepository();
                    List<House> houseList = new List<House>();
                    houseList = tempRepo.GetAllHouses().ToList();
                    SpaceRepository tempRepo2 = new SpaceRepository();
                    List<Space> spaceList = new List<Space>();
                    spaceList = tempRepo2.GetAllSpaces().ToList();

                    for (int i = 0; i < spaceList.Count; i++)            // If space already exist, update it
                    {
                        if (spaceList[i].SpaceId == space.SpaceId)
                        {
                            spaceList[i] = space;
                            ctx.Cache[CacheKey] = spaceList.ToArray();
                            throw new Exception("Already have Space: " + space.SpaceName +
                            " updating properties");
                        }
                    }

                    for (int i = 0; i < houseList.Count; i++)
                    {
                        if (houseList[i].HouseId == space.HouseId)
                        {
                            spaceList.Add(space);
                            added = true;
                        }                                                           //add the new device
                    }

                    if (!added)
                        throw new Exception("Could not find House ID: " + space.HouseId);

                    ctx.Cache[CacheKey] = spaceList.ToArray();                //recache the array
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return ex;
                }
            }
            */
            return new Exception("none");
        }

        public Exception DeleteSpace(Space space)
        {
           /* var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Space[])ctx.Cache[CacheKey]).ToList();
                    bool found = false;
                    for (int i = 0; i < currentData.Count; i++)
                    {
                        if (space.SpaceId == currentData.ElementAt(i).SpaceId) //search for the matching name to delete
                        {
                            currentData.RemoveAt(i);
                            found = true;
                        }
                    }
                    for (int i = 0; i < currentData.Count; i++)
                        System.Diagnostics.Debug.WriteLine(currentData.ElementAt(i).SpaceId);  //this serves as a check to see if the item was deleted
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    if (!found)
                        throw new Exception("Could not find Space: " + space.SpaceName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return ex;
                }
            }*/
            return new Exception("none");
        }
    }
}
