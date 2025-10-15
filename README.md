<p align="center">
  <img src="resources/logo/ContentDotNet-logo.png" alt="Logo" />
</p>

> [!CAUTION]
> This library **is in development** 🚧. No releases, including alpha, beta, rc, or preview releases were made.
> At the moment, this library could be incomplete, bizarre, unstable, or just lacking necessary features.
>
> However, we highly appreciate any feedback.
>
> Use at your own risk—you might discover functions that do the opposite of what you'd expect!

ContentDotNet is a high-performance & cross-platform framework for C# to create videos, audios, images and other multimedia. It allows reading them,
creating them, and otherwise inspecting or editing their contents. It provides a user-friendly API, as well as
extensive documentation and samples. It does not use any native libraries; everything is pure C#, following the
specification for each format.

Powered by .NET 8.0, it allows processing multimedia content very quickly and under a memory efficient
context, while also being compatible with NativeAOT, ReadyToRun, Mobile/Desktop, Web, and Embedded/IoT scenarios.

It is cross-platform and platform-independent, allowing ContentDotNet to run on a wide range of systems without
worrying about compatibility, while also making deployment easier. It will work where .NET can run - yes, even Blazor WebAssembly.

## What's supported?
### Formats

- Video:
    - Codecs:
        - H.261
        - H.264
        - MJPEG
    - Formats:
        - MP4
        - AVI
- Image:
    - Formats:
        - PNG
        - BMP
        - JPEG
- Audio:
    - Codecs:
        - G.711
        - G.722
        - G.726
- Protocols:
    - SDP
    - RTSP
    - BGP
    - RTP
    - IPP

### Capabilities

H.264 (Decoding-only):
  - CABAC decoding
  - MBAFF
  - Intra and Inter prediction
  - Motion compensation
  - Deblocking filter
  - Baseline, Main, High and High10 profiles (pretty much what about 90% of H.264 videos are coded with)
  - All chroma subsampling types
  - NAL units, parameter sets, slice header/data, macroblocks, and residuals (both luma and chroma)
  - Accessing internal data (e.g. macroblocks, residuals, slice header/data, parameter sets and NALs) with raw values directly provided
  - Low allocation, focusing on high performance while still being platform independent (e.g. no unsafe code or filesystem operations)
  - Compatible with NativeAOT, ReadyToRun, Mobile/Desktop, Web, Embedded/IoT, and even Blazor WebAssembly
  - VUI parameters
  - Weighted Prediction

MP4:
  - Sample entries
  - Raw box editing
  - Loading direct H.264/audio streams
  - Editing options in a simple way (e.g. FPS, duration, etc.)

PNG:
  - Support for decoding/encoding
  - Direct access to raw PNG chunks (+editing)
  - Interlace (null/Adam7) support
  - ICC metadata
  - RGB, RGBA, and Grayscale pixel formats
  - Editing

BMP:
  - No platform dependency (just like with all other supported formats)
  - All structures predating to OS/2
  - All compression types
  - RGB, BGR, BGRA, RGBA, Grayscale pixel formats
  - Encoding and decoding

G.711:
  - Async support
  - Zero allocation (stack-only) for both decode/encode operations (except for async methods)
  - Encoding and decoding
  - Writing either a single sample or multiple at once
  - Interleaving
  - Support for either a single channel, or up to three channels, either interleaved or not
  - Clean API design
  - Blazing fast speed, without any initialization overhead

G.722:
  - Everything that the G.711 encoder/decoder supports

G.726:
  - Everything that the G.711 encoder/decoder supports

## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md).

## Other

[Building instructions](BUILDING.md)

[Changelog](CHANGELOG.md)
