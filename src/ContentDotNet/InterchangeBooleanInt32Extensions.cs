namespace ContentDotNet
{
    using ContentDotNet.Primitives;

    public static class InterchangeBooleanInt32Extensions
    {
        public static int AsInt32(this bool value) => Int32Boolean.I32(value);
        public static bool AsBoolean(this int value) => Int32Boolean.B(value);
    }
}
