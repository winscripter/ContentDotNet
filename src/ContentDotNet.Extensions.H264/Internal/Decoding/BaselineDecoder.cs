using System.Drawing;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed partial class BaselineDecoder
{
    private readonly Intra _intra;
    private readonly Inter _inter;

    public BaselineDecoder(DerivationContext derivationContext, IMacroblockUtility macroblockUtility, IReferencePictureListFactory factory, Size frameSize)
    {
        _intra = new Intra(macroblockUtility);
        _inter = new Inter(derivationContext, macroblockUtility, factory, frameSize);
    }

    public Intra IntraPredictor => _intra;
    public Inter InterPredictor => _inter;
}
