namespace ContentDotNet.Extensions.Video.H264.Models
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   The H.264 macroblock type.
    /// </summary>
    /// <param name="SliceType">Slice type</param>
    /// <param name="MacroblockTypeNumber">Macroblock type number</param>
    /// <param name="Inferred">Inferred?</param>
    public record struct H264MacroblockType(H264SliceType SliceType, int MacroblockTypeNumber, bool Inferred);
}
