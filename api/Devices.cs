/*
 * Declaration of all devices and the unique classes that inherit from device class that hold parameters and 
 * characteristics of each device.
 * Contributors:
 *   Pedro Sorto
 *   Steven Cho
 *   Dong Nan
 *   Aakruthi Gopisetty
 *   Kara Dodenhoff
 *   Danny Mota
 *   Jason Ziglar <jpz@vt.edu>
*/
using System;
using System.Collections.Generic;

namespace api
{
/**
 * Represents a device which can be enabled or disabled.
 */
interface IEnableable
{
	bool Enabled
	{
		get;
		set;
	}
};

/**
 * Interface defining an object which can accept some number of discrete states
 */
interface IDiscreteSetting
{
	/**
	 * Discrete State of this device.
	 */
	Int64 State
	{
		get;
		set;
	}

	//! Minimum value this particular device will accept
	Int64 MinState();
	//! Maximum value this device will accept
	Int64 MaxState();
};

/**
 * Interface defining how to set a set point for the behavior of a given Device
 */
interface ISetPointable<Type> where Type : ControlTypes
{
	/**
	 * Target set point of this device. For example, the set point of a thermostat
	 */
	Type SetPoint
	{
		get;
		set;
	}
}

/**
 * Class which represents the ability to read a particular type of data.
 */
interface IReadable<Type> where Type : ControlTypes
{
	Type Value
	{
		get;
	}
}

public class FullID
{
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
	public UInt64 Room
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

public interface IDeviceOutput
{
	bool write(Device dev);
}

public interface IDeviceInput
{
	bool read(Device dev);
}

/**
 * Base class representing the common parameters for any given device. All Devices inherit from this
 */
public abstract class Device
{
	public Device(IDeviceInput inp, IDeviceOutput outp)
	{
		_in = inp;
		_out = outp;
	}

	public FullID ID
	{
		get;
		set;
	}

	/**
	 * User friendly name for this device.
	 */
	public string Name
	{
		get;
		set;
	}

	protected IDeviceInput _in;
	protected IDeviceOutput _out;
}

/**
 * Class representing a garage door in the house.
 */
public class GarageDoor : Device, IEnableable
{
	public GarageDoor(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
	}

	public bool Enabled
	{
		get;
		set;
	}
}

/**
 * A ceiling fan, which can be turned on and off, and also have a speed setting
 */
public class CeilingFan : Device, IEnableable, IDiscreteSetting
{
	public CeilingFan(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
	}
	public bool Enabled
	{
		get;
		set;
	}

	public Int64 State
	{
		get;
		set;
	}

	public Int64 MinState()
	{
		return 0;
	}

	public Int64 MaxState()
	{
		return 100;
	}
}

/**
 * Alarm which can be enabled/disabled
 */
public class AlarmSystem : Device, IEnableable
{
	public AlarmSystem(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
	}

	public bool Enabled
	{
		get;
		set;
	}
}

/**
 * Binary light switch.
 */
public class LightSwitch : Device, IEnableable, IReadable<Light>
{
	public LightSwitch(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
	}

	public bool Enabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			if(value)
			{
				_light.Brightness = 1.0;
			}
			else
			{
				_light.Brightness = 0.0;
			}
			_enabled = value;
		}
	}

	public Light Value
	{
		get
		{
			return _light;
		}
		protected set
		{
			_light = value;
		}
	}
	protected Light _light;
	protected bool _enabled;
}

/**
 * Thermostat for a house, which can have a setpoint and a measured value
 */
public class Thermostat : Device, IEnableable, ISetPointable<Temperature>, IReadable<Temperature>
{
	public Thermostat(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
	}
	public bool Enabled
	{
		get;
		set;
	}

	public Temperature SetPoint
	{
		get;
		set;
	}

	public Temperature Value
	{
		get;
		protected set;
	}

	public Int64 MinState()
	{
		return -100;
	}
	public Int64 MaxState()
	{
		return 200;
	}
}
}
