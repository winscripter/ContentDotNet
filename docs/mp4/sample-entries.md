# Sample Entries and Sample Entry Handlers
> [!IMPORTANT]
> This document assumes that you're already aware of sample entries in an MP4 video file, and what they are. If you're
> not aware of much (including sample entries) in the MP4 video file and just want to read/write the videos themselves, check out our guide on using ContentDotNet
> for video editing and creating videos, which also describes working with MP4 files without getting in depth.

ContentDotNet allows you to work with MP4 file sample entries, those used in boxes, like `stsd`.

Sample entries and related operations are located under the `ContentDotNet.Extensions.Mp4.Models.Samples` namespace, under
the `ContentDotNet.Extensions.Mp4` package. Make sure to include that namespace first.

We also provided an example at the bottom of this document.

```cs
using ContentDotNet.Extensions.Mp4.Models.Samples;
```

Sample entries are represented by a `SampleEntry` class.
```cs
public abstract class SampleEntry
```

It has 3 properties:
```cs
public BoxHeader Header { get; set; }
```
This represents the box header. The `BoxHeader.Size`
field defines the size, whereas the `BoxHeader.Type`
field defines the format of the sample entry - for example, mp4a
or avc1.

```cs
public ushort DataReferenceIndex { get; set; }
```
This is the data reference index. Its value is typically 1.

```cs
public ISampleEntryData Data { get; set; }
```
This is the actual data of the sample entry. It can be anything. We'll describe
the ISampleEntryData interface, what is it for, and how to use below in this document.

There's one constructor:
```cs
protected SampleEntry(BoxHeader header, ushort dataReferenceIndex, ISampleEntryData data)
```
which initializes all the three above properties with desired values.

> [!NOTE]
> The sample entries also have 6 reserved bytes, though, they're always set to
> 0, and any read or write operations involving sample entries handle the
> reserved 6 bytes automatically.

<hr />

There are several read methods that all adapt to different cases. For instance,
if you already have the data but not the header, or if you
already have the header but not data, or both, or neither.

```cs
public static SampleEntry Read(BinaryReader reader, SampleEntries entries)
```
This reads the sample entry from your `reader`, including the box header.
As for the sample entry data, that's where the `entries` parameter is
required. (We'll describe it below in this document)

```cs
public static SampleEntry Read(BoxHeader header, BinaryReader reader, SampleEntries entries)
```
This reads the sample entry from your `reader`, skipping the box header and
instead, using the `header` parameter.
As for the sample entry data, that's where the `entries` parameter is
required. (We'll describe it below in this document)

```cs
public static SampleEntry Read(BoxHeader header, BinaryReader reader, ISampleEntryData data)
```
This reads the sample entry from your `reader`, skipping the box header and
instead, using the `header` parameter.
In this method overload, the sample entry data is provided manually.

```cs
public static SampleEntry Read(BinaryReader reader, ISampleEntryData data)
```
This reads the sample entry from your `reader`, including the box header.
In this method overload, the sample entry data is provided manually.

<hr />

There's also one `Write` method that writes to the given `BinaryWriter` instance:
```cs
public void Write(BinaryWriter writer)
```
This will use all three properties of the SampleEntry class. The reserved
6 bytes are automatically written as 6 0x00 bytes. If you wish to change
how the data is written, you can modify the SampleEntry's properties, then
invoke the `Write` method.

<hr />

## The ISampleEntryData interface
This interface is used to represent the sample entry's data. Based on the
sample entry's type (in the box header, the Type field), different data may
proceed with what the SampleEntry class provides. For example, if the
sample entry type is mp4a, audio-specific data is preceded, and that audio
specific data is provided by the `ISampleEntryData` interface.

The interface doesn't have much in it- it just has a single `Write(BinaryWriter)`
method that writes all data in the interface to the given binary writer.
```cs
public interface ISampleEntryData
{
    void Write(BinaryWriter writer);
}
```

## The ISampleEntryHandler interface
Picture this: an MP4 video file can have lots of sample entries, and a new codec
could emerge later, become popular, and introduce its own sample entry
with its own data. How would you make one type that could support reading/writing
*any* sample entry, even if it's not yet a thing but will be later on? This
is where the `ISampleEntryHandler` interface (and the `SampleEntries` class)
can become **very useful**.

The `ISampleEntryHandler` essentially handles how the sample entry's data is read, based on its type.

It has multiple properties:
```cs
ISampleEntryData Read(BinaryReader reader);
```
This reads the sample entry's data from the binary reader.

```cs
Type DataType { get; }
```
This represents the actual type of whatever inherits from `ISampleEntryData`
returned by the `Read(BinaryReader)` method.

For example:
```cs
internal sealed class MyData : ISampleEntryData
{
    // ...
}

internal sealed class MyHandler : ISampleEntryHandler
{
    ISampleEntryData Read(BinaryReader reader)
    {
        return new MyData();
    }

    Type DataType => typeof(MyData);

    // ...
}
```
Notice how the `Read` method always returns the `MyData` class? So,
`DataType` should always return `typeof(MyData)`. Essentially, the DataType
property represents the specific type being returned, rather than the broad
`ISampleEntryData` interface.

```cs
uint Identifier { get; }
```
This represents the sample entry's type that's expected to be in the
box header's type. For instance, if your implementation of the
`ISampleEntryHandler` handles reading `mp4a` sample entries, the
`Identifier` property should always return `mp4a`, in the binary form.

```cs
string IdentifierText { get; }
```
This should serve the same purpose as the `Identifier` property, with a
small difference: now, if your implementation handles reading `mp4a`
sample entries, this should not be `mp4a` in binary (integer) form,
but rather, the string "mp4a".

Or, you could implement it as follows:
```cs
public string IdentifierText => new FourCC(this.Identifier).ValueText;
```

<hr />

## The SampleEntries class
Let's say you expect that your MP4 video file will have boxes like mp4a, avc1, or something else. What
can you do to support reading all of them, and potentially, even more?

The `SampleEntries` class comes in handy for this scenario. It's essentially
a group of `ISampleEntryHandler` interfaces.

```cs
public sealed class SampleEntries
```

Creating it is simple: just instantiate it:
```cs
var sampleEntries = new SampleEntries();
```

It has two properties:
```cs
public int Count { get; }
```
This represents number of registered sample entry handlers.

```cs
public List<ISampleEntryHandler> Backing { get; }
```
This represents all sample entries that are already registered.

<hr />

Now, onto methods.

```cs
public void RegisterHandler(ISampleEntryHandler handler)
```
This registers the sample entry handler, meaning, if the decoder
ever encounters the, let's say, `mp4a` sample entry, it will use the provided
handler to read it, if its Identifier property is `mp4a`.

This method will throw `ArgumentException` if there already exists
a handler whose type is the same as the given handler.

```cs
public bool ContainsHandler(ISampleEntryHandler handler)
```
This goes through all registered handlers and checks if there's one
whose the Identifier property is equal to the provided sample entry handler's
Identifier property.

```cs
public bool TryRead(FourCC type, BinaryReader reader, out ISampleEntryData? data)
```
This reads the sample entry's data. It uses the `type` parameter to check
the type of the sample entry (e.g. mp4a). It then composes the sample
entry data based on the first handler that matches the given type.

<hr />

## Example: let's make our own sample entry!
By now, everything else that was documented might not be very clear, so let's
provide a simple example that registers our own sample entry.

Start by providing the sample entry's data:
```cs
public sealed class VideoQualityEntryData : ISampleEntryData
{
    public byte UserFeedback { get; set; }
    public bool IsGoodVideo { get; set; }
    public bool IsHighQualityAudio { get; set; }

    public VideoQualityEntryData(byte userFeedback, bool isGoodVideo, bool isHighQualityAudio)
    {
        UserFeedback = userFeedback;
        IsGoodVideo = isGoodVideo;
        IsHighQualityAudio = isHighQualityAudio;
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(UserFeedback);
        writer.Write(IsGoodVideo);
        writer.Write(IsHighQualityAudio);
    }
}
```

Then, define the handler so we can actually read the data:
```cs
public sealed class VideoQualityEntryHandler : ISampleEntryHandler
{
    public Type DataType => typeof(VideoQualityEntryData);

    public uint Identifier => new FourCC("vidq").Value;

    public string IdentifierText => "vidq";

    public ISampleEntryData Read(BinaryReader reader)
    {
        byte userFeedback = reader.ReadByte();
        bool isGoodVideo = reader.ReadBoolean();
        bool isHighQualityAudio = reader.ReadBoolean();
        return new VideoQualityEntryData(userFeedback, isGoodVideo, isHighQualityAudio);
    }
}
```

Now, register it:
```cs
// Assuming the SampleEntries instance already exists
var sampleEntries = new SampleEntries();

sampleEntries.RegisterHandler(new VideoQualityEntryHandler());
```

That's it! Now, every time you encounter a sample entry whose
type is `vidq`, the `VideoQualityEntryHandler` type will automatically
be used, and the `ISampleEntryData` interface will be `VideoQualityEntryData`.
