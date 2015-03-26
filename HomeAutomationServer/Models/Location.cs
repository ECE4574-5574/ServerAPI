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

        private int corner1, corner2, corner3, corner4;     // The four corners of the space
        public int GetCorner1()                             // Get corner1
        {
            return corner1;
        }

        public bool ChangeCorner1(int newCorner)             // Change corner 1
        {
            if (_permission)
            {
                corner1 = newCorner;
                return true;
            }
            else return false;
        }

        public int GetCorner2()                     // Get corner 2
        {
            return corner2;
        }

        public bool ChangeCorner2(int newCorner)     // Change corner 2
        {
            if (_permission)
            {
                corner2 = newCorner;
                return true;
            }
            else return false;
        }

        public int GetCorner3()                     // Get corner 3
        {
            return corner3;
        }

        public bool ChangeCorner3(int newCorner)     // Change corner 3
        {
            if (_permission)
            {
                corner3 = newCorner;
                return true;
            }
            else return false;
        }

        public int GetCorner4()                 // Get corner 4
        {
            return corner4;
        }

        public bool ChangeCorner4(int newCorner)        // Change corner 4
        {
            if (_permission)
            {
                corner4 = newCorner;
                return true;
            }
            else return false;
        }

        public List<int> GetWall1()                 // A list of coordinates for the first wall
        {
            List<int> wall = new List<int>();
            wall.Add(corner1);
            wall.Add(corner2);
            return wall;
        }

        public List<int> GetWall2()                 // A list of coordinates for the second wall
        {
            List<int> wall = new List<int>();
            wall.Add(corner2);
            wall.Add(corner3);
            return wall;
        }

        public List<int> GetWall3()             // A list of coordinates for the third wall
        {
            List<int> wall = new List<int>();
            wall.Add(corner3);
            wall.Add(corner4);
            return wall;
        }

        public List<int> GetWall4()             // A list of coordinates for the fourth wall
        {
            List<int> wall = new List<int>();
            wall.Add(corner4);
            wall.Add(corner1);
            return wall;
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
