namespace ContentDotNet.Api.Pictures.Implementation
{
    using ContentDotNet.Api.Colors;
    using ContentDotNet.Api.Pictures;

    internal class DefaultSinglePictureCache<T> : ISinglePictureCache<T>
        where T : unmanaged, IColor
    {
        private Picture<T>? _cache = null;

        public bool Cached { get; private set; } = false;

        public void Dispose()
        {
            _cache?.Dispose();
            _cache = null; // hint the GC to collect it
            GC.SuppressFinalize(this);
        }

        public Picture<T> GetPicture()
        {
            return _cache ?? throw new InvalidOperationException("Picture was not cached");
        }

        public void StorePicture(Picture<T> picture)
        {
            _cache ??= picture;
            Cached = true;
        }
    }
}
