namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine
{
    internal static class ArithmeticThrowHelper
    {
        private static readonly InvalidOperationException s_noContextVariable = new("Context variables are required when decoding a decision bin");
        private static readonly InvalidOperationException s_offsetIs510or511 = new("The offset value cannot be 510 or 511. Try specifying a smaller value.");

        public static InvalidOperationException NoContextVariable() => s_noContextVariable;

        public static InvalidOperationException OffsetIs510Or511() => s_offsetIs510or511;
    }
}
