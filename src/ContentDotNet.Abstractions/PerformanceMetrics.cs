using System.ComponentModel;

namespace ContentDotNet.Abstractions;

/// <summary>
/// Performance Metrics control performance options for the encoder/decoder.
/// </summary>
public sealed class PerformanceMetrics : INotifyPropertyChanged
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
    public PerformanceMetrics() : this(false, 1, false, null)
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

    private void RaisePropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
