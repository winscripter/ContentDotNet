namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine
{
    /// <summary>
    ///   Tracks recent bins.
    /// </summary>
    public interface IBinTracker
    {
        /// <summary>
        ///   Last 32 bins.
        /// </summary>
        BinHistory RecentBins { get; set; }

        /// <summary>
        ///   Should tracking bins be enabled?
        /// </summary>
        bool Track { get; set; }

        /// <summary>
        ///   Resets the bin history.
        /// </summary>
        void Reset();

        /// <summary>
        ///   Feeds a bin to the tracker.
        /// </summary>
        /// <param name="bin">Bin to feed.</param>
        void Feed(bool bin);
    }
}
