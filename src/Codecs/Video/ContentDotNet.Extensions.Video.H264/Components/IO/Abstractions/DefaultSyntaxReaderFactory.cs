namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Implementation;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Implementation;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.IOImplementation;

    /// <summary>
    ///   The default syntax reader factory.
    /// </summary>
    public class DefaultSyntaxReaderFactory : IH264SyntaxReaderFactory
    {
        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly DefaultSyntaxReaderFactory Instance = new();

        /// <inheritdoc cref="IH264SyntaxReaderFactory.CreateSyntaxReader(H264State, BitStreamReader)" />
        public IH264SyntaxReader CreateSyntaxReader(H264State state, BitStreamReader reader)
        {
            if (state.H264RbspState?.PictureParameterSet?.EntropyCodingModeFlag == true)
            {
                var arithmeticDecoder = new ArithmeticDecodingEngine(reader, new BinTrackerImpl(), 510, (int)reader.ReadBits(9));
                var cabacDecoder = new H264CabacDecoder(arithmeticDecoder, state);
                return new H264CabacReader(cabacDecoder, state.DeriveSliceQpy(),
                    state.GetSliceType(),
                    (int?)state.H264RbspState?.SliceHeader?.CabacInitIdc ?? 0,
                    state);
            }
            else
            {
                // TODO
                throw new NotImplementedException("CAVLC");
            }
        }
    }
}
