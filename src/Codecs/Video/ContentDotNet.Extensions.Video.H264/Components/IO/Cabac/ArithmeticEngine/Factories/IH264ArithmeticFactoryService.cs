namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine.Factories
{
    /// <summary>
    ///   H.264 arithmetic factory service.
    /// </summary>
    public interface IH264ArithmeticFactoryService
    {
        IH264ArithmeticReaderFactory CreateArithmeticReaderFactory();
        IH264ArithmeticWriterFactory CreateArithmeticWriterFactory();
        IBinTrackerFactory CreateBinTrackerFactory();
    }
}
