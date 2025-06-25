using ContentDotNet.Primitives;

namespace ContentDotNet.Abstractions.Tests.Primitives;

public class Int32BooleanTests
{
    [Fact]
    public void TestFlippedBoolean()
    {
        bool source = false;
        source = Int32Boolean.B(1 - Int32Boolean.I32(source));
        Assert.True(source);

        source = Int32Boolean.B(1 - Int32Boolean.I32(source));
        Assert.False(source);
    }
}
