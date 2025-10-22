# Pictures
ContentDotNet can represent pictures, images and frames with the `Picture` class.

All types and methods here are located in the `ContentDotNet.Pictures` namespace under the `ContentDotNet` assembly.

# Picture class
The non-generic Picture class is an abstract class with the following properties:
```cs
public abstract System.Drawing.Size ImageSize { get; }
```
Represents the image size.

```cs
public abstract ContentDotNet.Configuration? Configuration { get; }
```
Configuration to manage memory allocation.

```cs
public abstract void Dispose();
```
Same purpose as `System.IDisposable.Dispose()`.

The generic Picture class,
```cs
public abstract class Picture<TPixel> : Picture
    where TPixel : unmanaged, IColor
```
inherits from the non-generic version, and the type of the generic type is the representation of the pixel format for use in pixels in the image. It adds an indexer to access pixels in the image using x/y coordinates.
```cs
public abstract TPixel this[int x, int y] { get; set; }
```

# In-memory picture
This implementation is instantiated with this code.
```cs
var superSmallImage =
    MemoryPictureFactory.Instance.CreatePicture<Rgb>(640, 480);
```
All pixel data is represented in memory, as a .NET multidimensional array.
