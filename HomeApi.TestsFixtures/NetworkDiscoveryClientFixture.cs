using HomeApi.Clients;
using System.Net.NetworkInformation;

namespace HomeApi.TestsFixtures;

public sealed class NetworkDiscoveryClientFixture : IdentityClientFixture
{
	private static readonly Uri _baseAddress = new("https://networkdiscovery");
	private readonly HttpClientHandler _httpClientHandler;
	private readonly HttpClient _httpClient;

	public NetworkDiscoveryClientFixture()
	{
		_httpClientHandler = new() { AllowAutoRedirect = false, };
		_httpClient = new(_httpClientHandler) { BaseAddress = _baseAddress, };

		var deviceLookup = new Models.DeviceLookup
		{
			[Models.Devices.AmpSmartPlug] = PhysicalAddress.Parse("003192e1a68b"),
			[Models.Devices.IRBlaster] = PhysicalAddress.Parse("000c1e059cad"),
		};

		var deviceAliasesOptions = Microsoft.Extensions.Options.Options.Create(deviceLookup);

		NetworkDiscoveryClient = new Clients.Concrete.NetworkDiscoveryClient(_httpClient, base.IdentityClient, deviceAliasesOptions);
	}

	public INetworkDiscoveryClient NetworkDiscoveryClient { get; }

	public new void Dispose()
	{
		_httpClient.Dispose();
		_httpClientHandler.Dispose();
		base.Dispose();
	}
}
