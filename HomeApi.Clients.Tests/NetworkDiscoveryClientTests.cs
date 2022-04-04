using System.Net;
using System.Net.NetworkInformation;

namespace HomeApi.Clients.Tests;

public class NetworkDiscoveryClientTests : IClassFixture<TestsFixtures.NetworkDiscoveryClientFixture>
{
	private readonly INetworkDiscoveryClient _sut;

	public NetworkDiscoveryClientTests(TestsFixtures.NetworkDiscoveryClientFixture fixture)
	{
		_sut = fixture.NetworkDiscoveryClient;
	}

	[Theory]
	[InlineData("003192e1a68b")]
	public async Task GetIPAddressFromPhysicalAddressTests(string physicalAddressString)
	{
		// Arrange
		var physicalAddress = PhysicalAddress.Parse(physicalAddressString);

		// Act
		var ip = await _sut.GetIPAddressFromPhysicalAddressAsync(physicalAddress);

		// Assert
		Assert.NotNull(ip);
		Assert.NotEqual(IPAddress.None, ip);
	}
}
