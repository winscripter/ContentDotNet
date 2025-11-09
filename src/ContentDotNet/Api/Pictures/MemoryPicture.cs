namespace ContentDotNet.Api.Pictures
{
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   A picture in memory.
    /// </summary>
    public class MemoryPicture : IPicture
    {
        private readonly List<List<int>> _data;
        private readonly int _planeCount;

        public MemoryPicture(int planeCount)
        {
            _data = [];
            for (int i = 0; i < planeCount; i++)
                _data[i] = [];
            _planeCount = planeCount;
        }

        public void AppendBlankPerPlanePictureData(Size frameSize)
        {
            for (int i = 0; i < frameSize.Width * frameSize.Height; i++)
            {
                for (int j = 0; j < Planes; j++)
                {
                    _data[j].Add(0);
                }
            }
            PictureSize = frameSize;
        }

        public int Planes => _planeCount;

        public int this[int plane, int x, int y]
        {
            get => _data[plane][x * PictureSize.Width + y];
            set => _data[plane][x * PictureSize.Width + y] = value;
        }

        public Size PictureSize { get; set; }

        public void WritePlanePixelData(Stream output, int planeIndex)
        {
            for (int i = 0; i < PictureSize.Width * PictureSize.Height; i++)
            {
                for (int bpp = 0; bpp < BytesPerPixel; bpp++)
                {
                    int number = _data[planeIndex][i];
                    output.WriteByte((byte)((number >> (8 * bpp)) & 0xFF));
                }
            }
        }

        public Task WritePlanePixelDataAsync(Stream output, int planeIndex)
        {
            WritePlanePixelData(output, planeIndex);
            return Task.CompletedTask;
        }

        public int BytesPerPixel { get; set; } = 1;
    }
}
