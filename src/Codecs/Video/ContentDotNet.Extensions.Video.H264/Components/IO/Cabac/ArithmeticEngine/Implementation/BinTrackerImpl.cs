namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Implementation
{
    internal class BinTrackerImpl : IBinTracker
    {
        public BinHistory RecentBins { get; set; } = default;
        public bool Track { get; set; } = false;

        public void Feed(bool bin)
        {
            if (Track)
            {
                RecentBins.Append(bin);
            }
        }

        public void Reset()
        {
            RecentBins = default;
        }
    }
}
