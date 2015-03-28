using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeAutomationServer.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int DeviceType { get; set; }
        public int SpaceId { get; set; }
        public bool State { get; set; }
    }
}