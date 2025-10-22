# SDP with ContentDotNet
ContentDotNet supports the Session Description Protocol (SDP).

This is located in the `ContentDotNet.Protocols.Sdp` assembly.

## Overview
The Session Description Protocol (SDP) is a format for describing the multimedia content of communication sessions. It is widely used in applications such as VoIP, video conferencing, and streaming media.

ContentDotNet provides a set of classes and methods to create, parse, and manipulate SDP messages easily.

## Creating an SDP Message
You can create an individual SDP message by instantiating it directly. Example:
```cs
using ContentDotNet.Protocols.Sdp.Lines;

var versionLine = new SdpVersionLine("v=0");

// ...

Assert.True(versionLine.TryGetVersion(out var version));
Assert.Equal(0, version);
```

## More flexible approach: Using the SDP service
Start by instantiating the SdpService class.

```cs
var sdpService = new SdpService();
```

Use `ReadLine`, it can read any SDP line and return the appropriate object.
```cs
using var reader = new StringReader(
"""
v=0
"""
);
var line = sdpService.ReadLine(reader); // Read one line
// returns SdpVersionLine
```

That class can also be used to read asynchronously, as well as write both synchronously and asynchronously.

```cs
using var writer = new StringWriter();
await sdpService.WriteLineAsync(writer, line);
Console.WriteLine(writer.ToString()); // prints "v=0"
```
