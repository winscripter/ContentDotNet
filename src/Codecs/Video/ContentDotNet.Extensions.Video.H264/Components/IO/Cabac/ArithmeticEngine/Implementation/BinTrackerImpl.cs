namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Implementation
{
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;

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
