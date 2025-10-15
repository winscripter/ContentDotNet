namespace ContentDotNet.Extensions.Video.Mp4
{
    using ContentDotNet.Primitives;

    /// <summary>
    ///   The MP4 box.
    /// </summary>
    public class Mp4Box
    {
        private IMp4BoxData? data;
        private int size;
        private FourCC type;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Mp4Box"/> class.
        /// </summary>
        public Mp4Box()
        {
        }

        /// <summary>
        ///   The MP4 box data.
        /// </summary>
        public IMp4BoxData? Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        /// <summary>
        ///   The box size.
        /// </summary>
        public int Size
        {
            get => size;
            set => size = value;
        }

        /// <summary>
        ///   The box type.
        /// </summary>
        public FourCC Type
        {
            get => type;
            set => type = value;
        }
    }
}
