namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;

    /// <summary>
    ///   DPB picture utilities
    /// </summary>
    public static class DpbPictureUtilities
    {
        /// <summary>
        ///   Is <paramref name="pic"/> a top field?
        /// </summary>
        /// <param name="pic">The picture</param>
        /// <returns>A boolean</returns>
        public static bool IsTopField(this DpbPicture pic)
            => pic is FieldPicture fp && fp.Side == H264PictureSide.Top;

        /// <summary>
        ///   Is <paramref name="pic"/> a bottom field?
        /// </summary>
        /// <param name="pic">The picture</param>
        /// <returns>A boolean</returns>
        public static bool IsBottomField(this DpbPicture pic)
            => pic is FieldPicture fp && fp.Side == H264PictureSide.Bottom;

        /// <summary>
        ///   <paramref name="pic"/> has the same parity as <paramref name="currState"/>?
        /// </summary>
        /// <param name="pic">The reference picture</param>
        /// <param name="currState">The current H.264 state</param>
        /// <returns>A boolean</returns>
        public static bool IsSameParity(this DpbPicture pic, H264State currState)
        {
            return pic.State?.H264RbspState?.SliceHeader?.FieldPicFlag == currState.H264RbspState?.SliceHeader?.FieldPicFlag &&
                   pic.State?.H264RbspState?.SliceHeader?.BottomFieldFlag == currState.H264RbspState?.SliceHeader?.BottomFieldFlag;
        }
    }
}
