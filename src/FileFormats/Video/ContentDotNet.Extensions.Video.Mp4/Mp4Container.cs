namespace ContentDotNet.Extensions.Video.Mp4
{
    /// <summary>
    ///   MP4 file stream.
    /// </summary>
    public class Mp4Container
    {
        private readonly List<Mp4Box> _rootBoxes;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Mp4Container"/> class.
        /// </summary>
        public Mp4Container()
        {
            _rootBoxes = [];
        }

        /// <summary>
        ///   Root boxes (a.k.a. atoms).
        /// </summary>
        public IList<Mp4Box> Root => _rootBoxes;
    }
}
