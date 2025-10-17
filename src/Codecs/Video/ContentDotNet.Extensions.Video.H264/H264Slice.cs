namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Collections.Generic;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    ///   The H.264 slice.
    /// </summary>
    public class H264Slice
    {
        /// <summary>
        ///   A service that provides IO reading access.
        /// </summary>
        private IH264IOReader? ioReader;

        /// <summary>
        ///   H.264 bitstream.
        /// </summary>
        private BitStreamReader rawBitStream;

        /// <summary>
        ///   H.264 state.
        /// </summary>
        private H264State h264State;

        /// <summary>
        ///   The macroblocks.
        /// </summary>
        private readonly LimitedList<H264MacroblockInfo> macroblocks;

        /// <summary>
        ///   The syntax reader factory.
        /// </summary>
        private IH264SyntaxReaderFactory? syntaxReaderFactory;

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264Slice"/> class.
        /// </summary>
        /// <param name="rawBitStream"></param>
        /// <param name="h264State"></param>
        public H264Slice(BitStreamReader rawBitStream, H264State h264State)
        {
            this.rawBitStream = rawBitStream;
            this.h264State = h264State;
            this.macroblocks = new LimitedList<H264MacroblockInfo>(h264State.DerivePicSizeInMbs() + 1);

            h264State.MacroblockUtility = new MacroblockUtilityImpl(this);
        }

        /// <summary>
        ///   The syntax reader factory.
        /// </summary>
        public IH264SyntaxReaderFactory? SyntaxReaderFactory
        {
            get => syntaxReaderFactory;
            set => syntaxReaderFactory = value;
        }

        /// <summary>
        ///   The CABAC service.
        /// </summary>
        public IH264IOReader? IOReader
        {
            get => ioReader;
            set => ioReader = value;
        }

        /// <summary>
        ///   The raw bitstream.
        /// </summary>
        public BitStreamReader RawBitStream
        {
            get => rawBitStream;
            set => rawBitStream = value;
        }

        /// <summary>
        ///   H.264 state.
        /// </summary>
        public H264State State
        {
            get => h264State;
            set => h264State = value;
        }

        /// <summary>
        ///   Gets a boolean indicating if <see cref="LoadSlice"/> was invoked.
        /// </summary>
        public bool SliceLoaded => macroblocks.Count > 0; // In an H.264 video there's always at least one macroblock

        /// <summary>
        ///   Loads the slice from the RBSP.
        /// </summary>
        /// <param name="sliceDecoder">The slice decoder</param>
        public void LoadSlice(ISliceDecoder sliceDecoder)
        {
            this.EnsureComponentsForLoadingSlicesAreLoaded();

            this.ioReader!.ReadSliceData(syntaxReaderFactory!, rawBitStream, ReceiveMb, this.h264State!, sliceDecoder);
        }

        /// <summary>
        ///   Asynchronously loads the slice from the RBSP.
        /// </summary>
        /// <returns>The awaitable task.</returns>
        /// <param name="sliceDecoder">The slice decoder</param>
        public async Task LoadSliceAsync(ISliceDecoder sliceDecoder)
        {
            this.EnsureComponentsForLoadingSlicesAreLoaded();

            await this.ioReader!.ReadSliceDataAsync(syntaxReaderFactory!, rawBitStream, ReceiveMb, this.h264State, sliceDecoder);
        }

        //private int mbCounter = 0;
        private void ReceiveMb(H264MacroblockInfo layer)
        {
            this.macroblocks.Add(layer);
            //Console.WriteLine(++mbCounter);
            //Thread.Sleep(1); // Slow allocation to give a chance to close Command Prompt if memory leaks out
        }

        private void EnsureComponentsForLoadingSlicesAreLoaded()
        {
            if (this.ioReader == null)
                throw new InvalidOperationException("Missing I/O reader");

            if (this.syntaxReaderFactory == null)
                throw new InvalidOperationException("Missing syntax reader");
        }

        /// <summary>
        ///   Enumerates all H.264 macroblocks discovered within this slice.
        /// </summary>
        /// <returns>The macroblocks.</returns>
        public IEnumerable<H264MacroblockInfo> EnumerateMacroblocks()
        {
            return macroblocks;
        }

        private sealed class MacroblockUtilityImpl : IMacroblockUtility
        {
            private readonly H264Slice self;

            public MacroblockUtilityImpl(H264Slice self) => this.self = self;

            public uint SliceType => self.h264State.H264RbspState?.SliceHeader?.SliceType ?? 0;

            public H264MacroblockInfo GetMacroblock(int mbAddr) => self.macroblocks[mbAddr];

            public H264SliceType GetSliceType(H264MacroblockInfo mb) => mb.SliceType;

            public void Infer(int mbAddr)
            {
                throw new NotImplementedException();
            }

            public bool IsFrame(int mbAddr)
            {
                return IsFrame(GetMacroblock(mbAddr));
            }

            public bool IsFrame(H264MacroblockInfo mb)
            {
                return !mb.MbFieldDecodingFlag && self.h264State.DeriveMbaffFrameFlag();
            }

            public bool IsMacroblock(int mbAddr)
            {
                return mbAddr >= 0 && mbAddr <= self.macroblocks.Count - 1;
            }
        }
    }
}
