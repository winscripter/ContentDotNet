namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures.Implementation
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks;
    using ContentDotNet.Pictures;
    using System;
    using System.Collections.Generic;

    internal class DpbFrame : FramePicture, IEquatable<DpbFrame?>
    {
        public override H264ReferencePictureImage Picture { get; }

        public DpbFrame(H264ReferencePictureImage picture, H264State state)
        {
            Picture = picture;
            State = state;
        }

        public override Picture<YCbCr> GetAndCacheRaw(ISinglePictureCache<YCbCr> cache, IPictureFactory? pictureFactory = null)
        {
            return cache.Cached ? cache.GetPicture() : CachePicture(cache);
        }

        private Picture<YCbCr> CachePicture(ISinglePictureCache<YCbCr> cache)
        {
            var pic = Picture.GetImage();
            cache.StorePicture(pic);
            return pic;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DpbFrame);
        }

        public bool Equals(DpbFrame? other)
        {
            return other is not null &&
                   EqualityComparer<H264State?>.Default.Equals(State, other.State);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(State);
        }

        public static bool operator ==(DpbFrame? left, DpbFrame? right)
        {
            return EqualityComparer<DpbFrame>.Default.Equals(left, right);
        }

        public static bool operator !=(DpbFrame? left, DpbFrame? right)
        {
            return !(left == right);
        }
    }
}
