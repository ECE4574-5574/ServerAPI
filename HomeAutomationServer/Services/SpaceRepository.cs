using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAutomationServerAPI.Models;

// This class tells the controller how to process the HTTP commands

namespace HomeAutomationServerAPI.Services
{
    public class SpaceRepository
    {
        public IEnumerable<Space> GetAllSpaces()
        {
            IEnumerable<Space> spaceEnumerable;
            spaceEnumerable = getAllSpaces();             // Persistent storage getAllSpaces() method
            return spaceEnumerable;
        }

        public Space GetSpace(int id)
        {
            return getSpace(id);                       // Persistent storage getSpace() method
        }

        public Exception SaveSpace(Space space)
        {
            if (getSpace(space.SpaceId) != null)          // Persistent storage getSpace() method
                return new Exception("Space with Space ID: " + space.SpaceId + " already exists");

            addSpace(space);                              // Persistent storage addSpace() method
            return new Exception("saved");
        }

        public Exception UpdateSpace(int id, string name, int type, int houseId)
        {
            Space space = new Space();
            space = getSpace(id);                             // Persistent storage getSpace() method

            if (space == null)
                return new Exception("Space with Space Id: " + id + " not found");

            if (name != "")
                space.SpaceName = name;

            if (type != -1)
                space.SpaceType = type;

            if (houseId != -1)
                space.HouseId = houseId;

            updateSpace(space);                                   // Persistent storage updateSpace() method
            return new Exception("updated");
        }

        public Exception UpdateSpace(Space space)
        {
            if (getSpace(space.SpaceId) == null)                         // Persistent storage getSpace() method
                return new Exception("Space with Space Id: " + space.SpaceId + " not found");
            else
            {
                updateSpace(space);                                   // Persistent storage updateSpace() method
                return new Exception("updated");
            }
        }

        public Exception DeleteSpace(int id)
        {
            if (getSpace(id) == null)                 // Persistent storage getSpace() method
                return new Exception("Space with Space Id: " + id + " not found");
            else
            {
                removeSpace(id);                      // Persistent storage removeSpace() method
                return new Exception("deleted");
            }
        }
    }
}
