namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks
{
    internal static class ResidualBlockThrowHelper
    {
        private static readonly InvalidOperationException s_missingResidualBlock = new("Missing residual block");

        public static InvalidOperationException MissingResidualBlock() => s_missingResidualBlock;
    }
}
