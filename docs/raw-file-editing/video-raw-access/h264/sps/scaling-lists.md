# Scaling Lists and Matrices
ContentDotNet lets you work with scaling lists in H.264 SPS.

# Reading scaling lists
To read scaling lists, you have two options.

### Just scaling lists
If you want to just read scaling lists, use:
```cs
using ContentDotNet.Extensions.H264.Models;

Span<int> scalingList = stackalloc int[16];
SequenceParameterSet.ReadOnlyScalingList(your_bitstream, 16, scalingList);
```

### SPS, alongside with scaling lists
After reading the SPS:
```cs
using ContentDotNet.Extensions.H264.Models;

var sps = SequenceParameterSet.Read(your_bitstream);
```
you have access to the scaling matrices:
```cs
ScalingMatrices? matrices = sps.ScalingMatrices;
if (matrices is not null)
{
    Span<int> list3 = stackalloc int[matrices.Value.GetListLength(3)];
    matrices.Value.ReadList(your_bitstream, list3);
}
```
