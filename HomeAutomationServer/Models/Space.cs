using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Space class model

namespace HomeAutomationServer.Models
{
    public class Space
    {
        public int SpaceId {get; set;}

       /* private bool _permission = true;                                                         // This indicates whether or not the user has access to the space
        public bool Permission { get { return _permission; } set { _permission = value; } }*/

        //private string _spacename;          // The spaces name
        public string SpaceName { get; set; }
        /*{
            get { return _spacename; }
            set
            {
                if (_permission)
                    _spacename = value;
            }
        }*/

        //private string _housename;          // The name of the house this room belongs to
        public int HouseId { get; set; }
        /*{
            get { return _housename; }
            set
            {
                if (_permission)
                    _housename = value;
            }
        }*/

        //private string _type;        // The type of Space it is
        public string Type { get; set; }
        /*{
            get { return _type; }

            set
            {
                if (value.ToLower() == "living room" || value.ToLower() == "kitchen" || value.ToLower() == "basement" || value.ToLower() == "dining room"
                    || value.ToLower() == "bedroom" || value.ToLower() == "bathroom" || value.ToLower() == "office" || value.ToLower() == "stairs" || value.ToLower() == "walkway"
                    || value.ToLower() == "garage" || value.ToLower() == "porch" || value.ToLower() == "patio" || value.ToLower() == "general" || value.ToLower() == "other")
                {
                    if (_permission)
                        _type = value;
                }
                else
                {
                    if (_permission)
                        _type = "Not Specified";
                }
            }
        }*/

        public List<Device> MyDevices = new List<Device>(); // A list of devices that belong to this room
        /*public List<Device> returnDevices()                     // Return a list of the devices
        {
            return MyDevices;
        }

        public bool addDevice(Device device)                // Add a device to the list
        {
            if (_permission)
            {
                MyDevices.Add(device);
                return true;
            }
            return false;
        }

        public bool removeDevice(Device device)         // Remove a device from the list
        {
            if (_permission)
            {
                for (int i = 0; i < MyDevices.Count; i++)
                {
                    if (MyDevices[i].DeviceId == device.DeviceId)
                    {
                        MyDevices.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool removeAllDevices()              // Remove all devices
        {
            if (_permission)
            {
                MyDevices = new List<Device>();
                return true;
            }
            return false;
        }

        public int deviceCount()            // Return number of devices
        {
            return MyDevices.Count;
        }*/

        /*public Location SpaceLocation = new Location();    // A location map
        public bool ChangeLocationPermission(bool permission)       // Change the permission of modifying the location
        {
            if (_permission)
            {
                SpaceLocation.Permission = permission;
                return true;
            }
            else return false;
        }*/
    }
}
