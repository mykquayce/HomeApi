using Dawn;
using HomeApi.Clients;
using HomeApi.Models;
using Microsoft.Extensions.Options;

namespace HomeApi.Services.Concrete;
public class InfraredService : IInfraredService
{
	private readonly InfraredLookup _infraredLookup;
	private readonly INetworkDiscoveryClient _networkDiscoveryClient;
	private readonly Helpers.GlobalCache.IService _globalCacheService;

	public InfraredService(
		IOptions<InfraredLookup> infraredLookupOptions!!,
		INetworkDiscoveryClient networkDiscoveryClient!!,
		Helpers.GlobalCache.IService globalCacheService!!)
	{
		_infraredLookup = Guard.Argument(infraredLookupOptions).NotNull().Wrap(o => o.Value)
			.NotNull().NotEmpty().DoesNotContainNull().Value;
		_networkDiscoveryClient = networkDiscoveryClient;
		_globalCacheService = globalCacheService;
	}

	public async Task SendInfraredMessageAsync(Devices device, Infrareds infrared, CancellationToken? cancellationToken = default)
	{
		Guard.Argument(device).NotDefault();
		Guard.Argument(infrared).NotDefault().Require(_infraredLookup.Keys.Contains);

		var message = _infraredLookup[infrared!];
		var ip = await _networkDiscoveryClient.GetIPAddressAsync(device);
		await _globalCacheService.ConnectAsync(ip);
		await _globalCacheService.SendAndReceiveAsync(message, cancellationToken);
	}
}
