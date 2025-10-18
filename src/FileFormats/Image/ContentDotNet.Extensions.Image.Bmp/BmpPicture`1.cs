namespace ContentDotNet.Extensions.Image.Bmp
{
    using ContentDotNet.Colors;
    using ContentDotNet.Pictures;
    using System.Drawing;

    public class BmpPicture : Picture<Rgba>
    {
        private readonly Picture<Rgba> _innerPicture;

        public BmpPicture(int width, int height, IPictureFactory factory)
        {
            _innerPicture = factory.CreatePicture<Rgba>(width, height);
        }

        public BmpPicture(int width, int height)
            : this(width, height, MemoryPictureFactory.Instance)
        {
        }

        public override Rgba this[int x, int y]
        {
            get => _innerPicture[x, y];
            set => _innerPicture[x, y] = value;
        }

        public override Size ImageSize => _innerPicture.ImageSize;

        public override void Dispose()
        {
            _innerPicture.Dispose();
            GC.SuppressFinalize(this);
        }

        private static BmpPicture Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
