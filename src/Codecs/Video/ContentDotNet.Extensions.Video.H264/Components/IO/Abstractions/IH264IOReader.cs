namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    /// Defines methods for reading H.264 RBSP (Raw Byte Sequence Payload) structures from a bitstream.
    /// </summary>
    public interface IH264IOReader
    {
        /// <summary>
        ///   The current macroblock.
        /// </summary>
        H264MacroblockInfo? CurrentMacroblock { get; set; }

        /// <summary>
        /// Reads the Sequence Parameter Set (SPS) data from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed Sequence Parameter Set data.</returns>
        RbspSequenceParameterSetData ReadSPSData(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the Sequence Parameter Set (SPS) data from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the SPS data.</returns>
        Task<RbspSequenceParameterSetData> ReadSPSDataAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the Picture Parameter Set (PPS) data from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed Picture Parameter Set data.</returns>
        RbspPictureParameterSet ReadPPSData(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the Picture Parameter Set (PPS) data from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the PPS data.</returns>
        Task<RbspPictureParameterSet> ReadPPSDataAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the slice header from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed slice header.</returns>
        RbspSliceHeader ReadSliceHeader(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the slice header from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the slice header.</returns>
        Task<RbspSliceHeader> ReadSliceHeaderAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the decoded reference picture marking from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed decoded reference picture marking.</returns>
        RbspDecRefPicMarking ReadDecRefPicMarking(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the decoded reference picture marking from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the reference picture marking.</returns>
        Task<RbspDecRefPicMarking> ReadDecRefPicMarkingAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the reference picture list modification from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed reference picture list modification.</returns>
        RbspRefPicListModification ReadRefPicListModification(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the reference picture list modification from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the list modification.</returns>
        Task<RbspRefPicListModification> ReadRefPicListModificationAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the MVC reference picture list modification from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed MVC reference picture list modification.</returns>
        MvcRbspRefPicListMvcModification ReadMvcRbspRefPicListMvcModification(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the MVC reference picture list modification from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the MVC list modification.</returns>
        Task<MvcRbspRefPicListMvcModification> ReadMvcRbspRefPicListMvcModificationAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the prediction weight table from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>The parsed prediction weight table.</returns>
        RbspPredWeightTable ReadPredWeightTable(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Asynchronously reads the prediction weight table from the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <param name="reader">The bitstream reader used to extract data.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the weight table.</returns>
        Task<RbspPredWeightTable> ReadPredWeightTableAsync(H264RbspState rbspState, BitStreamReader reader);

        /// <summary>
        /// Reads the slice data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReaderFactory">The syntax reader factory used to interpret slice data.</param>
        /// <param name="bitStream">The raw bit-stream</param>
        /// <param name="receiveMacroblock">Callback to invoke when a macroblock is received.</param>
        /// <param name="state">The current H.264 parsing state.</param>
        /// <param name="sliceDecoder">The slice decoder</param>
        /// <returns>The parsed slice data.</returns>
        void ReadSliceData(IH264SyntaxReaderFactory syntaxReaderFactory, BitStreamReader bitStream, SliceDataReceiveMacroblockCallback receiveMacroblock, H264State state, ISliceDecoder sliceDecoder);

        /// <summary>
        /// Asynchronously reads the slice data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReaderFactory">The syntax reader factory used to interpret slice data.</param>
        /// <param name="bitStream">The raw bit-stream</param>
        /// <param name="receiveMacroblock">Callback to invoke when a macroblock is received.</param>
        /// <param name="state">The current H.264 parsing state.</param>
        /// <param name="sliceDecoder">The slice decoder</param>
        /// <returns>A task that represents the asynchronous read operation, returning the slice data.</returns>
        Task ReadSliceDataAsync(IH264SyntaxReaderFactory syntaxReaderFactory, BitStreamReader bitStream, SliceDataReceiveMacroblockCallback receiveMacroblock, H264State state, ISliceDecoder sliceDecoder);

        /// <summary>
        /// Reads the macroblock layer using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret macroblock data.</param>
        /// <param name="mb">The macroblock</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>The parsed macroblock layer.</returns>
        void ReadMacroblockLayer(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState);

        /// <summary>
        /// Asynchronously reads the macroblock layer using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret macroblock data.</param>
        /// <param name="mb">The macroblock</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the macroblock layer.</returns>
        Task ReadMacroblockLayerAsync(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState);

        /// <summary>
        /// Reads the macroblock prediction data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret prediction data.</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>The parsed macroblock prediction data.</returns>
        RbspMbPred ReadMbPred(IH264SyntaxReader syntaxReader, H264RbspState rbspState);

        /// <summary>
        /// Asynchronously reads the macroblock prediction data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret prediction data.</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the macroblock prediction data.</returns>
        Task<RbspMbPred> ReadMbPredAsync(IH264SyntaxReader syntaxReader, H264RbspState rbspState);

        /// <summary>
        /// Reads the sub-macroblock prediction data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret sub-macroblock prediction data.</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>The parsed sub-macroblock prediction data.</returns>
        RbspSubMbPred ReadSubMbPred(IH264SyntaxReader syntaxReader, H264RbspState rbspState);

        /// <summary>
        /// Asynchronously reads the sub-macroblock prediction data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret sub-macroblock prediction data.</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the sub-macroblock prediction data.</returns>
        Task<RbspSubMbPred> ReadSubMbPredAsync(IH264SyntaxReader syntaxReader, H264RbspState rbspState);

        /// <summary>
        /// Reads the residual data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret residual data.</param>
        /// <param name="mb">The macroblock</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>The parsed residual data.</returns>
        RbspResidual ReadResidual(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState);

        /// <summary>
        /// Asynchronously reads the residual data using the syntax reader.
        /// </summary>
        /// <param name="syntaxReader">The syntax reader used to interpret residual data.</param>
        /// <param name="mb">The macroblock</param>
        /// <param name="rbspState">The current RBSP parsing state.</param>
        /// <returns>A task that represents the asynchronous read operation, returning the residual data.</returns>
        Task<RbspResidual> ReadResidualAsync(IH264SyntaxReader syntaxReader, H264MacroblockInfo mb, H264RbspState rbspState);
    }
}
