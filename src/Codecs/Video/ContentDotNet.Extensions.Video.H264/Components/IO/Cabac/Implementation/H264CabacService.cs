namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Implementation
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;

    internal class H264CabacService : IH264CabacService
    {
        public IH264CabacDecoder CreateDecoder(IH264ArithmeticReader arithmeticReader, H264State state)
        {
            return new H264CabacDecoder(arithmeticReader, state);
        }
    }
}
