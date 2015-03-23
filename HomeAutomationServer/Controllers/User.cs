public class User
{
    int userID;
    string firstName;
    string lastName;
    List<House> houses;

    public int UserID
    {
        get { return userID; }
    }

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public Person()
    {

    }

    public static void Save(User user)
    {
        //datalayer here
        //save the User class
    }
    
    public List<House> Houses()
    {
    	get { return houses; }
    	set { houses = value; }
    }
    
    public void AddHouse(House h)
    {
    	//Insert h in the house list
    }
}