using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Space class model

namespace HomeAPI.Models
{
    public class Space
    {
        private bool _permission = true;                                                         // This indicates whether or not the user has access to the space
        public bool Permission { get { return _permission; } set { _permission = value; } }

        private string _spacename;          // The spaces name
        public string SpaceName
        {
            get { return _spacename; }
            set
            {
                if (_permission)
                    _spacename = value;
            }
        }

        private string _housename;          // The name of the house this room belongs to
        public string HouseName
        {
            get { return _housename; }
            set
            {
                if (_permission)
                    _housename = value;
            }
        }

        private string _type;        // The type of Space it is
        public string Type
        {
            get { return _type; }

            set
            {
                if (_type.ToLower() == "living room" || _type.ToLower() == "kitchen" || _type.ToLower() == "basement" || _type.ToLower() == "dining room"
                    || _type.ToLower() == "bedroom" || _type.ToLower() == "bathroom" || _type.ToLower() == "office" || _type.ToLower() == "stairs" || _type.ToLower() == "walkway"
                    || _type.ToLower() == "garage" || _type.ToLower() == "porch" || _type.ToLower() == "patio" || _type.ToLower() == "general" || _type.ToLower() == "other")
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
        }

        private List<Device> MyDevices = new List<Device>(); // A list of devices that belong to this room
        public List<Device> returnDevices()                     // Return a list of the devices
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

        private List<int> LocationMap = new List<int>();     // A location map
    }
}
