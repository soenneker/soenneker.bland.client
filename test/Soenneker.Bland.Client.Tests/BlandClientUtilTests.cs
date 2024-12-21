using Soenneker.Bland.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Bland.Client.Tests;

[Collection("Collection")]
public class BlandClientUtilTests : FixturedUnitTest
{
    private readonly IBlandClientUtil _util;

    public BlandClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IBlandClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
