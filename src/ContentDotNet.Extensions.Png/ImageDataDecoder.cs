using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.Png;

internal static class ImageDataDecoder
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int PaethPredictor(int a, int b, int c)
    {
        int p = a + b - c;
        int pa = Math.Abs(p - a);
        int pb = Math.Abs(p - b);
        int pc = Math.Abs(p - c);
        int Pr;
        if (pa <= pb && pa <= pc) Pr = a; 
        else if (pb <= pc) Pr = b; 
        else Pr = c;
        return Pr;
    }

    public static byte Filter(byte x, byte a, byte b, byte c, int type)
    {
        return type switch
        {
            0 => x,
            1 => (byte)(x - a),
            2 => (byte)(x - Math.Floor((float)(a + b) / 2)),
            3 => (byte)(x - PaethPredictor(a, b, c)),
            _ => 255
        };
    }
}
