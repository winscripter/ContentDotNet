namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   H.264 arithmetic reader factory.
    /// </summary>
    public interface IH264ArithmeticReaderFactory
    {
        /// <summary>
        ///   Creates an implementation of an arithmetic decoder.
        /// </summary>
        /// <param name="reader">Bitstream reader</param>
        /// <param name="range">The range value</param>
        /// <param name="offset">The offset value</param>
        /// <param name="binTracker">The bin tracker factory</param>
        /// <returns>An arithmetic reader.</returns>
        IH264ArithmeticReader Create(
            BitStreamReader reader,
            int range,
            int offset,
            IBinTracker? binTracker);

        /// <summary>
        ///   Creates an implementation of an arithmetic decoder.
        /// </summary>
        /// <param name="reader">Bitstream reader</param>
        /// <param name="range">The range value</param>
        /// <param name="offset">The offset value</param>
        /// <param name="binTrackerFactory">The bin tracker factory</param>
        /// <returns>An arithmetic reader.</returns>
        IH264ArithmeticReader Create(
            BitStreamReader reader,
            int range,
            int offset,
            IBinTrackerFactory? binTrackerFactory);
    }
}
