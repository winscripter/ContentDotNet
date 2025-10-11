namespace ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures.Implementation
{
    using ContentDotNet.Colors;
    using ContentDotNet.Pictures;
    using System;
    using System.Collections.Generic;

    internal class DpbComplementaryFieldPair : ComplementaryFieldPair, IEquatable<DpbComplementaryFieldPair?>
    {
        public override PictureDescriptor Top { get; }
        public override PictureDescriptor Bottom { get; }

        public DpbComplementaryFieldPair(PictureDescriptor top, PictureDescriptor bottom, H264State? state)
        {
            Top = top;
            Bottom = bottom;
            State = state;
        }

        public override Picture<YCbCr> GetAndCacheRaw(ISinglePictureCache<YCbCr> cache, IPictureFactory? picFactory = null)
        {
            if (cache.Cached)
            {
                return cache.GetPicture();
            }
            else
            {
                return ProcessImage(cache, picFactory);
            }
        }

        private Picture<YCbCr> ProcessImage(ISinglePictureCache<YCbCr> cache, IPictureFactory? picFactory)
        {
            var Top = this.Top.Picture.GetAndCacheRaw(SinglePictureCacheProvider.CreateDefaultSinglePictureCache<YCbCr>());
            var Bottom = this.Bottom.Picture.GetAndCacheRaw(SinglePictureCacheProvider.CreateDefaultSinglePictureCache<YCbCr>());

            var newImage = (picFactory ?? MemoryPictureFactory.Instance).CreatePicture<YCbCr>(Top.ImageSize.Width, Top.ImageSize.Height + Bottom.ImageSize.Height);
            
            for (int i = 0; i < Top.ImageSize.Height + Bottom.ImageSize.Height; i++)
            {
                if (i % 2 == 0)
                {
                    for (int x = 0; x < Top.ImageSize.Width; x++)
                        newImage[i, x] = Top[i, x];
                }
                else
                {
                    for (int x = 0; x < Bottom.ImageSize.Width; x++)
                        newImage[i, x] = Top[i, x];
                }
            }

            cache.StorePicture(newImage);

            return newImage;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DpbComplementaryFieldPair);
        }

        public bool Equals(DpbComplementaryFieldPair? other)
        {
            return other is not null &&
                   EqualityComparer<H264State?>.Default.Equals(State, other.State);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(State);
        }

        public static bool operator ==(DpbComplementaryFieldPair? left, DpbComplementaryFieldPair? right)
        {
            return EqualityComparer<DpbComplementaryFieldPair>.Default.Equals(left, right);
        }

        public static bool operator !=(DpbComplementaryFieldPair? left, DpbComplementaryFieldPair? right)
        {
            return !(left == right);
        }
    }
}
