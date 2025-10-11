In this article:
- [SEI Abstractions](#abstractions)
- [Base namespace](#base-namespace)
- [IH264SeiIO](#io)
- [IH264SeiObject (non-generic)](#non-generic-object)
- [IH264SeiObject (generic)](#generic-object)
- [Examples!](#examples)

<a name="abstractions"></a>
# SEI Abstractions
This article describes the interfaces required to implement Supplemental Enhancement Information (SEI) in H.264.

<a name="base-namespace"></a>
# Base namespace
`ContentDotNet.Extensions.Video.H264.SeiModel.Abstractions`

<a name="io"></a>
# IH264SeiIO
Alright, let's start with the `IH264SeiIO` interface. This interface provides access to reading and writing your SEI payload implementations
into and from the bitstream. Both read and write methods can be synchronous and asynchronous.

```cs
public interface IH264SeiIO<TSeiElement>
```

TSeiElement is the class where the actual data of your SEI payload is stored - that is, the parsed syntax elements.

Implementing it is relatively simple. Here are the four methods:
- `TSeiElement Read(H264RbspState rbspState, BitStreamReader bitStreamReader)` will parse the payload and return the class that represents the parsed syntax elements of the payload.
- `Task<TSeiElement> ReadAsync(H264RbspState rbspState, BitStreamReader bitStreamReader)` is the same as the aforementioned `Read` method, except, that this variant is asynchronous, whereas the `Read` method is synchronous.
- `void Write(TSeiElement element, BitStreamWriter bitStreamWriter, H264RbspState rbspState)` will encode the SEI payload's parsed data (i.e. syntax elements) into the specified bit-stream, the same way as it will be decoded
- `Task WriteAsync(TSeiElement element, BitStreamWriter bitStreamWriter, H264RbspState rbspState);` is the same as the aforementioned `Write` method, except, that this variant is asynchronous, whereas the `Write` method is synchronous.

<a name="non-generic-object"></a>
# IH264SeiObject (non-generic)
This is a simple interface that describes common information about the SEI payload, and defines a property to actually get the `IH264SeiIO` interface to read/write that
SEI payload. This is the actual representation of the SEI object.

```cs
public interface IH264SeiObject
```

The properties are:
- `uint Id { get; }` describes the required `payloadType` variable in the `sei_message( )` syntax function described in the ITU-T H.264 spec. So for instance, if this is 42, and the `sei_message( )` has `payloadType` equal to 42, this exact `IH264SeiObject` will be triggered for decoding.
- `string FunctionName { get; }` is an annotation that defines the exact syntax function name. This is only important if the SEI payload being implemented comes from the H.264 spec. Examples: `pan_scan_rect`, `filler_payload`, `user_data_registered_itu_t_t35`, etc. Leave this as an empty string (`string.Empty`) if there's no function name supplied.
- `string Name { get; }` defines the name of the SEI payload visible to the user. Example: `Pan Scan Rectangle`, `Filler Payload`, etc. The name must be in English.
- `uint PayloadSize { get; set; }` will be automatically set by the `sei_message( )` decoder, and it specifies the size of the payload.
- `IH264SeiIO<object> IO { get; }` defines the actual ways to read/write this payload to/from the bit-stream. It is by default an `object`, but can be overriden to use any type when inheriting the generic `IH264SeiObject` interface.

<a name="generic-object"></a>
# IH264SeiObject (generic)
This generic interface overloads the `IO` property from the non-generic `IH264SeiObject` to use custom SEI payload models.

```cs
public interface IH264SeiObject<T> : IH264SeiObject
```

There's just one overriden property: `new IH264SeiIO<T> IO { get; }`. It has the same purpose as the `IO` property from the non-generic `IH264SeiObject`, except, that now, the `IH264SeiIO` instance uses a custom provided type as the SEI model.

<a name="examples"></a>
# Examples!
See examples.md for examples on implementing a custom SEI object.
