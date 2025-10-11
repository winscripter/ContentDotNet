namespace ContentDotNet.Extensions.Video.H264.Models
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using System.Collections.Generic;

    public record H264MacroblockInfo
    {
        public H264SliceType SliceType { get; set; }

        public RbspMacroblockLayer Rbsp { get; set; }

        public bool Inferred { get; set; }

        public bool MbSkipFlag { get; set; }

        public bool MbFieldDecodingFlag { get; set; }

        public H264MacroblockInfo(H264SliceType sliceType, RbspMacroblockLayer rbsp, bool inferred)
        {
            SliceType = sliceType;
            Rbsp = rbsp;
            Inferred = inferred;
        }

        public static bool operator ==(H264MacroblockInfo info, H264MacroblockType type)
        {
            if (type.Inferred)
                return info.Inferred == type.Inferred && info.SliceType == type.SliceType;

            return info.SliceType == type.SliceType &&
                info.Rbsp.MbType == type.MacroblockTypeNumber;
        }

        public static bool operator !=(H264MacroblockInfo info, H264MacroblockType type) => !(info == type);

        public override int GetHashCode()
        {
            return HashCode.Combine(SliceType, Rbsp, Inferred, MbSkipFlag);
        }
    }
}
