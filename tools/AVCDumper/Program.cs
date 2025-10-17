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

        for (int i = 0; i < 4; i++)
        {
            NalType nt = dcd.DecodeNal();
            Console.WriteLine(nt);

            if (i == 1)
                Console.WriteLine(dcd.State!.H264RbspState!.SequenceParameterSetData);
            else if (i == 2)
            {
                Console.WriteLine(dcd.State!.H264RbspState!.PictureParameterSet);
                Console.WriteLine(dcd.State!.H264RbspState!.PictureParameterSet!.RunLengthMinus1!.Length);
                Console.WriteLine(dcd.State!.H264RbspState!.PicSizeInMbs());
            }
        }
    }
}
