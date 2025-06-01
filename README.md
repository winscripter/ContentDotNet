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

ContentDotNet is a library for C# to create videos, audios, images and other multimedia. It allows reading them,
creating them, and otherwise inspecting or editing their contents. It provides a user-friendly API, as well as
extensive documentation and samples. It does not use any native libraries; everything is pure C#, following the
specification for each format.

Powered by nothing but .NET 8.0, it allows processing multimedia content very quickly and under a memory efficient
manner, while also being compatible with NativeAOT, ReadyToRun, Mobile/Desktop, Web, and Embedded/IoT scenarios.

It is cross-platform and platform-independent, allowing ContentDotNet to run on a wide range of systems without
worrying about compatibility, while also making deployment easier.

## What's supported?
ContentDotNet yet supports H.264 for video codecs and MP4 for video formats. This is an alpha release and
the stable one is expected to have more codecs and formats supported. Adding your own
codecs and formats is straightforward and is documented.

Besides, it also supports G.711, G.722 and G.726 for audio codecs; PNG and BMP for image formats; and TTF
for font formats.

H.264:
  - CAVLC and CABAC
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
  
> [!NOTE]
> Encoding isn't yet supported because the H.264 specification only provides decoding. However,
> this is an alpha release and the stable one is expected to have encoding support (it's on the TODO list).
> Writing raw H.264 structures is supported, except for CABAC.

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

## Licensing
The library is free and open-source and licensed under the [MIT License](LICENSE.md).
You can use ContentDotNet for free, even in commercial projects.

> [!WARNING]
> Some codecs and formats implemented by this library are patented and/or licensed (f.e. H.264) by the company/companies that owns/own them.
> In commercial, enterprise, or profit-generating context, you'll have to obtain the license and pay royalties to the company owning those patents yourself.

## Efficiency
Each codec describes recommended and maximum possible memory usage in its README.

## Other

[Building instructions](BUILDING.md)

[Changelog](CHANGELOG.md)
