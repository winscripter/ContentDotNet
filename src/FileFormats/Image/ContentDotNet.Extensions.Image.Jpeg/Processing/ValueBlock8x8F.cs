namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    internal readonly ref struct ValueBlock8x8F
    {
        private readonly Span<float> data;

        public ValueBlock8x8F(Span<float> data) => this.data = data;

        public Span<float> Data => this.data;

        public float this[int x, int y]
        {
            get => data[x * 8 + y];
            set => data[x * 8 + y] = value;
        }
    }
}
