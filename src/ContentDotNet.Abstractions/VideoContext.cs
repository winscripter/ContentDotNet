using System.ComponentModel;
using System.Drawing;

namespace ContentDotNet.Abstractions;

/// <summary>
///   Represents video information.
/// </summary>
public sealed class VideoContext : INotifyPropertyChanged, IEquatable<VideoContext?>
{
    /// <summary>
    ///   Invoked when a property is changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    ///   Initializes a new instance of the <see cref="VideoContext"/> class.
    /// </summary>
    public VideoContext()
    {
    }

    private float fps = 30F;

    /// <summary>
    ///   Represents Frames Per Second, e.g. how many pictures in a single second.
    ///   Default value is 30.
    /// </summary>
    public float FramesPerSecond
    {
        get
        {
            return fps;
        }

        set
        {
            if (fps != value)
            {
                fps = value;
                RaisePropertyChanged(nameof(FramesPerSecond));
            }
        }
    }

    private ChromaSubsampling chromaSubsampling = ChromaSubsampling.Shared422;

    /// <summary>
    ///   Represents the Chroma Subsampling scheme. By default,
    ///   <see cref="ChromaSubsampling.Shared422"/> is used for
    ///   balanced quality.
    /// </summary>
    public ChromaSubsampling ChromaSubsampling
    {
        get
        {
            return chromaSubsampling;
        }

        set
        {
            if (chromaSubsampling != value)
            {
                chromaSubsampling = value;
                RaisePropertyChanged(nameof(ChromaSubsampling));
            }
        }
    }

    private MiscellaneousDataBuilder? miscellaneousData;

    /// <summary>
    ///   Includes data that isn't necessary for the video but may still
    ///   be used. For example, this could represent SEI in H.264.
    /// </summary>
    public MiscellaneousDataBuilder? MiscellaneousData
    {
        get
        {
            return miscellaneousData;
        }

        set
        {
            if (miscellaneousData != value)
            {
                miscellaneousData = value;
                RaisePropertyChanged(nameof(MiscellaneousData));
            }
        }
    }

    private Dictionary<string, bool> permissions = [];

    /// <summary>
    ///   Controls specific permissions. Value is based on the codec.
    /// </summary>
    public Dictionary<string, bool> Permissions
    {
        get
        {
            return permissions;
        }

        set
        {
            if (permissions != value)
            {
                permissions = value;
                RaisePropertyChanged(nameof(Permissions));
            }
        }
    }

    private byte bitDepthLuma = 8;

    /// <summary>
    ///   Represents bit depth for the Luma channel. Default value is 8.
    /// </summary>
    /// <remarks>
    ///   If a codec does not use luma/chroma, use <see cref="GeneralBitDepth"/>
    ///   instead.
    /// </remarks>
    public byte BitDepthLuma
    {
        get
        {
            return bitDepthLuma;
        }

        set
        {
            if (bitDepthLuma != value)
            {
                bitDepthLuma = value;
                RaisePropertyChanged(nameof(BitDepthLuma));
            }
        }
    }

    private byte bitDepthChroma = 8;

    /// <summary>
    ///   Represents bit depth for the Chroma channel. Default value is 8.
    /// </summary>
    /// <remarks>
    ///   If a codec does not use luma/chroma, use <see cref="GeneralBitDepth"/>
    ///   instead.
    /// </remarks>
    public byte BitDepthChroma
    {
        get
        {
            return bitDepthChroma;
        }

        set
        {
            if (bitDepthChroma != value)
            {
                bitDepthChroma = value;
                RaisePropertyChanged(nameof(BitDepthChroma));
            }
        }
    }

    private byte generalBitDepth = 8;

    /// <summary>
    ///   Bit depth that is used if luma/chroma is not followed. Default
    ///   value is 8.
    /// </summary>
    public byte GeneralBitDepth
    {
        get
        {
            return generalBitDepth;
        }

        set
        {
            if (generalBitDepth != value)
            {
                generalBitDepth = value;
                RaisePropertyChanged(nameof(GeneralBitDepth));
            }
        }
    }

    private byte compressionStrength = 3;

    /// <summary>
    ///   Represents Compression Strength. This is based on
    ///   codecs. Default value is 3.
    /// </summary>
    public byte CompressionStrength
    {
        get
        {
            return compressionStrength;
        }

        set
        {
            if (compressionStrength != value)
            {
                compressionStrength = value;
                RaisePropertyChanged(nameof(CompressionStrength));
            }
        }
    }

    private Rectangle crop = default;

    /// <summary>
    ///   Represents video cropping. Default value is <see langword="default"/>.
    /// </summary>
    public Rectangle Crop
    {
        get
        {
            return crop;
        }

        set
        {
            if (crop != value)
            {
                crop = value;
                RaisePropertyChanged(nameof(Crop));
            }
        }
    }

    /// <summary>  
    /// Determines whether the specified object is equal to the current object.  
    /// </summary>  
    /// <param name="obj">The object to compare with the current object.</param>  
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>  
    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoContext);
    }

    /// <summary>  
    /// Determines whether the specified <see cref="VideoContext"/> is equal to the current <see cref="VideoContext"/>.  
    /// </summary>  
    /// <param name="other">The <see cref="VideoContext"/> to compare with the current <see cref="VideoContext"/>.</param>  
    /// <returns>True if the specified <see cref="VideoContext"/> is equal to the current <see cref="VideoContext"/>; otherwise, false.</returns>  
    public bool Equals(VideoContext? other)
    {
        return other is not null &&
               fps == other.fps &&
               FramesPerSecond == other.FramesPerSecond &&
               chromaSubsampling.Equals(other.chromaSubsampling) &&
               ChromaSubsampling.Equals(other.ChromaSubsampling) &&
               EqualityComparer<MiscellaneousDataBuilder?>.Default.Equals(miscellaneousData, other.miscellaneousData) &&
               EqualityComparer<MiscellaneousDataBuilder?>.Default.Equals(MiscellaneousData, other.MiscellaneousData) &&
               EqualityComparer<Dictionary<string, bool>>.Default.Equals(permissions, other.permissions) &&
               EqualityComparer<Dictionary<string, bool>>.Default.Equals(Permissions, other.Permissions) &&
               bitDepthLuma == other.bitDepthLuma &&
               BitDepthLuma == other.BitDepthLuma &&
               bitDepthChroma == other.bitDepthChroma &&
               BitDepthChroma == other.BitDepthChroma &&
               generalBitDepth == other.generalBitDepth &&
               GeneralBitDepth == other.GeneralBitDepth &&
               compressionStrength == other.compressionStrength &&
               CompressionStrength == other.CompressionStrength &&
               crop.Equals(other.crop) &&
               Crop.Equals(other.Crop);
    }

    /// <summary>  
    /// Serves as the default hash function.  
    /// </summary>  
    /// <returns>A hash code for the current object.</returns>  
    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(fps);
        hash.Add(FramesPerSecond);
        hash.Add(chromaSubsampling);
        hash.Add(ChromaSubsampling);
        hash.Add(miscellaneousData);
        hash.Add(MiscellaneousData);
        hash.Add(permissions);
        hash.Add(Permissions);
        hash.Add(bitDepthLuma);
        hash.Add(BitDepthLuma);
        hash.Add(bitDepthChroma);
        hash.Add(BitDepthChroma);
        hash.Add(generalBitDepth);
        hash.Add(GeneralBitDepth);
        hash.Add(compressionStrength);
        hash.Add(CompressionStrength);
        hash.Add(crop);
        hash.Add(Crop);

        return hash.ToHashCode();
    }


    /// <summary>  
    /// Determines whether two <see cref="VideoContext"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="VideoContext"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="VideoContext"/> instance to compare.</param>  
    /// <returns>True if the two instances are equal; otherwise, false.</returns>  
    public static bool operator ==(VideoContext? left, VideoContext? right)
    {
        return EqualityComparer<VideoContext>.Default.Equals(left, right);
    }

    /// <summary>  
    /// Determines whether two <see cref="VideoContext"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="VideoContext"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="VideoContext"/> instance to compare.</param>  
    /// <returns>True if the two instances are not equal; otherwise, false.</returns>  
    public static bool operator !=(VideoContext? left, VideoContext? right)
    {
        return !(left == right);
    }
}
