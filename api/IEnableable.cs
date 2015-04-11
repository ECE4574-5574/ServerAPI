/**
 * Interface represeneting a device which can be enabled/disabled.
 * \author Jason Ziglar <jpz@vt.edu>
 */
using System;

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
}

