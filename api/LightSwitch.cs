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
 * Binary light switch.
 */
public class LightSwitch : Device, IEnableable, IReadable<Light>
{
	public LightSwitch(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
		_enabled = false;
		_light = new Light();
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

}
