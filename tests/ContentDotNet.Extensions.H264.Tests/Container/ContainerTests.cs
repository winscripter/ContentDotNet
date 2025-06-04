using ContentDotNet.Containers;

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

    [Fact]
    public void TestContainerMatrix16x2()
    {
        var matrix = new ContainerMatrix16x2();
        var rand = new Random(456);

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                matrix[x, y] = (uint)rand.Next(1, 100_000_000);
            }
        }

        rand = new Random(456);

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                Assert.Equal(rand.Next(1, 100_000_000), (int)matrix[x, y]);
            }
        }
    }

    [Fact]
    public void TestContainerMatrix2x16x16()
    {
        var matrix = new ContainerMatrix2x16x16();
        var rand = new Random(789);

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                for (int z = 0; z < 16; z++)
                {
                    matrix[x, y, z] = (uint)rand.Next(1, 100_000_000);
                }
            }
        }

        rand = new Random(789);

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                for (int z = 0; z < 16; z++)
                {
                    Assert.Equal(rand.Next(1, 100_000_000), (int)matrix[x, y, z]);
                }
            }
        }
    }

    [Fact]
    public void TestContainerMatrix4x16x2()
    {
        var matrix = new ContainerMatrix4x16x2();
        var rand = new Random(123);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    matrix[x, y, z] = rand.Next(1, 100_000_000);
                }
            }
        }

        rand = new Random(123);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    Assert.Equal(rand.Next(1, 100_000_000), (int)matrix[x, y, z]);
                }
            }
        }
    }

    [Fact]
    public void TestContainerMatrix4x4x2()
    {
        var matrix = new ContainerMatrix4x4x2();
        var rand = new Random(456);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    matrix[x, y, z] = rand.Next(1, 100_000_000);
                }
            }
        }

        rand = new Random(456);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    Assert.Equal(rand.Next(1, 100_000_000), matrix[x, y, z]);
                }
            }
        }
    }

    [Fact]
    public void TestContainerMatrix4x64()
    {
        var matrix = new ContainerMatrix4x64();
        var rand = new Random(789);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                matrix[x, y] = (uint)rand.Next(1, 100_000_000);
            }
        }

        rand = new Random(789);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                Assert.Equal(rand.Next(1, 100_000_000), (int)matrix[x, y]);
            }
        }
    }
}
