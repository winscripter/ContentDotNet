using ContentDotNet.Extensions.H264.Containers;

namespace ContentDotNet.Extensions.H264.Tests.Container;

public class ContainerTests
{
    [Fact]
    public void TestContainerMatrix16x16()
    {
        var matrix = new ContainerMatrix16x16();
        var rand = new Random(123);

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                matrix[x, y] = (uint)rand.Next(1, 100_000_000);
            }
        }

        rand = new Random(123);

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                Assert.Equal(rand.Next(1, 100_000_000), (int)matrix[x, y]);
            }
        }
    }
}
