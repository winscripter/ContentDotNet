# Video Usability Information (VUI) Extensions
Aside from the `VuiParameters` structure that represents the raw VUI parameters data and methods to read/write them, ContentDotNet also includes a namespace in the H.264 extension called, `ContentDotNet.Extensions.H264.Usability`. This contains a few primitives and a single extension class that makes working with VUI parameters significantly easier.

### Overview of the VideoUsabilityExtensions class
This class provides useful utilities for VUI parameters that are likely to be used, such as obtaining color spaces and aspect ratio.

#### Aspect Ratio functions
The `IsExtendedSarAspectRatio(this VuiParameters)` method returns a boolean indicating whether the given VUI parameters use Extended SAR:
```cs
VuiParameters vuip = ...;
Console.WriteLine(vuip.IsExtendedSarAspectRatio());
```
There's also a variant, `IsExtendedSarAspectRatio(uint)`, which isn't an extension method. The `uint` parameter is `aspect_ratio_idc`, a syntax element of VUI parameters.

Then there's `GetAspectRatio(this VuiParameters)`, which returns the aspect ratio. It accounts for both scenarios when `aspect_ratio_idc` is or isn't Extended SAR. For example, if your VUI parameters specify 9:16 aspect ratio:
```cs
ContentDotNet.Primitives.AspectRatio aspectRatio = vuip.GetAspectRatio();
Console.WriteLine(aspectRatio); // 9:16
```
Cool, but then there's a method called `GetAspectRatioFields(ContentDotNet.Primitives.AspectRatio)`. This also isn't an extension method, but it returns a tuple of three elements: `(uint, int, int)` (aspect_ratio_idc, sar_width, sar_height), which essentially converts your aspect ratio back to the syntax elements required to represent it:
```cs
using ContentDotNet.Extensions.H264.Usability;
using ContentDotNet.Primitives;

// Example 1

var aspectRatio1 = VideoUsabilityExtensions.GetAspectRatioFields(new AspectRatio(4, 3));
Console.WriteLine(aspectRatio1.aspectRatioIdc); // 14

// Example 2

var aspectRatio2 = VideoUsabilityExtensions.GetAspectRatioFields(new AspectRatio(50, 50));
Console.WriteLine(aspectRatio2.aspectRatioIdc); // 255 (a.k.a. Extended SAR)
```

### Other
There are a few other methods you can use to further obtain more information from VUI parameters in a less cryptic way.

#### 1. GetMatrixCoefficients(this VuiParameters)
This reads the `matrix_coefficients` syntax element and turns the value into a meaningful enumeration.
```cs
MatrixCoefficients coeffs = vuip.GetMatrixCoefficients();
Console.WriteLine(coeffs == MatrixCoefficients.BT470BG); // Check if it's equal to ITU-R BT.470BG
```

#### 2. GetTransferCharacteristics(this VuiParameters)
This reads the `transfer_characteristics` syntax element and turns the value into a meaningful enumeration.
```cs
TransferCharacteristics transfer = vuip.GetTransferCharacteristics();
Console.WriteLine(transfer == TransferCharacteristics.BT709); // Check if it's equal to BT.709
```

#### 3. GetColorPrimaries(this VuiParameters)
This reads the `colour_primaries` syntax element and turns the value into a meaningful enumeration.
```cs
ColorPrimaries color = vuip.GetColorPrimaries();
Console.WriteLine(color == TransferCharacteristics.Film); // Check if it's equal to Generic Film Color Primaries
```

#### 4. GetVideoFormat(this VuiParameters)
This reads the `video_format` syntax element and turns the value into a meaningful enumeration.
```cs
VideoFormat format = vuip.GetVideoFormat();
Console.WriteLine(format == VideoFormat.Ntsc); // Check if it's equal to NTSC
```
