namespace ContentDotNet.Extensions.Video.H264
{
    using System.Diagnostics.CodeAnalysis;

    internal static class ThrowHelper
    {
        private static readonly InvalidOperationException s_noRBSPState = new("Missing RBSP State");

        [DoesNotReturn]
        public static T RbspStateUnavailable<T>() where T : unmanaged => throw s_noRBSPState;
    }
}
