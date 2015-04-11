/**
 * Interface for how a Device object will write out to the remote end.
 * \author Jason Ziglar <jpz@vt.edu>
 */
using System;

namespace api
{

public interface IDeviceOutput
{
	bool write(Device dev);
}

}

