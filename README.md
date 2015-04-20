----------------------------------------------------------------------------------------------------------------------------

***Decision***

PATCH api/decision/state
<Param = JSON blob>
Updates a devices state with the given device JSON data. The new device state should be reflected in the data. 
Returns true if the devices state has been updated.

GET api/decision/state
<Param = JSON blob>
Get the state of the device from the JSON data provided, which is either true or false.
Returns the state of the device. True if enabled, false if not.

----------------------------------------------------------------------------------------------------------------------------

***Storage***

---Device---
GET api/storage/device/{houseid}/{spaceid}/{deviceid}	
Gets the device information with the specified house ID, space ID, and device ID.

GET api/storage/device/{houseid}/{spaceid}	
Gets all of the devices information with the specified house ID and space ID.

GET api/storage/device/{houseid}/{spaceid}/{type}	
Gets all of the devices in the space of the type specified, with the provided house ID and space ID.

GET api/storage/device/{houseid}	
Gets all of the devices in the house with the specified house ID.

GET api/storage/device/{houseid}/{type}	
Gets all of the devices in the house of the specified type, with the provided house ID.

POST api/storage/device	
Posts a device with the JSON object data given.
Data should be in the following format:
{
	"houseID" : <house-id>
	"roomID" : <room-id>
	"Type": <devicetype>
	<any other JSON blob you want to store>: <value>
}
ROOMID 0 indicates that the device is stored directly in the house.
Returns a unique ID for this device as a description while giving the status(in the given house/room)

DELETE api/storage/device/{houseid}/{spaceid}/{deviceid}	
Deletes a device with the specified house ID, space ID, and device ID provided.

---Space---

GET api/storage/space/{houseid}/{spaceid}	
Gets the space information with the houseid and spaceid provided.

POST api/storage/space	
Posts the space with the JSON object data information provided.

DELETE api/storage/space/{houseid}/{spaceid}	
Deletes the space specified by the houseid and spaceid

---House---

GET api/storage/house/{houseid}	
Gets the houses information with the specified houseid.

POST api/storage/house	
Posts the house with the JSON object information provided.

DELETE api/storage/house/{houseid}	
Deletes the house with the specified houseid.

---User---

GET api/storage/user/{username}	
Gets the users information by the username provided via JSON object data.

POST api/storage/user	
Posts the users information provided by JSON object data.

DELETE api/storage/api/stroage/user/{username}	
Deletes the user specified by the username.

POST api/house/device/state
Posts the updated device info (blob) to persistent storage and updates the state change cache*
* The app won't receive the update until it makes a GET request

------------------------------------------------------------------------------------------------------------------------

***App***

---User---

POST api/app/user/updateposition/{username}	
Updates the user position and location time stamp to the user, requires JSON object data and the username for the user.

POST api/app/user/devicetoken/{username}
Posts the devicetoken for a given user.

POST api/app/user/brighten
A request from the App system to make something brighter near their location. Information provided in a JSON.
Returns a bool that is true if the information was posted, false if not.

---Device---

GET api/app/device/{deviceid}
Get pending JSON device data with the device ID provided.

GET api/app/device
Get all the pending JSON device data as an array of JSON data.

GET api/app/device/count
Get the count of the pending JSON device data.

-------------------------------------------------------------------------------------------------------------------------

***Sim***

POST api/sim/timeframe	
Post the information via JSON data regarding the SimHarneses time frame configurations to the Decision Making System.

-------------------------------------------------------------------------------------------------------------------------

***House***

POST api/house/device/state
Post and updated device blob to the system (persistent storage and app cache/queue)

-------------------------------------------------------------------------------------------------------------------------
