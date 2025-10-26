namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories.Implementation
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Implementation;

    internal class ArithmeticReaderFactoryImpl : IH264ArithmeticReaderFactory
    {
        public static readonly ArithmeticReaderFactoryImpl Instance = new();

        public IH264ArithmeticReader Create(BitStreamReader reader, int range, int offset, IBinTracker? binTracker)
        {
            return new ArithmeticDecodingEngine(reader, binTracker ?? new BinTrackerImpl(), range, offset);
        }

        public IH264ArithmeticReader Create(BitStreamReader reader, int range, int offset, IBinTrackerFactory? binTrackerFactory)
        {
            binTrackerFactory ??= BinTrackerFactoryImpl.Instance;
            return Create(reader, range, offset, binTrackerFactory.Create());
        }
    }
}
