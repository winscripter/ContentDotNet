using ContentDotNet.Abstractions;
using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264;

internal sealed class H264Codec : IVideoCodec
{
    private readonly Stream _stream;
    private readonly BitStreamReader _bitstream;
    private SequenceParameterSet? _lastSps;
    private PictureParameterSet? _lastPps;
    private readonly NeighboringMacroblocks _neighboringMbs;
    private readonly IntraInterDecoder? _decoder;
    private readonly IMacroblockUtility? _utility;
    private readonly DerivationContext _derivationContext;
    private readonly VideoContext? _context;
    private readonly Dictionary<string, object> _parameters;

    public H264Codec(Stream stream)
    {
        _stream = stream;
        _bitstream = new BitStreamReader(stream);
        _lastPps = null;
        _lastSps = null;
        _neighboringMbs = new(0, 0, 0, 0, true, true, true, true);
        _derivationContext = new();
        _decoder = null;
        _context = null;
        _parameters = [];
    }

    public Stream Stream => _stream;

    public VideoContext Context
    {
        get
        {
            if (_context is null)
                throw new InvalidOperationException("The decoder is not ready");

            return _context;
        }
    }

    public Dictionary<string, object> Parameters => _parameters;

    public int BlocksPerWidth
    {
        get
        {
            if (_lastSps is null)
                throw new InvalidOperationException("The decoder is not ready");

            return (int)_lastSps.Value.PicWidthInMbsMinus1 + 1;
        }
    }

    public int BlocksPerHeight
    {
        get
        {
            if (_lastSps is null)
                throw new InvalidOperationException("The decoder is not ready");

            return (int)_lastSps.Value.PicHeightInMapUnitsMinus1 + 1;
        }
    }
    
    /// <summary>
    ///   Returns the number of frames.
    /// </summary>
    /// <remarks>
    ///   This is a <b>very</b> slow method. Use it once and leave the result
    ///   in a variable.
    /// </remarks>
    public int Frames
    {
        get
        {
            //
            // NOTE: H.264 does not explicitly provide the number of frames in the video.
            //       To count it, we have two options.
            //
            //           A - calculate it using timing estimation from VUI parameters
            //           B - count frames by chucking through the entire H.264 stream
            //
            //       Approach A should help. However, VUI parameters are embedded in SPS's,
            //       and not all sequence parameter sets have VUIs, so in other case we have
            //       to chuck through the stream manually.
            //
            //       We also need duration in seconds, but that's not in the H.264. That's more
            //       of in the container format, like MP4. We have to check if there's a value of
            //       'DurationInSeconds' in decoder parameters. Use it if there is. Otherwise, use
            //       approach B.
            //

            if (_lastSps is not null && _lastSps.Value.VuiParameters is VuiParameters vuip && _parameters.TryGetValue(H264DecoderOptionNames.DurationInSeconds, out object? value))
            {
                if (value is uint u)
                {
                    if (vuip.FixedFrameRateFlag)
                    {
                        uint frameRate = vuip.TimeScale / (2 * vuip.NumUnitsInTick);
                        return (int)(u * frameRate);
                    }
                }
            }

            // Approach B
            ReaderState prevPos = this._bitstream.GetState();
            this._bitstream.GoToStart();

            RecursionCounter recursionCounter = new(2 * 1024 * 1024); // No more than 2 million frames

            SequenceParameterSet spsLast = _lastSps is not null ? _lastSps.Value : default;
            PictureParameterSet ppsLast = _lastPps is not null ? _lastPps.Value : default;

            int frameCount = 0;
            int lastFrameNum = -1;

            while (this._bitstream.BaseStream.Position < this._bitstream.BaseStream.Length)
            {
                try
                {
                    recursionCounter.Increment();
                    if (!NalUnit.SkipStartCode(_bitstream))
                        throw new InvalidOperationException("SkipStartCode");

                    ReaderState current = this._bitstream.GetState();
                    long delta = 0L;
                    long pos = current.ByteOffset;

                    try
                    {
                        _ = NalUnit.SkipStartCode(_bitstream);
                        delta = this._bitstream.BaseStream.Position - pos;
                    }
                    catch
                    {
                    }

                    this._bitstream.GoTo(current);

                    NalUnit nalu = NalUnit.Read(_bitstream, (int)delta);

                    if (nalu.IsIdr() || nalu.NalUnitType == 1 /*non-IDR*/)
                    {
                        SliceHeader shd = SliceHeader.Read(_bitstream, nalu, spsLast, ppsLast);
                        int frameNum = (int)shd.FrameNum;

                        if (frameNum != lastFrameNum)
                        {
                            frameCount++;
                            lastFrameNum = frameNum;
                        }
                    }
                    else if (nalu.IsSps())
                    {
                        SequenceParameterSet sps = SequenceParameterSet.Read(_bitstream);
                        spsLast = sps;
                    }
                    else if (nalu.IsPps())
                    {
                        //PictureParameterSet pps = PictureParameterSet.Read(_bitstream, spsLast);
                        //ppsLast = pps;
                    }
                }
                catch (InfiniteLoopException)
                {
                    break;
                }
                catch (InvalidOperationException e) when (e.Message == "SkipStartCode")
                {
                    break;
                }
            }

            this._bitstream.GoTo(prevPos);

            return frameCount;
        }
    }

    public int CurrentHorizontalBlock { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int CurrentVerticalBlock { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void CodeTo(IVideoCodecEncoder encoder)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Memory<Memory<Yuv>> GetNextFrameAsYuv()
    {
        throw new NotImplementedException();
    }

    public void NextBlock()
    {
        throw new NotImplementedException();
    }

    public void PreviousBlock()
    {
        throw new NotImplementedException();
    }

    public void WriteFrameBlock(Memory<Memory<Yuv>> block16x16)
    {
        throw new NotImplementedException();
    }

    public void WriteFrameBlock(Span<Yuv> block16x16)
    {
        throw new NotImplementedException();
    }
}
