using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;
using System.Drawing;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed partial class Decoder264
{
    private readonly DerivationContext _derivationContext;
    private readonly IMacroblockUtility _macroblockUtility;

    public Decoder264(DerivationContext derivationContext, IMacroblockUtility macroblockUtility)
    {
        _derivationContext = derivationContext;
        _macroblockUtility = macroblockUtility;

        InitializeInterPrediction();
    }
}
