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
		return url.split(':')[0]
	except Exception, e:
		print 'parse url failed'
		raise e

def getPortNumber(url):
	try:
		return url.split(':')[1]
	except Exception, e:
		raise e

class ServerAPITest:
	def __init__(self, url):
		self.server_url = url;
		self.hostName = getHostName(self.server_url);
		self.portNumber = getPortNumber(self.server_url);

	# #Decision

	# # Patch api/decision/state/{deviceid}/{state}
	# # Updates a devices state with the given device ID to the state specified. 
	# # Returns true if the devices state has been updated.

	# # POST api/decision/state/{deviceid}
	# # Update a device state to a device. Requires JSON object data.
	# # Returns true if the information was posted, false if not.
	# def post_decision_state_deviceid:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('POST', '/api/decision/state/{deviceid}')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	#storage device

	# GET api/storage/device/{houseid}/{spaceid}/{deviceid}
	# Gets the device information with the specified house ID, space ID, and device ID.
	def get_storage_device_houseid_spaceid_deviceid(houseid, spaceid, deviceid):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/device/'+ houseid + '/' + spaceid + '/' + deviceid)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}/{spaceid}
	# Gets all of the devices information with the specified house ID and space ID.
	def get_storage_device_houseid_spaceid(houseid, spaceid):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/device/'+ houseid + '/' + spaceid)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}/{spaceid}/{type}
	# Gets all of the devices in the space of the type specified, with the provided house ID and space ID.
	def get_storage_device_houseid_spaceid_type(houseid, spaceid, Type):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/device/' + houseid + '/' + spaceid + '/' + Type)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}
	# Gets all of the devices in the house with the specified house ID.
	def get_storage_device_houseid(houseid):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/device/' + houseid)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# GET api/storage/device/{houseid}/{type}
	# Gets all of the devices in the house of the specified type, with the provided house ID.
	def get_storage_device_houseid_type(houseid, Type):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/device/' + houseid + '/' +Type)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# # POST api/storage/device
	# # Posts a device with the JSON object data given.
	# def post_storage_device:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('POST', '/api/storage/device')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	# # DELETE api/storage/device/{houseid}/{spaceid}/{deviceid}
	# # Deletes a device with the specified house ID, space ID, and device ID provided.
	# def delete_storage_device_houseid_spaceid_deviceid:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('DELETE', '/api/storage/device/{houseid}/{spaceid}/{deviceid}')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	#storage space

	# GET api/storage/space/{houseid}/{spaceid}
	# Gets the space information with the houseid and spaceid provided.
	def get_storage_space_houseid_spaceid(houseid, spaceid):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/space/' + houseid +'/' + spaceid)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# # POST api/storage/space
	# # Posts the space with the JSON object data information provided.
	# def post_storage_space:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('POST', '/api/storage/space')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	# # DELETE api/storage/space/{houseid}/{spaceid}
	# # Deletes the space specified by the houseid and spaceid
	# def delete_storage_space_houseid_spaceid:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('DELETE', '/api/storage/space/{houseid}/{spaceid}')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	#storage house

	# GET api/storage/house/{houseid}
	# Gets the houses information with the specified houseid.
	def get_storage_house_houseid(houseid):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/house/' + houseid)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# # POST api/storage/house
	# # Posts the house with the JSON object information provided.
	# def post_storage_house:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('POST', '/api/storage/house')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	# # DELETE api/storage/house/{houseid}
	# # Deletes the house with the specified houseid.
	# def delete_storage_house_houseid:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('DELETE', '/api/storage/house/{houseid}')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	#storage user

	# GET api/storage/user/{username}
	# Gets the users information by the username provided via JSON object data.
	def get_storage_user_username(username):
		try:
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('GET', '/api/storage/user/' + username)
			res = conn.getresponse()
			if res.status == 200:
				res.read()
			print res.status
		except Exception, e:
			raise e

	# # POST api/storage/user
	# # Posts the users information provided by JSON object data.
	# def post_storage_user:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('POST', '/api/storage/user')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	# # DELETE api/storage/api/stroage/user/{username}
	# # Deletes the user specified by the username.
	# def delete_storage_user_username:
	# 	try:
	# 		conn = httplib.HTTPConnection(getHostName(server_url), getPortNumber(server_url))
	# 		conn.request('DELETE', '/api/storage/api/stroage/user/{username}')
	# 		res = conn.getresponse()
	# 		res.status
	# 	except Exception, e:
	# 		raise e

	# #app

	# POST api/app/user/updateposition/{username}
	# Updates the user position and location time stamp to the user, requires JSON object data and the username for the user.
	def post_app_user_updatePosition_username(self, location):
		print 'testing: POST api/app/user/updateposition/{username}'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('POST', '/api/app/user/updateposition/' + location['userId'], json.dumps(location), headers)
			res = conn.getresponse()
			print res.status
			print res.read()
		except Exception, e:
			raise e
 
	#Sim

	# POST api/sim/timeframe
	# Post the information via JSON data regarding the SimHarneses time frame configurations to the Decision Making System.
	def post_sim_timeframe(self, timeframe):
		print 'testing: POST api/sim/timeframe'
		try:
			headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
			conn = httplib.HTTPConnection(self.hostName, self.portNumber)
			conn.request('POST', '/api/sim/timeframe', json.dumps(timeframe), headers)
			res = conn.getresponse()
			print res.status
			print res.read()
		except Exception, e:
			raise e

if __name__ == "__main__":
	# start server
	url = 'localhost:8081'
	server = ServerAPITest(url)
	# POST api/sim/timeframe
	time = {"localTime":str(datetime.datetime.now())}
	server.post_sim_timeframe(time)
	# POST api/app/user/updateposition/{username}
	userID = 'ningli'
	location = {"userId": userID,
				"lat": "37.874342",
				"long": "-86.342234",
				"alt": "21.5452",
				"time": time}
	server.post_app_user_updatePosition_username(location)
