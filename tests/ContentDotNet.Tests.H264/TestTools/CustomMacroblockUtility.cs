namespace ContentDotNet.Tests.H264.TestTools
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    internal class CustomMacroblockUtility(uint sliceType, H264MacroblockInfo[] macroblocks) : IMacroblockUtility
    {
        public uint SliceType => sliceType;
        public H264MacroblockInfo GetMacroblock(int mbAddr) => macroblocks[mbAddr];
        public H264SliceType GetSliceType(H264MacroblockInfo mb) => mb.SliceType;
        public bool IsFrame(int mbAddr) => !GetMacroblock(mbAddr).MbFieldDecodingFlag;
        public bool IsFrame(H264MacroblockInfo mb) => mb.MbFieldDecodingFlag;
        public bool IsMacroblock(int mbAddr) => mbAddr < macroblocks.Length;
        public void Infer(int mbAddr) => throw new NotImplementedException();
    }
}
