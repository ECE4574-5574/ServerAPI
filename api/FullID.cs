/**
 * Complete ID for a given device.
 * \author: Jason Ziglar <jpz@vt.edu>
 */
using System;

namespace api
{
public class FullID
{
	public FullID()
	{
		HouseID = 0;
		RoomID = 0;
		DeviceID = 0;
	}
	/**
	 * Identifier for the house this device is contained within
	 */
	public UInt64 HouseID
	{
		get;
		set;
	}
	/**
	 * Identifier for the room in which a device is contained.
	 * The value 0 represents a device which isn't assigned to a specific room.
	 */
	public UInt64 RoomID
	{
		get;
		set;
	}
	/**
	 * House specific identifier for this device. This requires the previous two
	 * IDs in order to uniquely identify this particular device.
	 */
	public UInt64 DeviceID
	{
		get;
		set;
	}
}
}

