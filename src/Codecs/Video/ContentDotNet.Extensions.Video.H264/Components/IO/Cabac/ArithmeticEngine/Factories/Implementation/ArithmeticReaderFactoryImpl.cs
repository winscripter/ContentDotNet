namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories.Implementation
{
    using ContentDotNet.BitStream;

    internal class ArithmeticReaderFactoryImpl : IH264ArithmeticReaderFactory
    {
        public static readonly ArithmeticReaderFactoryImpl Instance = new();

        public IH264ArithmeticReader Create(BitStreamReader reader, int range, int offset, IBinTracker? binTracker)
        {
            return ArithmeticFactory.CreateArithmeticReader(reader, range, offset, binTracker);
        }

        public IH264ArithmeticReader Create(BitStreamReader reader, int range, int offset, IBinTrackerFactory? binTrackerFactory)
        {
            return ArithmeticFactory.CreateArithmeticReader(
                reader,
                range,
                offset,
                binTrackerFactory?.Create());
        }
    }
}
