/**
 * Interface describing a device which has some number of discrete states to enter/exit.
 * \author: Jason Ziglar <jpz@vt.edu>
 */
using System;

namespace api
{
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
}

