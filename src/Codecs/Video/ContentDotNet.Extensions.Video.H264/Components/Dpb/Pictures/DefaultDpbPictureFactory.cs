namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    using ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures.Implementation;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks;

    /// <summary>
    ///   Default DPB picture factory.
    /// </summary>
    public class DefaultDpbPictureFactory : IDpbPictureFactory
    {
        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly DefaultDpbPictureFactory Instance = new();

        /// <inheritdoc cref="IDpbPictureFactory.CreateComplementaryFieldPair(PictureDescriptor, PictureDescriptor, H264State?)" />
        public ComplementaryFieldPair CreateComplementaryFieldPair(PictureDescriptor top, PictureDescriptor bottom, H264State? state)
        {
            return new DpbComplementaryFieldPair(top, bottom, state);
        }

        /// <inheritdoc cref="IDpbPictureFactory.CreateField(H264PictureSide, FramePicture, H264State)" />
        public FieldPicture CreateField(H264PictureSide side, FramePicture picture, H264State state)
        {
            return new DpbFieldPicture(side, picture, state);
        }

        /// <inheritdoc cref="IDpbPictureFactory.CreateFrame(H264ReferencePictureImage, H264State)" />
        public FramePicture CreateFrame(H264ReferencePictureImage picture, H264State state)
        {
            return new DpbFrame(picture, state);
        }
    }
}
