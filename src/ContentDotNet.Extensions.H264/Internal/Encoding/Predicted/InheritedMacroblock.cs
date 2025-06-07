using System.Drawing;

namespace ContentDotNet.Extensions.H264.Internal.Encoding.Predicted;

internal struct InheritedMacroblock
{
    public int ReferencePictureIndex;
    public bool Direction; // false - left, true - right
    public Size Size;
}
