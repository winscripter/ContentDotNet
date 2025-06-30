using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Minimal;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   The abstract macroblock utility.
/// </summary>
public class MacroblockUtilityBase : IMacroblockUtility
{
    /// <summary>
    ///   A dummy macroblock utility that throws <see cref="NotImplementedException"/> when any operation
    ///   is attempted on it.
    /// </summary>
    public static readonly MacroblockUtilityBase Dummy = new();

    /// <inheritdoc />
    public virtual bool AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(int address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual void GetIntra8x8PredMode(int mbAddr, Span<int> output)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual MinimalMacroblockLayer GetMacroblock(int address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual MinimalMacroblockLayer? GetMacroblockToTheBottom(int address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual MinimalMacroblockLayer? GetMacroblockToTheLeft(int address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual MinimalMacroblockLayer? GetMacroblockToTheRight(int address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual MinimalMacroblockLayer? GetMacroblockToTheTop(int address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual uint GetMbType(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual uint GetSubMbType(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual int GetTotalCoefficient(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsCodedWithInter(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsCodedWithIntra(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsCodedWithIntra16x16(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsCodedWithIntra4x4(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsCodedWithIntra8x8(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsFieldMacroblock(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsFrameMacroblock(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsMacroblockOfTypeSi(int mbAddr)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual bool IsMbSkipFlagForMacroblock(int mbAddr)
    {
        throw new NotImplementedException();
    }
}
