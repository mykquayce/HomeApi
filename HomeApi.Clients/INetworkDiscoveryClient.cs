using System.Net;
using System.Net.NetworkInformation;

namespace HomeApi.Clients;

public interface INetworkDiscoveryClient
{
	Task<IPAddress> GetIPAddressAsync(Models.Devices device, CancellationToken? cancellationToken = default);
	Task<IPAddress> GetIPAddressFromPhysicalAddressAsync(PhysicalAddress physicalAddress, CancellationToken? cancellationToken = default);
}
