namespace ContentDotNet.Extensions.Video.H264.Convert
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   Convert FMO <see cref="H264RefinedSliceGroupMapType"/>.
    /// </summary>
    public static class ConvertFmoRefinedSliceGroupMapType
    {
        /// <summary>
        ///   Gets the refined slice group map type.
        /// </summary>
        /// <param name="sliceGroupMapType">Slice group map type</param>
        /// <param name="sliceGroupChangeDirectionFlag">Change direction flag</param>
        /// <returns>The <see cref="H264RefinedSliceGroupMapType"/> enum.</returns>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="sliceGroupMapType"/> is not within the range of 3 and 5.</exception>
        public static H264RefinedSliceGroupMapType GetRefinedSliceGroupMapType(int sliceGroupMapType, bool sliceGroupChangeDirectionFlag)
        {
            return (sliceGroupMapType, sliceGroupChangeDirectionFlag) switch
            {
                (3, false) => H264RefinedSliceGroupMapType.BoxOutClockwise,
                (3, true) => H264RefinedSliceGroupMapType.BoxOutCounterclockwise,
                (4, false) => H264RefinedSliceGroupMapType.RasterScan,
                (4, true) => H264RefinedSliceGroupMapType.ReverseRasterScan,
                (5, false) => H264RefinedSliceGroupMapType.WipeRight,
                (5, true) => H264RefinedSliceGroupMapType.WipeLeft,
                _ => throw new InvalidOperationException("Cannot get FMO Refined Slice Group Map Type: Slice group map type must be in range between 3 and 5")
            };
        }

        /// <summary>
        ///   Can get refined slice group map type?
        /// </summary>
        /// <param name="sliceGroupMapType">Slice group map type</param>
        /// <returns>A boolean, indicating if <paramref name="sliceGroupMapType"/> is within the range of 3 and 5.</returns>
        public static bool CanGetRefinedSliceGroupMapType(int sliceGroupMapType)
        {
            return sliceGroupMapType is >= 3 and <= 5;
        }

        /// <summary>
        ///   Return slice group fields for <paramref name="refinedSliceGroupMapType"/>.
        /// </summary>
        /// <param name="refinedSliceGroupMapType">The source refined slice group map type.</param>
        /// <returns>Fields necessary to encode <paramref name="refinedSliceGroupMapType"/> back.</returns>
        /// <exception cref="InvalidOperationException">Rarely thrown. Only thrown if <paramref name="refinedSliceGroupMapType"/> does not contain a valid/defined value.</exception>
        public static (int sliceGroupMapType, bool sliceGroupChangeDirectionFlag) GetFields(H264RefinedSliceGroupMapType refinedSliceGroupMapType)
        {
            return refinedSliceGroupMapType switch
            {
                H264RefinedSliceGroupMapType.BoxOutClockwise => (3, false),
                H264RefinedSliceGroupMapType.BoxOutCounterclockwise => (3, true),
                H264RefinedSliceGroupMapType.RasterScan => (4, false),
                H264RefinedSliceGroupMapType.ReverseRasterScan => (4, true),
                H264RefinedSliceGroupMapType.WipeRight => (5, false),
                H264RefinedSliceGroupMapType.WipeLeft => (5, true),
                _ => throw new InvalidOperationException("Not a valid refined slice group map type")
            };
        }
    }
}
