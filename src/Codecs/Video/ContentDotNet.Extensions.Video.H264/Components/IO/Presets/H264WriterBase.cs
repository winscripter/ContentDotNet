namespace ContentDotNet.Extensions.Video.H264.Components.IO.Presets
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using System.Threading.Tasks;

    internal abstract class H264WriterBase : IH264SyntaxWriter
    {
        public abstract bool UsesCabac { get; }

        public BitStreamWriter Writer { get; set; } = null!;
        public H264RbspState RbspState { get; set; } = null!;

        public abstract void WriteCodedBlockFlag(bool value);

        public abstract Task<bool> WriteCodedBlockFlagAsync(bool value);

        public abstract void WriteCodedBlockPattern(uint value);

        public abstract Task<uint> WriteCodedBlockPatternAsync(uint value);

        public abstract void WriteCoeffAbsLevelMinus1(uint value);

        public abstract Task<uint> WriteCoeffAbsLevelMinus1Async(uint value);

        public abstract void WriteCoeffSignFlag(bool value);

        public abstract Task<bool> WriteCoeffSignFlagAsync(bool value);

        public abstract void WriteCoeffToken(uint value);

        public abstract Task<uint> WriteCoeffTokenAsync(uint value);

        public abstract void WriteEndOfSliceFlag(bool value);

        public abstract Task<bool> WriteEndOfSliceFlagAsync(bool value);

        public abstract void WriteIntraChromaPredMode(uint value);

        public abstract Task<uint> WriteIntraChromaPredModeAsync(uint value);

        public abstract void WriteLastSignificantCoeffFlag(bool value);

        public abstract Task<bool> WriteLastSignificantCoeffFlagAsync(bool value);

        public abstract void WriteLevelPrefix(uint value);

        public abstract Task<uint> WriteLevelPrefixAsync(uint value);

        public abstract void WriteLevelSuffix(uint value);

        public abstract Task<uint> WriteLevelSuffixAsync(uint value);

        public abstract void WriteMbFieldDecodingFlag(bool value);

        public abstract Task<bool> WriteMbFieldDecodingFlagAsync(bool value);

        public abstract void WriteMbQpDelta(int value);

        public abstract Task<int> WriteMbQpDeltaAsync(int value);

        public abstract void WriteMbSkipFlag(bool value);

        public abstract Task<bool> WriteMbSkipFlagAsync(bool value);

        public abstract void WriteMbSkipRun(uint value);

        public abstract Task<uint> WriteMbSkipRunAsync(uint value);

        public abstract void WriteMbType(uint value);

        public abstract Task<uint> WriteMbTypeAsync(uint value);

        public abstract void WriteMvdL0(int value);

        public abstract Task<int> WriteMvdL0Async(int value);

        public abstract void WriteMvdL1(int value);

        public abstract Task<int> WriteMvdL1Async(int value);

        public abstract void WritePrevIntra4x4PredModeFlag(bool value);

        public abstract Task<bool> WritePrevIntra4x4PredModeFlagAsync(bool value);

        public abstract void WritePrevIntra8x8PredModeFlag(bool value);

        public abstract Task<bool> WritePrevIntra8x8PredModeFlagAsync(bool value);

        public abstract void WriteRefIdxL0(uint value);

        public abstract Task<uint> WriteRefIdxL0Async(uint value);

        public abstract void WriteRefIdxL1(uint value);

        public abstract Task<uint> WriteRefIdxL1Async(uint value);

        public abstract void WriteRemIntra4x4PredMode(uint value);

        public abstract Task<uint> WriteRemIntra4x4PredModeAsync(uint value);

        public abstract void WriteRemIntra8x8PredMode(uint value);

        public abstract Task<uint> WriteRemIntra8x8PredModeAsync(uint value);

        public abstract void WriteRunBefore(uint value);

        public abstract Task<uint> WriteRunBeforeAsync(uint value);

        public abstract void WriteSignificantCoeffFlag(bool value);

        public abstract Task<bool> WriteSignificantCoeffFlagAsync(bool value);

        public abstract void WriteSubMbType(uint value);

        public abstract Task<uint> WriteSubMbTypeAsync(uint value);

        public abstract void WriteTotalZeros(uint value);

        public abstract Task<uint> WriteTotalZerosAsync(uint value);

        public abstract void WriteTrailingOnesSignFlag(bool value);

        public abstract Task<bool> WriteTrailingOnesSignFlagAsync(bool value);

        public abstract void WriteTransformSize8X8Flag(bool value);

        public abstract Task<bool> WriteTransformSize8X8FlagAsync(bool value);
    }
}
