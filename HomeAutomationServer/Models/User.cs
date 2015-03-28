using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// User Model

namespace HomeAutomationServer.Models 
{
    public class User
    {
        public int UserID { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set;}
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