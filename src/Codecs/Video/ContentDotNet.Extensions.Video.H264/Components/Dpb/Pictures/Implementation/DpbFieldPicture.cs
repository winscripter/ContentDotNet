namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures.Implementation
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks;
    using ContentDotNet.Pictures;
    using System;
    using System.Collections.Generic;

    internal class DpbFieldPicture : FieldPicture, IEquatable<DpbFieldPicture?>
    {
        public override H264PictureSide Side { get; set; }
        public override FramePicture Picture { get; set; }

        public DpbFieldPicture(H264PictureSide side, FramePicture picture, H264State state)
        {
            Side = side;
            Picture = picture;
            State = state;
        }

        public override Picture<YCbCr> GetAndCacheRaw(ISinglePictureCache<YCbCr> cache, IPictureFactory? pictureFactory = null)
        {
            return cache.Cached ? cache.GetPicture() : CachePicture(cache);
        }

        private Picture<YCbCr> CachePicture(ISinglePictureCache<YCbCr> cache)
        {
            Picture<YCbCr> pic = Picture.Picture.GetImage();
            cache.StorePicture(pic);
            return pic;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DpbFieldPicture);
        }

        public bool Equals(DpbFieldPicture? other)
        {
            return other is not null &&
                   EqualityComparer<H264State?>.Default.Equals(State, other.State) &&
                   Side == other.Side;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(State, Side, Picture);
        }

        public static bool operator ==(DpbFieldPicture? left, DpbFieldPicture? right)
        {
            return EqualityComparer<DpbFieldPicture>.Default.Equals(left, right);
        }

        public static bool operator !=(DpbFieldPicture? left, DpbFieldPicture? right)
        {
            return !(left == right);
        }
    }
}
