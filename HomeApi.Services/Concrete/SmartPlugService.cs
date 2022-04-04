using Dawn;

namespace HomeApi.Services.Concrete;

public class SmartPlugService : ISmartPlugService
{
	private readonly Helpers.TPLink.ITPLinkClient _tpLinkClient;
	private readonly Clients.INetworkDiscoveryClient _networkDiscoveryClient;

	public SmartPlugService(
		Helpers.TPLink.ITPLinkClient tpLinkClient!!,
		Clients.INetworkDiscoveryClient networkDiscoveryClient!!)
	{
		_tpLinkClient = tpLinkClient;
		_networkDiscoveryClient = networkDiscoveryClient;
	}

	public async Task<(double amps, double volts, double watts)> GetPowerDrawAsync(Models.Devices device, CancellationToken? cancellationToken = default)
	{
		Guard.Argument(device).NotDefault();
		var ip = await _networkDiscoveryClient.GetIPAddressAsync(device, cancellationToken);
		var (amps, volts, watts) = await _tpLinkClient.GetRealtimeDataAsync(ip);
		return (amps, volts, watts);
	}
}
