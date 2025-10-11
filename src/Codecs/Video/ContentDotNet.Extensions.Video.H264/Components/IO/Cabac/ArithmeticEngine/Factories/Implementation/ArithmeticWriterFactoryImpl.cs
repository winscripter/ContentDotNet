namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories.Implementation
{
    using ContentDotNet.BitStream;

    internal class ArithmeticWriterFactoryImpl : IH264ArithmeticWriterFactory
    {
        public static readonly ArithmeticWriterFactoryImpl Instance = new();

        public IH264ArithmeticWriter Create(BitStreamWriter writer, int range, int low, IBinTracker? binTracker)
        {
            throw new NotImplementedException();
        }

        public IH264ArithmeticWriter Create(BitStreamWriter writer, int range, int low, IBinTrackerFactory? binTrackerFactory)
        {
            throw new NotImplementedException();
        }
    }
}
