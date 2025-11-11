# ContentDotNet
ContentDotNet is a cross-platform, open-source, managed framework for C# to read/write videos, audios, images and other multimedia. It allows reading them,
creating them, and inspecting or editing their contents. It provides a user-friendly API. It does not use any native libraries; everything is C#.

It is cross-platform and platform-independent, allowing ContentDotNet to run on a wide range of systems without
worrying about compatibility, while also making deployment easier. It will work where .NET can run - yes, even Blazor WebAssembly and Unity.

<!--
It's:
- ✅ Reliable: Real world releases are likely going to be able to process most kinds of formats and codecs without issues.
- 💨 Fast: Uses techniques like SIMD and Parallelism (if possible on the active environment) to process all kinds of multimedia as fast as possible
- ❤️ Well maintained: If you ever encounter an issue or have a suggestion, you're more than welcome to create an Issue post. We can also answer questions or just have a general conversation in Discussions.
- 🧠 Memory efficient: Uses techniques like `stackalloc` or structs to allocate as least memory as possible. Some codecs like H.264 might still result in lots of memory allocations, but this is the case for every other codec - there's no way around it, it's how codecs like H.264 are designed.
- 📑 Documented: Ensures that anyone interested in this framework can get started right away.
- 🧹 Got a user-friendly API: Provides ways to use the file formats, codecs and protocols without getting **too** techy - just basic concepts like "decode a frame" or "get frames per second". But it also allows access to internal components too! In H.264 for instance, it allows both a user-friendly API and direct access to internal components, like CABAC decoding or Intra prediction.
- 🪪 Completely free: It is licensed under the MIT License, and the project itself does not have a fee to use it - and it is open-source, too. Though, if you're using specific patented technologies like H.264, you'll have to obtain a license from the creators of these technologies, since they're patented. Note that this is the case for any other implementation of patented technologies - it's not explicitly a creator of the implementation can control - use of any implementation will require a license, that's said to be by the creators of patented multimedia codecs/protocols/formats themselves. But for **some** other general multimedia technologies like MP4, G.722 or RTSP, they're royalty-free. Before using a multimedia format, codec or protocol, be sure to check if it's royalty free.
-->

## What's supported?

All of the file formats in the "In-progress formats" are expected to be implemented by version 1.0. File formats
in "Postponed formats" are expected to be implemented by subsequent versions.

### Currently supported formats
These are now done.

| Type | Sub-type | Name | Notes |
| ---- | -------- | ---- | ----- |
| Codec | Video | V210 | Decoder only; supports async |
| File format | Audio | OGG | Includes a super easy-to-use System.IO.Stream subset for reading/writing audio data directly |
| File format | Video | WebP | Supports multiplexing and demultiplexing content, both synchronously and asynchronously |
| Codec | Audio | ITU-T G.722 | Decoder only &amp; not tested &amp; zero-allocation |
| File format | Audio | WAV (Wave) | Reader &amp; writer |
| File format | Subtitle | SSA (Advanced Substation Alpha) | Supports reading and writing + async support |
| File format | Subtitle | SRT (SubRip) | Supports reading and writing + async support |
| File format | Image | BMP | Decoding only |
| File format | Subtitle | WebVTT | |

### In-progress formats
We're actively working on those.

| Type | Sub-type | Name | Complexity |
| ---- | -------- | ---- | ----- |
| File format | Video | MP4, HEIF, HEIC, MOV, M4V, M4A | Might take some time due to large number of boxes in the ISOBMFF format, but we'll get there |
| File format | Video, audio | WMV, WMA | These two Microsoft Windows file formats are based on the ASF container, which can take some time to implement |
| File format | Audio | FLAC | Has many kinds of metadata blocks |
| File format | Image | JPEG | Uses Huffman and DCT compression which can take some time to implement |
| File format | Image | GIF | Compresses pixel data |
| File format | Image | PNG | Compresses pixel data, may support interlacing, supports many chunks, which all have to be implemented |
| File format | Video | AVI | We'll implement this after H.264 |
| File format | Video | MKV | We'll have to implement the EBML container first, as EBML is what MKV files consist of |
| File format | Subtitle | TTML | |
| Codec | Video | H.264 | Our top priority. Extremely complex: uses DCT, Intra/Inter prediction, arithmetic coding, deblocking filter, 5 types of frames (slices), etc. We're using JM Reference Software to finish the arithmetic coding part alone. This will take a long time to implement, but it is expected to be done by 1.0. |

### Postponed formats
We will work on them sometime later.

> [!NOTE]
> These and "In-progress formats" aren't the **only** formats/codecs that will ever be featured on ContentDotNet.
> It's just those that we have plans to implement right now. You can always propose a new codec/file format implementation in Issues.

| Type | Sub-type | Name | Notes |
| ---- | -------- | ---- | ----- |
| File format | Video, audio, ... | MPEG-TS | Includes 60 types of descriptors and we have to implement readers/writers (both sync and async) for all of them |
| Codec | Video | H.262 (better known as MPEG-2 Video) | Mostly used in Blu-ray discs, but not used for real-world video anymore |
| Codec | Video | VP8 | Used in WebP and WebM files |
| Codec | Audio | G.726 | Popular in the VoIP world |
| Codec | Audio | G.711 | Popular in the VoIP world |

## Why this project exists?
With the rise of multimedia content in applications, there is a growing need for robust and efficient libraries to handle various multimedia formats.
Many social media apps are composed of video, audio and image applications, and developers often need to manipulate multimedia content in their applications.

.NET began supporting open-source development and cross-platform capabilities with .NET Core, and it gets more powerful with each
release. C#, when used correctly, offers great performance and memory efficiency (SIMD with System.Runtime.Intrinsics,
Parallelism with the Parallel class, stackalloc, structs, you name it). Which means .NET can be used to create multimedia
frameworks and video codec implementations. If we use native libraries like FFmpeg, we lose the cross-platform capabilities and
we have to use different types of binaries for different architectures (x64, ARM64, RISC-V, ...) **and** platforms (Windows, macOS, Linux, WebAssembly, ...).
If there's one managed .NET framework to do this, you just need one binary for all platforms and architectures.

There are a few powerful image and VoIP libraries in .NET, but for comprehensive video and audio decoding without
platform dependencies, there's no library for that, and ContentDotNet could be the first. It will also always be open-source and use the MIT license.

As for patents like H.264, we believe that we can use a technique where we delegate the need to obtain licenses and pay royalties
to the end-user of the library. This applies to profit-generating and enterprise context, while personal and non-profit use legally remains free of charge.

## Good to know
In real-world scenarios, multimedia files may be malformed, corrupted, or even crafted as part of a denial-of-service (DoS) attack. Such files can trigger excessive memory allocations, potentially leading to server crashes due to OutOfMemory exceptions.

ContentDotNet includes robust safeguards to detect and mitigate these risks. Instead of allowing unsafe operations to proceed, the framework proactively aborts processing and throws a controlled exception - ensuring system stability and resilience under hostile or malformed input.

If a file that's malformed, corrupted, or crafted as a DoS attack slips through the cracks and causes a server crash, you're more than welcome to report the issue to us through GitHub Issues.

## Documentation
See [Docs/README.md](Docs/README.md).

## Packages
| Package Name | Package URL (NuGet) |
| ------------ | ------------------- |
| ContentDotNet | [https://www.nuget.org/packages/ContentDotNet](https://www.nuget.org/packages/ContentDotNet) |

We have only published the abstractions package for now. To publish other packages that handle video, audio,
image, protocols and subtitles, we'll have to finish them first. (See the "In-progress formats" section)

## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md).

## Other

[Building instructions](BUILDING.md)

[Changelog](CHANGELOG.md)
