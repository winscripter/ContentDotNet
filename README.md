# ContentDotNet

> [!WARNING]
> While some components of this framework are complete, others are still under development,
> such as H.264 decoding, and as such, are not yet published on NuGet.
>
> We have released this framework early hoping to get some community support and contributions
> with components like H.264 and JPEG decoding, writing tests, and finding bugs/sample files
> for the formats we support. It's really hard to write a framework like this alone, and we
> would appreciate any help we can get.
>
> Please note that this framework is not production-ready yet, and should not be used in
> production environments.
>
> We are open to contributions, including but not limited to bug reports, suggestions,
> writing better tests, and creating Pull Requests. See the CONTRIBUTING.md file.
> With your help, this framework could eventually unify multimedia processing in C#,
> and be the first framework ever in the .NET ecosystem for video, image and audio editing without relying on
> native libraries. It will always be cross-platform, open-source, and managed. See also the
> "Why this project exists?" section below in this README.

ContentDotNet is a cross-platform, open-source, managed framework for C# to read/write videos, audios, images and other multimedia. It allows reading them,
creating them, and inspecting or editing their contents. It provides a user-friendly API. It does not use any native libraries; everything is C#.

It is cross-platform and platform-independent, allowing ContentDotNet to run on a wide range of systems without
worrying about compatibility, while also making deployment easier. It will work where .NET can run - yes, even Blazor WebAssembly.

## What's supported?
### Formats

- Video:
    - Codecs:
        - H.264 (⚠️, see [issue #6](https://github.com/winscripter/ContentDotNet/issues/6) if you're willing to help - and it's potentially doable! Most of our work went into the H.264 implementation)
    - Formats:
        - MP4 (⚠️)
- Image:
    - Formats:
        - BMP
        - WebP
        - JPEG (⚠️)
- Audio:
    - Codecs:
        - G.722
- Protocols:
    - SDP
    - RTSP
    - RTP (⚠️)
 
> [!NOTE]
> We've released this framework **very early** - so early, that even simple components
> are still in development. Almost all of the work went into the H.264 implementation,
> though, which is still incomplete. The formats/codecs marked with (⚠️) are undone or unimplemented,
> and we need community help to finish them.

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

## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md).

## Other

[Building instructions](BUILDING.md)

[Changelog](CHANGELOG.md)
