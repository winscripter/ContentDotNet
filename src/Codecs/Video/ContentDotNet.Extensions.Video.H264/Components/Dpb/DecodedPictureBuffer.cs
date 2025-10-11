namespace ContentDotNet.Extensions.Video.H264.Components.Dpb
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using ContentDotNet.Pictures;

    public class DecodedPictureBuffer : IDecodedPictureBuffer
    {
        public int MaxSize { get; set; }

        public List<PictureDescriptor> Descriptors { get; } = [];

        public PictureDescriptor this[int idx]
        {
            get => Descriptors[idx];
            set => Descriptors[idx] = value;
        }

        public void Add(
             Picture<YCbCr> yuvPic,
             H264SliceType sliceType,
             bool isIDR,
             RefPicListModificationEntry? refPicListModification,
             MemoryManagementControl? pictureMmc)
        {
            if (isIDR)
            {
                Descriptors.Clear();
            }

            if (refPicListModification is not null)
            {
                ApplyRefPicListModification(refPicListModification);
            }

            // Evict if needed
            if (Descriptors.Count >= MaxSize)
            {
                var toRemove = Descriptors.FirstOrDefault(d => !d.UsedForReference);
                if (toRemove != null)
                    Descriptors.Remove(toRemove);
            }

            var descriptor = new PictureDescriptor(
                usedForReference: sliceType != H264SliceType.B, // B-slices typically aren't used for reference
                longevity: pictureMmc?.LongTermFrameIdx is not null ? PictureDuration.LongTerm : PictureDuration.ShortTerm,
                frameNumber: GetNextFrameNumber(),
                poc: new H264PictureOrderCount(0, 0, 0, 0), // Placeholder POC
                picture: new RawDpbPicture(yuvPic)
            )
            {
                Mmco = (int)(pictureMmc?.MemoryManagementControlOperation ?? 0),
                LongTermFrameIdx = (int)(pictureMmc?.LongTermFrameIdx ?? 0)
            };

            Descriptors.Add(descriptor);
        }

        private static void ApplyRefPicListModification(RefPicListModificationEntry entry)
        {
            // TODO
        }

        public void MarkAllUnused()
        {
            foreach (var descriptor in Descriptors)
            {
                descriptor.UsedForReference = false;
            }
        }

        public void LogPictures(TextWriter textWriter)
        {
            foreach (var descriptor in Descriptors)
            {
                textWriter.WriteLine($"Frame {descriptor.FrameNumber}, Ref: {descriptor.UsedForReference}, POC: {descriptor.Poc.PictureOrderCount}");
            }
        }

        public void OnStartOfNewSlice(bool isIDR)
        {
            if (isIDR)
            {
                Descriptors.Clear();
            }
            else
            {
                // Non-IDR slice: no action needed for this basic implementation
            }
        }

        private int GetNextFrameNumber()
        {
            return Descriptors.Count > 0 ? Descriptors.Max(d => d.FrameNumber) + 1 : 0;
        }

        // A simple concrete implementation of DpbPicture
        private class RawDpbPicture : DpbPicture
        {
            private readonly Picture<YCbCr> _picture;

            public RawDpbPicture(Picture<YCbCr> picture)
            {
                _picture = picture;
            }

            public override Picture<YCbCr> GetAndCacheRaw(ISinglePictureCache<YCbCr> cache, IPictureFactory? pictureFactory = null)
            {
                return _picture;
            }
        }
    }

}
