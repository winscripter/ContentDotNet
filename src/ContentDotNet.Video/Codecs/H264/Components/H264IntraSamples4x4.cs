namespace ContentDotNet.Video.Codecs.H264.Components
{
    using System.Runtime.Intrinsics;

    /// <summary>
    ///   Intra samples.
    /// </summary>
    public struct H264IntraSamples4x4
    {
        private Vector128<int> _left;
        private Vector128<int> _top;
        private Vector128<int> _topLeft;
        private Vector128<int> _topRight;

        private bool _leftAvailable = true;
        private bool _topAvailable = true;
        private bool _topLeftAvailable = true;
        private bool _topRightAvailable = true;

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264IntraSamples4x4"/> struct.
        /// </summary>
        public H264IntraSamples4x4()
        {
        }

        public Vector128<int> Left
        {
            readonly get => _left;
            set => _left = value;
        }

        public Vector128<int> Top
        {
            readonly get => _top;
            set => _top = value;
        }

        public Vector128<int> TopLeft
        {
            readonly get => _topLeft;
            set => _topLeft = value;
        }

        public Vector128<int> TopRight
        {
            readonly get => _topRight;
            set => _topRight = value;
        }

        public bool LeftAvailable
        {
            readonly get => _leftAvailable;
            set => _leftAvailable = value;
        }

        public bool TopAvailable
        {
            readonly get => _topAvailable;
            set => _topAvailable = value;
        }

        public bool TopLeftAvailable
        {
            readonly get => _topLeftAvailable;
            set => _topLeftAvailable = value;
        }

        public bool TopRightAvailable
        {
            readonly get => _topRightAvailable;
            set => _topRightAvailable = value;
        }
    }
}
