using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HomeApi.Clients.Tests;

public class ResolveAliasClientTests
{
	[Fact]
	public void DependencyInjectionTests()
	{
		// Arrange
		IServiceProvider serviceProvider;
		{
			IConfiguration configuration;
			{
				var initialData = new Dictionary<string, string?>
				{
					["AmpSmartPlug"] = "003192e1a68b",
					["IRBlaster"] = "000c1e059cad",
				};

				configuration = new ConfigurationManager()
					.AddInMemoryCollection(initialData)
					.Build();
			}

			serviceProvider = new ServiceCollection()
				.JsonConfig<Models.DeviceLookup>(configuration)
				//.AddTransient<IResolveAliasClient<int>, ResolveAliasClient<int>>()
				.BuildServiceProvider();
		}

		// Act
		var lookupOptions = serviceProvider.GetRequiredService<IOptions<Models.DeviceLookup>>();

		// Assert
		Assert.NotNull(lookupOptions);
		Assert.NotNull(lookupOptions.Value);

		var lookup = lookupOptions.Value;

		Assert.NotEmpty(lookup);
		Assert.Contains(Models.Devices.AmpSmartPlug, lookup.Keys);
		Assert.Contains(Models.Devices.IRBlaster, lookup.Keys);

		Assert.Equal("003192e1a68b", lookup[Models.Devices.AmpSmartPlug].ToString(), StringComparer.OrdinalIgnoreCase);
		Assert.Equal("000c1e059cad", lookup[Models.Devices.IRBlaster].ToString(), StringComparer.OrdinalIgnoreCase);

		/*
		// Act
		var client = serviceProvider.GetRequiredService<IResolveAliasClient<int>>();

		// Assert
		Assert.NotNull(client);
		var actual = client.Resolve(key);
		Assert.Equal(value, actual);*/
	}
}

public interface IResolveAliasClient<TValue>
{
	TValue Resolve(string alias);
}

public class ResolveAliasClient<TValue> : IResolveAliasClient<TValue>
{
	private readonly IDictionary<string, TValue> _lookup;

	public ResolveAliasClient(IOptions<Dictionary<string, TValue>> options)
	{
		_lookup = options.Value;
	}

	public TValue Resolve(string alias)
	{
		return _lookup.TryGetValue(alias, out var value)
			? value
			: throw new KeyNotFoundException($"'{alias}' not found in: {string.Join(',', _lookup.Keys)}");
	}
}

