using ContentDotNet.Containers;

namespace ContentDotNet.Extensions.H264.Internal;

internal static class ContainerConverter
{
    public static Container64Byte ToBytes(Container64UInt32 cnt)
    {
        var result = new Container64Byte();

        for (int i = 0; i < 64; i++)
        {
            result[i] = (byte)cnt[i];
        }

        return result;
    }

    public static ContainerMatrix16x16Byte ToBytes(ContainerMatrix16x16 cnt)
    {
        var result = new ContainerMatrix16x16Byte();

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                result[x, y] = (byte)cnt[x, y];
            }
        }

        return result;
    }

    public static ContainerMatrix4x64Byte ToBytes(ContainerMatrix4x64 cnt)
    {
        var result = new ContainerMatrix4x64Byte();

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                result[x, y] = (byte)cnt[x, y];
            }
        }

        return result;
    }

    public static Container64UInt32 ToUInt32(Container64Byte cnt)
    {
        var result = new Container64UInt32();

        for (int i = 0; i < 64; i++)
        {
            result[i] = cnt[i];
        }

        return result;
    }

    public static ContainerMatrix16x16 ToUInt32(ContainerMatrix16x16Byte cnt)
    {
        var result = new ContainerMatrix16x16();

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                result[x, y] = cnt[x, y];
            }
        }

        return result;
    }

    public static ContainerMatrix4x64 ToUInt32(ContainerMatrix4x64Byte cnt)
    {
        var result = new ContainerMatrix4x64();

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                result[x, y] = (byte)cnt[x, y];
            }
        }

        return result;
    }
}
