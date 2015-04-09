/**
 * Classes which represent meaningful quantities which can be measured in the system
 */
using System;

namespace api
{

/**
 * Base class which is used for contraining Devices, representing some quantity which can be
 * measured/controlled.
 */
public class ControlTypes
{
	public ControlTypes()
	{
	}
}

/**
 *  Light level in a room, as measured by brightness. Currently, unit-less.
 */
public class Light : ControlTypes
{
	public double Brightness
	{
		get;
		set;
	}
}

/**
 * Temperature in a room, as measured in Celsius.
 */
public class Temperature : ControlTypes
{
	/**
	 * Generic accessor, which returns underlying measure without explicit type.
	 */
	public double Temp
	{
		get
		{
			return _temp_c;
		}
		set
		{
			_temp_c = value;
		}
	}

	/**
	 * Property which always returns in Celsius
	 */
	public double C
	{
		get
		{
			return _temp_c;
		}
		set
		{
			_temp_c = value;
		}
	}

	/**
	 * Property which always returns in Farenheit
	 */
	public double F
	{
		get
		{
			return _temp_c * 1.8 + 32.0;
		}
		set
		{
			_temp_c = (value - 32.0) * (5.0 / 9.0);
		}
	}

	protected double _temp_c;
}

}

