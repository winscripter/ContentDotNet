using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Specifies the available intra 4x4 prediction modes for H.264 encoding.
/// </summary>
public enum Intra4x4Mode
{
    /// <summary>
    /// Predicts the block using vertical extrapolation from the top reference samples.
    /// </summary>
    Vertical,

    /// <summary>
    /// Predicts the block using horizontal extrapolation from the left reference samples.
    /// </summary>
    Horizontal,

    /// <summary>
    /// Predicts the block using the average of the top and left reference samples.
    /// </summary>
    DC,

    /// <summary>
    /// Predicts the block using diagonal extrapolation downwards to the left.
    /// </summary>
    DiagonalDownLeft,

    /// <summary>
    /// Predicts the block using diagonal extrapolation downwards to the right.
    /// </summary>
    DiagonalDownRight,

    /// <summary>
    /// Predicts the block using vertical extrapolation with a rightward shift.
    /// </summary>
    VerticalRight,

    /// <summary>
    /// Predicts the block using horizontal extrapolation with a downward shift.
    /// </summary>
    HorizontalDown,

    /// <summary>
    /// Predicts the block using vertical extrapolation with a leftward shift.
    /// </summary>
    VerticalLeft,

    /// <summary>
    /// Predicts the block using horizontal extrapolation with an upward shift.
    /// </summary>
    HorizontalUp
}

/// <summary>
///   H.264 prediction.
/// </summary>
public static class Prediction
{
#pragma warning disable CS1591
    public static void Predict(Intra4x4Mode mode, Span<byte> top, Span<byte> left, Matrix4x4 block, int chromaBlockSize, int lumaBlockSize, bool isChroma = false)
    {
#pragma warning restore
        int blockSize = isChroma ? chromaBlockSize : lumaBlockSize;
        if (top.Length < blockSize || left.Length < blockSize)
            throw new ArgumentException("Top and Left arrays must be at least the size of the block.");

        switch (mode)
        {
            case Intra4x4Mode.Vertical:
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = top[x];
                break;

            case Intra4x4Mode.Horizontal:
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = left[y];
                break;

            case Intra4x4Mode.DC:
                byte dc = 0;
                for (int i = 0; i < blockSize; i++)
                    dc += (byte)((top[i] + left[i]) / 2);
                dc = (byte)(dc / blockSize);
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = dc;
                break;

            case Intra4x4Mode.DiagonalDownLeft:
                byte[] extTop = new byte[blockSize + 3];
                for (int i = 0; i < blockSize; i++)
                    extTop[i] = top[i];
                for (int i = blockSize; i < extTop.Length; i++)
                    extTop[i] = top[blockSize - 1];
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = (byte)((extTop[x + y] + extTop[x + y + 1] + 1) / 2);
                break;

            case Intra4x4Mode.DiagonalDownRight:
                byte[] extTopRight = new byte[blockSize + 3];
                for (int i = 0; i < blockSize; i++)
                    extTopRight[i] = top[i];
                for (int i = blockSize; i < extTopRight.Length; i++)
                    extTopRight[i] = top[blockSize - 1];
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = (byte)((extTopRight[x + y] + extTopRight[x + y + 1] + 1) / 2);
                break;

            case Intra4x4Mode.VerticalRight:
                byte[] extTopRight2 = new byte[blockSize + 3];
                for (int i = 0; i < blockSize; i++)
                    extTopRight2[i] = top[i];
                for (int i = blockSize; i < extTopRight2.Length; i++)
                    extTopRight2[i] = top[blockSize - 1];
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = (byte)((extTopRight2[x + y] + extTopRight2[x + y + 1] + 1) / 2);
                break;

            case Intra4x4Mode.HorizontalDown:
                byte[] extLeftDown = new byte[blockSize + 3];
                for (int i = 0; i < blockSize; i++)
                    extLeftDown[i] = left[i];
                for (int i = blockSize; i < extLeftDown.Length; i++)
                    extLeftDown[i] = left[blockSize - 1];
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = (byte)((extLeftDown[x + y] + extLeftDown[x + y + 1] + 1) / 2);
                break;

            case Intra4x4Mode.VerticalLeft:
                byte[] extTopLeft = new byte[blockSize + 3];
                for (int i = 0; i < blockSize; i++)
                    extTopLeft[i] = top[i];
                for (int i = blockSize; i < extTopLeft.Length; i++)
                    extTopLeft[i] = top[blockSize - 1];
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = (byte)((extTopLeft[x + y] + extTopLeft[x + y + 1] + 1) / 2);
                break;

            case Intra4x4Mode.HorizontalUp:
                byte[] extLeftUp = new byte[blockSize + 3];
                for (int i = 0; i < blockSize; i++)
                    extLeftUp[i] = left[i];
                for (int i = blockSize; i < extLeftUp.Length; i++)
                    extLeftUp[i] = left[blockSize - 1];
                for (int y = 0; y < blockSize; y++)
                    for (int x = 0; x < blockSize; x++)
                        block[y, x] = (byte)((extLeftUp[x + y] + extLeftUp[x + y + 1] + 1) / 2);
                break;

            default:
                throw new NotImplementedException($"Mode {mode} is not implemented.");
        }
    }
}
