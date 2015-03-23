using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeAPI.Models
{
    public class Device
    {
            public int DeviceId { get; set; }
            public string DeviceName { get; set; }
            public int RoomId { get; set; }
    }
}