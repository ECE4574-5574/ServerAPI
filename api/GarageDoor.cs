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
 * Class representing a garage door in the house.
 */
public class GarageDoor : Device, IEnableable
{
	public GarageDoor(IDeviceInput inp, IDeviceOutput outp) :
	base(inp, outp)
	{
		Enabled = true;
	}

	public bool Enabled
	{
		get;
		set;
	}
}

}
