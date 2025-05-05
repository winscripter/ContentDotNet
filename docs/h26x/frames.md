# Frames and the IFrame interface
In the `ContentDotNet.Extensions.H26x` namespace, there is an interface called
`IFrame`, which represents a frame coded with the YUV (otherwise known as Y'Cb'Cr)
color format.

### Fields
```cs
int Width { get; set; }
```
Represents the width of the frame.

```cs
int Height { get; set; }
```
Represents the height of the frame.

```cs
ContentDotNet.Yuv this[int x, int y] { get; set; }
```
Gets/sets a pixel at index.

## Flavors
There are a few implementations which you can pick depending on your needs. All reside
in the same `ContentDotNet.Extensions.H26x` namespace.

First, there's a `HeapFrame` class. It implements `IFrame`, but all pixels
are backed by a multidimensional array. This makes the entire frame *heap allocated*,
hence *HeapFrame*. It also achieves best performance. This is recommended.

And there's `StreamFrame`. It's significantly slower than `HeapFrame` as the
pixels are backed by an explicit `System.IO.Stream` instance, hence *StreamFrame*. Thus, it doesn't allocate any new memory besides the instance itself, and can also be disposable.

To pick:
- if you're aiming for platform independency and best performance but automatic memory management, go for `HeapFrame`,
- if you're aiming for platform independency and not so good performance but manual memory management, go for `StreamFrame`

