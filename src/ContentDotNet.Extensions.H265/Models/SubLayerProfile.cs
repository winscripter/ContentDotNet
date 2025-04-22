using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H265.Models;

/// <summary>
/// Sub layer profile, from <see cref="ProfileTierLevel"/>.
/// </summary>
public struct SubLayerProfile : IEquatable<SubLayerProfile>
{
    /// <summary>
    /// Represents the profile space of the sub-layer.
    /// </summary>
    public uint ProfileSpace;

    /// <summary>
    /// Indicates whether the tier flag is set for the sub-layer.
    /// </summary>
    public bool TierFlag;

    /// <summary>
    /// Specifies the profile IDC (Identifier Code) for the sub-layer.
    /// </summary>
    public uint ProfileIdc;

    /// <summary>
    /// Represents compatibility flags for the sub-layer, stored as a 32-bit packed structure.
    /// </summary>
    public PackedFlags32 CompatibilityFlag;

    /// <summary>
    /// Indicates whether the sub-layer is a progressive source.
    /// </summary>
    public bool ProgressiveSourceFlag;

    /// <summary>
    /// Indicates whether the sub-layer is an interlaced source.
    /// </summary>
    public bool InterlacedSourceFlag;

    /// <summary>
    /// Indicates whether the sub-layer has a non-packed constraint.
    /// </summary>
    public bool NonPackedConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to frame-only content.
    /// </summary>
    public bool FrameOnlyConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to a maximum of 12-bit depth.
    /// </summary>
    public bool Max12BitConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to a maximum of 10-bit depth.
    /// </summary>
    public bool Max10BitConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to a maximum of 8-bit depth.
    /// </summary>
    public bool Max8BitConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to 4:2:2 chroma subsampling.
    /// </summary>
    public bool Max422ChromaConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to 4:2:0 chroma subsampling.
    /// </summary>
    public bool Max420ChromaConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to monochrome content.
    /// </summary>
    public bool MonochromeConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to intra-coded frames only.
    /// </summary>
    public bool IntraConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to a single picture only.
    /// </summary>
    public bool OnePictureOnlyConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to lower bit rates.
    /// </summary>
    public bool LowerBitRateConstraintFlag;

    /// <summary>
    /// Indicates whether the sub-layer is constrained to a maximum of 14-bit depth.
    /// </summary>
    public bool Max14BitConstraintFlag;

    /// <summary>
    /// Reserved flag for future use, currently set to indicate inbld (in bitstream level design).
    /// </summary>
    public bool InbldFlag;

    /// <summary>
    /// Specifies the level IDC (Identifier Code) for the sub-layer.
    /// </summary>
    public uint LevelIdc;

    public SubLayerProfile(uint profileSpace, bool tierFlag, uint profileIdc, PackedFlags32 compatibilityFlag, bool progressiveSourceFlag, bool interlacedSourceFlag, bool nonPackedConstraintFlag, bool frameOnlyConstraintFlag, bool max12BitConstraintFlag, bool max10BitConstraintFlag, bool max8BitConstraintFlag, bool max422ChromaConstraintFlag, bool max420ChromaConstraintFlag, bool monochromeConstraintFlag, bool intraConstraintFlag, bool onePictureOnlyConstraintFlag, bool lowerBitRateConstraintFlag, bool max14BitConstraintFlag, bool inbldFlag, uint levelIdc)
    {
        ProfileSpace = profileSpace;
        TierFlag = tierFlag;
        ProfileIdc = profileIdc;
        CompatibilityFlag = compatibilityFlag;
        ProgressiveSourceFlag = progressiveSourceFlag;
        InterlacedSourceFlag = interlacedSourceFlag;
        NonPackedConstraintFlag = nonPackedConstraintFlag;
        FrameOnlyConstraintFlag = frameOnlyConstraintFlag;
        Max12BitConstraintFlag = max12BitConstraintFlag;
        Max10BitConstraintFlag = max10BitConstraintFlag;
        Max8BitConstraintFlag = max8BitConstraintFlag;
        Max422ChromaConstraintFlag = max422ChromaConstraintFlag;
        Max420ChromaConstraintFlag = max420ChromaConstraintFlag;
        MonochromeConstraintFlag = monochromeConstraintFlag;
        IntraConstraintFlag = intraConstraintFlag;
        OnePictureOnlyConstraintFlag = onePictureOnlyConstraintFlag;
        LowerBitRateConstraintFlag = lowerBitRateConstraintFlag;
        Max14BitConstraintFlag = max14BitConstraintFlag;
        InbldFlag = inbldFlag;
        LevelIdc = levelIdc;
    }

    /// <summary>
    ///   Parses this sub layer profile from the given bitstream.
    /// </summary>
    /// <param name="reader">Reader from where the sub layer profile is read from</param>
    /// <param name="layerPresentFlag">Layer present flag</param>
    /// <returns>Sub layer profile, read from the bitstream.</returns>
    public static SubLayerProfile Parse(BitStreamReader reader, bool layerPresentFlag)
    {
        uint profileSpace = reader.ReadBits(2);
        bool tierFlag = reader.ReadBit();
        uint profileIdc = reader.ReadBits(5);
        PackedFlags32 profileCompatibilityFlag = new(reader.ReadBits(32));
        bool progressiveSourceFlag = reader.ReadBit();
        bool interlacedSourceFlag = reader.ReadBit();
        bool nonPackedConstraintFlag = reader.ReadBit();
        bool frameOnlyConstraintFlag = reader.ReadBit();

        bool max12BitConstraintFlag = false;
        bool max10BitConstraintFlag = false;
        bool max8BitConstraintFlag = false;
        bool max422ChromaConstraintFlag = false;
        bool max420ChromaConstraintFlag = false;
        bool maxMonochromeConstraintFlag = false;
        bool maxIntraConstraintFlag = false;
        bool onePictureOnlyConstraintFlag = false;
        bool lowerBitRateConstraintFlag = false;

        bool max14BitConstraintFlag = false;

        bool inbldFlag = false;
        uint levelIdc = 0;

        if (profileIdc == 4 || profileCompatibilityFlag[4] ||
            profileIdc == 5 || profileCompatibilityFlag[5] ||
            profileIdc == 6 || profileCompatibilityFlag[6] ||
            profileIdc == 7 || profileCompatibilityFlag[7] ||
            profileIdc == 8 || profileCompatibilityFlag[8] ||
            profileIdc == 9 || profileCompatibilityFlag[9] ||
            profileIdc == 10 || profileCompatibilityFlag[10] ||
            profileIdc == 11 || profileCompatibilityFlag[11])
        {
            max12BitConstraintFlag = reader.ReadBit();
            max10BitConstraintFlag = reader.ReadBit();
            max8BitConstraintFlag = reader.ReadBit();
            max422ChromaConstraintFlag = reader.ReadBit();
            max420ChromaConstraintFlag = reader.ReadBit();
            maxMonochromeConstraintFlag = reader.ReadBit();
            maxIntraConstraintFlag = reader.ReadBit();
            onePictureOnlyConstraintFlag = reader.ReadBit();
            lowerBitRateConstraintFlag = reader.ReadBit();

            if (profileIdc == 5 || profileCompatibilityFlag[5] ||
                profileIdc == 9 || profileCompatibilityFlag[9] ||
                profileIdc == 10 || profileCompatibilityFlag[10] ||
                profileIdc == 11 || profileCompatibilityFlag[11])
            {
                max14BitConstraintFlag = reader.ReadBit();
                _ = reader.ReadBits(33);
            }
            else
            {
                _ = reader.ReadBits(34);
            }
        }
        else if (profileIdc == 2 || profileCompatibilityFlag[2])
        {
            _ = reader.ReadBits(7);
            onePictureOnlyConstraintFlag = reader.ReadBit();
            _ = reader.ReadBits(35);
        }
        else
        {
            _ = reader.ReadBits(43);
        }

        if (profileIdc == 1 || profileCompatibilityFlag[1] ||
            profileIdc == 2 || profileCompatibilityFlag[2] ||
            profileIdc == 3 || profileCompatibilityFlag[3] ||
            profileIdc == 4 || profileCompatibilityFlag[4] ||
            profileIdc == 5 || profileCompatibilityFlag[5] ||
            profileIdc == 9 || profileCompatibilityFlag[9] ||
            profileIdc == 11 || profileCompatibilityFlag[11])
        {
            inbldFlag = reader.ReadBit();
        }
        else
        {
            _ = reader.ReadBit();
        }

        if (layerPresentFlag)
            levelIdc = reader.ReadBits(8);

        return new SubLayerProfile(
            profileSpace,
            tierFlag,
            profileIdc,
            profileCompatibilityFlag,
            progressiveSourceFlag,
            interlacedSourceFlag,
            nonPackedConstraintFlag,
            frameOnlyConstraintFlag,
            max12BitConstraintFlag,
            max10BitConstraintFlag,
            max8BitConstraintFlag,
            max422ChromaConstraintFlag,
            max420ChromaConstraintFlag,
            maxMonochromeConstraintFlag,
            maxIntraConstraintFlag,
            onePictureOnlyConstraintFlag,
            lowerBitRateConstraintFlag,
            max14BitConstraintFlag,
            inbldFlag,
            levelIdc);
    }

    /// <summary>
    ///   Writes this sub layer profile to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the current sub layer profile is written to</param>
    /// <param name="layerPresentFlag">Layer present flag</param>
    public readonly void Write(BitStreamWriter writer, bool layerPresentFlag)
    {
        writer.WriteBits(ProfileSpace, 2);
        writer.WriteBit(TierFlag);
        writer.WriteBits(ProfileIdc, 5);
        writer.WriteBits(this.CompatibilityFlag.Flags, 32);
        writer.WriteBit(ProgressiveSourceFlag);
        writer.WriteBit(InterlacedSourceFlag);
        writer.WriteBit(NonPackedConstraintFlag);
        writer.WriteBit(FrameOnlyConstraintFlag);

        if (ProfileIdc == 4 || CompatibilityFlag[4] ||
            ProfileIdc == 5 || CompatibilityFlag[5] ||
            ProfileIdc == 6 || CompatibilityFlag[6] ||
            ProfileIdc == 7 || CompatibilityFlag[7] ||
            ProfileIdc == 8 || CompatibilityFlag[8] ||
            ProfileIdc == 9 || CompatibilityFlag[9] ||
            ProfileIdc == 10 || CompatibilityFlag[10] ||
            ProfileIdc == 11 || CompatibilityFlag[11])
        {
            writer.WriteBit(Max12BitConstraintFlag);
            writer.WriteBit(Max10BitConstraintFlag);
            writer.WriteBit(Max8BitConstraintFlag);
            writer.WriteBit(Max422ChromaConstraintFlag);
            writer.WriteBit(Max420ChromaConstraintFlag);
            writer.WriteBit(MonochromeConstraintFlag);
            writer.WriteBit(IntraConstraintFlag);
            writer.WriteBit(OnePictureOnlyConstraintFlag);
            writer.WriteBit(LowerBitRateConstraintFlag);

            if (ProfileIdc == 5 || CompatibilityFlag[5] ||
                ProfileIdc == 9 || CompatibilityFlag[9] ||
                ProfileIdc == 10 || CompatibilityFlag[10] ||
                ProfileIdc == 11 || CompatibilityFlag[11])
            {
                writer.WriteBit(Max14BitConstraintFlag);
                for (int i = 0; i < 33; i++)
                    writer.WriteBit(false);
            }
            else
            {
                for (int i = 0; i < 34; i++)
                    writer.WriteBit(false);
            }
        }
        else if (ProfileIdc == 2 || CompatibilityFlag[2])
        {
            for (int i = 0; i < 7; i++)
                writer.WriteBit(false);

            writer.WriteBit(OnePictureOnlyConstraintFlag);

            for (int i = 0; i < 35; i++)
                writer.WriteBit(false);
        }
        else
        {
            for (int i = 0; i < 43; i++)
                writer.WriteBit(false);
        }

        if (ProfileIdc == 1 || CompatibilityFlag[1] ||
            ProfileIdc == 2 || CompatibilityFlag[2] ||
            ProfileIdc == 3 || CompatibilityFlag[3] ||
            ProfileIdc == 4 || CompatibilityFlag[4] ||
            ProfileIdc == 5 || CompatibilityFlag[5] ||
            ProfileIdc == 9 || CompatibilityFlag[9] ||
            ProfileIdc == 11 || CompatibilityFlag[11])
        {
            writer.WriteBit(InbldFlag);
        }
        else
        {
            writer.WriteBit(false);
        }

        if (layerPresentFlag)
            writer.WriteBits(LevelIdc, 8);
    }

    /// <summary>
    ///   Writes this sub layer profile to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the current sub layer profile is written to</param>
    /// <param name="layerPresentFlag">Layer present flag</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, bool layerPresentFlag)
    {
        await writer.WriteBitsAsync(ProfileSpace, 2);
        await writer.WriteBitAsync(TierFlag);
        await writer.WriteBitsAsync(ProfileIdc, 5);
        await writer.WriteBitsAsync(this.CompatibilityFlag.Flags, 32);
        await writer.WriteBitAsync(ProgressiveSourceFlag);
        await writer.WriteBitAsync(InterlacedSourceFlag);
        await writer.WriteBitAsync(NonPackedConstraintFlag);
        await writer.WriteBitAsync(FrameOnlyConstraintFlag);

        if (ProfileIdc == 4 || CompatibilityFlag[4] ||
            ProfileIdc == 5 || CompatibilityFlag[5] ||
            ProfileIdc == 6 || CompatibilityFlag[6] ||
            ProfileIdc == 7 || CompatibilityFlag[7] ||
            ProfileIdc == 8 || CompatibilityFlag[8] ||
            ProfileIdc == 9 || CompatibilityFlag[9] ||
            ProfileIdc == 10 || CompatibilityFlag[10] ||
            ProfileIdc == 11 || CompatibilityFlag[11])
        {
            await writer.WriteBitAsync(Max12BitConstraintFlag);
            await writer.WriteBitAsync(Max10BitConstraintFlag);
            await writer.WriteBitAsync(Max8BitConstraintFlag);
            await writer.WriteBitAsync(Max422ChromaConstraintFlag);
            await writer.WriteBitAsync(Max420ChromaConstraintFlag);
            await writer.WriteBitAsync(MonochromeConstraintFlag);
            await writer.WriteBitAsync(IntraConstraintFlag);
            await writer.WriteBitAsync(OnePictureOnlyConstraintFlag);
            await writer.WriteBitAsync(LowerBitRateConstraintFlag);

            if (ProfileIdc == 5 || CompatibilityFlag[5] ||
                ProfileIdc == 9 || CompatibilityFlag[9] ||
                ProfileIdc == 10 || CompatibilityFlag[10] ||
                ProfileIdc == 11 || CompatibilityFlag[11])
            {
                await writer.WriteBitAsync(Max14BitConstraintFlag);
                for (int i = 0; i < 33; i++)
                    await writer.WriteBitAsync(false);
            }
            else
            {
                for (int i = 0; i < 34; i++)
                    await writer.WriteBitAsync(false);
            }
        }
        else if (ProfileIdc == 2 || CompatibilityFlag[2])
        {
            for (int i = 0; i < 7; i++)
                await writer.WriteBitAsync(false);

            await writer.WriteBitAsync(OnePictureOnlyConstraintFlag);

            for (int i = 0; i < 35; i++)
                await writer.WriteBitAsync(false);
        }
        else
        {
            for (int i = 0; i < 43; i++)
                await writer.WriteBitAsync(false);
        }

        if (ProfileIdc == 1 || CompatibilityFlag[1] ||
            ProfileIdc == 2 || CompatibilityFlag[2] ||
            ProfileIdc == 3 || CompatibilityFlag[3] ||
            ProfileIdc == 4 || CompatibilityFlag[4] ||
            ProfileIdc == 5 || CompatibilityFlag[5] ||
            ProfileIdc == 9 || CompatibilityFlag[9] ||
            ProfileIdc == 11 || CompatibilityFlag[11])
        {
            await writer.WriteBitAsync(InbldFlag);
        }
        else
        {
            await writer.WriteBitAsync(false);
        }

        if (layerPresentFlag)
            await writer.WriteBitsAsync(LevelIdc, 8);
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is SubLayerProfile profile && Equals(profile);
    }

    public readonly bool Equals(SubLayerProfile other)
    {
        return ProfileSpace == other.ProfileSpace &&
               TierFlag == other.TierFlag &&
               ProfileIdc == other.ProfileIdc &&
               CompatibilityFlag.Equals(other.CompatibilityFlag) &&
               ProgressiveSourceFlag == other.ProgressiveSourceFlag &&
               InterlacedSourceFlag == other.InterlacedSourceFlag &&
               NonPackedConstraintFlag == other.NonPackedConstraintFlag &&
               FrameOnlyConstraintFlag == other.FrameOnlyConstraintFlag &&
               Max12BitConstraintFlag == other.Max12BitConstraintFlag &&
               Max10BitConstraintFlag == other.Max10BitConstraintFlag &&
               Max8BitConstraintFlag == other.Max8BitConstraintFlag &&
               Max422ChromaConstraintFlag == other.Max422ChromaConstraintFlag &&
               Max420ChromaConstraintFlag == other.Max420ChromaConstraintFlag &&
               MonochromeConstraintFlag == other.MonochromeConstraintFlag &&
               IntraConstraintFlag == other.IntraConstraintFlag &&
               OnePictureOnlyConstraintFlag == other.OnePictureOnlyConstraintFlag &&
               LowerBitRateConstraintFlag == other.LowerBitRateConstraintFlag &&
               Max14BitConstraintFlag == other.Max14BitConstraintFlag &&
               InbldFlag == other.InbldFlag &&
               LevelIdc == other.LevelIdc;
    }

    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(ProfileSpace);
        hash.Add(TierFlag);
        hash.Add(ProfileIdc);
        hash.Add(CompatibilityFlag);
        hash.Add(ProgressiveSourceFlag);
        hash.Add(InterlacedSourceFlag);
        hash.Add(NonPackedConstraintFlag);
        hash.Add(FrameOnlyConstraintFlag);
        hash.Add(Max12BitConstraintFlag);
        hash.Add(Max10BitConstraintFlag);
        hash.Add(Max8BitConstraintFlag);
        hash.Add(Max422ChromaConstraintFlag);
        hash.Add(Max420ChromaConstraintFlag);
        hash.Add(MonochromeConstraintFlag);
        hash.Add(IntraConstraintFlag);
        hash.Add(OnePictureOnlyConstraintFlag);
        hash.Add(LowerBitRateConstraintFlag);
        hash.Add(Max14BitConstraintFlag);
        hash.Add(InbldFlag);
        hash.Add(LevelIdc);
        return hash.ToHashCode();
    }

    public static bool operator ==(SubLayerProfile left, SubLayerProfile right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SubLayerProfile left, SubLayerProfile right)
    {
        return !(left == right);
    }
}
