using System.Net;

namespace HomeApi.TestsFixtures.Tests;

public class GlobalCacheFixtureTests : IClassFixture<GlobalCacheFixture>
{
	private readonly Helpers.GlobalCache.IService _service;

	public GlobalCacheFixtureTests(GlobalCacheFixture fixture)
	{
		_service = fixture.Service;
	}

	[Theory]
	[InlineData(
		"192.168.1.116",
		"sendir,1:1,3,40192,3,1,96,24,48,24,24,24,48,24,24,24,48,24,24,24,24,24,24,24,24,24,24,24,24,24,48,24,48,24,24,24,24,4000\r")]
	public async Task ToggleAmpPowerTests(string ipString, string message)
	{
		var ip = IPAddress.Parse(ipString);
		await _service.ConnectAsync(ip);
		await _service.SendAndReceiveAsync(message);
	}
}
