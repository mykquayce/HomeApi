namespace HomeApi.Services.Tests;

public class SmartPlugServiceTests :
	IClassFixture<TestsFixtures.NetworkDiscoveryClientFixture>,
	IClassFixture<TestsFixtures.TPLinkClientFixture>
{
	private readonly ISmartPlugService _sut;

	public SmartPlugServiceTests(
		TestsFixtures.NetworkDiscoveryClientFixture networkDiscoveryClientFixture,
		TestsFixtures.TPLinkClientFixture tpLinkClientFixture)
	{
		var networkDiscoveryClient = networkDiscoveryClientFixture.NetworkDiscoveryClient;
		var tpLinkClient = tpLinkClientFixture.TPLinkClient;

		_sut = new Concrete.SmartPlugService(tpLinkClient, networkDiscoveryClient);
	}

	[Theory]
	[InlineData(Models.Devices.AmpSmartPlug)]
	public async Task GetPowerDrawTests(Models.Devices device)
	{
		var (amps, volts, watts) = await _sut.GetPowerDrawAsync(device);

		Assert.InRange(amps, .01, .1);
		Assert.InRange(volts, 230, 250);
		Assert.InRange(watts, 1, 10);
	}
}
