namespace ContentDotNet.Extensions.Video.H264.Components.IO.Presets
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal abstract class H264ReaderBase : IH264SyntaxReader
    {
        public abstract bool UsesCabac { get; }
        public abstract H264MacroblockInfo? MacroblockInfo { get; set; }

        public BitStreamReader Reader { get; set; } = null!;
        public H264RbspState RbspState { get; set; } = null!;

        private readonly Dictionary<string, object> _misc = [];
        public Dictionary<string, object> Miscellaneous => _misc;

        public abstract bool ReadCodedBlockFlag();

        public abstract Task<bool> ReadCodedBlockFlagAsync();

        public abstract uint ReadCodedBlockPattern();

        public abstract Task<uint> ReadCodedBlockPatternAsync();

        public abstract uint ReadCoeffAbsLevelMinus1();

        public abstract Task<uint> ReadCoeffAbsLevelMinus1Async();

        public abstract bool ReadCoeffSignFlag();

        public abstract Task<bool> ReadCoeffSignFlagAsync();

        public abstract uint ReadCoeffToken();

        public abstract Task<uint> ReadCoeffTokenAsync();

        public abstract bool ReadEndOfSliceFlag();

        public abstract Task<bool> ReadEndOfSliceFlagAsync();

        public abstract uint ReadIntraChromaPredMode();

        public abstract Task<uint> ReadIntraChromaPredModeAsync();

        public abstract bool ReadLastSignificantCoeffFlag();

        public abstract Task<bool> ReadLastSignificantCoeffFlagAsync();

        public abstract uint ReadLevelPrefix();

        public abstract Task<uint> ReadLevelPrefixAsync();

        public abstract uint ReadLevelSuffix();

        public abstract Task<uint> ReadLevelSuffixAsync();

        public abstract bool ReadMbFieldDecodingFlag();

        public abstract Task<bool> ReadMbFieldDecodingFlagAsync();

        public abstract int ReadMbQpDelta();

        public abstract Task<int> ReadMbQpDeltaAsync();

        public abstract bool ReadMbSkipFlag();

        public abstract Task<bool> ReadMbSkipFlagAsync();

        public abstract uint ReadMbSkipRun();

        public abstract Task<uint> ReadMbSkipRunAsync();

        public abstract uint ReadMbType();

        public abstract Task<uint> ReadMbTypeAsync();

        public abstract int ReadMvdL0();

        public abstract Task<int> ReadMvdL0Async();

        public abstract int ReadMvdL1();

        public abstract Task<int> ReadMvdL1Async();

        public abstract bool ReadPrevIntra4x4PredModeFlag();

        public abstract Task<bool> ReadPrevIntra4x4PredModeFlagAsync();

        public abstract bool ReadPrevIntra8x8PredModeFlag();

        public abstract Task<bool> ReadPrevIntra8x8PredModeFlagAsync();

        public abstract uint ReadRefIdxL0();

        public abstract Task<uint> ReadRefIdxL0Async();

        public abstract uint ReadRefIdxL1();

        public abstract Task<uint> ReadRefIdxL1Async();

        public abstract uint ReadRemIntra4x4PredMode();

        public abstract Task<uint> ReadRemIntra4x4PredModeAsync();

        public abstract uint ReadRemIntra8x8PredMode();

        public abstract Task<uint> ReadRemIntra8x8PredModeAsync();

        public abstract uint ReadRunBefore();

        public abstract Task<uint> ReadRunBeforeAsync();

        public abstract bool ReadSignificantCoeffFlag();

        public abstract Task<bool> ReadSignificantCoeffFlagAsync();

        public abstract uint ReadSubMbType();

        public abstract Task<uint> ReadSubMbTypeAsync();

        public abstract uint ReadTotalZeros();

        public abstract Task<uint> ReadTotalZerosAsync();

        public abstract bool ReadTrailingOnesSignFlag();

        public abstract Task<bool> ReadTrailingOnesSignFlagAsync();

        public abstract bool ReadTransformSize8X8Flag();

        public abstract Task<bool> ReadTransformSize8X8FlagAsync();
    }
}
