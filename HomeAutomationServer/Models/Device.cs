using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeAutomationServerAPI.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int DeviceType { get; set; }
        public int RoomId { get; set; }
    }
}