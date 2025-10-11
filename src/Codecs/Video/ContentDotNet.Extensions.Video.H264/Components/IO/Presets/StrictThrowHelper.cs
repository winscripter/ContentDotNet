namespace ContentDotNet.Extensions.Video.H264.Components.IO.Presets
{
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using System.Diagnostics.CodeAnalysis;

    internal static class StrictThrowHelper
    {
        private static readonly MismatchedEntropyException s_cavlc = new("Attempted to read a CAVLC syntax element under a CABAC reader");
        private static readonly MismatchedEntropyException s_cabac = new("Attempted to read a CABAC syntax element under a CAVLC reader");

        [DoesNotReturn]
        public static void ThrowCavlcOnCabac() => throw s_cavlc;

        [DoesNotReturn]
        public static void ThrowCabacOnCavlc() => throw s_cabac;
    }
}
