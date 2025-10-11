namespace ContentDotNet.Extensions.Video.H264.Components.Dpb
{
    using ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   The picture descriptor.
    /// </summary>
    public class PictureDescriptor : IEquatable<PictureDescriptor?>
    {
        /// <summary>
        ///   Memory management control operation
        /// </summary>
        public int Mmco { get; set; }

        /// <summary>
        ///   Is this used for reference?
        /// </summary>
        public bool UsedForReference { get; set; }

        /// <summary>
        ///   The picture duration.
        /// </summary>
        public PictureDuration Duration { get; set; }

        /// <summary>
        ///   The frame number.
        /// </summary>
        public int FrameNumber { get; set; }

        /// <summary>
        ///   The Dpb picture.
        /// </summary>
        public DpbPicture Picture { get; set; }

        /// <summary>
        ///   Long term frame index.
        /// </summary>
        public int? LongTermFrameIdx { get; set; }

        /// <summary>
        ///   Has been outputted?
        /// </summary>
        public bool HasBeenOutputted { get; set; } = false;

        /// <summary>
        ///   Picture order count
        /// </summary>
        public H264PictureOrderCount Poc { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="PictureDescriptor"/> class.
        /// </summary>
        /// <param name="usedForReference"></param>
        /// <param name="longevity"></param>
        /// <param name="frameNumber"></param>
        /// <param name="poc"></param>
        /// <param name="picture"></param>
        public PictureDescriptor(bool usedForReference, PictureDuration longevity, int frameNumber, H264PictureOrderCount poc, DpbPicture picture)
        {
            UsedForReference = usedForReference;
            Duration = longevity;
            FrameNumber = frameNumber;
            Poc = poc;
            Picture = picture;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as PictureDescriptor);
        }

        public bool Equals(PictureDescriptor? other)
        {
            return other is not null &&
                   Mmco == other.Mmco &&
                   UsedForReference == other.UsedForReference &&
                   Duration == other.Duration &&
                   FrameNumber == other.FrameNumber &&
                   EqualityComparer<DpbPicture>.Default.Equals(Picture, other.Picture) &&
                   LongTermFrameIdx == other.LongTermFrameIdx &&
                   HasBeenOutputted == other.HasBeenOutputted &&
                   EqualityComparer<H264PictureOrderCount>.Default.Equals(Poc, other.Poc);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Mmco, UsedForReference, Duration, FrameNumber, Picture, LongTermFrameIdx, HasBeenOutputted, Poc);
        }

        public static bool operator ==(PictureDescriptor? left, PictureDescriptor? right)
        {
            return EqualityComparer<PictureDescriptor>.Default.Equals(left, right);
        }

        public static bool operator !=(PictureDescriptor? left, PictureDescriptor? right)
        {
            return !(left == right);
        }
    }
}
