using System.ComponentModel;

namespace ContentDotNet.Abstractions;

/// <summary>
/// Performance Metrics control performance options for the encoder/decoder.
/// </summary>
public sealed class PerformanceMetrics : INotifyPropertyChanged, IEquatable<PerformanceMetrics?>
{
    /// <summary>
    ///   Invoked when a property is changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///   Initializes a new instance of the <see cref="PerformanceMetrics"/> class.
    /// </summary>
    /// <param name="isMultithreaded">Is multithreading enabled?</param>
    /// <param name="threadCount">Number of threads</param>
    /// <param name="enableHardwareAcceleration">Is hardware acceleration enabled?</param>
    /// <param name="hardwareAcceleratorName">Name of the hardware accelerator</param>
    public PerformanceMetrics(bool isMultithreaded, int threadCount, bool enableHardwareAcceleration, string? hardwareAcceleratorName)
    {
        _isMultithreaded = isMultithreaded;
        _threadCount = threadCount;
        _enableHardwareAcceleration = enableHardwareAcceleration;
        _hardwareAcceleratorName = hardwareAcceleratorName;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PerformanceMetrics"/> class with default values,
    ///   which include disabling multithreading and not using any accelerators.
    /// </summary>
    public PerformanceMetrics()
        : this(false, 1, false, null)
    {
    }

    private bool _isMultithreaded;

    /// <summary>
    ///   Is multithreading enabled?
    /// </summary>
    public bool IsMultithreaded
    {
        get
        {
            return _isMultithreaded;
        }

        set
        {
            if (_isMultithreaded != value)
            {
                _isMultithreaded = value;
                RaisePropertyChanged(nameof(IsMultithreaded));
            }
        }
    }

    private int _threadCount;

    /// <summary>
    ///   Number of threads to use.
    /// </summary>
    public int ThreadCount
    {
        get
        {
            if (!this.IsMultithreaded)
                return 1;

            return _threadCount;
        }
        set
        {
            if (!this.IsMultithreaded)
                throw new InvalidOperationException("Multithreading is disabled");

            if (_threadCount != value)
            {
                _threadCount = value;
                RaisePropertyChanged(nameof(ThreadCount));
            }
        }
    }

    /// <summary>
    ///   Actual number of threads.
    /// </summary>
    public int ActualThreadCount
    {
        get => _threadCount;
    }

    private bool _enableHardwareAcceleration;

    /// <summary>
    ///   Should hardware acceleration be enabled?
    /// </summary>
    public bool EnableHardwareAcceleration
    {
        get
        {
            return _enableHardwareAcceleration;
        }

        set
        {
            if (value != _enableHardwareAcceleration)
            {
                _enableHardwareAcceleration = value;
                RaisePropertyChanged(nameof(EnableHardwareAcceleration));
            }
        }
    }

    private string? _hardwareAcceleratorName;

    /// <summary>
    ///   Name of the hardware accelerator, e.g. OpenCL. See <see cref="Accelerators"/>.
    /// </summary>
    public string? HardwareAcceleratorName
    {
        get
        {
            return _hardwareAcceleratorName;
        }

        set
        {
            if (_hardwareAcceleratorName != value)
            {
                _hardwareAcceleratorName = value;
                RaisePropertyChanged(nameof(HardwareAcceleratorName));
            }
        }
    }

    /// <summary>
    ///   Raises the <see cref="PropertyChanged"/> event for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    private void RaisePropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current <see cref="PerformanceMetrics"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as PerformanceMetrics);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="PerformanceMetrics"/> instance is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="PerformanceMetrics"/> instance to compare with the current instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(PerformanceMetrics? other)
    {
        return other is not null &&
               _isMultithreaded == other._isMultithreaded &&
               IsMultithreaded == other.IsMultithreaded &&
               _threadCount == other._threadCount &&
               ThreadCount == other.ThreadCount &&
               ActualThreadCount == other.ActualThreadCount &&
               _enableHardwareAcceleration == other._enableHardwareAcceleration &&
               EnableHardwareAcceleration == other.EnableHardwareAcceleration &&
               _hardwareAcceleratorName == other._hardwareAcceleratorName &&
               HardwareAcceleratorName == other.HardwareAcceleratorName;
    }

    /// <summary>
    ///   Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="PerformanceMetrics"/> instance.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_isMultithreaded);
        hash.Add(IsMultithreaded);
        hash.Add(_threadCount);
        hash.Add(ThreadCount);
        hash.Add(ActualThreadCount);
        hash.Add(_enableHardwareAcceleration);
        hash.Add(EnableHardwareAcceleration);
        hash.Add(_hardwareAcceleratorName);
        hash.Add(HardwareAcceleratorName);
        return hash.ToHashCode();
    }


    /// <summary>
    ///   Determines whether two <see cref="PerformanceMetrics"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="PerformanceMetrics"/> instance to compare.</param>
    /// <param name="right">The second <see cref="PerformanceMetrics"/> instance to compare.</param>
    /// <returns>
    ///   <c>true</c> if the two instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(PerformanceMetrics? left, PerformanceMetrics? right)
    {
        return EqualityComparer<PerformanceMetrics>.Default.Equals(left, right);
    }

    /// <summary>
    ///   Determines whether two <see cref="PerformanceMetrics"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="PerformanceMetrics"/> instance to compare.</param>
    /// <param name="right">The second <see cref="PerformanceMetrics"/> instance to compare.</param>
    /// <returns>
    ///   <c>true</c> if the two instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(PerformanceMetrics? left, PerformanceMetrics? right)
    {
        return !(left == right);
    }
}
