using Microsoft.Extensions.Options;

namespace HomeApi.Services.Tests;

public class InfraredServiceTests :
	IClassFixture<TestsFixtures.NetworkDiscoveryClientFixture>,
	IClassFixture<TestsFixtures.GlobalCacheFixture>
{
	private readonly IInfraredService _sut;

	public InfraredServiceTests(
		TestsFixtures.GlobalCacheFixture globalCacheFixture,
		TestsFixtures.NetworkDiscoveryClientFixture networkDiscoveryClientFixture)
	{
		var infraredLookup = new Models.InfraredLookup
		{
			[Models.Infrareds.Amp_Power] = "sendir,1:1,3,40192,3,1,96,24,48,24,24,24,48,24,24,24,48,24,24,24,24,24,24,24,24,24,24,24,24,24,48,24,48,24,24,24,24,4000\r",
		};

		var globalCacheService = globalCacheFixture.Service;
		var networkDiscoveryClient = networkDiscoveryClientFixture.NetworkDiscoveryClient;

		_sut = new Concrete.InfraredService(Options.Create(infraredLookup), networkDiscoveryClient, globalCacheService);
	}

	[Theory]
	[InlineData(Models.Devices.IRBlaster, Models.Infrareds.Amp_Power)]
	public Task SendInfraredMessageTests(Models.Devices device, Models.Infrareds infrared) => _sut.SendInfraredMessageAsync(device, infrared);
}
