namespace ContentDotNet.Extensions.Video.H264.Components.SliceDecoding
{
    using ContentDotNet.Extensions.Video.H264.Components.Dpb;
    using ContentDotNet.Extensions.Video.H264.Models;
    using System.Security.Cryptography;

    /// <summary>
    ///   The slice decoder.
    /// </summary>
    public interface ISliceDecoder
    {
        /// <summary>
        ///   Derives the POC.
        /// </summary>
        /// <param name="picX">The picture to get POC of.</param>
        /// <param name="prevPic">The previous picture.</param>
        /// <param name="frameNumOffsetOfPrevFrameIfPocTypeIs1">FrameNumOffset of previous frame</param>
        /// <returns>Picture order count</returns>
        int PictureOrderCount(PictureDescriptor picX, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1);

        /// <summary>
        ///   Returns the difference between provided two POCs.
        /// </summary>
        /// <param name="pocA">POC A</param>
        /// <param name="pocB">POC B</param>
        /// <returns>The POC difference</returns>
        int DiffPicOrderCnt(int pocA, int pocB);

        /// <summary>
        ///   Derives the picture order counts AND the top/bottom field order counts as well as MSB.
        /// </summary>
        /// <param name="currPic">The picture to derive POC of.</param>
        /// <param name="prevPic">Previous picture.</param>
        /// <param name="frameNumOffsetOfPrevFrameIfPocTypeIs1">FrameNumOffset of previous frame</param>
        /// <returns>Picture order count</returns>
        H264PictureOrderCount DerivePictureOrderCounts(PictureDescriptor currPic, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1);

        /// <summary>
        ///   Populate <paramref name="mapUnitToSliceGroupMap"/> with map unit to slice group map.
        /// </summary>
        /// <param name="h264">The H.264 state</param>
        /// <param name="mapUnitToSliceGroupMap">The buffer where map unit to slice group maps are added.</param>
        void PopulateWithMapUnitToSliceGroupMap(H264State h264, IList<int> mapUnitToSliceGroupMap);

        /// <summary>
        ///   Convert <paramref name="mapUnitToSliceGroupMap"/> to <paramref name="macroblockToSliceGroupMap"/>.
        /// </summary>
        /// <param name="h264">The state</param>
        /// <param name="mapUnitToSliceGroupMap">Map unit to slice group map is read here. See <see cref="PopulateWithMapUnitToSliceGroupMap(H264State, IList{int})"/></param>
        /// <param name="macroblockToSliceGroupMap">Macroblock to slice group map is added here.</param>
        void ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap(H264State h264, IList<int> mapUnitToSliceGroupMap, IList<int> macroblockToSliceGroupMap);

        /// <summary>
        ///   Computes the macroblock address after <paramref name="n"/>.
        /// </summary>
        /// <param name="h264">The H.264 state</param>
        /// <param name="mbToSliceGroupMap">See <see cref="ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap(H264State, IList{int}, IList{int})"/></param>
        /// <param name="n">The current macroblock address</param>
        /// <returns>The next macroblock address.</returns>
        int NextMbAddress(H264State h264, IList<int> mbToSliceGroupMap, int n);
    }
}
