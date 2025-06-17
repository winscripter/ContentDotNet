using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Minimal;

/// <summary>
///   Minimal macroblock layer
/// </summary>
public struct MinimalMacroblockLayer
{
    /// <summary>
    /// Gets or sets the macroblock type.
    /// </summary>
    public uint MbType;

    /// <summary>
    /// Gets or sets the PCM luma values for the macroblock.
    /// </summary>
    public Container256UInt32 PcmLuma;

    /// <summary>
    /// Gets or sets the PCM chroma values for the macroblock.
    /// </summary>
    public Container512UInt32 PcmChroma;

    /// <summary>
    /// Gets or sets a value indicating whether the transform size is 8x8.
    /// </summary>
    public bool TransformSize8x8Flag;

    /// <summary>
    /// Gets or sets the coded block pattern for the macroblock.
    /// </summary>
    public int CodedBlockPattern;

    /// <summary>
    /// Gets or sets the macroblock quantization parameter delta.
    /// </summary>
    public int MbQpDelta;

    /// <summary>
    /// Gets or sets the residual data for Intra 16x16 prediction mode.
    /// </summary>
    public MinimalResidual? Intra16x16Residual;

    /// <summary>
    /// Macroblock prediction
    /// </summary>
    public MacroblockPrediction? Prediction;

    /// <summary>
    /// Submacroblock prediction
    /// </summary>
    public SubMacroblockPrediction? SubMacroblockPrediction;

    /// <summary>
    ///   Initializes a new instance of the <see cref="MinimalMacroblockLayer"/> structure.
    /// </summary>
    public MinimalMacroblockLayer(uint mbType, Container256UInt32 pcmLuma, Container512UInt32 pcmChroma, bool transformSize8x8Flag, int codedBlockPattern, int mbQpDelta, MinimalResidual? intra16x16Residual, MacroblockPrediction? prediction, SubMacroblockPrediction? subMacroblockPrediction)
    {
        MbType = mbType;
        PcmLuma = pcmLuma;
        PcmChroma = pcmChroma;
        TransformSize8x8Flag = transformSize8x8Flag;
        CodedBlockPattern = codedBlockPattern;
        MbQpDelta = mbQpDelta;
        Intra16x16Residual = intra16x16Residual;
        Prediction = prediction;
        SubMacroblockPrediction = subMacroblockPrediction;
    }

    /// <summary>
    ///   Converts from an actual macroblock.
    /// </summary>
    /// <param name="mb"></param>
    /// <returns></returns>
    public static MinimalMacroblockLayer From(MacroblockLayer mb)
        => new(mb.MbType, mb.PcmLuma, mb.PcmChroma, mb.TransformSize8x8Flag, mb.CodedBlockPattern, mb.MbQpDelta, mb.Intra16x16Residual is not null ? MinimalResidual.From(mb.Intra16x16Residual.Value) : null, mb.Prediction, mb.SubMacroblockPrediction);
}
