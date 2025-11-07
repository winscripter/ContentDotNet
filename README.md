# ContentDotNet
ContentDotNet is a cross-platform, open-source, managed framework for C# to read/write videos, audios, images and other multimedia. It allows reading them,
creating them, and inspecting or editing their contents. It provides a user-friendly API. It does not use any native libraries; everything is C#.

It is cross-platform and platform-independent, allowing ContentDotNet to run on a wide range of systems without
worrying about compatibility, while also making deployment easier. It will work where .NET can run - yes, even Blazor WebAssembly.

## What's supported?

> [!NOTE]
> We've began rewriting the entire framework completely to get a fresh start. Because of this,
> more components are now incomplete.

- Video:
    - Codecs:
        - H.264 (⚠️)
    - Formats:
        - MP4 (⚠️)
- Image:
    - Formats:
        - BMP (⚠️)
        - WebP (⚠️)
        - JPEG (⚠️)
- Audio:
    - Codecs:
        - G.722
- Protocols:
    - SDP (⚠️)
    - RTSP (⚠️)
    - RTP (⚠️)
 
> [!NOTE]
> Almost all of the work went into the H.264 implementation,
> though, which is still incomplete. The formats/codecs marked with (⚠️) are undone or unimplemented.

## Why this project exists?
With the rise of multimedia content in applications, there is a growing need for robust and efficient libraries to handle various multimedia formats.
Many social media apps are composed of video, audio and image applications, and developers often need to manipulate multimedia content in their applications.

.NET began supporting open-source development and cross-platform capabilities with .NET Core, and it gets more powerful with each
release. C#, when used correctly, offers great performance and memory efficiency (SIMD with System.Runtime.Intrinsics,
Parallelism with the Parallel class, stackalloc, structs, you name it). Which means .NET can be used to create multimedia
frameworks and video codec implementations. If we use native libraries like FFmpeg, we lose the cross-platform capabilities and
we have to use different types of binaries for different architectures (x64, ARM64, RISC-V, ...) **and** platforms (Windows, macOS, Linux, WebAssembly, ...).
If there's one managed .NET framework to do this, you just need one binary for all platforms and architectures.

There are a few powerful image and VoIP libraries in .NET, like SixLabors.ImageSharp or SIPSorcery respectively, yes, but for comprehensive video and audio decoding without
platform dependencies, there's no library for that, and ContentDotNet could be the first. It will also always be open-source and use the MIT license.

As for patents like H.264, we believe that we can use a technique where we delegate the need to obtain licenses and pay royalties
to the end-user of the library. This applies to profit-generating and enterprise context, while personal and non-profit use legally remains free of charge.

## Documentation
See [Docs/README.md](Docs/README.md).

## Packages
| Package Name | Package URL (NuGet) |
| ------------ | ------------------- |
| ContentDotNet | [https://www.nuget.org/packages/ContentDotNet](https://www.nuget.org/packages/ContentDotNet) |
| ContentDotNet.Protocols.Sdp | [https://www.nuget.org/packages/ContentDotNet.Protocols.Sdp](https://www.nuget.org/packages/ContentDotNet.Protocols.Sdp) |
| ContentDotNet.Protocols.Rtsp | [https://www.nuget.org/packages/ContentDotNet.Protocols.Rtsp](https://www.nuget.org/packages/ContentDotNet.Protocols.Rtsp) |

## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md).

## Other

[Building instructions](BUILDING.md)

[Changelog](CHANGELOG.md)
