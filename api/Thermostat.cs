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

namespace api
{

/**
 * Thermostat for a house, which can have a setpoint and a measured value
 */
public class Thermostat : Device, IEnableable, ISetPointable<Temperature>, IReadable<Temperature>
{
	public Thermostat(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
		Enabled = false;
		SetPoint = new Temperature()
		{
			Temp = 0
		};
		Value = new Temperature()
		{
			Temp = 0
		};
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
