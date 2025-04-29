using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
///   This is a marker interface that can represent one of the following types:
///   <list type="bullet">
///     <item><see cref="Avc3DNalUnitHeaderExtension"/></item>
///     <item><see cref="MvcNalUnitHeaderExtension"/></item>
///     <item><see cref="SvcNalUnitHeaderExtension"/></item>
///   </list>
/// </summary>
public interface INalUnitHeaderExtension
{
    /// <summary>
    ///   Writes this NAL unit header extension to the specified bitstream.
    /// </summary>
    /// <param name="writer">Output bitstream</param>
    void Write(BitStreamWriter writer);

    /// <summary>
    ///   Writes this NAL unit header extension to the specified bitstream.
    /// </summary>
    /// <param name="writer">Output bitstream</param>
    Task WriteAsync(BitStreamWriter writer);
}

/// <summary>
/// Represents a Network Abstraction Layer (NAL) unit.
/// </summary>
public struct NalUnit : IEquatable<NalUnit>
{
    private static readonly int StartCodeFindingRecursionLimit = DataSize.Megabytes(1);

    /// <summary>
    /// The NAL reference IDC (importance level of the NAL unit for decoding).
    /// </summary>
    public uint NalRefIdc;

    /// <summary>
    /// Represents the type of the NAL unit.
    /// </summary>
    public uint NalUnitType;

    /// <summary>
    /// Indicates whether the SVC (Scalable Video Coding) extension is present.
    /// </summary>
    public bool SvcExtensionFlag;

    /// <summary>
    /// Indicates whether the AVC 3D extension is present.
    /// </summary>
    public bool Avc3DExtensionFlag;

    /// <summary>
    /// Represents the NAL unit header extension, if applicable.
    /// </summary>
    public INalUnitHeaderExtension? Extension;

    /// <summary>
    /// Initializes a new instance of the <see cref="NalUnit"/> struct.
    /// </summary>
    /// <param name="nalRefIdc">The NAL reference IDC.</param>
    /// <param name="nalUnitType">The NAL unit type.</param>
    /// <param name="svcExtensionFlag">Indicates if the SVC extension is present.</param>
    /// <param name="avc3DExtensionFlag">Indicates if the AVC 3D extension is present.</param>
    /// <param name="extension">The optional NAL unit header extension.</param>
    public NalUnit(uint nalRefIdc, uint nalUnitType, bool svcExtensionFlag, bool avc3DExtensionFlag, INalUnitHeaderExtension? extension)
    {
        NalRefIdc = nalRefIdc;
        NalUnitType = nalUnitType;
        SvcExtensionFlag = svcExtensionFlag;
        Avc3DExtensionFlag = avc3DExtensionFlag;
        Extension = extension;
    }

    /// <summary>
    /// Reads a <see cref="NalUnit"/> instance from a <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bitstream reader to read from.</param>
    /// <param name="numOfBytesInNalUnit">Number of bytes in the NAL unit.</param>
    /// <returns>A new instance of <see cref="NalUnit"/>.</returns>
    public static NalUnit Read(BitStreamReader reader, int numOfBytesInNalUnit)
    {
        _ = reader.ReadBit(); // forbidden_zero_bit
        uint nalRefIdc = reader.ReadBits(2);
        uint nalUnitType = reader.ReadBits(5);

        int nuhBytes = 0;
        INalUnitHeaderExtension? nuhExt = null;

        bool svcExtensionFlag = false;
        bool avc3DExtensionFlag = false;

        if (nalUnitType is 14 or 20 or 21)
        {
            svcExtensionFlag = reader.ReadBit();
            avc3DExtensionFlag = reader.ReadBit();

            if (svcExtensionFlag)
            {
                nuhExt = SvcNalUnitHeaderExtension.Read(reader);
                nuhBytes += 3;
            }
            else if (avc3DExtensionFlag)
            {
                nuhExt = Avc3DNalUnitHeaderExtension.Read(reader);
                nuhBytes += 2;
            }
            else
            {
                nuhExt = MvcNalUnitHeaderExtension.Read(reader);
                nuhBytes += 3;
            }
        }

        for (int i = nuhBytes; i < numOfBytesInNalUnit; i++)
        {
            if (i + 2 < numOfBytesInNalUnit && reader.PeekBits(24) == 0x000003)
            {
                _ = reader.ReadBits(8);
                _ = reader.ReadBits(8);
                i += 2;
                uint ep3b = reader.ReadBits(8);
                if (ep3b != 0x03)
                    throw new InvalidDataException("Emulation Prevention 3 Byte is not 0x03");
            }
            else
            {
                _ = reader.ReadBits(8);
            }
        }

        return new NalUnit(
            nalRefIdc,
            nalUnitType,
            svcExtensionFlag,
            avc3DExtensionFlag,
            nuhExt
        );
    }

    /// <summary>
    /// Writes this NAL unit to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to use.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteBit(false); // forbidden_zero_bit
        writer.WriteBits(NalRefIdc, 2);
        writer.WriteBits(NalUnitType, 5);

        if (NalUnitType is 14 or 20 or 21)
        {
            writer.WriteBit(SvcExtensionFlag);
            writer.WriteBit(Avc3DExtensionFlag);

            this.Extension!.Write(writer);
        }
    }

    /// <summary>
    /// Asynchronously writes this NAL unit to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to use.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteBitAsync(false); // forbidden_zero_bit
        await writer.WriteBitsAsync(NalRefIdc, 2);
        await writer.WriteBitsAsync(NalUnitType, 5);

        if (NalUnitType is 14 or 20 or 21)
        {
            await writer.WriteBitAsync(SvcExtensionFlag);
            await writer.WriteBitAsync(Avc3DExtensionFlag);

            await this.Extension!.WriteAsync(writer);
        }
    }

    /// <summary>
    /// Finds and skips the start code 0x00 0x00 0x00 0x01 in the stream.
    /// After execution, you're within the NAL unit itself.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <returns>
    /// <see langword="true"/> if the start code was successfully skipped;
    /// <see langword="false"/> if end of stream was reached first.
    /// </returns>
    public static bool SkipStartCode(BitStreamReader reader)
    {
        RecursionCounter recursionCounter = new(StartCodeFindingRecursionLimit);
        int stream = 0;
        while (true)
        {
            try
            {
                recursionCounter.Increment();

                byte current = (byte)reader.ReadBits(8);
                if (stream != 3 && current == 0)
                {
                    stream++;
                    continue;
                }
                else if (stream == 3 && current == 1)
                {
                    return true;
                }
                else
                {
                    stream -= 1;
                }
            }
            catch (EndOfStreamException)
            {
                return false;
            }
            catch (InfiniteLoopException)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Determines whether this instance is equal to another <see cref="NalUnit"/>.
    /// </summary>
    /// <param name="other">The other <see cref="NalUnit"/> to compare to.</param>
    /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(NalUnit other)
    {
        return NalRefIdc == other.NalRefIdc &&
               NalUnitType == other.NalUnitType &&
               SvcExtensionFlag == other.SvcExtensionFlag &&
               Avc3DExtensionFlag == other.Avc3DExtensionFlag &&
               EqualityComparer<INalUnitHeaderExtension?>.Default.Equals(Extension, other.Extension);
    }

    /// <summary>
    /// Determines whether this instance is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is NalUnit unit && Equals(unit);
    }

    /// <summary>
    /// Gets the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(NalRefIdc, NalUnitType, SvcExtensionFlag, Avc3DExtensionFlag, Extension);
    }

    /// <summary>
    /// Determines whether two <see cref="NalUnit"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="NalUnit"/>.</param>
    /// <param name="right">The second <see cref="NalUnit"/>.</param>
    /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(NalUnit left, NalUnit right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="NalUnit"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="NalUnit"/>.</param>
    /// <param name="right">The second <see cref="NalUnit"/>.</param>
    /// <returns><see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(NalUnit left, NalUnit right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents the AVC 3D NAL Unit Header Extension structure.
/// </summary>
public struct Avc3DNalUnitHeaderExtension : INalUnitHeaderExtension, IEquatable<Avc3DNalUnitHeaderExtension>
{
    /// <summary>
    /// The view index (0-255) associated with this NAL unit.
    /// </summary>
    public uint ViewIdx;

    /// <summary>
    /// Indicates whether this NAL unit contains depth information.
    /// </summary>
    public bool DepthFlag;

    /// <summary>
    /// Indicates whether this NAL unit is part of a non-IDR frame.
    /// </summary>
    public bool NonIDRFlag;

    /// <summary>
    /// The temporal ID associated with this NAL unit (0-7).
    /// </summary>
    public uint TemporalId;

    /// <summary>
    /// Indicates whether this NAL unit is associated with an anchor picture.
    /// </summary>
    public bool AnchorPicFlag;

    /// <summary>
    /// Indicates whether this NAL unit contains inter-view information.
    /// </summary>
    public bool InterViewFlag;

    /// <summary>
    /// Initializes a new instance of the <see cref="Avc3DNalUnitHeaderExtension"/> struct.
    /// </summary>
    /// <param name="viewIdx">The view index.</param>
    /// <param name="depthFlag">The depth flag.</param>
    /// <param name="nonIDRFlag">The non-IDR flag.</param>
    /// <param name="temporalId">The temporal ID.</param>
    /// <param name="anchorPicFlag">The anchor picture flag.</param>
    /// <param name="interViewFlag">The inter-view flag.</param>
    public Avc3DNalUnitHeaderExtension(uint viewIdx, bool depthFlag, bool nonIDRFlag, uint temporalId, bool anchorPicFlag, bool interViewFlag)
    {
        ViewIdx = viewIdx;
        DepthFlag = depthFlag;
        NonIDRFlag = nonIDRFlag;
        TemporalId = temporalId;
        AnchorPicFlag = anchorPicFlag;
        InterViewFlag = interViewFlag;
    }

    /// <summary>
    /// Reads an instance of <see cref="Avc3DNalUnitHeaderExtension"/> from a <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="BitStreamReader"/> to read from.</param>
    /// <returns>A new instance of <see cref="Avc3DNalUnitHeaderExtension"/>.</returns>
    public static Avc3DNalUnitHeaderExtension Read(BitStreamReader reader)
    {
        uint viewIdx = reader.ReadBits(8);
        bool depthFlag = reader.ReadBit();
        bool nonIDRFlag = reader.ReadBit();
        uint temporalId = reader.ReadBits(3);
        bool anchorPicFlag = reader.ReadBit();
        bool interViewFlag = reader.ReadBit();

        return new Avc3DNalUnitHeaderExtension(
            viewIdx,
            depthFlag,
            nonIDRFlag,
            temporalId,
            anchorPicFlag,
            interViewFlag
        );
    }

    /// <summary>
    /// Determines whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified object is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is Avc3DNalUnitHeaderExtension extension && Equals(extension);
    }

    /// <summary>
    /// Determines whether this instance is equal to another <see cref="Avc3DNalUnitHeaderExtension"/>.
    /// </summary>
    /// <param name="other">The <see cref="Avc3DNalUnitHeaderExtension"/> to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="Avc3DNalUnitHeaderExtension"/> is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool Equals(Avc3DNalUnitHeaderExtension other)
    {
        return ViewIdx == other.ViewIdx &&
               DepthFlag == other.DepthFlag &&
               NonIDRFlag == other.NonIDRFlag &&
               TemporalId == other.TemporalId &&
               AnchorPicFlag == other.AnchorPicFlag &&
               InterViewFlag == other.InterViewFlag;
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(ViewIdx, DepthFlag, NonIDRFlag, TemporalId, AnchorPicFlag, InterViewFlag);
    }

    /// <summary>
    /// Writes this instance to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteBits(ViewIdx, 8);
        writer.WriteBit(DepthFlag);
        writer.WriteBit(NonIDRFlag);
        writer.WriteBits(TemporalId, 3);
        writer.WriteBit(AnchorPicFlag);
        writer.WriteBit(InterViewFlag);
    }

    /// <summary>
    /// Asynchronously writes this instance to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteBitsAsync(ViewIdx, 8);
        await writer.WriteBitAsync(DepthFlag);
        await writer.WriteBitAsync(NonIDRFlag);
        await writer.WriteBitsAsync(TemporalId, 3);
        await writer.WriteBitAsync(AnchorPicFlag);
        await writer.WriteBitAsync(InterViewFlag);
    }


    /// <summary>
    /// Determines whether two <see cref="Avc3DNalUnitHeaderExtension"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Avc3DNalUnitHeaderExtension"/> instance.</param>
    /// <param name="right">The second <see cref="Avc3DNalUnitHeaderExtension"/> instance.</param>
    /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Avc3DNalUnitHeaderExtension left, Avc3DNalUnitHeaderExtension right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Avc3DNalUnitHeaderExtension"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Avc3DNalUnitHeaderExtension"/> instance.</param>
    /// <param name="right">The second <see cref="Avc3DNalUnitHeaderExtension"/> instance.</param>
    /// <returns><see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Avc3DNalUnitHeaderExtension left, Avc3DNalUnitHeaderExtension right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents the MVC (Multiview Video Coding) NAL Unit Header Extension structure.
/// </summary>
public struct MvcNalUnitHeaderExtension : INalUnitHeaderExtension, IEquatable<MvcNalUnitHeaderExtension>
{
    /// <summary>
    /// Indicates whether this NAL unit is part of a non-IDR frame.
    /// </summary>
    public bool NonIDRFlag;

    /// <summary>
    /// The priority ID of this NAL unit (0-63).
    /// </summary>
    public uint PriorityId;

    /// <summary>
    /// The view ID of this NAL unit (0-1023).
    /// </summary>
    public uint ViewId;

    /// <summary>
    /// The temporal ID of this NAL unit (0-7).
    /// </summary>
    public uint TemporalId;

    /// <summary>
    /// Indicates whether this NAL unit is associated with an anchor picture.
    /// </summary>
    public bool AnchorPicFlag;

    /// <summary>
    /// Indicates whether this NAL unit contains inter-view information.
    /// </summary>
    public bool InterViewFlag;

    /// <summary>
    /// Reserved bit, always set to 1.
    /// </summary>
    public bool ReservedOneBit;

    /// <summary>
    /// Initializes a new instance of the <see cref="MvcNalUnitHeaderExtension"/> struct.
    /// </summary>
    /// <param name="nonIDRFlag">The non-IDR flag.</param>
    /// <param name="priorityId">The priority ID.</param>
    /// <param name="viewId">The view ID.</param>
    /// <param name="temporalId">The temporal ID.</param>
    /// <param name="anchorPicFlag">The anchor picture flag.</param>
    /// <param name="interViewFlag">The inter-view flag.</param>
    /// <param name="reservedOneBit">The reserved bit, always set to 1.</param>
    public MvcNalUnitHeaderExtension(bool nonIDRFlag, uint priorityId, uint viewId, uint temporalId, bool anchorPicFlag, bool interViewFlag, bool reservedOneBit)
    {
        NonIDRFlag = nonIDRFlag;
        PriorityId = priorityId;
        ViewId = viewId;
        TemporalId = temporalId;
        AnchorPicFlag = anchorPicFlag;
        InterViewFlag = interViewFlag;
        ReservedOneBit = reservedOneBit;
    }

    /// <summary>
    /// Reads an instance of <see cref="MvcNalUnitHeaderExtension"/> from a <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="BitStreamReader"/> to read from.</param>
    /// <returns>A new instance of <see cref="MvcNalUnitHeaderExtension"/>.</returns>
    public static MvcNalUnitHeaderExtension Read(BitStreamReader reader)
    {
        bool nonIDRFlag = reader.ReadBit();
        uint priorityId = reader.ReadBits(6);
        uint viewId = reader.ReadBits(10);
        uint temporalId = reader.ReadBits(3);
        bool anchorPicFlag = reader.ReadBit();
        bool interViewFlag = reader.ReadBit();
        _ = reader.ReadBit();

        return new MvcNalUnitHeaderExtension(
            nonIDRFlag,
            priorityId,
            viewId,
            temporalId,
            anchorPicFlag,
            interViewFlag,
            true
        );
    }

    /// <summary>
    /// Checks whether this instance equals another <see cref="MvcNalUnitHeaderExtension"/> object.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns><see langword="true"/> if equal, <see langword="false"/> otherwise.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is MvcNalUnitHeaderExtension extension && Equals(extension);
    }

    /// <summary>
    /// Checks whether this instance equals another <see cref="MvcNalUnitHeaderExtension"/>.
    /// </summary>
    /// <param name="other">The other instance to compare to.</param>
    /// <returns><see langword="true"/> if equal, <see langword="false"/> otherwise.</returns>
    public readonly bool Equals(MvcNalUnitHeaderExtension other)
    {
        return NonIDRFlag == other.NonIDRFlag &&
               PriorityId == other.PriorityId &&
               ViewId == other.ViewId &&
               TemporalId == other.TemporalId &&
               AnchorPicFlag == other.AnchorPicFlag &&
               InterViewFlag == other.InterViewFlag &&
               ReservedOneBit == other.ReservedOneBit;
    }

    /// <summary>
    /// Gets the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(NonIDRFlag, PriorityId, ViewId, TemporalId, AnchorPicFlag, InterViewFlag, ReservedOneBit);
    }

    /// <summary>
    /// Writes this instance to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteBit(NonIDRFlag);
        writer.WriteBits(PriorityId, 6);
        writer.WriteBits(ViewId, 10);
        writer.WriteBits(TemporalId, 3);
        writer.WriteBit(AnchorPicFlag);
        writer.WriteBit(InterViewFlag);
        writer.WriteBit(true);
    }

    /// <summary>
    /// Asynchronously writes this instance to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteBitAsync(NonIDRFlag);
        await writer.WriteBitsAsync(PriorityId, 6);
        await writer.WriteBitsAsync(ViewId, 10);
        await writer.WriteBitsAsync(TemporalId, 3);
        await writer.WriteBitAsync(AnchorPicFlag);
        await writer.WriteBitAsync(InterViewFlag);
        await writer.WriteBitAsync(true);
    }

    /// <summary>
    /// Compares two <see cref="MvcNalUnitHeaderExtension"/> objects for equality.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><see langword="true"/> if equal, <see langword="false"/> otherwise.</returns>
    public static bool operator ==(MvcNalUnitHeaderExtension left, MvcNalUnitHeaderExtension right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="MvcNalUnitHeaderExtension"/> objects for inequality.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><see langword="true"/> if not equal, <see langword="false"/> otherwise.</returns>
    public static bool operator !=(MvcNalUnitHeaderExtension left, MvcNalUnitHeaderExtension right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents the SVC (Scalable Video Coding) NAL Unit Header Extension structure.
/// </summary>
public struct SvcNalUnitHeaderExtension : INalUnitHeaderExtension, IEquatable<SvcNalUnitHeaderExtension>
{
    /// <summary>
    /// Indicates whether this NAL unit is part of an IDR (Instantaneous Decoding Refresh) frame.
    /// </summary>
    public bool IDRFlag;

    /// <summary>
    /// The priority ID of this NAL unit (0-63).
    /// </summary>
    public uint PriorityId;

    /// <summary>
    /// Indicates whether inter-layer prediction is disabled for this NAL unit.
    /// </summary>
    public bool NoInterLayerPredFlag;

    /// <summary>
    /// The dependency ID of this NAL unit (0-7).
    /// </summary>
    public uint DependencyId;

    /// <summary>
    /// The quality ID of this NAL unit (0-15).
    /// </summary>
    public uint QualityId;

    /// <summary>
    /// The temporal ID of this NAL unit (0-7).
    /// </summary>
    public uint TemporalId;

    /// <summary>
    /// Indicates whether this NAL unit uses a reference picture base.
    /// </summary>
    public bool UseRefPicBaseFlag;

    /// <summary>
    /// Indicates whether this NAL unit is discardable.
    /// </summary>
    public bool DiscardableFlag;

    /// <summary>
    /// Indicates whether this NAL unit should be output.
    /// </summary>
    public bool OutputFlag;

    /// <summary>
    /// Reserved bits, always set to 3.
    /// </summary>
    public uint ReservedThree2Bits;

    /// <summary>
    /// Initializes a new instance of the <see cref="SvcNalUnitHeaderExtension"/> struct.
    /// </summary>
    /// <param name="iDRFlag">The IDR flag.</param>
    /// <param name="priorityId">The priority ID.</param>
    /// <param name="noInterLayerPredFlag">The no inter-layer prediction flag.</param>
    /// <param name="dependencyId">The dependency ID.</param>
    /// <param name="qualityId">The quality ID.</param>
    /// <param name="temporalId">The temporal ID.</param>
    /// <param name="useRefPicBaseFlag">The use reference picture base flag.</param>
    /// <param name="discardableFlag">The discardable flag.</param>
    /// <param name="outputFlag">The output flag.</param>
    /// <param name="reservedThree2Bits">The reserved bits, always set to 3.</param>
    public SvcNalUnitHeaderExtension(bool iDRFlag, uint priorityId, bool noInterLayerPredFlag, uint dependencyId, uint qualityId, uint temporalId, bool useRefPicBaseFlag, bool discardableFlag, bool outputFlag, uint reservedThree2Bits)
    {
        IDRFlag = iDRFlag;
        PriorityId = priorityId;
        NoInterLayerPredFlag = noInterLayerPredFlag;
        DependencyId = dependencyId;
        QualityId = qualityId;
        TemporalId = temporalId;
        UseRefPicBaseFlag = useRefPicBaseFlag;
        DiscardableFlag = discardableFlag;
        OutputFlag = outputFlag;
        ReservedThree2Bits = reservedThree2Bits;
    }

    /// <summary>
    /// Reads an instance of <see cref="SvcNalUnitHeaderExtension"/> from a <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="BitStreamReader"/> to read from.</param>
    /// <returns>A new instance of <see cref="SvcNalUnitHeaderExtension"/>.</returns>
    public static SvcNalUnitHeaderExtension Read(BitStreamReader reader)
    {
        bool idrFlag = reader.ReadBit();
        uint priorityId = reader.ReadBits(6);
        bool noInterLayerPredFlag = reader.ReadBit();
        uint dependencyId = reader.ReadBits(3);
        uint qualityId = reader.ReadBits(4);
        uint temporalId = reader.ReadBits(3);
        bool useRefPicBaseFlag = reader.ReadBit();
        bool discardableFlag = reader.ReadBit();
        bool outputFlag = reader.ReadBit();
        _ = reader.ReadBits(2);

        return new SvcNalUnitHeaderExtension(
            idrFlag,
            priorityId,
            noInterLayerPredFlag,
            dependencyId,
            qualityId,
            temporalId,
            useRefPicBaseFlag,
            discardableFlag,
            outputFlag,
            3u);
    }

    /// <summary>
    /// Checks whether this instance equals another <see cref="SvcNalUnitHeaderExtension"/> object.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns><see langword="true"/> if equal, <see langword="false"/> otherwise.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is SvcNalUnitHeaderExtension extension && Equals(extension);
    }

    /// <summary>
    /// Checks whether this instance equals another <see cref="SvcNalUnitHeaderExtension"/>.
    /// </summary>
    /// <param name="other">The other instance to compare to.</param>
    /// <returns><see langword="true"/> if equal, <see langword="false"/> otherwise.</returns>
    public readonly bool Equals(SvcNalUnitHeaderExtension other)
    {
        return IDRFlag == other.IDRFlag &&
               PriorityId == other.PriorityId &&
               NoInterLayerPredFlag == other.NoInterLayerPredFlag &&
               DependencyId == other.DependencyId &&
               QualityId == other.QualityId &&
               TemporalId == other.TemporalId &&
               UseRefPicBaseFlag == other.UseRefPicBaseFlag &&
               DiscardableFlag == other.DiscardableFlag &&
               OutputFlag == other.OutputFlag &&
               ReservedThree2Bits == other.ReservedThree2Bits;
    }

    /// <summary>
    /// Gets the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(IDRFlag);
        hash.Add(PriorityId);
        hash.Add(NoInterLayerPredFlag);
        hash.Add(DependencyId);
        hash.Add(QualityId);
        hash.Add(TemporalId);
        hash.Add(UseRefPicBaseFlag);
        hash.Add(DiscardableFlag);
        hash.Add(OutputFlag);
        hash.Add(ReservedThree2Bits);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Writes this instance to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteBit(IDRFlag);
        writer.WriteBits(PriorityId, 6);
        writer.WriteBit(NoInterLayerPredFlag);
        writer.WriteBits(DependencyId, 3);
        writer.WriteBits(QualityId, 4);
        writer.WriteBits(TemporalId, 3);
        writer.WriteBit(UseRefPicBaseFlag);
        writer.WriteBit(DiscardableFlag);
        writer.WriteBit(OutputFlag);
        writer.WriteBits(3u, 2u);
    }

    /// <summary>
    /// Asynchronously writes this instance to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BitStreamWriter"/> to write to.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteBitAsync(IDRFlag);
        await writer.WriteBitsAsync(PriorityId, 6);
        await writer.WriteBitAsync(NoInterLayerPredFlag);
        await writer.WriteBitsAsync(DependencyId, 3);
        await writer.WriteBitsAsync(QualityId, 4);
        await writer.WriteBitsAsync(TemporalId, 3);
        await writer.WriteBitAsync(UseRefPicBaseFlag);
        await writer.WriteBitAsync(DiscardableFlag);
        await writer.WriteBitAsync(OutputFlag);
        await writer.WriteBitsAsync(3u, 2u);
    }

    /// <summary>
    /// Compares two <see cref="SvcNalUnitHeaderExtension"/> objects for equality.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><see langword="true"/> if equal, <see langword="false"/> otherwise.</returns>
    public static bool operator ==(SvcNalUnitHeaderExtension left, SvcNalUnitHeaderExtension right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="SvcNalUnitHeaderExtension"/> objects for inequality.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><see langword="true"/> if not equal, <see langword="false"/> otherwise.</returns>
    public static bool operator !=(SvcNalUnitHeaderExtension left, SvcNalUnitHeaderExtension right)
    {
        return !(left == right);
    }
}
