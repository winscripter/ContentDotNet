using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Encoding options for H.264.
/// </summary>
internal sealed class H264EncodingOptions : INotifyPropertyChanged
{
    // Prefer CABAC as the default entropy coding mode due to its popularity and compression efficiency
    private EntropyCodingMode _entropyCodingMode = EntropyCodingMode.Cabac;

    private H264Profile _profile = H264Profile.High;

    // Slice QP/QS for Luma is at least 26; for instance, 2 is 28 because 26 + 2.
    private int _sliceQPY = 0;
    private int _sliceQSY = 0;

    private bool _deblock = true;
    private bool _enforceAllMacroblocksAsPCM = false;
    private bool _enforceAllMacroblocksAsIntra = false;
    private bool _allowSubMacroblocks = true;
    private bool _noBFrames = false;
    private int _maxRefPics = 4;

    /// <summary>
    ///   Event invoked when a property changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///   Specifies the entropy coding mode. Default: <see cref="EntropyCodingMode.Cabac"/>
    /// </summary>
    public EntropyCodingMode EntropyCodingMode
    {
        get => _entropyCodingMode;
        set
        {
            if (_entropyCodingMode != value)
            {
                _entropyCodingMode = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Specifies the profile. Default: <see cref="H264Profile.High"/>
    /// </summary>
    public H264Profile Profile
    {
        get => _profile;
        set
        {
            if (_profile != value)
            {
                _profile = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Specifies the Slice Quantization Parameter for the Luma channel. Default: 0 (is actually 26).
    /// </summary>
    public int SliceQPY
    {
        get => _sliceQPY;
        set
        {
            if (_sliceQPY != value)
            {
                _sliceQPY = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Specifies the Slice QS for the Luma channel. Default: 0 (is actually 26).
    /// </summary>
    public int SliceQSY
    {
        get => _sliceQSY;
        set
        {
            if (_sliceQSY != value)
            {
                _sliceQSY = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Should the deblocking filter be applied? Default: <see langword="true"/>.
    /// </summary>
    public bool Deblock
    {
        get => _deblock;
        set
        {
            if (_deblock != value)
            {
                _deblock = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Should all macroblocks be PCM? Default: <see langword="false"/>.
    /// </summary>
    public bool EnforceAllMacroblocksAsPCM
    {
        get => _enforceAllMacroblocksAsPCM;
        set
        {
            if (_enforceAllMacroblocksAsPCM != value)
            {
                _enforceAllMacroblocksAsPCM = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Should all macroblocks be Intra only? Default: <see langword="false"/>.
    /// </summary>
    public bool EnforceAllMacroblocksAsIntra
    {
        get => _enforceAllMacroblocksAsIntra;
        set
        {
            if (_enforceAllMacroblocksAsIntra != value)
            {
                _enforceAllMacroblocksAsIntra = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Can macroblocks have sub-macroblocks? Default: <see langword="true"/>.
    /// </summary>
    public bool AllowSubMacroblocks
    {
        get => _allowSubMacroblocks;
        set
        {
            if (_allowSubMacroblocks != value)
            {
                _allowSubMacroblocks = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Can macroblocks be B-frames? Default: <see langword="false"/>.
    /// </summary>
    public bool NoBFrames
    {
        get => _noBFrames;
        set
        {
            if (_noBFrames != value)
            {
                _noBFrames = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    ///   Maximum number of reference pictures. Default: 4.
    /// </summary>
    public int MaxReferencePictures
    {
        get => _maxRefPics;
        set
        {
            if (_maxRefPics != value)
            {
                _maxRefPics = value;
                RaisePropertyChanged();
            }
        }
    }
}
