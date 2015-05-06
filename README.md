Hostname: http://serverapi1.azurewebsites.net

----------------------------------------------------------------------------------------------------------------------------

***Decision***

PATCH api/decision/state
<Param = JSON blob>
Updates a devices state with the given device JSON data. The new device state should be reflected in the data. 
Returns true if the devices state has been updated.

POST api/decision/device/state
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

POST api/house/device/state
Posts the updated device info (blob) to persistent storage and updates the state change cache*
* The app won't receive the update until it makes a GET request

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

GET api/user/userid/{username}/{pass}
Gets the userid of the user provided username and password.


---Device---

GET api/app/device/{fullid}
Get pending JSON device data with the FullID object provided.

GET api/app/device
Get all the pending JSON device data as an array of JSON data.

GET api/app/device/count
Get the count of the pending JSON device data.

GET api/app/device/enumeratedevices/{houseid}
A request from the App to get list of all the devices that are not registered to the house.
Requires a houseID as an input in the uri.
Returns all the elements which don’t match any devices registered to a given house.

POST api/app/device/viaSimulation/{houseid}/{roomid}/{deviceid}
A request from app to update the Device state. This call assumes that the device state is being changed as an action of the user (ex. physically switching the ligh on/off). 
Information regarding state, device-ID and all other things are provided as a JSON blob which will be stored in Persistent Storage, and sent to the House. This will not be sent to Decision Systems.
Returns true if successful

POST api/app/device/viaActual/{houseid}/{roomid}/{deviceid}
A request from app to update the Device state. This call assumes that the device state is being changed from the application itself. Information regarding state, device-ID and all other things are provided as a JSON blob which will be stored in Persistent Storage, sent to the House and to the Decision Systems.
Returns true if successful


---Notification---
POST api/app/user/devicetoken/{username}/{pass}
Posts a device token related to the user. Need to call this to sign up users for notifications. JSon blob should be

{
	"deviceToken" : <device-token>
}

POST api/app/user/notify/{username}/{pass}
Posts a notification to the desired user. JSon blob should be 
{
	"message" : <message>
}

---Commands---
POST api/app/user/command
Posts a command to the decision making team. JSon blob specified by decision making team.

-------------------------------------------------------------------------------------------------------------------------

***Sim***

POST api/sim/timeframe	
Post the information via JSON data regarding the SimHarneses time frame configurations to the Decision Making System.

-------------------------------------------------------------------------------------------------------------------------

***House***

POST api/house/device/state
Post and updated device blob to the system (persistent storage and app cache/queue)

-------------------------------------------------------------------------------------------------------------------------

***LogFile***

GET api/logfile
Get the logfile information via a string

GET api/logfile/count
Get the count of logs in the logfile

DELETE api/logfile
Delete the logs in the logfile

---------------------------------------------------------------------------------------------------------------------------
