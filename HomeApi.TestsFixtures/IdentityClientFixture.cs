using Microsoft.Extensions.Caching.Memory;

namespace HomeApi.TestsFixtures;

public class IdentityClientFixture : IDisposable
{
	private static readonly Uri _authority = new("https://identityserver");
	private const string _clientId = "elgatoapi";
	private const string _clientSecret = "8556e52c6ab90d042bb83b3f0c8894498beeb65cf908f519a2152aceb131d3ee";
	private const string _scope = "networkdiscovery";

	private readonly HttpClientHandler _httpClientHandler;
	private readonly HttpClient _httpClient;
	private readonly IMemoryCache _memoryCache;

	public IdentityClientFixture()
	{
		var config = new Helpers.Identity.Config(_authority, _clientId, _clientSecret, _scope);
		_httpClientHandler = new() { AllowAutoRedirect = false, };
		_httpClient = new(_httpClientHandler) { BaseAddress = _authority, };
		_memoryCache = new MemoryCache(new MemoryCacheOptions());

		IdentityClient = new Helpers.Identity.Clients.Concrete.IdentityClient(config, _httpClient, _memoryCache);
	}

	public Helpers.Identity.Clients.IIdentityClient IdentityClient { get; set; }

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "derived types don't introduce a finalizer")]
	public void Dispose()
	{
		_httpClient.Dispose();
		_httpClientHandler.Dispose();
		_memoryCache.Dispose();
	}
}
