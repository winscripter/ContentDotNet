namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories.Implementation
{
    internal class ArithmeticFactoryService : IH264ArithmeticFactoryService
    {
        public IH264ArithmeticReaderFactory CreateArithmeticReaderFactory()
        {
            return ArithmeticReaderFactoryImpl.Instance;
        }

        public IH264ArithmeticWriterFactory CreateArithmeticWriterFactory()
        {
            throw new NotImplementedException();
        }

        public IBinTrackerFactory CreateBinTrackerFactory()
        {
            return BinTrackerFactoryImpl.Instance;
        }
    }
}
