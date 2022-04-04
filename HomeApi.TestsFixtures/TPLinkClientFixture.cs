namespace HomeApi.TestsFixtures;

public class TPLinkClientFixture
{
	public TPLinkClientFixture()
	{
		var config = Helpers.TPLink.Config.Defaults;
		TPLinkClient = new Helpers.TPLink.Concrete.TPLinkClient(config);
	}

	public Helpers.TPLink.ITPLinkClient TPLinkClient { get; }
}
