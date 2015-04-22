'''
This script successfully tests api:
POST api/sim/timeframe
POST api/app/user/updateposition/{username}

Please note that at the time of tests, we use 52.5.152.139:8085 for decision making, which is set up by Mark.
Persistent storage are not working right now. 

We have not host our server code remotely. So you have to build and run the server code locally before perform
these tests.

To successfully perform these tests, following changes has been made in our server code:
1. As mentioned above, change all links to decision making to 52.5.152.139:8085
2. Comment out AWS SNS notification code, which seems to be broken after previous AWS box being hacked
3. Change the server host and port number according to your running application 
(might be localhsot:8081 by default if you run it locally)

If you have any question or cannot do these tests, contact ningli@vt.edu
'''


import httplib, urllib
import datetime
import json

def getHostName(url):
	try:
		return url
	except Exception, e:
		print 'parse url failed'
		raise e

# def getHostName(url):
# 	try:
# 		return url.split(':')[0]
# 	except Exception, e:
# 		print 'parse url failed'
# 		raise e

# def getPortNumber(url):
# 	try:
# 		return url.split(':')[1]
# 	except Exception, e:
# 		raise e

class ServerAPITest:
	def __init__(self, url):
		self.server_url = url;
		self.hostName = getHostName(self.server_url);
		self.portNumber = ''
		# self.portNumber = getPortNumber(self.server_url);

	#------------------------------------Decision------------------------------------#

	# Patch api/decision/state/{deviceid}/{state}
	# Updates a devices state with the given device ID to the state specified. 
	# Returns true if the devices state has been updated.
	def patch_decision_state_deviceid_state(self, deviceid, state):
		print 'Patch api/decision/state/{deviceid}/{state}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			# headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn.request('PATCH', '/api/decision/state/' + deviceid + '/' + state)
			res = conn.getresponse()
			print res.status, res.reason
			if (res.status == 404):
				return
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# GET api/decision/state
	# Get the state of the device from the JSON data provided, which is either true or false. 
	# Returns the state of the device. True if enabled, false if not.
	def get_decision_state(self, device_data):
		print 'GET api/decision/state'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn.request('GET', '/api/decision/state/', json.dumps(device_data), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	#------------------------------------Storage------------------------------------#
	#----Device----#

	# GET api/storage/device/{houseid}/{spaceid}/{deviceid}
	# Gets the device information with the specified house ID, space ID, and device ID.
	def get_storage_device_houseid_spaceid_deviceid(self, houseid, spaceid, deviceid):
		print 'GET api/storage/device/{houseid}/{spaceid}/{deviceid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/device/'+ houseid + '/' + spaceid + '/' + deviceid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}/{spaceid}/{type}
	# Gets all of the devices in the space of the type specified, with the provided house ID and space ID.
	def get_storage_device_houseid_spaceid_type(self, houseid, spaceid, Type):
		print 'GET api/storage/device/{houseid}/{type}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/device/' + houseid + '/' + spaceid + '/' + Type)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}/{spaceid}
	# Gets all of the devices information with the specified house ID and space ID.
	def get_storage_device_houseid_spaceid(self, houseid, spaceid):
		print 'GET api/storage/device/{houseid}/{spaceid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/device/'+ houseid + '/' + spaceid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}
	# Gets all of the devices in the house with the specified house ID.
	def get_storage_device_houseid(self, houseid):
		print 'GET api/storage/device/{houseid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/device/' + houseid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}/{type}
	# Gets all of the devices in the house of the specified type, with the provided house ID.
	def get_storage_device_houseid_type(self, houseid, Type):
		print 'GET api/storage/device/{houseid}/{type}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/device/' + houseid + '/' +Type)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# POST api/storage/device
	# Post a device with the JSON object data given.
	def post_storage_device(self, device_data):
		print 'POST api/storage/device'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/storage/device', json.dumps(device_data), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# DELETE api/storage/device/{houseid}/{spaceid}/{deviceid}
	# Deletes a device with the specified house ID, space ID, and device ID provided.
	def delete_storage_device_houseid_spaceid_deviceid(self, houseid, spaceid, deviceid):
		print "DELETE api/storage/device/{houseid}/{spaceid}/{deviceid}"
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('DELETE', '/api/storage/device/' + houseid + '/' +spaceid + '/' + deviceid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	#----Space----#

	# GET api/storage/space/{houseid}/{spaceid}
	# Gets the space information with the houseid and spaceid provided.
	def get_storage_space_houseid_spaceid(self, houseid, spaceid):
		print 'GET api/storage/space/{houseid}/{spaceid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/space/' + houseid +'/' + spaceid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# POST api/storage/space
	# Posts the space with the JSON object data information provided.
	def post_storage_space(self, space_data):
		print 'POST api/storage/space'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/storage/space', json.dumps(space_data), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True) 
		except Exception, e:
			raise e

	# DELETE api/storage/space/{houseid}/{spaceid}
	# Deletes the space specified by the houseid and spaceid
	def delete_storage_space_houseid_spaceid(self, houseid, spaceid):
		print 'DELETE api/storage/space/{houseid}/{spaceid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('DELETE', '/api/storage/space/' + houseid + '/' + spaceid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	#----House----#

	# GET api/storage/house/{houseid}
	# Gets the houses information with the specified houseid.
	def get_storage_house_houseid(self, houseid):
		print 'GET api/storage/house/{houseid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/house/' + houseid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# POST api/storage/house
	# Posts the house with the JSON object information provided.
	def post_storage_house(self, house_data):
		print 'POST api/storage/house'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/storage/house', json.dumps(house_data), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# DELETE api/storage/house/{houseid}
	# Deletes the house with the specified houseid.
	def delete_storage_house_houseid(self, houseid):
		print 'DELETE api/storage/house/{houseid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('DELETE', '/api/storage/house/' + houseid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	#----user----#

	# GET api/storage/user/{username}
	# Gets the users information by the username provided via JSON object data.
	def get_storage_user_username(self, username):
		print 'GET api/storage/user/{username}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/storage/user/' + username)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# POST api/storage/user
	# Posts the users information provided by JSON object data.
	def post_storage_user(self, user_data):
		print 'POST api/storage/user'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/storage/user', json.dumps(user_data), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# DELETE api/storage/api/stroage/user/{username}
	# Deletes the user specified by the username.
	def delete_storage_user_username(self, username):
		print 'DELETE api/storage/api/stroage/user/{username}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('DELETE', '/api/storage/api/stroage/user/' + username)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# POST api/house/device/state 
	# Posts the updated device info (blob) to persistent storage and updates the state change cache

	#------------------------------------App------------------------------------#
	#----User----#
	# POST api/app/user/updateposition/{username}
	# Updates the user position and location time stamp to the user, requires JSON object data and the username for the user.
	def post_app_user_updatePosition_username(self, location):
		print 'testing: POST api/app/user/updateposition/{username}'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			# conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/app/user/updateposition/' + location['userId'], json.dumps(location), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# POST api/app/user/devicetoken/{username} 
	# Posts the devicetoken for a given user.
	def post_app_user_devicetoken_username(self, devicetoken):
		print 'POST api/app/user/devicetoken/{username}'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			# conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/app/user/devicetoken/' + devicetoken['username'], json.dumps(devicetoken), headers)
			res = conn.getresponse()
			print res.status, res.reason
			if (res.status == 404):
				return
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# POST api/app/user/brighten
	# A request from the App system to make something brighter near their location. Information provided in a JSON. 
	# Returns a bool that is true if the information was posted, false if not.
	def post_app_user_brighten(self, brighten_info):
		print 'POST api/app/user/brighten'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/app/user/brighten/', json.dumps(brighten_info), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	#----Device----#
	# GET api/app/device/{deviceid} 
	# Get pending JSON device data with the device ID provided.
	def get_app_device_deviceid(self, deviceid):
		print 'GET api/app/device/{deviceid}'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/app/device/' + deviceid)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# GET api/app/device 
	# Get all the pending JSON device data as an array of JSON data.
	def get_app_device_all(self):
		print 'GET api/app/device'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/app/device/')
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	# GET api/app/device/count 
	# Get the count of the pending JSON device data.
	def get_app_device_count(self):
		print 'GET api/app/device/count'
		try:
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('GET', '/api/app/device/count')
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	#------------------------------------Sim------------------------------------#
	# POST api/sim/timeframe
	# Post the information via JSON data regarding the SimHarneses time frame configurations to the Decision Making System.
	def post_sim_timeframe(self, timeframe):
		print 'testing: POST api/sim/timeframe'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			# conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/sim/timeframe', json.dumps(timeframe), headers)
			res = conn.getresponse()
			print res.status, res.reason
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e

	#------------------------------------House------------------------------------#
	# POST api/house/device/state
	# Post and updated device blob to the system (persistent storage and app cache/queue)
	def post_house_device_state(self, device_data):
		print 'POST api/house/device/state'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName)
			conn.request('POST', '/api/house/device/state', json.dumps(device_data), headers)
			res = conn.getresponse()
			print res.status, res.reason
			if (res.status == 404):
				return
			s = json.loads(res.read())
			print json.dumps(s, indent=4, sort_keys=True)
		except Exception, e:
			raise e
