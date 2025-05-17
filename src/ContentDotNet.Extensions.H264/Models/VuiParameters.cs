using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Video Usability Information parameters.
/// </summary>
public struct VuiParameters : IEquatable<VuiParameters>
{
    private const byte ExtendedSar = 255;

    /// <summary>
    ///   Specifies if aspect ratio information is present.
    /// </summary>
    public bool AspectRatioInfoPresentFlag;

    /// <summary>
    ///   Aspect ratio IDC (identifier code).
    /// </summary>
    public uint AspectRatioIdc;

    /// <summary>
    ///   Sample aspect ratio width.
    /// </summary>
    public uint SarWidth;

    /// <summary>
    ///   Sample aspect ratio height.
    /// </summary>
    public uint SarHeight;

    /// <summary>
    ///   Specifies if overscan information is present.
    /// </summary>
    public bool OverscanInfoPresentFlag;

    /// <summary>
    ///   Specifies if overscan is appropriate.
    /// </summary>
    public bool OverscanAppropriateFlag;

    /// <summary>
    ///   Specifies if video signal type information is present.
    /// </summary>
    public bool VideoSignalTypePresentFlag;

    /// <summary>
    ///   The video format.
    /// </summary>
    public uint VideoFormat;

    /// <summary>
    ///   Indicates if the video has full range signals.
    /// </summary>
    public bool VideoFullRangeFlag;

    /// <summary>
    ///   Specifies if color description information is present.
    /// </summary>
    public bool ColourDescriptionPresentFlag;

    /// <summary>
    ///   Specifies the color primaries used.
    /// </summary>
    public uint ColourPrimaries;

    /// <summary>
    ///   Transfer characteristics of the video.
    /// </summary>
    public uint TransferCharacteristics;

    /// <summary>
    ///   Matrix coefficients for the video.
    /// </summary>
    public uint MatrixCoefficients;

    /// <summary>
    ///   Specifies if chroma location information is present.
    /// </summary>
    public bool ChromaLocInfoPresentFlag;

    /// <summary>
    ///   Chroma sample location for the top field.
    /// </summary>
    public uint ChromaSampleLocTypeTopField;

    /// <summary>
    ///   Chroma sample location for the bottom field.
    /// </summary>
    public uint ChromaSampleLocTypeBottomField;

    /// <summary>
    ///   Specifies if timing information is present.
    /// </summary>
    public bool TimingInfoPresentFlag;

    /// <summary>
    ///   Number of units in a single tick of the clock.
    /// </summary>
    public uint NumUnitsInTick;

    /// <summary>
    ///   Specifies the time scale.
    /// </summary>
    public uint TimeScale;

    /// <summary>
    ///   Indicates if the frame rate is fixed.
    /// </summary>
    public bool FixedFrameRateFlag;

    /// <summary>
    ///   Specifies if NAL HRD parameters are present.
    /// </summary>
    public bool NalHrdParametersPresentFlag;

    /// <summary>
    ///   NAL HRD parameters.
    /// </summary>
    public HrdParameters? NalHrdParameters;

    /// <summary>
    ///   Specifies if VCL HRD parameters are present.
    /// </summary>
    public bool VclHrdParametersPresentFlag;

    /// <summary>
    ///   VCL HRD parameters.
    /// </summary>
    public HrdParameters? VclHrdParameters;

    /// <summary>
    ///   Specifies if low delay HRD flag is set.
    /// </summary>
    public bool LowDelayHrdFlag;

    /// <summary>
    ///   Specifies if picture structure information is present.
    /// </summary>
    public bool PicStructPresentFlag;

    /// <summary>
    ///   Specifies if bitstream restriction information is present.
    /// </summary>
    public bool BitstreamRestrictionFlag;

    /// <summary>
    ///   Specifies if motion vectors can cross picture boundaries.
    /// </summary>
    public bool MotionVectorsOverPicBoundariesFlag;

    /// <summary>
    ///   Maximum bytes per picture denominator.
    /// </summary>
    public uint MaxBytesPerPicDenom;

    /// <summary>
    ///   Maximum bits per macroblock denominator.
    /// </summary>
    public uint MaxBitsPerMbDenom;

    /// <summary>
    ///   Log base-2 of the maximum horizontal motion vector length.
    /// </summary>
    public uint Log2MaxMvLengthHorizontal;

    /// <summary>
    ///   Log base-2 of the maximum vertical motion vector length.
    /// </summary>
    public uint Log2MaxMvLengthVertical;

    /// <summary>
    ///   Maximum number of reorder frames.
    /// </summary>
    public uint MaxNumReorderFrames;

    /// <summary>
    ///   Maximum decoder frame buffering.
    /// </summary>
    public uint MaxDecFrameBuffering;

    /// <summary>
    ///   Initializes a new instance of the <see cref="VuiParameters"/> structure.
    /// </summary>
    /// <param name="aspectRatioInfoPresentFlag"></param>
    /// <param name="aspectRatioIdc"></param>
    /// <param name="sarWidth"></param>
    /// <param name="sarHeight"></param>
    /// <param name="overscanInfoPresentFlag"></param>
    /// <param name="overscanAppropriateFlag"></param>
    /// <param name="videoSignalTypePresentFlag"></param>
    /// <param name="videoFormat"></param>
    /// <param name="videoFullRangeFlag"></param>
    /// <param name="colourDescriptionPresentFlag"></param>
    /// <param name="colourPrimaries"></param>
    /// <param name="transferCharacteristics"></param>
    /// <param name="matrixCoefficients"></param>
    /// <param name="chromaLocInfoPresentFlag"></param>
    /// <param name="chromaSampleLocTypeTopField"></param>
    /// <param name="chromaSampleLocTypeBottomField"></param>
    /// <param name="timingInfoPresentFlag"></param>
    /// <param name="numUnitsInTick"></param>
    /// <param name="timeScale"></param>
    /// <param name="fixedFrameRateFlag"></param>
    /// <param name="nalHrdParametersPresentFlag"></param>
    /// <param name="nalHrdParameters"></param>
    /// <param name="vclHrdParametersPresentFlag"></param>
    /// <param name="vclHrdParameters"></param>
    /// <param name="lowDelayHrdFlag"></param>
    /// <param name="picStructPresentFlag"></param>
    /// <param name="bitstreamRestrictionFlag"></param>
    /// <param name="motionVectorsOverPicBoundariesFlag"></param>
    /// <param name="maxBytesPerPicDenom"></param>
    /// <param name="maxBitsPerMbDenom"></param>
    /// <param name="log2MaxMvLengthHorizontal"></param>
    /// <param name="log2MaxMvLengthVertical"></param>
    /// <param name="maxNumReorderFrames"></param>
    /// <param name="maxDecFrameBuffering"></param>
    public VuiParameters(bool aspectRatioInfoPresentFlag, uint aspectRatioIdc, uint sarWidth, uint sarHeight, bool overscanInfoPresentFlag, bool overscanAppropriateFlag, bool videoSignalTypePresentFlag, uint videoFormat, bool videoFullRangeFlag, bool colourDescriptionPresentFlag, uint colourPrimaries, uint transferCharacteristics, uint matrixCoefficients, bool chromaLocInfoPresentFlag, uint chromaSampleLocTypeTopField, uint chromaSampleLocTypeBottomField, bool timingInfoPresentFlag, uint numUnitsInTick, uint timeScale, bool fixedFrameRateFlag, bool nalHrdParametersPresentFlag, HrdParameters? nalHrdParameters, bool vclHrdParametersPresentFlag, HrdParameters? vclHrdParameters, bool lowDelayHrdFlag, bool picStructPresentFlag, bool bitstreamRestrictionFlag, bool motionVectorsOverPicBoundariesFlag, uint maxBytesPerPicDenom, uint maxBitsPerMbDenom, uint log2MaxMvLengthHorizontal, uint log2MaxMvLengthVertical, uint maxNumReorderFrames, uint maxDecFrameBuffering)
    {
        AspectRatioInfoPresentFlag = aspectRatioInfoPresentFlag;
        AspectRatioIdc = aspectRatioIdc;
        SarWidth = sarWidth;
        SarHeight = sarHeight;
        OverscanInfoPresentFlag = overscanInfoPresentFlag;
        OverscanAppropriateFlag = overscanAppropriateFlag;
        VideoSignalTypePresentFlag = videoSignalTypePresentFlag;
        VideoFormat = videoFormat;
        VideoFullRangeFlag = videoFullRangeFlag;
        ColourDescriptionPresentFlag = colourDescriptionPresentFlag;
        ColourPrimaries = colourPrimaries;
        TransferCharacteristics = transferCharacteristics;
        MatrixCoefficients = matrixCoefficients;
        ChromaLocInfoPresentFlag = chromaLocInfoPresentFlag;
        ChromaSampleLocTypeTopField = chromaSampleLocTypeTopField;
        ChromaSampleLocTypeBottomField = chromaSampleLocTypeBottomField;
        TimingInfoPresentFlag = timingInfoPresentFlag;
        NumUnitsInTick = numUnitsInTick;
        TimeScale = timeScale;
        FixedFrameRateFlag = fixedFrameRateFlag;
        NalHrdParametersPresentFlag = nalHrdParametersPresentFlag;
        NalHrdParameters = nalHrdParameters;
        VclHrdParametersPresentFlag = vclHrdParametersPresentFlag;
        VclHrdParameters = vclHrdParameters;
        LowDelayHrdFlag = lowDelayHrdFlag;
        PicStructPresentFlag = picStructPresentFlag;
        BitstreamRestrictionFlag = bitstreamRestrictionFlag;
        MotionVectorsOverPicBoundariesFlag = motionVectorsOverPicBoundariesFlag;
        MaxBytesPerPicDenom = maxBytesPerPicDenom;
        MaxBitsPerMbDenom = maxBitsPerMbDenom;
        Log2MaxMvLengthHorizontal = log2MaxMvLengthHorizontal;
        Log2MaxMvLengthVertical = log2MaxMvLengthVertical;
        MaxNumReorderFrames = maxNumReorderFrames;
        MaxDecFrameBuffering = maxDecFrameBuffering;
    }

    /// <summary>
    /// Reads VUI parameters from the bitstream.
    /// </summary>
    /// <param name="reader">Bitstream reader to read from.</param>
    /// <returns>VUI parameters.</returns>
    public static VuiParameters Read(BitStreamReader reader)
    {
        bool aspectRatioPresentFlag = reader.ReadBit();

        uint aspectRatioIdc = 0u;
        uint sarWidth = 0u, sarHeight = 0u;

        if (aspectRatioPresentFlag)
        {
            aspectRatioIdc = reader.ReadBits(8);
            if (aspectRatioIdc == ExtendedSar)
            {
                sarWidth = reader.ReadBits(16);
                sarHeight = reader.ReadBits(16);
            }
        }

        bool overscanInfoPresentFlag = reader.ReadBit();
        bool overscanAppropriateFlag = false;
        if (overscanInfoPresentFlag)
            overscanAppropriateFlag = reader.ReadBit();

        bool videoSignalTypePresentFlag = reader.ReadBit();

        uint videoFormat = 0u;
        bool videoFullRangeFlag = false;
        bool colourDescriptionPresentFlag = false;
        uint colourPrimaries = 0u;
        uint transferCharacteristics = 0u;
        uint matrixCoefficients = 0u;

        if (videoSignalTypePresentFlag)
        {
            videoFormat = reader.ReadBits(3);
            videoFullRangeFlag = reader.ReadBit();
            colourDescriptionPresentFlag = reader.ReadBit();

            if (colourDescriptionPresentFlag)
            {
                colourPrimaries = reader.ReadBits(8);
                transferCharacteristics = reader.ReadBits(8);
                matrixCoefficients = reader.ReadBits(8);
            }
        }

        bool chromaLocInfoPresentFlag = reader.ReadBit();
        uint chromaSampleLocTypeTopField = 0u;
        uint chromaSampleLocTypeBottomField = 0u;

        if (chromaLocInfoPresentFlag)
        {
            chromaSampleLocTypeTopField = reader.ReadUE();
            chromaSampleLocTypeBottomField = reader.ReadUE();
        }

        bool timingInfoPresentFlag = reader.ReadBit();

        uint numUnitsInTick = 0u;
        uint timeScale = 0u;
        bool fixedFrameRateFlag = false;

        if (timingInfoPresentFlag)
        {
            numUnitsInTick = reader.ReadBits(32);
            timeScale = reader.ReadBits(32);
            fixedFrameRateFlag = reader.ReadBit();
        }

        bool nalHrdParametersPresentFlag = reader.ReadBit();
        HrdParameters? nalHrdParameters = null;
        if (nalHrdParametersPresentFlag)
            nalHrdParameters = HrdParameters.Read(reader);

        bool vclHrdParametersPresentFlag = reader.ReadBit();
        HrdParameters? vclHrdParameters = null;
        if (vclHrdParametersPresentFlag)
            vclHrdParameters = HrdParameters.Read(reader);

        bool lowDelayHrdFlag = false;
        if (nalHrdParametersPresentFlag || vclHrdParametersPresentFlag)
            lowDelayHrdFlag = reader.ReadBit();

        bool picStructPresentFlag = reader.ReadBit();
        bool bitstreamRestrictionFlag = reader.ReadBit();

        bool motionVectorsOverPicBoundariesFlag = false;
        uint maxBytesPerPicDenom = 0u;
        uint maxBitsPerMbDenom = 0u;
        uint log2MaxMvLengthHorizontal = 0u;
        uint log2MaxMvLengthVertical = 0u;
        uint numReorderFrames = 0u;
        uint maxDecFrameBuffering = 0u;

        if (bitstreamRestrictionFlag)
        {
            motionVectorsOverPicBoundariesFlag = reader.ReadBit();
            maxBytesPerPicDenom = reader.ReadUE();
            maxBitsPerMbDenom = reader.ReadUE();
            log2MaxMvLengthHorizontal = reader.ReadUE();
            log2MaxMvLengthVertical = reader.ReadUE();
            numReorderFrames = reader.ReadUE();
            maxDecFrameBuffering = reader.ReadUE();
        }

        return new VuiParameters(
            aspectRatioPresentFlag,
            aspectRatioIdc,
            sarWidth,
            sarHeight,
            overscanInfoPresentFlag,
            overscanAppropriateFlag,
            videoSignalTypePresentFlag,
            videoFormat,
            videoFullRangeFlag,
            colourDescriptionPresentFlag,
            colourPrimaries,
            transferCharacteristics,
            matrixCoefficients,
            chromaLocInfoPresentFlag,
            chromaSampleLocTypeTopField,
            chromaSampleLocTypeBottomField,
            timingInfoPresentFlag,
            numUnitsInTick,
            timeScale,
            fixedFrameRateFlag,
            nalHrdParametersPresentFlag,
            nalHrdParameters,
            vclHrdParametersPresentFlag,
            vclHrdParameters,
            lowDelayHrdFlag,
            picStructPresentFlag,
            bitstreamRestrictionFlag,
            motionVectorsOverPicBoundariesFlag,
            maxBytesPerPicDenom,
            maxBitsPerMbDenom,
            log2MaxMvLengthHorizontal,
            log2MaxMvLengthVertical,
            numReorderFrames,
            maxDecFrameBuffering
        );
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <param name="nalHrdWriteOptions">NAL HRD write options.</param>
    /// <param name="vclHrdWriteOptions">VCL HRD write options.</param>
    public readonly void Write(BitStreamWriter writer, HrdWriteOptions nalHrdWriteOptions, HrdWriteOptions vclHrdWriteOptions)
    {
        writer.WriteBit(AspectRatioInfoPresentFlag);
        if (AspectRatioInfoPresentFlag)
        {
            writer.WriteBits(AspectRatioIdc, 8);
            if (AspectRatioIdc == ExtendedSar)
            {
                writer.WriteBits(SarWidth, 16);
                writer.WriteBits(SarHeight, 16);
            }
        }

        writer.WriteBit(OverscanInfoPresentFlag);
        if (OverscanInfoPresentFlag)
            writer.WriteBit(OverscanAppropriateFlag);

        writer.WriteBit(VideoSignalTypePresentFlag);
        if (VideoSignalTypePresentFlag)
        {
            writer.WriteBits(VideoFormat, 3);
            writer.WriteBit(VideoFullRangeFlag);
            writer.WriteBit(ColourDescriptionPresentFlag);
            if (ColourDescriptionPresentFlag)
            {
                writer.WriteBits(ColourPrimaries, 8);
                writer.WriteBits(TransferCharacteristics, 8);
                writer.WriteBits(MatrixCoefficients, 8);
            }
        }

        writer.WriteBit(ChromaLocInfoPresentFlag);
        if (ChromaLocInfoPresentFlag)
        {
            writer.WriteUE(ChromaSampleLocTypeTopField);
            writer.WriteUE(ChromaSampleLocTypeBottomField);
        }

        writer.WriteBit(TimingInfoPresentFlag);
        if (TimingInfoPresentFlag)
        {
            writer.WriteBits(NumUnitsInTick, 32);
            writer.WriteBits(TimeScale, 32);
            writer.WriteBit(FixedFrameRateFlag);
        }
        
        writer.WriteBit(NalHrdParametersPresentFlag);
        if (NalHrdParametersPresentFlag)
            NalHrdParameters!.Value.Write(writer, nalHrdWriteOptions.BitRateValueMinus1, nalHrdWriteOptions.CpbSizeValueMinus1, nalHrdWriteOptions.CbrFlag);
        
        writer.WriteBit(NalHrdParametersPresentFlag);
        if (VclHrdParametersPresentFlag)
            VclHrdParameters!.Value.Write(writer, vclHrdWriteOptions.BitRateValueMinus1, vclHrdWriteOptions.CpbSizeValueMinus1, vclHrdWriteOptions.CbrFlag);
        
        if (NalHrdParametersPresentFlag || VclHrdParametersPresentFlag)
            writer.WriteBit(LowDelayHrdFlag);

        writer.WriteBit(PicStructPresentFlag);
        writer.WriteBit(BitstreamRestrictionFlag);

        if (BitstreamRestrictionFlag)
        {
            writer.WriteBit(MotionVectorsOverPicBoundariesFlag);
            writer.WriteUE(MaxBytesPerPicDenom);
            writer.WriteUE(MaxBitsPerMbDenom);
            writer.WriteUE(Log2MaxMvLengthHorizontal);
            writer.WriteUE(Log2MaxMvLengthVertical);
            writer.WriteUE(MaxNumReorderFrames);
            writer.WriteUE(MaxDecFrameBuffering);
        }
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream, but omits VCL HRD write options.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <param name="nalHrdWriteOptions">NAL HRD write options.</param>
    public readonly void WriteWithEmptyVclHrdWriteOptions(BitStreamWriter writer, HrdWriteOptions nalHrdWriteOptions)
    {
        Span<uint> sp1 = stackalloc uint[1];
        Span<uint> sp2 = stackalloc uint[1];
        Span<bool> sp3 = stackalloc bool[1];
        HrdWriteOptions options = new(sp1, sp2, sp3);

        Write(writer, nalHrdWriteOptions, options);
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream, but omits NAL HRD write options.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <param name="vclHrdWriteOptions">VCL HRD write options.</param>
    public readonly void WriteWithEmptyNalHrdWriteOptions(BitStreamWriter writer, HrdWriteOptions vclHrdWriteOptions)
    {
        Span<uint> sp1 = stackalloc uint[1];
        Span<uint> sp2 = stackalloc uint[1];
        Span<bool> sp3 = stackalloc bool[1];
        HrdWriteOptions options = new(sp1, sp2, sp3);

        Write(writer, options, vclHrdWriteOptions);
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream, but without any HRD write options.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    public readonly void WriteWithEmptyHrdWriteOptions(BitStreamWriter writer)
    {
        Span<uint> sp1x1 = stackalloc uint[1];
        Span<uint> sp1x2 = stackalloc uint[1];
        Span<bool> sp1x3 = stackalloc bool[1];

        Span<uint> sp2x1 = stackalloc uint[1];
        Span<uint> sp2x2 = stackalloc uint[1];
        Span<bool> sp2x3 = stackalloc bool[1];

        Write(writer, new HrdWriteOptions(sp1x1, sp1x2, sp1x3), new HrdWriteOptions(sp2x1, sp2x2, sp2x3));
    }

    /// <summary>
    ///   Writes VUI to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the VUI is written.</param>
    /// <param name="options">Options for writing the VUI.</param>
    public readonly void Write(BitStreamWriter writer, VuiWriteOptions options)
    {
        // NOTE: We have to be careful here so we don't accidentally
        //       copy spans. Memory efficiency is key here.

        if (!options.IsVclPresent && !options.IsNalPresent)
        {
            WriteWithEmptyHrdWriteOptions(writer);
        }
        else if (options.IsVclPresent && !options.IsNalPresent)
        {
            WriteWithEmptyNalHrdWriteOptions(writer, options.VclHrdWriteOptions);
        }
        else if (options.IsNalPresent && !options.IsVclPresent)
        {
            WriteWithEmptyVclHrdWriteOptions(writer, options.NalHrdWriteOptions);
        }
        else
        {
            Write(writer, options.NalHrdWriteOptions, options.VclHrdWriteOptions);
        }
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <param name="nalHrdWriteOptions">NAL HRD write options.</param>
    /// <param name="vclHrdWriteOptions">VCL HRD write options.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, MemoryHrdWriteOptions nalHrdWriteOptions, MemoryHrdWriteOptions vclHrdWriteOptions)
    {
        await writer.WriteBitAsync(AspectRatioInfoPresentFlag);
        if (AspectRatioInfoPresentFlag)
        {
            await writer.WriteBitsAsync(AspectRatioIdc, 8);
            if (AspectRatioIdc == ExtendedSar)
            {
                await writer.WriteBitsAsync(SarWidth, 16);
                await writer.WriteBitsAsync(SarHeight, 16);
            }
        }

        await writer.WriteBitAsync(OverscanInfoPresentFlag);
        if (OverscanInfoPresentFlag)
            await writer.WriteBitAsync(OverscanAppropriateFlag);

        await writer.WriteBitAsync(VideoSignalTypePresentFlag);
        if (VideoSignalTypePresentFlag)
        {
            await writer.WriteBitsAsync(VideoFormat, 3);
            await writer.WriteBitAsync(VideoFullRangeFlag);
            await writer.WriteBitAsync(ColourDescriptionPresentFlag);
            if (ColourDescriptionPresentFlag)
            {
                await writer.WriteBitsAsync(ColourPrimaries, 8);
                await writer.WriteBitsAsync(TransferCharacteristics, 8);
                await writer.WriteBitsAsync(MatrixCoefficients, 8);
            }
        }

        await writer.WriteBitAsync(ChromaLocInfoPresentFlag);
        if (ChromaLocInfoPresentFlag)
        {
            await writer.WriteUEAsync(ChromaSampleLocTypeTopField);
            await writer.WriteUEAsync(ChromaSampleLocTypeBottomField);
        }

        await writer.WriteBitAsync(TimingInfoPresentFlag);
        if (TimingInfoPresentFlag)
        {
            await writer.WriteBitsAsync(NumUnitsInTick, 32);
            await writer.WriteBitsAsync(TimeScale, 32);
            await writer.WriteBitAsync(FixedFrameRateFlag);
        }

        await writer.WriteBitAsync(NalHrdParametersPresentFlag);
        if (NalHrdParametersPresentFlag)
            NalHrdParameters!.Value.Write(writer, nalHrdWriteOptions.BitRateValueMinus1.Span, nalHrdWriteOptions.CpbSizeValueMinus1.Span, nalHrdWriteOptions.CbrFlag.Span);

        await writer.WriteBitAsync(NalHrdParametersPresentFlag);
        if (VclHrdParametersPresentFlag)
            VclHrdParameters!.Value.Write(writer, vclHrdWriteOptions.BitRateValueMinus1.Span, vclHrdWriteOptions.CpbSizeValueMinus1.Span, vclHrdWriteOptions.CbrFlag.Span);

        if (NalHrdParametersPresentFlag || VclHrdParametersPresentFlag)
            await writer.WriteBitAsync(LowDelayHrdFlag);

        await writer.WriteBitAsync(PicStructPresentFlag);
        await writer.WriteBitAsync(BitstreamRestrictionFlag);

        if (BitstreamRestrictionFlag)
        {
            await writer.WriteBitAsync(MotionVectorsOverPicBoundariesFlag);
            await writer.WriteUEAsync(MaxBytesPerPicDenom);
            await writer.WriteUEAsync(MaxBitsPerMbDenom);
            await writer.WriteUEAsync(Log2MaxMvLengthHorizontal);
            await writer.WriteUEAsync(Log2MaxMvLengthVertical);
            await writer.WriteUEAsync(MaxNumReorderFrames);
            await writer.WriteUEAsync(MaxDecFrameBuffering);
        }
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream, but omits VCL HRD write options.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <param name="nalHrdWriteOptions">NAL HRD write options.</param>
    public readonly async Task WriteAsyncWithEmptyVclHrdWriteOptions(BitStreamWriter writer, MemoryHrdWriteOptions nalHrdWriteOptions)
    {
        MemoryHrdWriteOptions options = new(new([]), new([]), new([]));

        await WriteAsync(writer, nalHrdWriteOptions, options);
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream, but omits NAL HRD write options.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <param name="vclHrdWriteOptions">VCL HRD write options.</param>
    public readonly async Task WriteAsyncWithEmptyNalHrdWriteOptions(BitStreamWriter writer, MemoryHrdWriteOptions vclHrdWriteOptions)
    {
        MemoryHrdWriteOptions options = new(new([]), new([]), new([]));

        await WriteAsync(writer, options, vclHrdWriteOptions);
    }

    /// <summary>
    /// Writes these VUI parameters to the bitstream, but without any HRD write options.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    public readonly async Task WriteAsyncWithEmptyHrdWriteOptions(BitStreamWriter writer)
    {
        await WriteAsync(writer, new(new([]), new([]), new([])), new(new([]), new([]), new([])));
    }

    /// <summary>
    ///   Writes VUI to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the VUI is written.</param>
    /// <param name="options">Options for writing the VUI.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, MemoryVuiWriteOptions options)
    {
        if (!options.IsVclPresent && !options.IsNalPresent)
        {
            await WriteAsyncWithEmptyHrdWriteOptions(writer);
        }
        else if (options.IsVclPresent && !options.IsNalPresent)
        {
            await WriteAsyncWithEmptyNalHrdWriteOptions(writer, options.VclHrdWriteOptions);
        }
        else if (options.IsNalPresent && !options.IsVclPresent)
        {
            await WriteAsyncWithEmptyVclHrdWriteOptions(writer, options.NalHrdWriteOptions);
        }
        else
        {
            await WriteAsync(writer, options.NalHrdWriteOptions, options.VclHrdWriteOptions);
        }
    }

    /// <summary>  
    /// Determines whether the specified object is equal to the current instance.  
    /// </summary>  
    /// <param name="obj">The object to compare with the current instance.</param>  
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>  
    public readonly override bool Equals(object? obj)
    {
        return obj is VuiParameters parameters && Equals(parameters);
    }

    /// <summary>  
    /// Determines whether the specified <see cref="PictureParameterSet"/> is equal to the current instance.  
    /// </summary>  
    /// <param name="other">The <see cref="PictureParameterSet"/> to compare with the current instance.</param>  
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>  
    public readonly bool Equals(VuiParameters other)
    {
        return AspectRatioInfoPresentFlag == other.AspectRatioInfoPresentFlag &&
               AspectRatioIdc == other.AspectRatioIdc &&
               SarWidth == other.SarWidth &&
               SarHeight == other.SarHeight &&
               OverscanInfoPresentFlag == other.OverscanInfoPresentFlag &&
               OverscanAppropriateFlag == other.OverscanAppropriateFlag &&
               VideoSignalTypePresentFlag == other.VideoSignalTypePresentFlag &&
               VideoFormat == other.VideoFormat &&
               VideoFullRangeFlag == other.VideoFullRangeFlag &&
               ColourDescriptionPresentFlag == other.ColourDescriptionPresentFlag &&
               ColourPrimaries == other.ColourPrimaries &&
               TransferCharacteristics == other.TransferCharacteristics &&
               MatrixCoefficients == other.MatrixCoefficients &&
               ChromaLocInfoPresentFlag == other.ChromaLocInfoPresentFlag &&
               ChromaSampleLocTypeTopField == other.ChromaSampleLocTypeTopField &&
               ChromaSampleLocTypeBottomField == other.ChromaSampleLocTypeBottomField &&
               TimingInfoPresentFlag == other.TimingInfoPresentFlag &&
               NumUnitsInTick == other.NumUnitsInTick &&
               TimeScale == other.TimeScale &&
               FixedFrameRateFlag == other.FixedFrameRateFlag &&
               NalHrdParametersPresentFlag == other.NalHrdParametersPresentFlag &&
               EqualityComparer<HrdParameters?>.Default.Equals(NalHrdParameters, other.NalHrdParameters) &&
               VclHrdParametersPresentFlag == other.VclHrdParametersPresentFlag &&
               EqualityComparer<HrdParameters?>.Default.Equals(VclHrdParameters, other.VclHrdParameters) &&
               LowDelayHrdFlag == other.LowDelayHrdFlag &&
               PicStructPresentFlag == other.PicStructPresentFlag &&
               BitstreamRestrictionFlag == other.BitstreamRestrictionFlag &&
               MotionVectorsOverPicBoundariesFlag == other.MotionVectorsOverPicBoundariesFlag &&
               MaxBytesPerPicDenom == other.MaxBytesPerPicDenom &&
               MaxBitsPerMbDenom == other.MaxBitsPerMbDenom &&
               Log2MaxMvLengthHorizontal == other.Log2MaxMvLengthHorizontal &&
               Log2MaxMvLengthVertical == other.Log2MaxMvLengthVertical &&
               MaxNumReorderFrames == other.MaxNumReorderFrames &&
               MaxDecFrameBuffering == other.MaxDecFrameBuffering;
    }

    /// <summary>
    ///   Determines the hash code for the PPS.
    /// </summary>
    /// <returns>PPS hash code.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(AspectRatioInfoPresentFlag);
        hash.Add(AspectRatioIdc);
        hash.Add(SarWidth);
        hash.Add(SarHeight);
        hash.Add(OverscanInfoPresentFlag);
        hash.Add(OverscanAppropriateFlag);
        hash.Add(VideoSignalTypePresentFlag);
        hash.Add(VideoFormat);
        hash.Add(VideoFullRangeFlag);
        hash.Add(ColourDescriptionPresentFlag);
        hash.Add(ColourPrimaries);
        hash.Add(TransferCharacteristics);
        hash.Add(MatrixCoefficients);
        hash.Add(ChromaLocInfoPresentFlag);
        hash.Add(ChromaSampleLocTypeTopField);
        hash.Add(ChromaSampleLocTypeBottomField);
        hash.Add(TimingInfoPresentFlag);
        hash.Add(NumUnitsInTick);
        hash.Add(TimeScale);
        hash.Add(FixedFrameRateFlag);
        hash.Add(NalHrdParametersPresentFlag);
        hash.Add(NalHrdParameters);
        hash.Add(VclHrdParametersPresentFlag);
        hash.Add(VclHrdParameters);
        hash.Add(LowDelayHrdFlag);
        hash.Add(PicStructPresentFlag);
        hash.Add(BitstreamRestrictionFlag);
        hash.Add(MotionVectorsOverPicBoundariesFlag);
        hash.Add(MaxBytesPerPicDenom);
        hash.Add(MaxBitsPerMbDenom);
        hash.Add(Log2MaxMvLengthHorizontal);
        hash.Add(Log2MaxMvLengthVertical);
        hash.Add(MaxNumReorderFrames);
        hash.Add(MaxDecFrameBuffering);

        return hash.ToHashCode();
    }

    /// <summary>  
    /// Determines whether two <see cref="VuiParameters"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="VuiParameters"/> to compare.</param>  
    /// <param name="right">The second <see cref="VuiParameters"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(VuiParameters left, VuiParameters right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    /// Determines whether two <see cref="VuiParameters"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="VuiParameters"/> to compare.</param>  
    /// <param name="right">The second <see cref="VuiParameters"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(VuiParameters left, VuiParameters right)
    {
        return !(left == right);
    }
}
