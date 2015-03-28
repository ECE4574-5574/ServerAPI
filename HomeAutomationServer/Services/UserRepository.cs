using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAutomationServerAPI.Models;

// This class tells the controller how to process the HTTP commands

namespace HomeAutomationServerAPI.Services
{
    public class UserRepository
    {
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> userEnumerable;
            userEnumerable = getAllUsers();             // Persistent storage getAllUsers() method
            return userEnumerable;
        }

        public User GetUser(int id)
        {
            return getUser(id);                       // Persistent storage getUser() method
        }

        public Exception SaveUser(User user)
        {
            if (getUser(user.UserId) != null)          // Persistent storage getUser() method
                return new Exception("User with User ID: " + user.UserId + " already exists");

            addUser(user);                              // Persistent storage addUser() method
            return new Exception("saved");
        }

        public Exception UpdateUser(int id, string firstName, string lastName)
        {
            User user = new User();
            user = getUser(id);                             // Persistent storage getUser() method

            if (user == null)
                return new Exception("User with User Id: " + id + " not found");

            if (firstName != "")
                user.FirstName = firstName;

            if (lastName != "")
                user.LastName = lastName;

            updateUser(user);                                   // Persistent storage updateUser() method
            return new Exception("updated");
        }

        public Exception UpdateUser(User user)
        {
            if (getUser(user.UserId) == null)                         // Persistent storage getUser() method
                return new Exception("User with User Id: " + user.UserId + " not found");
            else
            {
                updateUser(user);                                   // Persistent storage updateUser() method
                return new Exception("updated");
            }
        }

        public Exception DeleteUser(int id)
        {
            if (getUser(id) == null)                 // Persistent storage getUser() method
                return new Exception("User with User Id: " + id + " not found");
            else
            {
                removeUser(id);                      // Persistent storage removeUser() method
                return new Exception("deleted");
            }
        }
    }
}

