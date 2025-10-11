namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    /// Defines methods for writing H.264 RBSP (Raw Byte Sequence Payload) structures into a bitstream.
    /// </summary>
    public interface IH264IOWriter
    {
        /// <summary>
        /// Writes the Sequence Parameter Set (SPS) data to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="spsData">The Sequence Parameter Set data to write.</param>
        void WriteSPSData(H264RbspState rbspState, BitStreamWriter writer, RbspSequenceParameterSetData spsData);

        /// <summary>
        /// Asynchronously writes the Sequence Parameter Set (SPS) data to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="spsData">The Sequence Parameter Set data to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteSPSDataAsync(H264RbspState rbspState, BitStreamWriter writer, RbspSequenceParameterSetData spsData);

        /// <summary>
        /// Writes the Picture Parameter Set (PPS) data to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="ppsData">The Picture Parameter Set data to write.</param>
        void WritePPSData(H264RbspState rbspState, BitStreamWriter writer, RbspPictureParameterSet ppsData);

        /// <summary>
        /// Asynchronously writes the Picture Parameter Set (PPS) data to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="ppsData">The Picture Parameter Set data to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WritePPSDataAsync(H264RbspState rbspState, BitStreamWriter writer, RbspPictureParameterSet ppsData);

        /// <summary>
        /// Writes the slice header to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="sliceHeader">The slice header to write.</param>
        void WriteSliceHeader(H264RbspState rbspState, BitStreamWriter writer, RbspSliceHeader sliceHeader);

        /// <summary>
        /// Asynchronously writes the slice header to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="sliceHeader">The slice header to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteSliceHeaderAsync(H264RbspState rbspState, BitStreamWriter writer, RbspSliceHeader sliceHeader);

        /// <summary>
        /// Writes the decoded reference picture marking to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="decRefPicMarking">The decoded reference picture marking to write.</param>
        void WriteDecRefPicMarking(H264RbspState rbspState, BitStreamWriter writer, RbspDecRefPicMarking decRefPicMarking);

        /// <summary>
        /// Asynchronously writes the decoded reference picture marking to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="decRefPicMarking">The decoded reference picture marking to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteDecRefPicMarkingAsync(H264RbspState rbspState, BitStreamWriter writer, RbspDecRefPicMarking decRefPicMarking);

        /// <summary>
        /// Writes the reference picture list modification to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="refPicListModification">The reference picture list modification to write.</param>
        void WriteRefPicListModification(H264RbspState rbspState, BitStreamWriter writer, RbspRefPicListModification refPicListModification);

        /// <summary>
        /// Asynchronously writes the reference picture list modification to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="refPicListModification">The reference picture list modification to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteRefPicListModificationAsync(H264RbspState rbspState, BitStreamWriter writer, RbspRefPicListModification refPicListModification);

        /// <summary>
        /// Writes the MVC reference picture list modification to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="mvcModification">The MVC reference picture list modification to write.</param>
        void WriteMvcRbspRefPicListMvcModification(H264RbspState rbspState, BitStreamWriter writer, MvcRbspRefPicListMvcModification mvcModification);

        /// <summary>
        /// Asynchronously writes the MVC reference picture list modification to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="mvcModification">The MVC reference picture list modification to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteMvcRbspRefPicListMvcModificationAsync(H264RbspState rbspState, BitStreamWriter writer, MvcRbspRefPicListMvcModification mvcModification);

        /// <summary>
        /// Writes the prediction weight table to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="predWeightTable">The prediction weight table to write.</param>
        void WritePredWeightTable(H264RbspState rbspState, BitStreamWriter writer, RbspPredWeightTable predWeightTable);

        /// <summary>
        /// Asynchronously writes the prediction weight table to the bitstream.
        /// </summary>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="writer">The bitstream writer used to output data.</param>
        /// <param name="predWeightTable">The prediction weight table to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WritePredWeightTableAsync(H264RbspState rbspState, BitStreamWriter writer, RbspPredWeightTable predWeightTable);

        /// <summary>
        /// Writes the macroblock layer using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode macroblock data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="macroblockLayer">The macroblock layer to write.</param>
        void WriteMacroblockLayer(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspMacroblockLayer macroblockLayer);

        /// <summary>
        /// Asynchronously writes the macroblock layer using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode macroblock data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="macroblockLayer">The macroblock layer to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteMacroblockLayerAsync(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspMacroblockLayer macroblockLayer);

        /// <summary>
        /// Writes the macroblock prediction data using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode prediction data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="mbPred">The macroblock prediction data to write.</param>
        void WriteMbPred(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspMbPred mbPred);

        /// <summary>
        /// Asynchronously writes the macroblock prediction data using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode prediction data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="mbPred">The macroblock prediction data to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteMbPredAsync(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspMbPred mbPred);

        /// <summary>
        /// Writes the sub-macroblock prediction data using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode sub-macroblock prediction data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="subMbPred">The sub-macroblock prediction data to write.</param>
        void WriteSubMbPred(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspSubMbPred subMbPred);

        /// <summary>
        /// Asynchronously writes the sub-macroblock prediction data using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode sub-macroblock prediction data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="subMbPred">The sub-macroblock prediction data to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteSubMbPredAsync(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspSubMbPred subMbPred);

        /// <summary>
        /// Writes the residual data using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode residual data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="residual">The residual data to write.</param>
        void WriteResidual(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspResidual residual);

        /// <summary>
        /// Asynchronously writes the residual data using the syntax writer.
        /// </summary>
        /// <param name="syntaxWriter">The syntax writer used to encode residual data.</param>
        /// <param name="rbspState">The current RBSP encoding state.</param>
        /// <param name="residual">The residual data to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        Task WriteResidualAsync(IH264SyntaxWriter syntaxWriter, H264RbspState rbspState, RbspResidual residual);
    }
}
