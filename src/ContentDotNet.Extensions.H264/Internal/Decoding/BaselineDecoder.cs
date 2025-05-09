using ContentDotNet.Extensions.H264.Macroblocks;
using System.Drawing;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed partial class BaselineDecoder
{
    private readonly Intra _intra;
    private readonly Inter _inter;

    public BaselineDecoder(DerivationContext derivationContext, IMacroblockUtility macroblockUtility, Size frameSize)
    {
        _intra = new Intra(macroblockUtility);
        _inter = new Inter(derivationContext, macroblockUtility, frameSize);
    }

    public Intra IntraPredictor => _intra;
    public Inter InterPredictor => _inter;
}
