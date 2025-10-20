namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories.Implementation;

    /// <summary>
    ///   CABAC arithmetic factory
    /// </summary>
    public static class ArithmeticFactory
    {
        /// <summary>
        ///   Creates a default arithmetic factory service.
        /// </summary>
        /// <returns></returns>
        public static IH264ArithmeticFactoryService CreateDefaultArithmeticFactoryService() => new ArithmeticFactoryService();

        /// <summary>
        ///   Creates a default implementation of a bin tracker.
        /// </summary>
        /// <returns>The bin tracker.</returns>
        public static IBinTracker CreateBinTracker() => CreateDefaultArithmeticFactoryService().CreateBinTrackerFactory().Create();

        /// <summary>
        ///   Creates a default implementation of an arithmetic decoder.
        /// </summary>
        /// <param name="reader">Bitstream reader</param>
        /// <param name="range">The range value</param>
        /// <param name="offset">The offset value</param>
        /// <param name="binTracker">The bin tracker (defaults to <see cref="CreateBinTracker"/>)</param>
        /// <returns>An arithmetic reader.</returns>
        public static IH264ArithmeticReader CreateArithmeticReader(
            BitStreamReader reader,
            int range,
            int offset,
            IBinTracker? binTracker = null)
            => CreateDefaultArithmeticFactoryService()
                .CreateArithmeticReaderFactory()
                .Create(reader, range, offset, binTracker);

        /// <summary>
        ///   Creates a default implementation of an arithmetic decoder, with the range value being
        ///   set equal to 510.
        /// </summary>
        /// <param name="reader">Bitstream reader</param>
        /// <param name="offset">The offset value</param>
        /// <param name="binTracker">The bin tracker (defaults to <see cref="CreateBinTracker"/>)</param>
        /// <returns>An arithmetic reader.</returns>
        public static IH264ArithmeticReader CreateArithmeticReader(
            BitStreamReader reader,
            int offset,
            IBinTracker? binTracker = null)
            => CreateArithmeticReader(reader, 510, offset, binTracker);
    }
}
