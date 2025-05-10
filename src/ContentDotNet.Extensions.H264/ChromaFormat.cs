using ContentDotNet.Extensions.H264.Models;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents the Chroma format in H.264.
/// </summary>
public struct ChromaFormat : IEquatable<ChromaFormat>
{
    internal static readonly Dictionary<(uint chromaFormatIdc, bool separateColorPlaneFlag), ChromaFormat> LookupTable = new()
    {
        { (0, false), new ChromaFormat(ChromaSubsampling.Shared400, false, 0, 0) },
        { (1, false), new ChromaFormat(ChromaSubsampling.Shared420, false, 2, 2) },
        { (2, false), new ChromaFormat(ChromaSubsampling.Shared422, false, 2, 1) },
        { (3, false), new ChromaFormat(ChromaSubsampling.Shared444, false, 1, 1) },
        { (3, false), new ChromaFormat(ChromaSubsampling.Shared444, false, 0, 0) },
    };

    internal static ChromaFormat GetSubsamplingAndSize(uint chromaFormatIdc, bool separateColourPlaneFlag) =>
        LookupTable[(chromaFormatIdc, separateColourPlaneFlag)];

    internal static ChromaFormat GetSubsamplingAndSize(SequenceParameterSet sps) =>
        LookupTable[(sps.ChromaFormatIdc, sps.SeparateColourPlaneFlag)];

    /// <summary>
    ///   Shared 1x1 monochrome chroma format.
    /// </summary>
    public static readonly ChromaFormat Monochrome1x1 = new(ChromaSubsampling.Shared400, false, 1);

    /// <summary>
    ///   Shared 2x2 monochrome chroma format.
    /// </summary>
    public static readonly ChromaFormat Monochrome2x2 = new(ChromaSubsampling.Shared400, false, 2);

    /// <summary>
    ///   The subsampling.
    /// </summary>
    public ChromaSubsampling Subsampling { get; set; }

    /// <summary>
    ///   Is the color plane separate?
    /// </summary>
    public bool IsSeparateColorPlane { get; set; }

    /// <summary>
    ///   Represents the width. Similar to the SubWidthC symbol in the specification.
    /// </summary>
    public int ChromaWidth { get; set; }

    /// <summary>
    ///   Represents the height. Similar to the SubHeightC symbol in the specification.
    /// </summary>
    public int ChromaHeight { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromaFormat"/> struct with specified subsampling, color plane separation, width, and height.
    /// </summary>
    /// <param name="subsampling">The chroma subsampling scheme.</param>
    /// <param name="isSeparateColorPlane">Indicates whether the color plane is separate.</param>
    /// <param name="chromaWidth">The width of the chroma format.</param>
    /// <param name="chromaHeight">The height of the chroma format.</param>
    public ChromaFormat(ChromaSubsampling subsampling, bool isSeparateColorPlane, int chromaWidth, int chromaHeight)
    {
        Subsampling = subsampling;
        IsSeparateColorPlane = isSeparateColorPlane;
        ChromaWidth = chromaWidth;
        ChromaHeight = chromaHeight;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromaFormat"/> struct with specified subsampling, color plane separation, and uniform width and height.
    /// </summary>
    /// <param name="subsampling">The chroma subsampling scheme.</param>
    /// <param name="isSeparateColorPlane">Indicates whether the color plane is separate.</param>
    /// <param name="uniformChromaWidthHeight">The uniform width and height of the chroma format.</param>
    public ChromaFormat(ChromaSubsampling subsampling, bool isSeparateColorPlane, int uniformChromaWidthHeight)
        : this(subsampling, isSeparateColorPlane, uniformChromaWidthHeight, uniformChromaWidthHeight)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromaFormat"/> struct with specified subsampling, color plane separation, and chroma size.
    /// </summary>
    /// <param name="subsampling">The chroma subsampling scheme.</param>
    /// <param name="isSeparateColorPlane">Indicates whether the color plane is separate.</param>
    /// <param name="chromaSize">The size of the chroma format.</param>
    public ChromaFormat(ChromaSubsampling subsampling, bool isSeparateColorPlane, Size chromaSize)
        : this(subsampling, isSeparateColorPlane, chromaSize.Width, chromaSize.Height)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromaFormat"/> struct with specified width and height, assuming monochrome format.
    /// </summary>
    /// <param name="chromaWidth">The width of the chroma format.</param>
    /// <param name="chromaHeight">The height of the chroma format.</param>
    public ChromaFormat(int chromaWidth, int chromaHeight)
        : this(ChromaSubsampling.Shared400, true, chromaWidth, chromaHeight)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromaFormat"/> struct by copying another instance.
    /// </summary>
    /// <param name="other">The other <see cref="ChromaFormat"/> instance to copy.</param>
    public ChromaFormat(ChromaFormat other)
        : this(other.Subsampling, other.IsSeparateColorPlane, other.ChromaWidth, other.ChromaHeight)
    {
    }

    /// <summary>
    ///   Is the chroma format monochrome?
    /// </summary>
    public readonly bool IsMonochrome => Subsampling == ChromaSubsampling.Shared400;

    /// <summary>
    /// Determines if the current chroma format matches the specified width and height.
    /// </summary>
    /// <param name="chromaWidth">The width to compare against.</param>
    /// <param name="chromaHeight">The height to compare against.</param>
    /// <returns><c>true</c> if the current chroma format matches the specified width and height; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool IsSize(int chromaWidth, int chromaHeight)
    {
        return ChromaWidth == chromaWidth && ChromaHeight == chromaHeight;
    }

    /// <summary>
    /// Determines if the current chroma format matches the specified size.
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to compare against.</param>
    /// <returns><c>true</c> if the current chroma format matches the specified size; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool IsSize(Size size) => IsSize(size.Width, size.Height);

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="ChromaFormat"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="ChromaFormat"/> instance.</param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current <see cref="ChromaFormat"/> instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is ChromaFormat format && Equals(format);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ChromaFormat"/> instance is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="ChromaFormat"/> instance to compare with the current instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="ChromaFormat"/> instance is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly bool Equals(ChromaFormat other)
    {
        return Subsampling.Equals(other.Subsampling) &&
               IsSeparateColorPlane == other.IsSeparateColorPlane &&
               ChromaWidth == other.ChromaWidth &&
               ChromaHeight == other.ChromaHeight &&
               IsMonochrome == other.IsMonochrome;
    }

    /// <summary>
    /// Serves as the default hash function for the <see cref="ChromaFormat"/> type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="ChromaFormat"/> instance.
    /// </returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Subsampling, IsSeparateColorPlane, ChromaWidth, ChromaHeight, IsMonochrome);
    }


    /// <summary>  
    /// Determines whether two <see cref="ChromaFormat"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="ChromaFormat"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="ChromaFormat"/> instance to compare.</param>  
    /// <returns><c>true</c> if the two <see cref="ChromaFormat"/> instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(ChromaFormat left, ChromaFormat right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    /// Determines whether two <see cref="ChromaFormat"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="ChromaFormat"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="ChromaFormat"/> instance to compare.</param>  
    /// <returns><c>true</c> if the two <see cref="ChromaFormat"/> instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(ChromaFormat left, ChromaFormat right)
    {
        return !(left == right);
    }
}
