using ContentDotNet.BitStream;
using ContentDotNet.Extensions.Video.H264;
using ContentDotNet.Extensions.Video.H264.Enumerations;

internal class Program
{
    private static void Main(string[] args)
    {
        const string filename = "output.h264";

        using var fs = File.OpenRead(filename);
        var bsr = new BitStreamReader(fs);

        var dcd = new H264Service().CreateDecoder(bsr);

        NalType nt = dcd.DecodeNal();
        Console.WriteLine(nt);
    }
}
