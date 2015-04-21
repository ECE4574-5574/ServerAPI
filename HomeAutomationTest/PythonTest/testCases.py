import testServerAPI
import datetime
import json
import httplib, urllib

url = 'serverapi1d.azurewebsites.net'
server = testServerAPI.ServerAPITest(url)

#------------------------------------Decision------------------------------------#

# Patch api/decision/state/{deviceid}/{state}
deviceid = '005'
state = 'True'
server.patch_decision_state_deviceid_state(deviceid, state)
print ""

device_data = {
	"Name": 'device',
	"Enabled": True
}
# GET api/decision/state
server.get_decision_state(device_data)
print ""

#------------------------------------Storage------------------------------------#
#----Device----#

houseid = '001'
spaceid = '002'
deviceid = 'id'
deviceType = '003'

# GET api/storage/device/{houseid}/{spaceid}/{deviceid}
server.get_storage_device_houseid_spaceid_deviceid(houseid, spaceid, deviceid)
print ""

# GET api/storage/device/{houseid}/{spaceid}/{type}
server.get_storage_device_houseid_spaceid_type(houseid, spaceid, deviceType)
print ""

# GET api/storage/device/{houseid}/{spaceid}
server.get_storage_device_houseid_spaceid(houseid, spaceid)
print ""

# GET api/storage/device/{houseid}
server.get_storage_device_houseid(houseid)
print ""

# GET api/storage/device/{houseid}/{type}
server.get_storage_device_houseid_type(houseid, deviceType)
print ""

device_data = {
	"Name": 'device',
	"Enabled": True
}
# POST api/storage/device
server.post_storage_device(device_data)
print ""

# DELETE api/storage/device/{houseid}/{spaceid}/{deviceid}
server.delete_storage_device_houseid_spaceid_deviceid(houseid, spaceid, deviceid)
print ""

#----Space----#

# GET api/storage/space/{houseid}/{spaceid}
server.get_storage_space_houseid_spaceid(houseid, spaceid)
print ""

space_data = {
	"roomId": "003",
	"type": "Simulated",
	"name": "room"
}
# POST api/storage/space
server.post_storage_space(space_data)
print ""

# DELETE api/storage/space/{houseid}/{spaceid}
server.delete_storage_space_houseid_spaceid(houseid, spaceid)
print ""

#----House----#

# GET api/storage/house/{houseid}
server.get_storage_house_houseid(houseid)
print ""

house_data = {
	"houseid": "001",
	"name": "my house"
}
# POST api/storage/house
server.post_storage_house(house_data)
print ""

# DELETE api/storage/house/{houseid}
server.delete_storage_house_houseid(houseid)
print ""

#----user----#

username = 'user'
# GET api/storage/user/{username}
server.get_storage_user_username(username)
print ""

user_data = {
	"userID": "007",
	"Password": "12345"
}
# POST api/storage/user
server.post_storage_user(user_data)
print ""

username = 'user'
# DELETE api/storage/api/stroage/user/{username}
server.delete_storage_user_username(username)
print ""

#------------------------------------App------------------------------------#
#----User----#

location = {"userId": "user_id",
			"lat": "37.874342",
			"long": "-86.342234",
			"alt": "21.5452",
			"time": "2015-03-21 03:40:23"}
# POST api/app/user/updateposition/{username}
server.post_app_user_updatePosition_username(location)
print ""

devicetoken = {
	"username": "user",
	"token": "some token"
}
# POST api/app/user/devicetoken/{username} 
server.post_app_user_devicetoken_username(devicetoken)
print ""

brighten_info = {
	
}
# POST api/app/user/brighten
server.post_app_user_brighten(brighten_info)
print ""

#----Device----#

deviceid = '3'
# GET api/app/device/{deviceid}
server.get_app_device_deviceid(deviceid)
print ""

# GET api/app/device
server.get_app_device_all()
print ""

# GET api/app/device/count 
server.get_app_device_count()
print ""

#------------------------------------Sim------------------------------------#

time = {"localTime":str(datetime.datetime.now())}
# POST api/sim/timeframe
server.post_sim_timeframe(time)
print ""

#------------------------------------House------------------------------------#

device_data = {
	"Name": 'device',
	"Enabled": True
}
# POST api/house/device/state
server.post_house_device_state(device_data)
print ""





