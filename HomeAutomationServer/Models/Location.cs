using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// This is the location of a space

namespace HomeAutomationServerAPI.Models
{
    public class Location
    {
        private bool _permission = false;                                    // Permission to change the location information
        public bool Permission { get { return _permission; } set { _permission = value; } }

        private List<int> Center = new List<int>();                     // The center of the space
        public bool NewCenter(List<int> newCenter)                      // Specify a new center to the space
        {
            if (_permission)
            {
                Center = newCenter;
                return true;
            }
            else return false;
        }

        public List<int> GetCenter()                            // Get the center of the space
        {
            return Center;
        }

        private int Radius;                                     // The radius of the space
        public bool NewRadius(int newRadius)                   // Specify a new radius to the space
        {
            if (_permission)
            {
                Radius = newRadius;
                return true;
            }
            else return false;
        }

        public int GetRadius()                                  // Get the radius of the space
        {  
            return Radius;
        }

        private int floor = 0;                              // The floor of the space
        public int GetFloor()                               // Get the floor of the space
        {
            return floor;
        }

        public bool ChangeFloor(int newFloor)               // Change the floor to the space
        {
            if (_permission)
            {
                floor = newFloor;
                return true;
            }
            else return false;
        }
    }
}
