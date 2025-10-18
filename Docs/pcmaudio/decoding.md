# Decoding IPcmAudioCodec
This article documents how to decode uncompressed audio codec data into PCM with IPcmAudioCodec.

IPcmAudioCodec supports async, interleaving, and reading multiple samples at once.

# 1. Reading samples
Use the ReadSamples method.

```cs
IPcmAudioCodec codec = ...;

Span<short> pcm = stackalloc short[44100]; // 44100 samples
// Use stackalloc for better performance. Does not perform
// any heap allocations.
// You can also use Span<byte> and stackalloc byte[44100].

codec.ReadSamples(pcm);

// pcm now contains Pulse Code Mutation samples
```

The example above reads the next 44100 samples and stores them in the variable 'pcm'.

There's also the asynchronous version of that called `ReadSamplesAsync`. Instead of receiving `Span`, it receives
a byte/short array instead, which is heap allocated. That's because in .NET, async methods **cannot**
use `Span` as parameters, which is why we're using arrays here.

```cs
IPcmAudioCodec codec = ...;

// Heap allocated
short[] pcm = short[44100];

await codec.ReadSamplesAsync(pcm);

// pcm now contains Pulse Code Mutation samples
```

> [!NOTE]
> `ReadSamples` will also work by providing arrays directly, since arrays implicitly
> cast to a Span.

# 2. Reading Interleaved Samples
First, the `IPcmAudioCodec.ChannelCount` property is a property that returns `int`, with the default
value being 1. It specifies the number of channels for use in interleaving. The maximum number of channels
is usually infinite, but some custom implementations may limit it to, for instance, 3 channels.

Let's say we want to interleave with three channels. Then we'd do:
```cs
codec.ChannelCount = 3; // 3 channels
```

Let's create a buffer for interleaved samples. Since we want 3 channels, that's `NumberOfSamples * 3` samples.

```cs
const int NumberOfSamples = 44100;

Span<short> pcm = stackalloc short[NumberOfSamples * 3];
```

Now, invoke `ReadInterleavedSamples`. It takes two parameters. The first parameter is the buffer for PCM samples,
which we have here, the, `pcm` variable, and the other parameter is the length for each channel, which is `NumberOfSamples`,
or, the length of the buffer divided by the number of channels.

```cs
codec.ReadInterleavedSamples(pcm, NumberOfSamples);
```

Now we have interleaved samples in the `pcm` span.

This also works with the async version, `ReadInterleavedSamplesAsync`. Example below:
```cs
short[] pcm = new short[44100 * 3];
await codec.ReadInterleavedSamplesAsync(pcm, 44100);
// Done
```

# 3. Properties
The `SampleRate` property returns the default sample rate. The `CanChangeSampleRate` property, if false, means that setting `SampleRate` will throw an exception. Similarly, `BitRate` returns the default bit rate, and if `CanChangeBitRate` is false, setting `BitRate` will throw.

The `Name` property returns the codec identifier as a string, and `DisplayName` returns the codec name as user-readable text. Both properties can hold any value. For example, for the G.722 codec, it'd be:
```
{
	Name = "G722",
	DisplayName = "G.722"
}
```

Finally, `Stream` is a `BitStreamReader` where codec data is read. It is usually byte-aligned. For instance, in G.722, each sample is usually 14-bit, but the implementation reads each sample as 16 bits.
