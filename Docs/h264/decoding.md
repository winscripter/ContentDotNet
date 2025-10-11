# 📽️✨ Decoding H.264
This article describes how to use ContentDotNet to decode H.264 frames directly within C# code. No native
dependencies; guaranteed to work wherever .NET can run.

Start by creating the H.264 service. It provides access to H.264 components, like Intra prediction, Deblocking
Filter, etc, and the only one we need here - **decoding H.264 streams**.

```cs
using ContentDotNet.BitStream;
using ContentDotNet.Colors;
using ContentDotNet.Extensions.Video.H264.Extensions;
using ContentDotNet.Extensions.Video.H264;
using ContentDotNet.Extensions.Video.H264.Enumerations;
using ContentDotNet.Pictures;

var service = new H264Service();
AbstractH264Decoder decoder =
	service.CreateDecoder(
		new BitStreamReader(
			your_System_IO_Stream
		)
	);
```

An H.264 video consists of NAL units. Some of these NAL units serve as configuration to the video decoder,
like the frame size (stored in SPS) or entropy coding (stored in PPS). However, the actual frames are stored
in either IDR or Non-IDR NAL units - both contain frame data.

The `.DecodeNal()` method decodes the current NAL unit and stores its parsed information
inside the decoder. It returns the type of the NAL unit that was parsed. So we can basically keep invoking
this method until we reach an IDR or Non-IDR NAL unit.

> [!NOTE]
> The first few NAL units are guaranteed to be SPS, PPS, or AUD.

Example:
```cs
while (decoder.DecodeNal() is not
	NalType.Idr and not
	NalType.NonIdr)
{
}
```

When this loop exits, it means we've reached a frame NAL unit - it's either IDR or Non-IDR. When this is the case,
just invoke the `.ReadPicture()` method. Yep - that's all you have to do!

```cs
Picture<YCbCr> picture = decoder.ReadPicture();
// Your H.264 frame is now stored here. Congratulations!
```

H.264 videos output data in the YUV (a.k.a. Y'Cb'Cr) pixel format. This is the case here. You can later export
this Picture instance as, let's say, PNG or BMP. You can also use other libraries, like SixLabors.ImageSharp, to get
access to even more image formats to export into. ContentDotNet supports some image formats, but only a small portion of them.

# ⚡ Pro Tip
You can simplify creation of `AbstractH264Decoder`.

Just make sure to import the `ContentDotNet.Extensions.Video.H264.Extensions` namespace.

First, to read directly from a `System.IO.Stream`, you can just pass the `System.IO.Stream` instance.

```cs
Stream stream = ...;
AbstractH264Decoder videoDecoder =
	service.CreateDecoder(stream);
```

And also, to read directly from a file on disk, just specify the file name as a string!
```cs
const string FileName =
	"Movie.h264";

AbstractH264Decoder videoDecoder =
	service.CreateDecoder(FileName);
```

Easy!

# ⚠️ Watch out
If `AbstractH264Decoder.DecodeNal()` ever returns `NalType.DidNotRead` - stop decoding immediately.
It means you've reached the end of the file and there's no more frames left to read - that's where the
video ends. If you keep parsing, you could get stuck in an infinite loop of checking if `DecodeNal()`
returned an IDR or Non-IDR frame - but not 'DidNotRead'.
