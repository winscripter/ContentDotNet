namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed partial class BaselineDecoder
{
    private readonly Intra _intra;
    private readonly Inter _inter;

    public BaselineDecoder(DerivationContext derivationContext, IMacroblockUtility macroblockUtility)
    {
        _intra = new Intra(macroblockUtility);
        _inter = new Inter(derivationContext, macroblockUtility);
    }

    public Intra Intra => _intra;
    public Inter Inter => _inter;
}
