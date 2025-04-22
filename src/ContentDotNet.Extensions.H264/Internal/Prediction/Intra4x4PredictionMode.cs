namespace ContentDotNet.Extensions.H264.Internal.Prediction;

internal enum Intra4x4PredictionMode : byte
{
    Vertical,
    Horizontal,
    Dc,
    DiagonalDownLeft,
    DiagonalDownRight,
    VerticalRight,
    HorizontalDown,
    VerticalLeft,
    HorizontalUp,
    Unknown = 255
}
