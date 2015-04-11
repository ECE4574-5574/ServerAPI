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
 * Alarm which can be enabled/disabled
 */
public class AlarmSystem : Device, IEnableable
{
	public AlarmSystem(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
		Enabled = false;
	}

	public bool Enabled
	{
		get;
		set;
	}
}

}
