# H.264/265/266 conversion
ContentDotNet makes conversion between H.264, H.265 and H.266 very easy. It involves first loading the input, then coding all frames in it to another encoder.

Start by taking your input coded video:
```cs
IH264Service service = H264Factory.H264Service;
using (IH264Codec codec = service.Load("MyFile.264"))
{
    // ...
}
```

Create the factory for the codec you want to convert to (f.e. H.265):
```cs
IH265Service h265 = H265Factory.H265Service;
IH265Factory factory = h265.GetFactory();
```

Finally, use the `CodeTo` method on your codec, passing factory:
```cs
using (IH265Codec result = codec.CodeTo<IH265Codec>(factory))
{
    // ...
}
```

Congratulations! You just converted from H.264 to H.265. You can also do this vice versa and with other codecs, like H.266 or VP9, too.
