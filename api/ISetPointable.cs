/**
 * Interface defining how to set a set point for a Device
 * \author: Jason Ziglar <jpz@vt.edu>
 */
using System;

namespace api
{
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
}

