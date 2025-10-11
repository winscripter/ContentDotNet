namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation
{
    using ContentDotNet.Extensions.Video.H264.Exceptions;

    internal static class IntraThrowHelper
    {
        private static readonly IntraPredictionException s_ipsNeighboringPixelsUnavailable = new("Neighboring pixels unavailable");
        private static readonly IntraPredictionException s_noH264State = new("Missing H.264 state (did you assign the H264State property yet?)");

        public static IntraPredictionException NeighboringPixelsUnavailable() => s_ipsNeighboringPixelsUnavailable;
        public static IntraPredictionException NoH264State() => s_noH264State;
    }
}
