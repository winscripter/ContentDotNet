namespace ContentDotNet.Extensions.H26x.Sei.H264.Parameters;

/// <summary>
///   Parameters for writing SEI filler payload.
/// </summary>
public sealed record FillerPayloadParameters : ISeiModelParameter
{
    /// <summary>
    ///   Specifies bits worth of filler data to write.
    /// </summary>
    public int BitsToWrite { get; }

    /// <summary>
    ///   Use bits of 1 instead of 0?
    /// </summary>
    public bool UseOneBits { get; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="FillerPayloadParameters"/> class.
    /// </summary>
    /// <param name="bitsToWrite">Specifies bits worth of filler data to write.</param>
    /// <param name="useOneBits">Use bits of 1 instead of 0?</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public FillerPayloadParameters(int bitsToWrite, bool useOneBits)
    {
        if (bitsToWrite < 0)
            throw new ArgumentOutOfRangeException(nameof(bitsToWrite), "Bits to write must be non-negative");

        BitsToWrite = bitsToWrite;
        UseOneBits = useOneBits;
    }
}
