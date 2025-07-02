using ContentDotNet.Extensions.H264.Helpers;

namespace ContentDotNet.Extensions.H264.Tests.Helpers;

public class Clip3Tests
{
    [Fact]
    public void TestValueInRange()
    {
        Assert.Equal(42, Util264.Clip3(1, 50, 42));
    }

    [Fact]
    public void TestValueBelowRange()
    {
        Assert.Equal(42, Util264.Clip3(42, 420, 36));
    }

    [Fact]
    public void TestValueAboveRange()
    {
        Assert.Equal(420, Util264.Clip3(42, 420, 500));
    }
}
