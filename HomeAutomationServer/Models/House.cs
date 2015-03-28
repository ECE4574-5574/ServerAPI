using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// House Model

namespace HomeAutomationServer.Models
{
    public class House
    {
        public string HouseName { get; set; }
        public int HouseId { get; set; }
        public int UserId { get; set; }
        public List<Space> MySpaces = new List<Space>();
    }
}