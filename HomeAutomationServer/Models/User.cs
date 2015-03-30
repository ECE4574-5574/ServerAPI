using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// User Model

namespace HomeAutomationServer.Models 
{
    public class User
    {
        public string UserName { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string Password { get; set; }
        public List<House> MyHouses = new List<House>();

        /*public Person()
        {

        }*/

        /*public static void Save(User user)
        {
            //datalayer here
            //save the User class
        }*/
    
    
        /*public void AddHouse(House h)
        {
    	    //Insert h in the house list
        }*/
    }
}