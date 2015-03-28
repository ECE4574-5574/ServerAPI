using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAutomationServerAPI.Models;

// This class tells the controller how to process the HTTP commands

namespace HomeAutomationServerAPI.Services
{
    public class HouseRepository
    {
        public IEnumerable<House> GetAllHouses()
        {
            IEnumerable<House> houseEnumerable;
            houseEnumerable = getAllHouses();             // Persistent storage getAllHouses() method
            return houseEnumerable;
        }

        public House GetHouse(int id)
        {
            return getHouse(id);                       // Persistent storage getHouse() method
        }

        public Exception SaveHouse(House house)
        {
            if (getHouse(house.HouseId) != null)          // Persistent storage getHouse() method
                return new Exception("House with House ID: " + house.HouseId + " already exists");

            addHouse(house);                              // Persistent storage addHouse() method
            return new Exception("saved");
        }

        public Exception UpdateHouse(int id, string name, int userId)
        {
            House house = new House();
            house = getHouse(id);                             // Persistent storage getHouse() method

            if (house == null)
                return new Exception("House with House Id: " + id + " not found");

            if (name != "")
                house.HouseName = name;

            if (userId != -1)
                house.UserId = userId;

            updateHouse(house);                                   // Persistent storage updateHouse() method
            return new Exception("updated");
        }

        public Exception UpdateHouse(House house)
        {
            if (getHouse(house.HouseId) == null)                         // Persistent storage getHouse() method
                return new Exception("House with House Id: " + house.HouseId + " not found");
            else
            {
                updateHouse(house);                                   // Persistent storage updateHouse() method
                return new Exception("updated");
            }
        }

        public Exception DeleteHouse(int id)
        {
            if (getHouse(id) == null)                 // Persistent storage getHouse() method
                return new Exception("House with House Id: " + id + " not found");
            else
            {
                removeHouse(id);                      // Persistent storage removeHouse() method
                return new Exception("deleted");
            }
        }
    }
}
