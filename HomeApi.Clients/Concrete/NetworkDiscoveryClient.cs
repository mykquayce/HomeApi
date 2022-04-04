using Dawn;
using HomeApi.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.NetworkInformation;

namespace HomeApi.Clients.Concrete;

public class NetworkDiscoveryClient : Helpers.Identity.SecureWebClientBase, INetworkDiscoveryClient
{
	private readonly DeviceLookup _devicesLookup;

	public NetworkDiscoveryClient(
		HttpClient httpClient!!,
		Helpers.Identity.Clients.IIdentityClient identityClient!!,
		IOptions<DeviceLookup> deviceAliasesOptions!!)
		: base(httpClient, identityClient)
	{
		Guard.Argument(httpClient!).NotNull().Wrap(c => c.BaseAddress!)
			.NotNull().Require(uri => uri.IsAbsoluteUri, _ => "must be absolute uri").Wrap(uri => uri.OriginalString)
			.NotNull().NotEmpty().NotWhiteSpace();

		_devicesLookup = Guard.Argument(deviceAliasesOptions).NotNull().Wrap(o => o.Value)
			.NotNull().NotEmpty().DoesNotContainNull().Value;
	}

	public Task<IPAddress> GetIPAddressAsync(Devices device, CancellationToken? cancellationToken = default)
	{
		Guard.Argument(device).NotDefault().Require(_devicesLookup.Keys.Contains);
		var physicalAddress = _devicesLookup[device];
		return GetIPAddressFromPhysicalAddressAsync(physicalAddress, cancellationToken);
	}

	public async Task<IPAddress> GetIPAddressFromPhysicalAddressAsync(PhysicalAddress physicalAddress!!, CancellationToken? cancellationToken = default)
	{
		Guard.Argument(physicalAddress).NotNull().NotEqual(PhysicalAddress.None);
		var uri = new Uri("/api/router/" + physicalAddress, UriKind.Relative);
		(_, _, var model) = await SendAsync<Helpers.NetworkDiscoveryApi.Models.DhcpResponseObject>(HttpMethod.Get, uri, cancellationToken: cancellationToken);
		return model.ipAddress;
	}
}
