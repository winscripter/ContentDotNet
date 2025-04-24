namespace ContentDotNet.Abstractions;

/// <summary>
///   Hardware acceleration names.
/// </summary>
/// <remarks>
///   This class only includes most popular and likely to be used accelerators - it does not
///   cover every single one of them.
/// </remarks>
public static class Accelerators
{
    /// <summary>
    ///   OpenCL
    /// </summary>
    public const string OpenCL = nameof(OpenCL);

    /// <summary>
    ///   NVIDIA's CUDA
    /// </summary>
    public const string CUDA = nameof(CUDA);

    /// <summary>
    ///   OpenGL
    /// </summary>
    public const string OpenGL = nameof(OpenGL);

    /// <summary>
    ///   OpenGL ES, for embedded systems.
    /// </summary>
    public const string GLES = nameof(GLES);

    /// <summary>
    ///   Vulkan
    /// </summary>
    public const string Vulkan = nameof(Vulkan);

    /// <summary>
    ///   DirectX Direct Compute
    /// </summary>
    public const string DirectCompute = nameof(DirectCompute);
}
