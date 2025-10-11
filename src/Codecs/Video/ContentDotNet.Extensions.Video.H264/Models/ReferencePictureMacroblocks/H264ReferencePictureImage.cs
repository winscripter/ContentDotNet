namespace ContentDotNet.Extensions.Video.H264.Models.ReferencePictureMacroblocks
{
    using ContentDotNet.Colors;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   H.264 reference picture image
    /// </summary>
    public class H264ReferencePictureImage
    {
        /// <summary>
        ///   The macroblocks.
        /// </summary>
        public H264ReferencePictureMacroblock[,] Macroblocks { get; set; }

        public H264ReferencePictureImage(H264ReferencePictureMacroblock[,] macroblocks)
        {
            Macroblocks = macroblocks;
        }

        public Picture<YCbCr> GetImage(IPictureFactory? factory = null)
        {
            int iW = Macroblocks.GetLength(0);
            int iH = Macroblocks.GetLength(1);

            Picture<YCbCr> picture = (factory ?? MemoryPictureFactory.Instance).CreatePicture<YCbCr>(iW, iH);

            for (int i = 0; i < iW; i++)
            {
                for (int j = 0; j < iH; j++)
                {
                    H264ReferencePictureMacroblock mb = Macroblocks[i, j];
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            picture[i * 16 + x, j * 16 + y] = mb.Picture16x16[x, y];
                        }
                    }
                }
            }

            return picture;
        }

        public H264ReferencePictureMacroblock this[int i]
        {
            get => Macroblocks[i % Macroblocks.Length, i / Macroblocks.Length];
            set => Macroblocks[i % Macroblocks.Length, i / Macroblocks.Length] = value;
        }
    }
}
