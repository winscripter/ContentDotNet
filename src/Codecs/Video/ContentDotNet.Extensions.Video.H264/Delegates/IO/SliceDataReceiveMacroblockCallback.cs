namespace ContentDotNet.Extensions.Video.H264.Delegates.IO
{
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   A delegate that is invoked during the parsing of slice data, when a new macroblock is parsed/parsing.
    /// </summary>
    /// <param name="mb">The macroblock that was received.</param>
    public delegate void SliceDataReceiveMacroblockCallback(H264MacroblockInfo mb);
}
