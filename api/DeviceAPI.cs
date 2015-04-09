using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace api
{

public class Interfaces
{
	protected HttpClient _http;
	protected Uri _server;

	public Interfaces(Uri serverAddress)
	{
		_http = new HttpClient();
		_server = serverAddress;
	}

	/**
	 * Registers a device with the server, in essence creating it for use in HATS.
	 * \param[in] address Address to locate the device
	 * \param[in] name User friendly name to use for the device.
	 * \param[in] type Type name of the device. Should map to a class name in Devices
	 * \param[in] house_id ID of the house this device is in.
	 * \param[in room_id ID of the room this device is in, if set.
	 */
	Device registerDevice(Uri address, string name, string type, UInt64 house_id, UInt64 room_id = 0)
	{
		//TODO: Post to Server API to request the device be recorded, and get the device.
		var device = (Device)Activator.CreateInstance(Type.GetType(type));
		return device;
	}

	/**
	 * Function to get a list of devices from the server, given parameters
	 */
	List<Device> getDevices(UInt64 houseID)
	{
		var devices = new List<Device>();
		//TODO: Query all devices in a given house.
		return devices;
	}

	/**
	 * Function to get a List of devices from the server, given house & room.
	 */
	List<Device> getDevices(UInt64 houseID, UInt64 roomID)
	{
		var devices = new List<Device>();
		//TODO: Query all devices in a given room.
		return devices;
	}

	/**
	 * Given a JSON string representing a device, instantiates the device as desired.
	 * \param[in] info JSON string representing device. Must have a key named "class" which
	 *            names the class deriving from Device to instantiate.
	 */
	public static Device DeserializeDevice(string info, IDeviceInput inp, IDeviceOutput outp)
	{
		JObject device_obj = JObject.Parse(info);
		JToken type_tok;
		if(!device_obj.TryGetValue("class", out type_tok))
		{
			return null;
		}

		var device_type = GetDeviceType(type_tok.ToString());
		Device device = null;
		if(device_type != null)
		{
			device = (Device)Activator.CreateInstance(device_type, inp, outp);
			JsonConvert.PopulateObject(info, device);
		}
		return device;
	}

	/**
	 * Attempts to get the type of a specific device, given the fully
	 * qualified name.
	 * \param[in] typeName Fully qualified classname of device
	 * \param[out] Type representing device, or NULL if it doesn't exist
	 */
	private static Type GetDeviceType(string typeName)
	{
		Type device_type = Type.GetType("api." + typeName);
		if(device_type != null)
		{
			return device_type;
		}
		/*
		foreach(var a in AppDomain.CurrentDomain.GetAssemblies())
		{
			device_type = a.GetType(typeName);

			if(type != null && device_type.IsSubclassOf(typeof(Device)))
			{
				return type;
			}
		}
		*/

		return null;
	}
}
}

