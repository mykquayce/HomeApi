namespace HomeApi.TestsFixtures.Tests;

public class IdentityClientFixtureTests : IClassFixture<IdentityClientFixture>
{
	private readonly Helpers.Identity.Clients.IIdentityClient _identityClient;

	public IdentityClientFixtureTests(IdentityClientFixture fixture)
	{
		_identityClient = fixture.IdentityClient;
	}

	[Theory]
	[InlineData(3_000, @"^[-0-9A-Z_a-z]+\.[-0-9A-Z_a-z]+\.[-0-9A-Z_a-z]+$")]
	public async Task GetAccessTokenTests(int millisecondsDelay, string pattern)
	{
		string token;
		{
			using var cts = new CancellationTokenSource(millisecondsDelay);
			token = await _identityClient.GetAccessTokenAsync(cts.Token);
		}

		Assert.Matches(pattern, token);
	}
}
