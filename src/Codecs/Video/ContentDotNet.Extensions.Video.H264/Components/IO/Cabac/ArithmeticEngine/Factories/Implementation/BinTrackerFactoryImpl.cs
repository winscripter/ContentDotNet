namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories.Implementation
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Implementation;

    internal class BinTrackerFactoryImpl : IBinTrackerFactory
    {
        public static readonly BinTrackerFactoryImpl Instance = new();

        public IBinTracker Create() => new BinTrackerImpl();
    }
}
