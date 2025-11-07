namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Video.Codecs.H264.Rbsp;

    public enum H264SliceType
    {
        I,
        P,
        B,
        SI,
        SP
    }

    /// <summary>
    ///   Kinds of H.264 slices.
    /// </summary>
    public static class H264SliceTypes
    {
        /// <summary>
        ///   P slice
        /// </summary>
        public static readonly H264SliceTypeValuePair P = new(0, 5);

        /// <summary>
        ///   B slice
        /// </summary>
        public static readonly H264SliceTypeValuePair B = new(1, 8);

        /// <summary>
        ///   I slice
        /// </summary>
        public static readonly H264SliceTypeValuePair I = new(2, 7);

        /// <summary>
        ///   SP slice
        /// </summary>
        public static readonly H264SliceTypeValuePair SP = new(3, 8);
        
        /// <summary>
        ///   SI slice
        /// </summary>
        public static readonly H264SliceTypeValuePair SI = new(4, 9);

        public static H264SliceType Derive(int sliceType)
        {
            if (sliceType == P) return H264SliceType.P;
            if (sliceType == B) return H264SliceType.B;
            if (sliceType == I) return H264SliceType.I;
            if (sliceType == SP) return H264SliceType.SP;
            if (sliceType == SI) return H264SliceType.SI;
            throw new InvalidOperationException("Invalid slice type");
        }
    }

    public readonly record struct H264SliceTypeValuePair(int Main, int Alternative)
    {
        public static bool operator ==(RbspSliceHeader sh, H264SliceTypeValuePair pair) => sh.SliceType == pair.Main || sh.SliceType == pair.Alternative;
        public static bool operator !=(RbspSliceHeader sh, H264SliceTypeValuePair pair) => !(sh == pair);

        public static bool operator ==(int sh, H264SliceTypeValuePair pair) => sh == pair.Main || sh == pair.Alternative;
        public static bool operator !=(int sh, H264SliceTypeValuePair pair) => !(sh == pair);
    }

    public static class H264Slice
    {
    }
}
