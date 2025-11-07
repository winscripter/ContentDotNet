namespace ContentDotNet.Video.Codecs.H264.Components
{
    public interface IMacroblockUtility
    {
        H264Macroblock GetMacroblock(int index);
        H264Macroblock? GetMacroblockOrNull(int index);
        bool IsMacroblock(int index);
        bool IsFrameMacroblock(H264Macroblock mb);
    }
}
