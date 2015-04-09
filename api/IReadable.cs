/**
 * Interface for a Device to report some sensed amount.
 * \author: Jason Ziglar <jpz@vt.edu>
 */
using System;

namespace api
{
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
}

