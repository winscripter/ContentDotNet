namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed partial class BaselineDecoder
{
    private readonly DerivationContext _derivationContext;
    private readonly IMacroblockUtility _macroblockUtility;

    public BaselineDecoder(DerivationContext derivationContext, IMacroblockUtility macroblockUtility)
    {
        _derivationContext = derivationContext;
        _macroblockUtility = macroblockUtility;

        InitializeInterPrediction();
    }
}
