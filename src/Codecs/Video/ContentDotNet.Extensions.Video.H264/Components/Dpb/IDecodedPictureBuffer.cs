namespace ContentDotNet.Extensions.Video.H264.Components.Dpb
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   Abstracts the DPB.
    /// </summary>
    public interface IDecodedPictureBuffer
    {
        /// <summary>
        ///   DPB maximum size.
        /// </summary>
        int MaxSize { get; set; }

        /// <summary>
        ///   All pictures.
        /// </summary>
        List<PictureDescriptor> Descriptors { get; }

        /// <summary>
        ///   Adds a picture.
        /// </summary>
        /// <param name="yuvPic">The YUV picture</param>
        /// <param name="sliceType">The slice type</param>
        /// <param name="isIDR">Is the IDR NAL unit?</param>
        /// <param name="refPicListModification">Reference picture list modification</param>
        /// <param name="pictureMmc">Single Memory Management Control from the Decoded Reference Picture Marking from the RBSP</param>
        void Add(Picture<YCbCr> yuvPic, H264SliceType sliceType, bool isIDR, RefPicListModificationEntry? refPicListModification, MemoryManagementControl? pictureMmc);

        /// <summary>
        ///   Marks all frames as unused.
        /// </summary>
        void MarkAllUnused();

        /// <summary>
        ///   Outputs pictures.
        /// </summary>
        /// <param name="textWriter">The text writer</param>
        void LogPictures(TextWriter textWriter);

        /// <summary>
        ///   Prepare when the new slice starts.
        /// </summary>
        /// <param name="isIDR">true - it's an IDR NAL. false - it's a non-IDR NAL.</param>
        void OnStartOfNewSlice(bool isIDR);

        /// <summary>
        ///   Returns the picture from the DPB.
        /// </summary>
        /// <param name="idx">Picture index.</param>
        /// <returns>The pic</returns>
        PictureDescriptor this[int idx] { get; set; }
    }
}
