namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet;
    using ContentDotNet.Abstractions;
    using ContentDotNet.BitStream;
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Pictures;
    using ContentDotNet.Shared.ItuT.RbspBuilder;
    using System.Threading.Tasks;

    /// <summary>
    ///   Abstracts an H.264 codec.
    /// </summary>
    public abstract partial class AbstractH264Decoder : IVideoCodecDecoder<YCbCr>
    {
        private string name = "H264";
        private string displayName = "H.264";
        private long cfi = 0;
        private BitStreamReader bsr;
        private Configuration cfg = new();
        
        /// <summary>
        ///   Initializes a new instance of the <see cref="AbstractH264Decoder"/> class with the specified bit-stream reader for
        ///   the backing H.264 I/O decoding.
        /// </summary>
        /// <param name="bsr">H.264 data is read from here</param>
        protected AbstractH264Decoder(BitStreamReader bsr)
        {
            this.bsr = bsr;
        }

        /// <summary>
        ///   This is the name of the codec. It is by default, "H264", although, its name can be changed to virtually
        ///   any codec name using the <see langword="set"/> accessor. Likewise, it is not recommended to do so.
        /// </summary>
        public string Name { get => name; set => name = value; }
        
        /// <summary>
        ///   This is the name of the codec that's displayed to the UI. It is, by default, "H.264", and its name can be
        ///   changed to anything using the <see langword="set"/> accessor, though, that isn't recommended.
        /// </summary>
        public string DisplayName { get => displayName; set => displayName = value; }

        /// <summary>
        ///   This returns the current index (counter) of a frame. It can be changed, however, changing it to incorrect
        ///   values can cause one to lose track of the current frame.
        /// </summary>
        public long CurrentFrameIndex { get => cfi; set => cfi = value; }

        /// <summary>
        ///   This returns the bit-stream reader that backs this H.264 codec. H.264 bit-stream data is read from here.
        /// </summary>
        public BitStreamReader BitStreamReader { get => bsr; set => bsr = value; }

        /// <summary>
        ///   This returns the memory allocation configuration, however, it's unimplemented.
        /// </summary>
        public Configuration Configuration { get => cfg; set => cfg = value; }

#if DEBUG_NALS
        /// <summary>
        ///   Debug?
        /// </summary>
        public bool Debug { get; set; } = false;
#endif

        /// <summary>
        ///   This decodes a single H.264 picture. When this happens, the data is read off of the <see cref="BitStreamReader"/>
        ///   property, and the <see cref="CurrentFrameIndex"/> property is incremented by one. The resulting picture is
        ///   returned in the Y'Cb'Cr (a.k.a. YUV) pixel format.
        /// </summary>
        /// <returns>The decoded picture in the YUV pixel format.</returns>
        public abstract Picture<YCbCr> ReadPicture();

        /// <summary>
        ///   This decodes a single H.264 picture. When this happens, the data is read off of the <see cref="BitStreamReader"/>
        ///   property, and the <see cref="CurrentFrameIndex"/> property is incremented by one. The resulting picture is
        ///   returned in the Y'Cb'Cr (a.k.a. YUV) pixel format.
        /// </summary>
        /// <returns>The decoded picture in the YUV pixel format.</returns>
        public abstract Task<Picture<YCbCr>> ReadPictureAsync();

        /// <summary>
        ///   This is the active state of the H.264 decoder.
        /// </summary>
        public H264State? State { get; set; }

        /// <summary>
        ///   The macroblock currently being parsed.
        /// </summary>
        public H264MacroblockInfo? ActiveMacroblock { get; set; }

        /// <summary>
        ///   List0 reference picture list.
        /// </summary>
        public IDecodedPictureBuffer? RefPicList0 { get; set; }

        /// <summary>
        ///   List1 reference picture list.
        /// </summary>
        public IDecodedPictureBuffer? RefPicList1 { get; set; }

        /// <summary>
        ///   The slice decoding service.
        /// </summary>
        public ISliceDecoder? SliceDecoder { get; set; }

        /// <summary>
        ///   Current H.264 slice.
        /// </summary>
        public H264Slice? Slice { get; set; }

        /// <summary>
        ///   H.264 decoder factories
        /// </summary>
        public DecoderFactories DecoderFactories { get; set; } = new();

        /// <summary>
        ///   This returns the H.264 I/O reader that facilitates decoding data from the H.264 bit-stream.
        /// </summary>
        public IH264IOReader? IOReader { get; set; }

        /// <summary>
        ///   The RBSP builder factory.
        /// </summary>
        public IItuRbspBufferFactory? RbspBuilderFactory { get; set; } = new CustomRbspBufferFactory(() => new MemoryRbspBufferBuilder()
        {
            MaxSize = 2 * 1024 * 1024 // No more than 2MB
        });

        /// <summary>
        ///   Moves to the start code of the next NAL unit.
        /// </summary>
        /// <returns>A boolean that indicates if the start code of the next NAL unit was found.</returns>
        public abstract bool SkipToNalStart();

        /// <summary>
        ///   Returns the length of the current NAL unit.
        /// </summary>
        /// <returns>The length of the current NAL unit in bytes.</returns>
        public abstract long ProcessNalLength();
        
        /// <summary>
        ///   Decodes an H.264 nal unit.
        /// </summary>
        /// <param name="skipToNalStart">When false, you'll have to invoke <see cref="SkipToNalStart"/> prior to invoking this method.</param>
        /// <param name="decodeRbsp">For example, if the NAL unit is a PPS NAL unit, and this parameter is true, the PPS will be read too, automatically</param>
        /// <returns>The type of the NAL unit that was decoded.</returns>
        public abstract NalType DecodeNal(bool skipToNalStart = true, bool decodeRbsp = true);

        /// <summary>
        ///   Decodes an H.264 nal unit (asynchronous version).
        /// </summary>
        /// <param name="skipToNalStart">When false, you'll have to invoke <see cref="SkipToNalStart"/> prior to invoking this method.</param>
        /// <param name="decodeRbsp">For example, if the NAL unit is a PPS NAL unit, and this parameter is true, the PPS will be read too, automatically</param>
        /// <returns>The type of the NAL unit that was decoded.</returns>
        public abstract Task<NalType> DecodeNalAsync(bool skipToNalStart = true, bool decodeRbsp = true);
    }
}
