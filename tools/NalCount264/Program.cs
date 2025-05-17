using ContentDotNet;
using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Models;
using System.Diagnostics;

using var fs = File.OpenRead("output.h264");
using var br = new BitStreamReader(fs);

int cnt = 0;
var sw = Stopwatch.StartNew();

try
{
    while (NalUnit.SkipStartCode(br))
    {
        cnt++;
    }
}
catch (InfiniteLoopException)
{
    Console.WriteLine("Infiniloop!");
}

Console.WriteLine(cnt);
Console.WriteLine("Completed in " + sw.Elapsed.TotalSeconds + "s");
