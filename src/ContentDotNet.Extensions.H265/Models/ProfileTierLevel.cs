using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H265.Models;

/// <summary>
///   Represents H.265 Profile, Tier and Level.
/// </summary>
public struct ProfileTierLevel : IEquatable<ProfileTierLevel>
{
    /// <summary>
    ///   Specifies the general profile space (2 bits) for the H.265 stream.
    /// </summary>
    public uint GeneralProfileSpace;

    /// <summary>
    ///   Indicates the general tier flag for the H.265 stream.
    /// </summary>
    public bool GeneralTierFlag;

    /// <summary>
    ///   Specifies the general profile IDC (5 bits) for the H.265 stream.
    /// </summary>
    public uint GeneralProfileIdc;

    /// <summary>
    ///   Contains the general profile compatibility flags (32 bits).
    /// </summary>
    public PackedFlags32 GeneralProfileCompatibilityFlag;

    /// <summary>
    ///   Indicates if the general progressive source flag is set.
    /// </summary>
    public bool GeneralProgressiveSourceFlag;

    /// <summary>
    ///   Indicates if the general interlaced source flag is set.
    /// </summary>
    public bool GeneralInterlacedSourceFlag;

    /// <summary>
    ///   Indicates if the general non-packed constraint flag is set.
    /// </summary>
    public bool GeneralNonPackedConstraintFlag;

    /// <summary>
    ///   Indicates if the general frame-only constraint flag is set.
    /// </summary>
    public bool GeneralFrameOnlyConstraintFlag;

    /// <summary>
    ///   Indicates if the general max 12-bit constraint flag is set.
    /// </summary>
    public bool GeneralMax12BitConstraintFlag;

    /// <summary>
    ///   Indicates if the general max 10-bit constraint flag is set.
    /// </summary>
    public bool GeneralMax10BitConstraintFlag;

    /// <summary>
    ///   Indicates if the general max 8-bit constraint flag is set.
    /// </summary>
    public bool GeneralMax8BitConstraintFlag;

    /// <summary>
    ///   Indicates if the general max 4:2:2 chroma constraint flag is set.
    /// </summary>
    public bool GeneralMax422ChromaConstraintFlag;

    /// <summary>
    ///   Indicates if the general max 4:2:0 chroma constraint flag is set.
    /// </summary>
    public bool GeneralMax420ChromaConstraintFlag;

    /// <summary>
    ///   Indicates if the general monochrome constraint flag is set.
    /// </summary>
    public bool GeneralMonochromeConstraintFlag;

    /// <summary>
    ///   Indicates if the general intra constraint flag is set.
    /// </summary>
    public bool GeneralIntraConstraintFlag;

    /// <summary>
    ///   Indicates if the general one picture only constraint flag is set.
    /// </summary>
    public bool GeneralOnePictureOnlyConstraintFlag;

    /// <summary>
    ///   Indicates if the general lower bit rate constraint flag is set.
    /// </summary>
    public bool GeneralLowerBitRateConstraintFlag;

    /// <summary>
    ///   Indicates if the general max 14-bit constraint flag is set.
    /// </summary>
    public bool GeneralMax14BitConstraintFlag;

    /// <summary>
    ///   Indicates if the general inbld flag is set.
    /// </summary>
    public bool GeneralInbldFlag;

    /// <summary>
    ///   Specifies the general level IDC (8 bits) for the H.265 stream.
    /// </summary>
    public uint GeneralLevelIdc;

    /// <summary>
    ///   Contains the sub-layer profile present flags (up to 8 layers).
    /// </summary>
    public PackedFlags32 SubLayerProfilePresentFlag;

    /// <summary>
    ///   Contains the sub-layer level present flags (up to 8 layers).
    /// </summary>
    public PackedFlags32 SubLayerLevelPresentFlag;

    /// <summary>
    ///   Contains the sub-layer profile space values for up to 8 sub-layers.
    /// </summary>
    public Container8UInt32 SubLayerProfileSpace;

    /// <summary>
    ///   Contains the sub-layer tier flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerTierFlag;

    /// <summary>
    ///   Contains the sub-layer profile IDC values for up to 8 sub-layers.
    /// </summary>
    public Container8UInt32 SubLayerProfileIdc;

    /// <summary>
    ///   Contains the sub-layer profile compatibility flags for up to 8 sub-layers.
    /// </summary>
    public Container8PackedFlags32 SubLayerProfileCompatibilityFlag;

    /// <summary>
    ///   Contains the sub-layer progressive source flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerProgressiveSourceFlag;

    /// <summary>
    ///   Contains the sub-layer interlaced source flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerInterlacedSourceFlag;

    /// <summary>
    ///   Contains the sub-layer non-packed constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerNonPackedConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer frame-only constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerFrameOnlyConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max 12-bit constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMax12BitConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max 10-bit constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMax10BitConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max 8-bit constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMax8BitConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max 4:2:2 chroma constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMax422ChromaConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max 4:2:0 chroma constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMax420ChromaConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max monochrome constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMaxMonochromeConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer intra constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerIntraConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer one picture only constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerOnePictureOnlyConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer lower bit rate constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerLowerBitRateConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer max 14-bit constraint flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerMax14BitConstraintFlag;

    /// <summary>
    ///   Contains the sub-layer inbld flags for up to 8 sub-layers.
    /// </summary>
    public Container8Boolean SubLayerInbldFlag;

    /// <summary>
    ///   Contains the sub-layer level IDC values for up to 8 sub-layers.
    /// </summary>
    public Container8UInt32 SubLayerLevelIdc;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ProfileTierLevel"/> structure.
    /// </summary>
    /// <param name="generalProfileSpace"></param>
    /// <param name="generalTierFlag"></param>
    /// <param name="generalProfileIdc"></param>
    /// <param name="generalProfileCompatibilityFlag"></param>
    /// <param name="generalProgressiveSourceFlag"></param>
    /// <param name="generalInterlacedSourceFlag"></param>
    /// <param name="generalNonPackedConstraintFlag"></param>
    /// <param name="generalFrameOnlyConstraintFlag"></param>
    /// <param name="generalMax12BitConstraintFlag"></param>
    /// <param name="generalMax10BitConstraintFlag"></param>
    /// <param name="generalMax8BitConstraintFlag"></param>
    /// <param name="generalMax422ChromaConstraintFlag"></param>
    /// <param name="generalMax420ChromaConstraintFlag"></param>
    /// <param name="generalMonochromeConstraintFlag"></param>
    /// <param name="generalIntraConstraintFlag"></param>
    /// <param name="generalOnePictureOnlyConstraintFlag"></param>
    /// <param name="generalLowerBitRateConstraintFlag"></param>
    /// <param name="generalMax14BitConstraintFlag"></param>
    /// <param name="generalInbldFlag"></param>
    /// <param name="generalLevelIdc"></param>
    /// <param name="subLayerProfilePresentFlag"></param>
    /// <param name="subLayerLevelPresentFlag"></param>
    /// <param name="subLayerProfileSpace"></param>
    /// <param name="subLayerTierFlag"></param>
    /// <param name="subLayerProfileIdc"></param>
    /// <param name="subLayerProfileCompatibilityFlag"></param>
    /// <param name="subLayerProgressiveSourceFlag"></param>
    /// <param name="subLayerInterlacedSourceFlag"></param>
    /// <param name="subLayerNonPackedConstraintFlag"></param>
    /// <param name="subLayerFrameOnlyConstraintFlag"></param>
    /// <param name="subLayerMax12BitConstraintFlag"></param>
    /// <param name="subLayerMax10BitConstraintFlag"></param>
    /// <param name="subLayerMax8BitConstraintFlag"></param>
    /// <param name="subLayerMax422ChromaConstraintFlag"></param>
    /// <param name="subLayerMax420ChromaConstraintFlag"></param>
    /// <param name="subLayerMaxMonochromeConstraintFlag"></param>
    /// <param name="subLayerIntraConstraintFlag"></param>
    /// <param name="subLayerOnePictureOnlyConstraintFlag"></param>
    /// <param name="subLayerLowerBitRateConstraintFlag"></param>
    /// <param name="subLayerMax14BitConstraintFlag"></param>
    /// <param name="subLayerInbldFlag"></param>
    /// <param name="subLayerLevelIdc"></param>
    public ProfileTierLevel(uint generalProfileSpace, bool generalTierFlag, uint generalProfileIdc, PackedFlags32 generalProfileCompatibilityFlag, bool generalProgressiveSourceFlag, bool generalInterlacedSourceFlag, bool generalNonPackedConstraintFlag, bool generalFrameOnlyConstraintFlag, bool generalMax12BitConstraintFlag, bool generalMax10BitConstraintFlag, bool generalMax8BitConstraintFlag, bool generalMax422ChromaConstraintFlag, bool generalMax420ChromaConstraintFlag, bool generalMonochromeConstraintFlag, bool generalIntraConstraintFlag, bool generalOnePictureOnlyConstraintFlag, bool generalLowerBitRateConstraintFlag, bool generalMax14BitConstraintFlag, bool generalInbldFlag, uint generalLevelIdc, PackedFlags32 subLayerProfilePresentFlag, PackedFlags32 subLayerLevelPresentFlag, Container8UInt32 subLayerProfileSpace, Container8Boolean subLayerTierFlag, Container8UInt32 subLayerProfileIdc, Container8PackedFlags32 subLayerProfileCompatibilityFlag, Container8Boolean subLayerProgressiveSourceFlag, Container8Boolean subLayerInterlacedSourceFlag, Container8Boolean subLayerNonPackedConstraintFlag, Container8Boolean subLayerFrameOnlyConstraintFlag, Container8Boolean subLayerMax12BitConstraintFlag, Container8Boolean subLayerMax10BitConstraintFlag, Container8Boolean subLayerMax8BitConstraintFlag, Container8Boolean subLayerMax422ChromaConstraintFlag, Container8Boolean subLayerMax420ChromaConstraintFlag, Container8Boolean subLayerMaxMonochromeConstraintFlag, Container8Boolean subLayerIntraConstraintFlag, Container8Boolean subLayerOnePictureOnlyConstraintFlag, Container8Boolean subLayerLowerBitRateConstraintFlag, Container8Boolean subLayerMax14BitConstraintFlag, Container8Boolean subLayerInbldFlag, Container8UInt32 subLayerLevelIdc)
    {
        GeneralProfileSpace = generalProfileSpace;
        GeneralTierFlag = generalTierFlag;
        GeneralProfileIdc = generalProfileIdc;
        GeneralProfileCompatibilityFlag = generalProfileCompatibilityFlag;
        GeneralProgressiveSourceFlag = generalProgressiveSourceFlag;
        GeneralInterlacedSourceFlag = generalInterlacedSourceFlag;
        GeneralNonPackedConstraintFlag = generalNonPackedConstraintFlag;
        GeneralFrameOnlyConstraintFlag = generalFrameOnlyConstraintFlag;
        GeneralMax12BitConstraintFlag = generalMax12BitConstraintFlag;
        GeneralMax10BitConstraintFlag = generalMax10BitConstraintFlag;
        GeneralMax8BitConstraintFlag = generalMax8BitConstraintFlag;
        GeneralMax422ChromaConstraintFlag = generalMax422ChromaConstraintFlag;
        GeneralMax420ChromaConstraintFlag = generalMax420ChromaConstraintFlag;
        GeneralMonochromeConstraintFlag = generalMonochromeConstraintFlag;
        GeneralIntraConstraintFlag = generalIntraConstraintFlag;
        GeneralOnePictureOnlyConstraintFlag = generalOnePictureOnlyConstraintFlag;
        GeneralLowerBitRateConstraintFlag = generalLowerBitRateConstraintFlag;
        GeneralMax14BitConstraintFlag = generalMax14BitConstraintFlag;
        GeneralInbldFlag = generalInbldFlag;
        GeneralLevelIdc = generalLevelIdc;
        SubLayerProfilePresentFlag = subLayerProfilePresentFlag;
        SubLayerLevelPresentFlag = subLayerLevelPresentFlag;
        SubLayerProfileSpace = subLayerProfileSpace;
        SubLayerTierFlag = subLayerTierFlag;
        SubLayerProfileIdc = subLayerProfileIdc;
        SubLayerProfileCompatibilityFlag = subLayerProfileCompatibilityFlag;
        SubLayerProgressiveSourceFlag = subLayerProgressiveSourceFlag;
        SubLayerInterlacedSourceFlag = subLayerInterlacedSourceFlag;
        SubLayerNonPackedConstraintFlag = subLayerNonPackedConstraintFlag;
        SubLayerFrameOnlyConstraintFlag = subLayerFrameOnlyConstraintFlag;
        SubLayerMax12BitConstraintFlag = subLayerMax12BitConstraintFlag;
        SubLayerMax10BitConstraintFlag = subLayerMax10BitConstraintFlag;
        SubLayerMax8BitConstraintFlag = subLayerMax8BitConstraintFlag;
        SubLayerMax422ChromaConstraintFlag = subLayerMax422ChromaConstraintFlag;
        SubLayerMax420ChromaConstraintFlag = subLayerMax420ChromaConstraintFlag;
        SubLayerMaxMonochromeConstraintFlag = subLayerMaxMonochromeConstraintFlag;
        SubLayerIntraConstraintFlag = subLayerIntraConstraintFlag;
        SubLayerOnePictureOnlyConstraintFlag = subLayerOnePictureOnlyConstraintFlag;
        SubLayerLowerBitRateConstraintFlag = subLayerLowerBitRateConstraintFlag;
        SubLayerMax14BitConstraintFlag = subLayerMax14BitConstraintFlag;
        SubLayerInbldFlag = subLayerInbldFlag;
        SubLayerLevelIdc = subLayerLevelIdc;
    }

    public static ProfileTierLevel Read(BitStreamReader reader, bool profilePresentFlag, int maxNumSubLayersMinus1)
    {
        uint generalProfileSpace = 0u;
        bool generalTierFlag = false;
        uint generalProfileIdc = 0u;
        PackedFlags32 generalProfileCompatibilityFlag = default;
        bool generalProgressiveSourceFlag = false;
        bool generalInterlacedSourceFlag = false;
        bool generalNonPackedConstraintFlag = false;
        bool generalFrameOnlyConstraintFlag = false;
        bool generalMax12BitConstraintFlag = false;
        bool generalMax10BitConstraintFlag = false;
        bool generalMax8BitConstraintFlag = false;
        bool generalMax422ChromaConstraintFlag = false;
        bool generalMax420ChromaConstraintFlag = false;
        bool generalMaxMonochromeConstraintFlag = false;
        bool generalIntraConstraintFlag = false;
        bool generalOnePictureOnlyConstraintFlag = false;
        bool generalLowerBitRateConstraintFlag = false;
        bool generalMax14BitConstraintFlag = false;
        bool generalInbldFlag = false;
        uint generalLevelIdc;
        PackedFlags32 subLayerProfilePresentFlag = default;
        PackedFlags32 subLayerLevelPresentFlag = default;
        Container8UInt32 subLayerProfileSpace = default;
        Container8Boolean subLayerTierFlag = default;
        Container8UInt32 subLayerProfileIdc = default;
        Container8PackedFlags32 subLayerProfileCompatibilityFlag = default;
        Container8Boolean subLayerProgressiveSourceFlag = default;
        Container8Boolean subLayerInterlacedSourceFlag = default;
        Container8Boolean subLayerNonPackedConstraintFlag = default;
        Container8Boolean subLayerFrameOnlyConstraintFlag = default;
        Container8Boolean subLayerMax12BitConstraintFlag = default;
        Container8Boolean subLayerMax10BitConstraintFlag = default;
        Container8Boolean subLayerMax8BitConstraintFlag = default;
        Container8Boolean subLayerMax422ChromaConstraintFlag = default;
        Container8Boolean subLayerMax420ChromaConstraintFlag = default;
        Container8Boolean subLayerMaxMonochromeConstraintFlag = default;
        Container8Boolean subLayerIntraConstraintFlag = default;
        Container8Boolean subLayerOnePictureOnlyConstraintFlag = default;
        Container8Boolean subLayerLowerBitRateConstraintFlag = default;
        Container8Boolean subLayerMax14BitConstraintFlag = default;
        Container8Boolean subLayerInbldFlag = default;
        Container8UInt32 subLayerLevelIdc = default;

        if (profilePresentFlag)
        {
            generalProfileSpace = reader.ReadBits(2);
            generalTierFlag = reader.ReadBit();
            generalProfileIdc = reader.ReadBits(5);
            for (int i = 0; i < 32; i++)
                generalProfileCompatibilityFlag[i] = reader.ReadBit();
            generalProgressiveSourceFlag = reader.ReadBit();
            generalInterlacedSourceFlag = reader.ReadBit();
            generalNonPackedConstraintFlag = reader.ReadBit();
            generalFrameOnlyConstraintFlag = reader.ReadBit();
            if (generalProfileIdc == 4 || generalProfileCompatibilityFlag[4] ||
                generalProfileIdc == 5 || generalProfileCompatibilityFlag[5] ||
                generalProfileIdc == 6 || generalProfileCompatibilityFlag[6] ||
                generalProfileIdc == 7 || generalProfileCompatibilityFlag[7] ||
                generalProfileIdc == 8 || generalProfileCompatibilityFlag[8] ||
                generalProfileIdc == 9 || generalProfileCompatibilityFlag[9] ||
                generalProfileIdc == 10 || generalProfileCompatibilityFlag[10] ||
                generalProfileIdc == 11 || generalProfileCompatibilityFlag[11])
            {
                generalMax12BitConstraintFlag = reader.ReadBit();
                generalMax10BitConstraintFlag = reader.ReadBit();
                generalMax8BitConstraintFlag = reader.ReadBit();
                generalMax422ChromaConstraintFlag = reader.ReadBit();
                generalMax420ChromaConstraintFlag = reader.ReadBit();
                generalMaxMonochromeConstraintFlag = reader.ReadBit();
                generalIntraConstraintFlag = reader.ReadBit();
                generalOnePictureOnlyConstraintFlag = reader.ReadBit();
                generalLowerBitRateConstraintFlag = reader.ReadBit();
                if (generalProfileIdc == 5 || generalProfileCompatibilityFlag[5] ||
                    generalProfileIdc == 9 || generalProfileCompatibilityFlag[9] ||
                    generalProfileIdc == 10 || generalProfileCompatibilityFlag[10] ||
                    generalProfileIdc == 11 || generalProfileCompatibilityFlag[11])
                {
                    generalMax14BitConstraintFlag = reader.ReadBit();
                    _ = reader.ReadBits(33);
                }
                else
                {
                    _ = reader.ReadBits(34);
                }
            }
            else if (generalProfileIdc == 2 || generalProfileCompatibilityFlag[2])
            {
                _ = reader.ReadBits(7);
                generalOnePictureOnlyConstraintFlag = reader.ReadBit();
                _ = reader.ReadBits(35);
            }
            else
            {
                _ = reader.ReadBits(43);
            }

            if (generalProfileIdc == 1 || generalProfileCompatibilityFlag[1] ||
                generalProfileIdc == 2 || generalProfileCompatibilityFlag[2] ||
                generalProfileIdc == 3 || generalProfileCompatibilityFlag[3] ||
                generalProfileIdc == 4 || generalProfileCompatibilityFlag[4] ||
                generalProfileIdc == 5 || generalProfileCompatibilityFlag[5] ||
                generalProfileIdc == 9 || generalProfileCompatibilityFlag[9] ||
                generalProfileIdc == 11 || generalProfileCompatibilityFlag[11])
            {
                generalInbldFlag = reader.ReadBit();
            }
            else
            {
                _ = reader.ReadBit();
            }
        }

        generalLevelIdc = reader.ReadBits(8);

        for (int i = 0; i < maxNumSubLayersMinus1; i++)
        {
            subLayerProfilePresentFlag[i] = reader.ReadBit();
            subLayerLevelPresentFlag[i] = reader.ReadBit();
        }

        if (maxNumSubLayersMinus1 > 0)
            for (int i = maxNumSubLayersMinus1; i < 8; i++)
                _ = reader.ReadBits(2);

        for (int i = 0; i < maxNumSubLayersMinus1; i++)
        {
            if (subLayerProfilePresentFlag[i])
            {
                subLayerProfileSpace[i] = reader.ReadBits(2);
                subLayerTierFlag[i] = reader.ReadBit();
                subLayerProfileIdc[i] = reader.ReadBits(5);
                for (int j = 0; j < 32; j++)
                {
                    PackedFlags32 flags = subLayerProfileCompatibilityFlag[i];
                    flags[j] = reader.ReadBit();
                    subLayerProfileCompatibilityFlag[i] = flags;
                }
                subLayerProgressiveSourceFlag[i] = reader.ReadBit();
                subLayerInterlacedSourceFlag[i] = reader.ReadBit();
                subLayerNonPackedConstraintFlag[i] = reader.ReadBit();
                subLayerFrameOnlyConstraintFlag[i] = reader.ReadBit();
                if (subLayerProfileIdc[i] == 4 ||
                    subLayerProfileCompatibilityFlag[i][4] ||
                    subLayerProfileIdc[i] == 5 ||
                    subLayerProfileCompatibilityFlag[i][5] ||
                    subLayerProfileIdc[i] == 6 ||
                    subLayerProfileCompatibilityFlag[i][6] ||
                    subLayerProfileIdc[i] == 7 ||
                    subLayerProfileCompatibilityFlag[i][7] ||
                    subLayerProfileIdc[i] == 8 ||
                    subLayerProfileCompatibilityFlag[i][8] ||
                    subLayerProfileIdc[i] == 9 ||
                    subLayerProfileCompatibilityFlag[i][9] ||
                    subLayerProfileIdc[i] == 10 ||
                    subLayerProfileCompatibilityFlag[i][10] ||
                    subLayerProfileIdc[i] == 11 ||
                    subLayerProfileCompatibilityFlag[i][11])
                {
                    subLayerMax12BitConstraintFlag[i] = reader.ReadBit();
                    subLayerMax10BitConstraintFlag[i] = reader.ReadBit();
                    subLayerMax8BitConstraintFlag[i] = reader.ReadBit();
                    subLayerMax422ChromaConstraintFlag[i] = reader.ReadBit();
                    subLayerMax420ChromaConstraintFlag[i] = reader.ReadBit();
                    subLayerMaxMonochromeConstraintFlag[i] = reader.ReadBit();
                    subLayerIntraConstraintFlag[i] = reader.ReadBit();
                    subLayerOnePictureOnlyConstraintFlag[i] = reader.ReadBit();
                    subLayerLowerBitRateConstraintFlag[i] = reader.ReadBit();
                    if (subLayerProfileIdc[i] == 5 ||
                        subLayerProfileCompatibilityFlag[i][5] ||
                        subLayerProfileIdc[i] == 9 ||
                        subLayerProfileCompatibilityFlag[i][9] ||
                        subLayerProfileIdc[i] == 10 ||
                        subLayerProfileCompatibilityFlag[i][10] ||
                        subLayerProfileIdc[i] == 11 ||
                        subLayerProfileCompatibilityFlag[i][11])
                    {
                        subLayerMax14BitConstraintFlag[i] = reader.ReadBit();
                        _ = reader.ReadBits(33);
                    }
                    else
                    {
                        _ = reader.ReadBits(34);
                    }
                }
                else if (subLayerProfileIdc[i] == 2 || subLayerProfileCompatibilityFlag[i][2])
                {
                    _ = reader.ReadBits(7);
                    subLayerOnePictureOnlyConstraintFlag[i] = reader.ReadBit();
                    _ = reader.ReadBits(35);
                }
                else
                {
                    _ = reader.ReadBits(43);
                }

                if (subLayerProfileIdc[i] == 1 ||
                    subLayerProfileCompatibilityFlag[i][1] ||
                    subLayerProfileIdc[i] == 2 ||
                    subLayerProfileCompatibilityFlag[i][2] ||
                    subLayerProfileIdc[i] == 3 ||
                    subLayerProfileCompatibilityFlag[i][3] ||
                    subLayerProfileIdc[i] == 4 ||
                    subLayerProfileCompatibilityFlag[i][4] ||
                    subLayerProfileIdc[i] == 5 ||
                    subLayerProfileCompatibilityFlag[i][5] ||
                    subLayerProfileIdc[i] == 9 ||
                    subLayerProfileCompatibilityFlag[i][9] ||
                    subLayerProfileIdc[i] == 11 ||
                    subLayerProfileCompatibilityFlag[i][11])
                {
                    subLayerInbldFlag[i] = reader.ReadBit();
                }
                else
                {
                    _ = reader.ReadBit();
                }
            }

            if (subLayerLevelPresentFlag[i])
                subLayerLevelIdc[i] = reader.ReadBits(8);
        }

        return new ProfileTierLevel(
            generalProfileSpace,
            generalTierFlag,
            generalProfileIdc,
            generalProfileCompatibilityFlag,
            generalProgressiveSourceFlag,
            generalInterlacedSourceFlag,
            generalNonPackedConstraintFlag,
            generalFrameOnlyConstraintFlag,
            generalMax12BitConstraintFlag,
            generalMax10BitConstraintFlag,
            generalMax8BitConstraintFlag,
            generalMax422ChromaConstraintFlag,
            generalMax420ChromaConstraintFlag,
            generalMaxMonochromeConstraintFlag,
            generalIntraConstraintFlag,
            generalOnePictureOnlyConstraintFlag,
            generalLowerBitRateConstraintFlag,
            generalMax14BitConstraintFlag,
            generalInbldFlag,
            generalLevelIdc,
            subLayerProfilePresentFlag,
            subLayerLevelPresentFlag,
            subLayerProfileSpace,
            subLayerTierFlag,
            subLayerProfileIdc,
            subLayerProfileCompatibilityFlag,
            subLayerProgressiveSourceFlag,
            subLayerInterlacedSourceFlag,
            subLayerNonPackedConstraintFlag,
            subLayerFrameOnlyConstraintFlag,
            subLayerMax12BitConstraintFlag,
            subLayerMax10BitConstraintFlag,
            subLayerMax8BitConstraintFlag,
            subLayerMax422ChromaConstraintFlag,
            subLayerMax420ChromaConstraintFlag,
            subLayerMaxMonochromeConstraintFlag,
            subLayerIntraConstraintFlag,
            subLayerOnePictureOnlyConstraintFlag,
            subLayerLowerBitRateConstraintFlag,
            subLayerMax14BitConstraintFlag,
            subLayerInbldFlag,
            subLayerLevelIdc);
    }

    public readonly void Write(BitStreamWriter writer, bool profilePresentFlag, int maxNumSubLayersMinus1)
    {
        if (profilePresentFlag)
        {
            writer.WriteBits(GeneralProfileSpace, 2);
            writer.WriteBit(GeneralTierFlag);
            writer.WriteBits(GeneralProfileIdc, 5);
            for (int i = 0; i < 32; i++)
                writer.WriteBit(GeneralProfileCompatibilityFlag[i]);
            writer.WriteBit(GeneralProgressiveSourceFlag);
            writer.WriteBit(GeneralInterlacedSourceFlag);
            writer.WriteBit(GeneralNonPackedConstraintFlag);
            writer.WriteBit(GeneralFrameOnlyConstraintFlag);
            if (GeneralProfileIdc == 4 || GeneralProfileCompatibilityFlag[4] ||
                GeneralProfileIdc == 5 || GeneralProfileCompatibilityFlag[5] ||
                GeneralProfileIdc == 6 || GeneralProfileCompatibilityFlag[6] ||
                GeneralProfileIdc == 7 || GeneralProfileCompatibilityFlag[7] ||
                GeneralProfileIdc == 8 || GeneralProfileCompatibilityFlag[8] ||
                GeneralProfileIdc == 9 || GeneralProfileCompatibilityFlag[9] ||
                GeneralProfileIdc == 10 || GeneralProfileCompatibilityFlag[10] ||
                GeneralProfileIdc == 11 || GeneralProfileCompatibilityFlag[11])
            {
                writer.WriteBit(GeneralMax12BitConstraintFlag);
                writer.WriteBit(GeneralMax10BitConstraintFlag);
                writer.WriteBit(GeneralMax8BitConstraintFlag);
                writer.WriteBit(GeneralMax422ChromaConstraintFlag);
                writer.WriteBit(GeneralMax420ChromaConstraintFlag);
                writer.WriteBit(GeneralMonochromeConstraintFlag);
                writer.WriteBit(GeneralIntraConstraintFlag);
                writer.WriteBit(GeneralOnePictureOnlyConstraintFlag);
                writer.WriteBit(GeneralLowerBitRateConstraintFlag);
                if (GeneralProfileIdc == 5 || GeneralProfileCompatibilityFlag[5] ||
                    GeneralProfileIdc == 9 || GeneralProfileCompatibilityFlag[9] ||
                    GeneralProfileIdc == 10 || GeneralProfileCompatibilityFlag[10] ||
                    GeneralProfileIdc == 11 || GeneralProfileCompatibilityFlag[11])
                {
                    writer.WriteBit(GeneralMax14BitConstraintFlag);
                    writer.WriteBits(0, 33);
                }
                else
                {
                    writer.WriteBits(0, 34);
                }
            }
            else if (GeneralProfileIdc == 2 || GeneralProfileCompatibilityFlag[2])
            {
                writer.WriteBits(0, 7);
                writer.WriteBit(GeneralOnePictureOnlyConstraintFlag);
                writer.WriteBits(0, 35);
            }
            else
            {
                writer.WriteBits(0, 43);
            }

            if (GeneralProfileIdc == 1 || GeneralProfileCompatibilityFlag[1] ||
                GeneralProfileIdc == 2 || GeneralProfileCompatibilityFlag[2] ||
                GeneralProfileIdc == 3 || GeneralProfileCompatibilityFlag[3] ||
                GeneralProfileIdc == 4 || GeneralProfileCompatibilityFlag[4] ||
                GeneralProfileIdc == 5 || GeneralProfileCompatibilityFlag[5] ||
                GeneralProfileIdc == 11 || GeneralProfileCompatibilityFlag[11])
            {
                writer.WriteBit(GeneralInbldFlag);
            }
            else
            {
                writer.WriteBit(false);
            }
        }

        writer.WriteBits(GeneralLevelIdc, 8);

        for (int i = 0; i < maxNumSubLayersMinus1; i++)
        {
            writer.WriteBit(SubLayerProfilePresentFlag[i]);
            writer.WriteBit(SubLayerLevelPresentFlag[i]);
        }

        if (maxNumSubLayersMinus1 > 0)
            for (int i = maxNumSubLayersMinus1; i < 8; i++)
                writer.WriteBits(0, 2);

        for (int i = 0; i < maxNumSubLayersMinus1; i++)
        {
            if (SubLayerProfilePresentFlag[i])
            {
                writer.WriteBits(SubLayerProfileSpace[i], 2);
                writer.WriteBit(SubLayerTierFlag[i]);
                writer.WriteBits(SubLayerProfileIdc[i], 5);
                for (int j = 0; j < 32; j++)
                    writer.WriteBit(SubLayerProfileCompatibilityFlag[i][j]);
                writer.WriteBit(SubLayerProgressiveSourceFlag[i]);
                writer.WriteBit(SubLayerInterlacedSourceFlag[i]);
                writer.WriteBit(SubLayerNonPackedConstraintFlag[i]);
                writer.WriteBit(SubLayerFrameOnlyConstraintFlag[i]);
                if (SubLayerProfileIdc[i] == 4 ||
                    SubLayerProfileCompatibilityFlag[i][4] ||
                    SubLayerProfileIdc[i] == 5 ||
                    SubLayerProfileCompatibilityFlag[i][5] ||
                    SubLayerProfileIdc[i] == 6 ||
                    SubLayerProfileCompatibilityFlag[i][6] ||
                    SubLayerProfileIdc[i] == 7 ||
                    SubLayerProfileCompatibilityFlag[i][7] ||
                    SubLayerProfileIdc[i] == 8 ||
                    SubLayerProfileCompatibilityFlag[i][8] ||
                    SubLayerProfileIdc[i] == 9 ||
                    SubLayerProfileCompatibilityFlag[i][9] ||
                    SubLayerProfileIdc[i] == 10 ||
                    SubLayerProfileCompatibilityFlag[i][10] ||
                    SubLayerProfileIdc[i] == 11 ||
                    SubLayerProfileCompatibilityFlag[i][11])
                {
                    writer.WriteBit(SubLayerMax12BitConstraintFlag[i]);
                    writer.WriteBit(SubLayerMax10BitConstraintFlag[i]);
                    writer.WriteBit(SubLayerMax8BitConstraintFlag[i]);
                    writer.WriteBit(SubLayerMax422ChromaConstraintFlag[i]);
                    writer.WriteBit(SubLayerMax420ChromaConstraintFlag[i]);
                    writer.WriteBit(SubLayerMaxMonochromeConstraintFlag[i]);
                    writer.WriteBit(SubLayerIntraConstraintFlag[i]);
                    writer.WriteBit(SubLayerOnePictureOnlyConstraintFlag[i]);
                    writer.WriteBit(SubLayerLowerBitRateConstraintFlag[i]);

                    if (SubLayerProfileIdc[i] == 5 ||
                        SubLayerProfileCompatibilityFlag[i][5] ||
                        SubLayerProfileIdc[i] == 9 ||
                        SubLayerProfileCompatibilityFlag[i][9] ||
                        SubLayerProfileIdc[i] == 10 ||
                        SubLayerProfileCompatibilityFlag[i][10] ||
                        SubLayerProfileIdc[i] == 11 ||
                        SubLayerProfileCompatibilityFlag[i][11])
                    {
                        writer.WriteBit(SubLayerMax14BitConstraintFlag[i]);
                        writer.WriteBits(0, 33);
                    }
                    else
                    {
                        writer.WriteBits(0, 34);
                    }
                }
                else if (SubLayerProfileIdc[i] == 2 || SubLayerProfileCompatibilityFlag[i][2])
                {
                    writer.WriteBits(0, 7);
                    writer.WriteBit(SubLayerOnePictureOnlyConstraintFlag[i]);
                    writer.WriteBits(0, 35);
                }
                else
                {
                    writer.WriteBits(0, 43);
                }

                if (SubLayerProfileIdc[i] == 1 ||
                    SubLayerProfileCompatibilityFlag[i][1] ||
                    SubLayerProfileIdc[i] == 2 ||
                    SubLayerProfileCompatibilityFlag[i][2] ||
                    SubLayerProfileIdc[i] == 3 ||
                    SubLayerProfileCompatibilityFlag[i][3] ||
                    SubLayerProfileIdc[i] == 4 ||
                    SubLayerProfileCompatibilityFlag[i][4] ||
                    SubLayerProfileIdc[i] == 5 ||
                    SubLayerProfileCompatibilityFlag[i][5] ||
                    SubLayerProfileIdc[i] == 9 ||
                    SubLayerProfileCompatibilityFlag[i][9] ||
                    SubLayerProfileIdc[i] == 11 ||
                    SubLayerProfileCompatibilityFlag[i][11])
                {
                    writer.WriteBit(SubLayerInbldFlag[i]);
                }
                else
                {
                    writer.WriteBit(false);
                }
            }

            if (SubLayerLevelPresentFlag[i])
                writer.WriteBits(SubLayerProfileIdc[i], 8);
        }
    }

    public readonly async Task WriteAsync(BitStreamWriter writer, bool profilePresentFlag, int maxNumSubLayersMinus1)
    {
        if (profilePresentFlag)
        {
            await writer.WriteBitsAsync(GeneralProfileSpace, 2);
            await writer.WriteBitAsync(GeneralTierFlag);
            await writer.WriteBitsAsync(GeneralProfileIdc, 5);
            for (int i = 0; i < 32; i++)
                await writer.WriteBitAsync(GeneralProfileCompatibilityFlag[i]);
            await writer.WriteBitAsync(GeneralProgressiveSourceFlag);
            await writer.WriteBitAsync(GeneralInterlacedSourceFlag);
            await writer.WriteBitAsync(GeneralNonPackedConstraintFlag);
            await writer.WriteBitAsync(GeneralFrameOnlyConstraintFlag);
            if (GeneralProfileIdc == 4 || GeneralProfileCompatibilityFlag[4] ||
                GeneralProfileIdc == 5 || GeneralProfileCompatibilityFlag[5] ||
                GeneralProfileIdc == 6 || GeneralProfileCompatibilityFlag[6] ||
                GeneralProfileIdc == 7 || GeneralProfileCompatibilityFlag[7] ||
                GeneralProfileIdc == 8 || GeneralProfileCompatibilityFlag[8] ||
                GeneralProfileIdc == 9 || GeneralProfileCompatibilityFlag[9] ||
                GeneralProfileIdc == 10 || GeneralProfileCompatibilityFlag[10] ||
                GeneralProfileIdc == 11 || GeneralProfileCompatibilityFlag[11])
            {
                await writer.WriteBitAsync(GeneralMax12BitConstraintFlag);
                await writer.WriteBitAsync(GeneralMax10BitConstraintFlag);
                await writer.WriteBitAsync(GeneralMax8BitConstraintFlag);
                await writer.WriteBitAsync(GeneralMax422ChromaConstraintFlag);
                await writer.WriteBitAsync(GeneralMax420ChromaConstraintFlag);
                await writer.WriteBitAsync(GeneralMonochromeConstraintFlag);
                await writer.WriteBitAsync(GeneralIntraConstraintFlag);
                await writer.WriteBitAsync(GeneralOnePictureOnlyConstraintFlag);
                await writer.WriteBitAsync(GeneralLowerBitRateConstraintFlag);
                if (GeneralProfileIdc == 5 || GeneralProfileCompatibilityFlag[5] ||
                    GeneralProfileIdc == 9 || GeneralProfileCompatibilityFlag[9] ||
                    GeneralProfileIdc == 10 || GeneralProfileCompatibilityFlag[10] ||
                    GeneralProfileIdc == 11 || GeneralProfileCompatibilityFlag[11])
                {
                    await writer.WriteBitAsync(GeneralMax14BitConstraintFlag);
                    await writer.WriteBitsAsync(0, 33);
                }
                else
                {
                    await writer.WriteBitsAsync(0, 34);
                }
            }
            else if (GeneralProfileIdc == 2 || GeneralProfileCompatibilityFlag[2])
            {
                await writer.WriteBitsAsync(0, 7);
                await writer.WriteBitAsync(GeneralOnePictureOnlyConstraintFlag);
                await writer.WriteBitsAsync(0, 35);
            }
            else
            {
                await writer.WriteBitsAsync(0, 43);
            }

            if (GeneralProfileIdc == 1 || GeneralProfileCompatibilityFlag[1] ||
                GeneralProfileIdc == 2 || GeneralProfileCompatibilityFlag[2] ||
                GeneralProfileIdc == 3 || GeneralProfileCompatibilityFlag[3] ||
                GeneralProfileIdc == 4 || GeneralProfileCompatibilityFlag[4] ||
                GeneralProfileIdc == 5 || GeneralProfileCompatibilityFlag[5] ||
                GeneralProfileIdc == 11 || GeneralProfileCompatibilityFlag[11])
            {
                await writer.WriteBitAsync(GeneralInbldFlag);
            }
            else
            {
                await writer.WriteBitAsync(false);
            }
        }

        await writer.WriteBitsAsync(GeneralLevelIdc, 8);

        for (int i = 0; i < maxNumSubLayersMinus1; i++)
        {
            await writer.WriteBitAsync(SubLayerProfilePresentFlag[i]);
            await writer.WriteBitAsync(SubLayerLevelPresentFlag[i]);
        }

        if (maxNumSubLayersMinus1 > 0)
            for (int i = maxNumSubLayersMinus1; i < 8; i++)
                await writer.WriteBitsAsync(0, 2);

        for (int i = 0; i < maxNumSubLayersMinus1; i++)
        {
            if (SubLayerProfilePresentFlag[i])
            {
                await writer.WriteBitsAsync(SubLayerProfileSpace[i], 2);
                await writer.WriteBitAsync(SubLayerTierFlag[i]);
                await writer.WriteBitsAsync(SubLayerProfileIdc[i], 5);
                for (int j = 0; j < 32; j++)
                    await writer.WriteBitAsync(SubLayerProfileCompatibilityFlag[i][j]);
                await writer.WriteBitAsync(SubLayerProgressiveSourceFlag[i]);
                await writer.WriteBitAsync(SubLayerInterlacedSourceFlag[i]);
                await writer.WriteBitAsync(SubLayerNonPackedConstraintFlag[i]);
                await writer.WriteBitAsync(SubLayerFrameOnlyConstraintFlag[i]);
                if (SubLayerProfileIdc[i] == 4 ||
                    SubLayerProfileCompatibilityFlag[i][4] ||
                    SubLayerProfileIdc[i] == 5 ||
                    SubLayerProfileCompatibilityFlag[i][5] ||
                    SubLayerProfileIdc[i] == 6 ||
                    SubLayerProfileCompatibilityFlag[i][6] ||
                    SubLayerProfileIdc[i] == 7 ||
                    SubLayerProfileCompatibilityFlag[i][7] ||
                    SubLayerProfileIdc[i] == 8 ||
                    SubLayerProfileCompatibilityFlag[i][8] ||
                    SubLayerProfileIdc[i] == 9 ||
                    SubLayerProfileCompatibilityFlag[i][9] ||
                    SubLayerProfileIdc[i] == 10 ||
                    SubLayerProfileCompatibilityFlag[i][10] ||
                    SubLayerProfileIdc[i] == 11 ||
                    SubLayerProfileCompatibilityFlag[i][11])
                {
                    await writer.WriteBitAsync(SubLayerMax12BitConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerMax10BitConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerMax8BitConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerMax422ChromaConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerMax420ChromaConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerMaxMonochromeConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerIntraConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerOnePictureOnlyConstraintFlag[i]);
                    await writer.WriteBitAsync(SubLayerLowerBitRateConstraintFlag[i]);

                    if (SubLayerProfileIdc[i] == 5 ||
                        SubLayerProfileCompatibilityFlag[i][5] ||
                        SubLayerProfileIdc[i] == 9 ||
                        SubLayerProfileCompatibilityFlag[i][9] ||
                        SubLayerProfileIdc[i] == 10 ||
                        SubLayerProfileCompatibilityFlag[i][10] ||
                        SubLayerProfileIdc[i] == 11 ||
                        SubLayerProfileCompatibilityFlag[i][11])
                    {
                        await writer.WriteBitAsync(SubLayerMax14BitConstraintFlag[i]);
                        await writer.WriteBitsAsync(0, 33);
                    }
                    else
                    {
                        await writer.WriteBitsAsync(0, 34);
                    }
                }
                else if (SubLayerProfileIdc[i] == 2 || SubLayerProfileCompatibilityFlag[i][2])
                {
                    await writer.WriteBitsAsync(0, 7);
                    await writer.WriteBitAsync(SubLayerOnePictureOnlyConstraintFlag[i]);
                    await writer.WriteBitsAsync(0, 35);
                }
                else
                {
                    await writer.WriteBitsAsync(0, 43);
                }

                if (SubLayerProfileIdc[i] == 1 ||
                    SubLayerProfileCompatibilityFlag[i][1] ||
                    SubLayerProfileIdc[i] == 2 ||
                    SubLayerProfileCompatibilityFlag[i][2] ||
                    SubLayerProfileIdc[i] == 3 ||
                    SubLayerProfileCompatibilityFlag[i][3] ||
                    SubLayerProfileIdc[i] == 4 ||
                    SubLayerProfileCompatibilityFlag[i][4] ||
                    SubLayerProfileIdc[i] == 5 ||
                    SubLayerProfileCompatibilityFlag[i][5] ||
                    SubLayerProfileIdc[i] == 9 ||
                    SubLayerProfileCompatibilityFlag[i][9] ||
                    SubLayerProfileIdc[i] == 11 ||
                    SubLayerProfileCompatibilityFlag[i][11])
                {
                    await writer.WriteBitAsync(SubLayerInbldFlag[i]);
                }
                else
                {
                    await writer.WriteBitAsync(false);
                }
            }

            if (SubLayerLevelPresentFlag[i])
                await writer.WriteBitsAsync(SubLayerProfileIdc[i], 8);
        }
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is ProfileTierLevel level && Equals(level);
    }

    public readonly bool Equals(ProfileTierLevel other)
    {
        return GeneralProfileSpace == other.GeneralProfileSpace &&
               GeneralTierFlag == other.GeneralTierFlag &&
               GeneralProfileIdc == other.GeneralProfileIdc &&
               GeneralProfileCompatibilityFlag.Equals(other.GeneralProfileCompatibilityFlag) &&
               GeneralProgressiveSourceFlag == other.GeneralProgressiveSourceFlag &&
               GeneralInterlacedSourceFlag == other.GeneralInterlacedSourceFlag &&
               GeneralNonPackedConstraintFlag == other.GeneralNonPackedConstraintFlag &&
               GeneralFrameOnlyConstraintFlag == other.GeneralFrameOnlyConstraintFlag &&
               GeneralMax12BitConstraintFlag == other.GeneralMax12BitConstraintFlag &&
               GeneralMax10BitConstraintFlag == other.GeneralMax10BitConstraintFlag &&
               GeneralMax8BitConstraintFlag == other.GeneralMax8BitConstraintFlag &&
               GeneralMax422ChromaConstraintFlag == other.GeneralMax422ChromaConstraintFlag &&
               GeneralMax420ChromaConstraintFlag == other.GeneralMax420ChromaConstraintFlag &&
               GeneralMonochromeConstraintFlag == other.GeneralMonochromeConstraintFlag &&
               GeneralIntraConstraintFlag == other.GeneralIntraConstraintFlag &&
               GeneralOnePictureOnlyConstraintFlag == other.GeneralOnePictureOnlyConstraintFlag &&
               GeneralLowerBitRateConstraintFlag == other.GeneralLowerBitRateConstraintFlag &&
               GeneralMax14BitConstraintFlag == other.GeneralMax14BitConstraintFlag &&
               GeneralInbldFlag == other.GeneralInbldFlag &&
               GeneralLevelIdc == other.GeneralLevelIdc &&
               SubLayerProfilePresentFlag.Equals(other.SubLayerProfilePresentFlag) &&
               SubLayerLevelPresentFlag.Equals(other.SubLayerLevelPresentFlag) &&
               SubLayerProfileSpace.Equals(other.SubLayerProfileSpace) &&
               SubLayerTierFlag.Equals(other.SubLayerTierFlag) &&
               SubLayerProfileIdc.Equals(other.SubLayerProfileIdc) &&
               SubLayerProfileCompatibilityFlag.Equals(other.SubLayerProfileCompatibilityFlag) &&
               SubLayerProgressiveSourceFlag.Equals(other.SubLayerProgressiveSourceFlag) &&
               SubLayerInterlacedSourceFlag.Equals(other.SubLayerInterlacedSourceFlag) &&
               SubLayerNonPackedConstraintFlag.Equals(other.SubLayerNonPackedConstraintFlag) &&
               SubLayerFrameOnlyConstraintFlag.Equals(other.SubLayerFrameOnlyConstraintFlag) &&
               SubLayerMax12BitConstraintFlag.Equals(other.SubLayerMax12BitConstraintFlag) &&
               SubLayerMax10BitConstraintFlag.Equals(other.SubLayerMax10BitConstraintFlag) &&
               SubLayerMax8BitConstraintFlag.Equals(other.SubLayerMax8BitConstraintFlag) &&
               SubLayerMax422ChromaConstraintFlag.Equals(other.SubLayerMax422ChromaConstraintFlag) &&
               SubLayerMax420ChromaConstraintFlag.Equals(other.SubLayerMax420ChromaConstraintFlag) &&
               SubLayerMaxMonochromeConstraintFlag.Equals(other.SubLayerMaxMonochromeConstraintFlag) &&
               SubLayerIntraConstraintFlag.Equals(other.SubLayerIntraConstraintFlag) &&
               SubLayerOnePictureOnlyConstraintFlag.Equals(other.SubLayerOnePictureOnlyConstraintFlag) &&
               SubLayerLowerBitRateConstraintFlag.Equals(other.SubLayerLowerBitRateConstraintFlag) &&
               SubLayerMax14BitConstraintFlag.Equals(other.SubLayerMax14BitConstraintFlag) &&
               SubLayerInbldFlag.Equals(other.SubLayerInbldFlag) &&
               SubLayerLevelIdc.Equals(other.SubLayerLevelIdc);
    }

    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(GeneralProfileSpace);
        hash.Add(GeneralTierFlag);
        hash.Add(GeneralProfileIdc);
        hash.Add(GeneralProfileCompatibilityFlag);
        hash.Add(GeneralProgressiveSourceFlag);
        hash.Add(GeneralInterlacedSourceFlag);
        hash.Add(GeneralNonPackedConstraintFlag);
        hash.Add(GeneralFrameOnlyConstraintFlag);
        hash.Add(GeneralMax12BitConstraintFlag);
        hash.Add(GeneralMax10BitConstraintFlag);
        hash.Add(GeneralMax8BitConstraintFlag);
        hash.Add(GeneralMax422ChromaConstraintFlag);
        hash.Add(GeneralMax420ChromaConstraintFlag);
        hash.Add(GeneralMonochromeConstraintFlag);
        hash.Add(GeneralIntraConstraintFlag);
        hash.Add(GeneralOnePictureOnlyConstraintFlag);
        hash.Add(GeneralLowerBitRateConstraintFlag);
        hash.Add(GeneralMax14BitConstraintFlag);
        hash.Add(GeneralInbldFlag);
        hash.Add(GeneralLevelIdc);
        hash.Add(SubLayerProfilePresentFlag);
        hash.Add(SubLayerLevelPresentFlag);
        hash.Add(SubLayerProfileSpace);
        hash.Add(SubLayerTierFlag);
        hash.Add(SubLayerProfileIdc);
        hash.Add(SubLayerProfileCompatibilityFlag);
        hash.Add(SubLayerProgressiveSourceFlag);
        hash.Add(SubLayerInterlacedSourceFlag);
        hash.Add(SubLayerNonPackedConstraintFlag);
        hash.Add(SubLayerFrameOnlyConstraintFlag);
        hash.Add(SubLayerMax12BitConstraintFlag);
        hash.Add(SubLayerMax10BitConstraintFlag);
        hash.Add(SubLayerMax8BitConstraintFlag);
        hash.Add(SubLayerMax422ChromaConstraintFlag);
        hash.Add(SubLayerMax420ChromaConstraintFlag);
        hash.Add(SubLayerMaxMonochromeConstraintFlag);
        hash.Add(SubLayerIntraConstraintFlag);
        hash.Add(SubLayerOnePictureOnlyConstraintFlag);
        hash.Add(SubLayerLowerBitRateConstraintFlag);
        hash.Add(SubLayerMax14BitConstraintFlag);
        hash.Add(SubLayerInbldFlag);
        hash.Add(SubLayerLevelIdc);
        return hash.ToHashCode();
    }

    public static bool operator ==(ProfileTierLevel left, ProfileTierLevel right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ProfileTierLevel left, ProfileTierLevel right)
    {
        return !(left == right);
    }
}
