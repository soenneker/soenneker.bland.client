using Soenneker.Bland.Client.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Bland.Client.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class BlandClientUtilTests : HostedUnitTest
{
    private readonly IBlandClientUtil _util;

    public BlandClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<IBlandClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
