using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using System.Net;

namespace ContentDotNet.Extensions.H264.Impl;

internal sealed class SliceDecoder : IMacroblockUtility
{
    private readonly Dictionary<int, MacroblockLayer> AddressCache = [];
    private ReaderState SliceOffset;
    private readonly SequenceParameterSet _sps;
    private readonly PictureParameterSet _pps;
    private readonly SliceHeader _shd;
    private readonly BitStreamReader _Reader;

    public SliceDecoder(ReaderState sliceOffset, SequenceParameterSet sps, PictureParameterSet pps, SliceHeader shd, BitStreamReader reader)
    {
        SliceOffset = sliceOffset;
        _sps = sps;
        _pps = pps;
        _shd = shd;
        _Reader = reader;
    }

    private void SearchAndAddMacroblock(int address)
    {
        if (!AddressCache.ContainsKey(address))
        {
            var init = _Reader.GetState();
            _Reader.GoTo(this.SliceOffset);

            for (int i = 0; i < address - 1; i++)
            {
                ParseSliceElement();
            }

            MacroblockLayer mb = ParseSliceElement();
            AddressCache.Add(address, mb);

            _Reader.GoTo(init);
        }
    }

    private MacroblockLayer ParseSliceElement() => throw new NotImplementedException();

    public bool AllAcResidualTransformsAreZeroDueToCodedBlockPatternsBeingZero(int address)
    {
        MacroblockLayer mb = GetMacroblock(address);
        if (mb.CodedBlockPattern == 0 &&
            mb.Intra16x16Residual is not null &&
            IsAllZero(mb.Intra16x16Residual!.Value.Intra16x16ACLevel) &&
            IsAllZero(mb.Intra16x16Residual!.Value.Cb16x16ACLevel) &&
            IsAllZero(mb.Intra16x16Residual!.Value.Cr16x16ACLevel))
            return true;

        return false;

        static bool IsAllZero(ContainerMatrix16x16 container)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (container[x, y] != 0)
                        return false;
                }
            }

            return true;
        }
    }

    public void Get16x16LumaBlock(int luma16x16BlkIdx, ContainerMatrix16x16 output)
    {
        throw new NotImplementedException();
    }

    public void Get4x4LumaBlock(int luma4x4BlkIdx, ContainerMatrix4x4 output)
    {
        throw new NotImplementedException();
    }

    public void Get8x8LumaBlock(int luma8x8BlkIdx, ContainerMatrix8x8 output)
    {
        throw new NotImplementedException();
    }

    public void GetIntra16x16PredMode(int mbAddr, Span<int> output)
    {
        var mb = GetMacroblock(mbAddr);
        output[0] = H264Decoder.DeriveIntra16x16PredictionMode((int)mb.MbType);
    }

    public void GetIntra4x4PredMode(int mbAddr, Span<int> output)
    {
        var mb = GetMacroblock(mbAddr);
        var pred = mb.Prediction ?? throw new InvalidOperationException("No macroblock prediction");

        Span<int> remIntra4x4PredMode = stackalloc int[16];
        Span<int> remIntra8x8PredMode = stackalloc int[16];
        Span<bool> prevIntra4x4PredModeFlag = stackalloc bool[16];
        Span<bool> prevIntra8x8PredModeFlag = stackalloc bool[16];

        for (int i = 0; i < 16; i++)
        {
            remIntra4x4PredMode[i] = (int)pred.RemIntra4x4PredMode[i];
            remIntra8x8PredMode[i] = (int)pred.RemIntra8x8PredMode[i];
            prevIntra4x4PredModeFlag[i] = pred.PrevIntra4x4PredModeFlag[i];
            prevIntra8x8PredModeFlag[i] = pred.PrevIntra8x8PredModeFlag[i];
        }

        Span<int> intra4x4PredMode = stackalloc int[16];
        Span<int> intra8x8PredMode = stackalloc int[16];

        for (int luma4x4BlkIdx = 0; luma4x4BlkIdx < 16; luma4x4BlkIdx++)
        {
            IntraInterDecoder.Intra.DeriveIntra4x4PredMode(
                luma4x4BlkIdx,
                new DerivationContext(0, false, false, default, default, mbAddr, (int)mb.MbType, 0, 0, false, 0, 0),
                false,
                intra4x4PredMode,
                intra8x8PredMode,
                remIntra4x4PredMode,
                prevIntra4x4PredModeFlag
            );
        }

        for (int i = 0; i < 16; i++)
            output[i] = intra4x4PredMode[i];
    }

    public void GetIntra8x8PredMode(int mbAddr, Span<int> output)
    {
        throw new NotImplementedException();
    }

    public MacroblockLayer GetMacroblock(int address)
    {
        SearchAndAddMacroblock(address);

        if (AddressCache.TryGetValue(address, out var mbLayer))
        {
            return mbLayer;
        }

        throw new InvalidOperationException("Cannot fetch macroblock " + address);
    }

    public MacroblockLayer? GetMacroblockToTheBottom(int address)
    {
        return GetMacroblock(
            new AddressFlow(
                address,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Down()
            .Value
        );
    }

    public MacroblockLayer? GetMacroblockToTheLeft(int address)
    {
        return GetMacroblock(
            new AddressFlow(
                address,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Left()
            .Value
        );
    }

    public MacroblockLayer? GetMacroblockToTheRight(int address)
    {
        return GetMacroblock(
            new AddressFlow(
                address,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Right()
            .Value
        );
    }

    public MacroblockLayer? GetMacroblockToTheTop(int address)
    {
        return GetMacroblock(
            new AddressFlow(
                address,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Up()
            .Value
        );
    }

    public uint GetMbType(int mbAddr)
    {
        return GetMacroblock(mbAddr).MbType;
    }

    public ContainerMatrix16x16 GetPixels(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public ContainerMatrix16x16? GetPixelsToBottom(int mbAddr)
    {
        return GetPixels(
            new AddressFlow(
                mbAddr,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Down()
            .Value
        );
    }

    public ContainerMatrix16x16? GetPixelsToLeft(int mbAddr)
    {
        return GetPixels(
            new AddressFlow(
                mbAddr,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Left()
            .Value
        );
    }

    public ContainerMatrix16x16? GetPixelsToRight(int mbAddr)
    {
        return GetPixels(
            new AddressFlow(
                mbAddr,
                (int)(_sps.PicWidthInMbsMinus1 + 1),
                (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
            )
            .Right()
            .Value
        );
    }

    public ContainerMatrix16x16? GetPixelsToTop(int mbAddr)
    {
        return GetPixels(
             new AddressFlow(
                 mbAddr,
                 (int)(_sps.PicWidthInMbsMinus1 + 1),
                 (int)(_sps.PicWidthInMbsMinus1 + 1) * (int)(_sps.PicHeightInMapUnitsMinus1 + 1)
             )
             .Up()
             .Value
         );
    }

    public uint GetSubMbType(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public int GetTotalCoefficient(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool HasPixels(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool HasPixelsToBottom(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool HasPixelsToLeft(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool HasPixelsToRight(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool HasPixelsToTop(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public bool IsCodedWithInter(int mbAddr)
    {
        return _shd.GetSliceType() is GeneralSliceType.P or GeneralSliceType.B;
    }

    public bool IsCodedWithIntra(int mbAddr)
    {
        return _shd.GetSliceType() is GeneralSliceType.I;
    }

    public bool IsCodedWithIntra16x16(int mbAddr)
    {
        if (!IsCodedWithIntra(mbAddr))
            return false;

        var mb = GetMacroblock(mbAddr);
        return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _shd.GetSliceType()) is SliceTypes.Intra_16x16;
    }

    public bool IsCodedWithIntra4x4(int mbAddr)
    {
        if (!IsCodedWithIntra(mbAddr))
            return false;

        var mb = GetMacroblock(mbAddr);
        return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _shd.GetSliceType()) is SliceTypes.Intra_4x4;
    }

    public bool IsCodedWithIntra8x8(int mbAddr)
    {
        if (!IsCodedWithIntra(mbAddr))
            return false;

        var mb = GetMacroblock(mbAddr);
        return Util264.MbPartPredMode((int)mb.MbType, 0, mb.TransformSize8x8Flag, _shd.GetSliceType()) is SliceTypes.Intra_8x8;
    }

    public bool IsFieldMacroblock(int mbAddr)
    {
        return _shd.FieldPicFlag;
    }

    public bool IsFrameMacroblock(int mbAddr)
    {
        return !IsFieldMacroblock(mbAddr);
    }

    public bool IsMacroblockOfTypeSi(int mbAddr)
    {
        return _shd.GetSliceType() == GeneralSliceType.SI;
    }

    public bool IsMbSkipFlagForMacroblock(int mbAddr)
    {
        throw new NotImplementedException();
    }

    public void SetPixels(int mbAddr, ContainerMatrix16x16 pixels)
    {
        throw new NotImplementedException();
    }
}
