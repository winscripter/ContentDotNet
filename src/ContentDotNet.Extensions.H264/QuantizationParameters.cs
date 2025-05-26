namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents H.264 Quantization Parameters.
/// </summary>
/// <param name="QPY"></param>
/// <param name="QSY"></param>
/// <param name="QPC"></param>
/// <param name="QSC"></param>
public record struct QuantizationParameters(int QPY, int QSY, int QPC, int QSC);
