# Decoding G.722
Start by creating the service.

```cs
IG722Service service = new G722Service();
```
Then use `CreateDecoder` and pass in the input `BitStreamReader`.

```cs
using Stream stream = File.OpenRead("input.g722"); // Example stream
IPcmAudioCodec audioCodec = service.CreateDecoder(new BitStreamReader(stream));
```

This returns an `IPcmAudioCodec` (actually `G722Decoder`). The decoding process is
further described [here](../pcmaudio/decoding.md).
