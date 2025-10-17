namespace ContentDotNet.Tests.H264.TestTools
{
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    internal class BogusMacroblockUtility : IMacroblockUtility
    {
        public uint SliceType => throw new NotImplementedException();

        public H264MacroblockInfo GetMacroblock(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public H264SliceType GetSliceType(H264MacroblockInfo mb)
        {
            throw new NotImplementedException();
        }

        public void Infer(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool IsFrame(int mbAddr)
        {
            throw new NotImplementedException();
        }

        public bool IsFrame(H264MacroblockInfo mb)
        {
            throw new NotImplementedException();
        }

        public bool IsMacroblock(int mbAddr)
        {
            throw new NotImplementedException();
        }
    }
}
