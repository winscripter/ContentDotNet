# ContentDotNet.Extensions.H264

Adds H.264 parsing, encoding and editing support for ContentDotNet.

All H.264 code is implemented directly in C#; no external libraries
and kits like FFmpeg, DirectShow, Media Foundation, VLC, OpenH264,
or x264, are used.

Note that this package does not come with SEI support by default- it does, but
it can't represent any SEI model. To add support for parsing SEI models and
payloads, make sure to add the `ContentDotNet.Extensions.H26x.Sei` package.

### What is H.264?
It's a video codec, essentially dictates the binary format required to represent
video. It's usually embedded in video formats like MP4, MKV, AVI and TS, and is the
most popular video codec in the world. Actually, 90% of all videos you see on the
web are coded with H.264.

H.264 was standardized in 2003 and is known for its performance and compression.
It uses what's known as interframe compression to, simply put, incorporate chunks
of video from the current or previous frame directly.

### Quickstart
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

