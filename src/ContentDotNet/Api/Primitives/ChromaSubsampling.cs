namespace ContentDotNet.Api.Primitives
{
    /// <summary>
    /// Immutable chroma subsampling representation with singleton schemes.
    /// </summary>
    public readonly struct ChromaSubsampling : IEquatable<ChromaSubsampling>
    {
        /// <summary>
        /// Singleton chroma subsampling with value of 4:0:0. In essence, this is a
        /// grayscale image.
        /// </summary>
        public static readonly ChromaSubsampling Shared400 = new(4, 0, 0);

        /// <summary>
        /// Singleton chroma subsampling with value of 4:1:1. Less common but is used in
        /// some older videos.
        /// </summary>
        public static readonly ChromaSubsampling Shared411 = new(4, 1, 1);

        /// <summary>
        /// Singleton chroma subsampling with value of 4:2:0. Widely used in consumer video
        /// formats like Blu-ray and streaming services.
        /// </summary>
        public static readonly ChromaSubsampling Shared420 = new(4, 2, 0);

        /// <summary>
        /// Singleton chroma subsampling with value of 4:2:2. Common in professional video
        /// systems.
        /// </summary>
        public static readonly ChromaSubsampling Shared422 = new(4, 2, 2);

        /// <summary>
        /// Singleton chroma subsampling with value of 4:4:4. This retains the highest color
        /// fidelity and is used in high-quality video applications.
        /// </summary>
        public static readonly ChromaSubsampling Shared444 = new(4, 4, 4);

        /// <summary>
        /// Samples for the Y (Luminance) component.
        /// </summary>
        public readonly byte Y;

        /// <summary>
        /// Samples for the U (Chrominance #1) component.
        /// </summary>
        public readonly byte U;

        /// <summary>
        /// Samples for the V (Chrominance #2) component.
        /// </summary>
        public readonly byte V;

        public ChromaSubsampling(byte y, byte u, byte v)
        {
            if (y != 4) throw new ArgumentException("Y component of Chroma Subsampling shall be 4", nameof(y));

            Y = y;
            U = u;
            V = v;
        }

        public override bool Equals(object? obj)
        {
            return obj is ChromaSubsampling subsampling && Equals(subsampling);
        }

        public bool Equals(ChromaSubsampling other)
        {
            return Y == other.Y &&
                   U == other.U &&
                   V == other.V;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Y, U, V);
        }

        public static bool operator ==(ChromaSubsampling left, ChromaSubsampling right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ChromaSubsampling left, ChromaSubsampling right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"4:{U}:{V}";
        }
    }
}
