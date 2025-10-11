namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   The H.264 arithmetic writer factory.
    /// </summary>
    public interface IH264ArithmeticWriterFactory
    {
        /// <summary>
        ///   Creates an implementation of an arithmetic encoder.
        /// </summary>
        /// <param name="writer">Bitstream writer</param>
        /// <param name="range">The range value</param>
        /// <param name="low">The low value</param>
        /// <param name="binTracker">The bin tracker factory</param>
        /// <returns>An arithmetic writer.</returns>
        IH264ArithmeticWriter Create(
            BitStreamWriter writer,
            int range,
            int low,
            IBinTracker? binTracker);

        /// <summary>
        ///   Creates an implementation of an arithmetic encoder.
        /// </summary>
        /// <param name="writer">Bitstream writer</param>
        /// <param name="range">The range value</param>
        /// <param name="low">The low value</param>
        /// <param name="binTrackerFactory">The bin tracker factory</param>
        /// <returns>An arithmetic writer.</returns>
        IH264ArithmeticWriter Create(
            BitStreamWriter writer,
            int range,
            int low,
            IBinTrackerFactory? binTrackerFactory);
    }
}
