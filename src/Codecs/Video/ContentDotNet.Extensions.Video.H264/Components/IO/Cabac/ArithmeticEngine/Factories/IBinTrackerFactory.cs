namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories
{
    /// <summary>
    ///   The bin tracker factory.
    /// </summary>
    public interface IBinTrackerFactory
    {
        /// <summary>
        ///   Creates a new bin tracker.
        /// </summary>
        /// <returns>A new bin tracker.</returns>
        IBinTracker Create();
    }
}
