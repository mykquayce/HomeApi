namespace HomeApi.TestsFixtures;

public sealed class GlobalCacheFixture : IDisposable
{
	public GlobalCacheFixture()
	{
		var config = Helpers.GlobalCache.Config.Defaults;

		Client = new Helpers.GlobalCache.Concrete.Client(config);
		Service = new Helpers.GlobalCache.Concrete.Service(config);
	}

	public Helpers.GlobalCache.IClient Client { get; }
	public Helpers.GlobalCache.IService Service { get; }

	public void Dispose() => Service.Dispose();
}
