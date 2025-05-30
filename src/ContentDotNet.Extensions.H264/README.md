# ContentDotNet.Extensions.H264
**Note**: Also see the file LEGAL_NOTICE.txt in the current directory.

Adds H.264 parsing, encoding and editing support for ContentDotNet.

All H.264 code is implemented directly in C#; no external libraries
and kits like FFmpeg, DirectShow, Media Foundation, VLC, OpenH264,
or x264, are used.

### 1. What is H.264?
It's a video codec, essentially dictates the binary format required to represent
video. It's usually embedded in video formats like MP4, MKV, AVI and TS, and is the
most popular video codec in the world. Actually, 90% of all videos you see on the
web are coded with H.264.

H.264 was standardized in 2003 and is known for its performance and compression.
It uses what's known as interframe compression to, simply put, incorporate chunks
of video from the current or previous frame directly.

### 2. Quickstart
There is a documentation on this, but to get started, we'll demonstrate how
you can read and write an H.264 file using PNGs as pictures.

Install ContentDotNet.Extensions.ImageFormats package first, then run this code
snippet:

```cs
using var file = File.OpenRead("video.264");

IH264Service h264 = H264ServiceFactory.H264Service;
using IH264Reader reader = h264.Read(file);

IImageService image = ImageServiceFactory.ImageService;

for (int i = 0; i < reader.Metrics.PictureCount; i++)
{
    using var imageStream = File.OpenWrite("Image " + i + ".png");
    IImageFactory factory = image.CreateFactory(imageStream);

    IH264Frame frame = reader.GetFrame(i);

    // Use stride based approach for zero allocation
    Core();

    void Core()
    {
        while (frame.HasNextStrides)
        {
            ProcessStride();
        }

        factory.NextStride();
        
        void ProcessStride()
        {
            Span<Yuv> yuvs = stackalloc Yuv[frame.NextStride(128)];
            factory.WriteStridePart(yuvs);
        }
    }
}
```

# 3. Examples

## 3.1. Parsing NAL unit &amp; Skipping start code
Example program below demonstrates how to open an H.264 file, skip NALU start code
which is at least 3 0x00 bytes followed by 0x01, and then read the NALU, displaying
its values.

There's a structure called `NalUnit` under the `ContentDotNet.Extensions.H264.Models`
namespace that represents a NAL Unit: it represents its values, allows reading/writing
them, and has a method to skip start codes.

```cs
using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;

using var fs = File.OpenRead("video.h264");
using var br = new BitStreamReader(fs);

if (!NalUnit.SkipStartCode(br))
    throw new InvalidOperationException("Could not retrieve start code.");

var nalu = NalUnit.Read(br, 1);

Console.WriteLine("Type: " + nalu.NalUnitType);
Console.WriteLine("Ref Idc: " + nalu.NalRefIdc);
```

### 4. What's supported?
> **Note**: This is a preview release.

1. CAVLC
    - CABAC is being worked on, with most work already being done
2. MBAFF
3. I, P and B slices
4. Extensions like 3D AVC, SVC, MVC can be parsed, though not processed
5. Encoding

### 5. Memory usage
ContentDotNet is quite memory efficient, thanks to the powers of stack allocation
and the trusty .NET garbage collector.

Available RAM can also be determined, which is necessary in cases where memory
usage is a concern, such as in mobile, embedded, or IoT devices. Here's a breakdown.

| Video size | Maximum possible memory usage | Recommended remaining memory |
| ----- | ---- | ---- |
| 640x480 | ~43.95MB | ~13.73MB |
| 1024x768 | ~72.00MB | ~22.50MB |
| 1366x768 | ~95.98MB | ~30.00MB |
| 1920x1080 | ~189.98MB | ~59.37MB |
| 2048x1080 (2K) | ~202.50MB | ~63.28MB |
| 2560x1440 (1440p) | ~337.53MB | ~105.47MB |
| 3840x2160 (4K) | ~759.38MB | ~237.30MB |

This is determined with the following algorithm:
```py
def determine_max_memory_usage(width: int, height: int) -> int:
    return (width * height * 3) * 32

def determine_recommended_memory(width: int, height: int) -> int:
    return (width * height * 3) * 10
```
> [!NOTE]
> Above code snippet assumes 4:4:4 chroma format, which is the highest possible chroma format,
> and is quite rare. Most videos use smaller chroma formats - 4:2:0 or 4:2:2.
> Despite this, we use 4:4:4 as a safe upper bound to account for worst-case scenarios —
> such as decoding streams with high-quality color fidelity and the maximum number of reference frames.

We multiply the result by 32 as that's the most possible reference pictures (B slices)
which in turn is the largest possible memory usage in H.264. As for recommended
memory, most H.264 videos probably won't have > 5 reference pictures for P/B slices.

However, you'll almost never reach the maximum possible memory usage, since 6 reference
pictures is already rare, and 8 is exceptionally rare. Not to mention, not only achieving
highest memory usage require reaching the limit of reference pictures, but also the
highest possible chroma format, which is 4:4:4, and most videos only have 4:2:0/4:2:2.
