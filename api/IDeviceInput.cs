/**
 * Base interface for reading state from a device.
 * \author Jason Ziglar <jpz@vt.edu>
 */
using System;

namespace api
{

public interface IDeviceInput
{
	bool read(Device dev);
}

}
