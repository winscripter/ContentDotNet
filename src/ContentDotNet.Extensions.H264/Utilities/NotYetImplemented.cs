namespace ContentDotNet.Extensions.H264.Utilities;

internal static class NotYetImplemented
{
    static readonly Exception s_nyi = new InvalidOperationException("Not yet implemented: should be implemented later");

    public static void ImplementLater() => throw s_nyi;
}
