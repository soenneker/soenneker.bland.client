using Soenneker.Bland.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Bland.Client.Tests;

[Collection("Collection")]
public class BlandClientUtilTests : FixturedUnitTest
{
    private readonly IBlandClient _util;

    public BlandClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IBlandClient>(true);
    }
}
