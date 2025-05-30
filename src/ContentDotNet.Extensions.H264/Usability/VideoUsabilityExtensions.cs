using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Usability;

/// <summary>
///   Video Usability Information (VUI) extensions.
/// </summary>
public static class VideoUsabilityExtensions
{
    /// <summary>
    /// Determines if the specified aspect ratio IDC represents an extended SAR (Sample Aspect Ratio).
    /// </summary>
    /// <param name="aspectRatioIdc">The aspect ratio IDC value.</param>
    /// <returns>True if the aspect ratio IDC is for extended SAR; otherwise, false.</returns>
    public static bool IsExtendedSarAspectRatio(uint aspectRatioIdc) => AspectRatioMapping.IsExtendedSAR(aspectRatioIdc);

    /// <summary>
    /// Determines if the VUI parameters specify an extended SAR (Sample Aspect Ratio).
    /// </summary>
    /// <param name="vuip">The VUI parameters instance.</param>
    /// <returns>True if the aspect ratio IDC is for extended SAR; otherwise, false.</returns>
    public static bool IsExtendedSarAspectRatio(this VuiParameters vuip) => AspectRatioMapping.IsExtendedSAR(vuip.AspectRatioIdc);

    /// <summary>
    /// Gets the <see cref="AspectRatio"/> for the specified VUI parameters.
    /// </summary>
    /// <param name="vuip">The VUI parameters instance.</param>
    /// <returns>The <see cref="AspectRatio"/> value.</returns>
    public static AspectRatio GetAspectRatio(this VuiParameters vuip) => AspectRatioMapping.GetAspectRatio(vuip);

    /// <summary>
    /// Gets the aspect ratio fields (IDC, SAR width, SAR height) for the specified <see cref="AspectRatio"/>.
    /// </summary>
    /// <param name="aspectRatio">The aspect ratio value.</param>
    /// <returns>A tuple containing the aspect ratio IDC, SAR width, and SAR height.</returns>
    public static (uint aspectRatioIdc, int sarWidth, int sarHeight) GetAspectRatioFields(AspectRatio aspectRatio)
        => AspectRatioMapping.GetAspectRatioFields(aspectRatio);

    /// <summary>
    /// Gets the <see cref="MatrixCoefficients"/> value from the specified VUI parameters.
    /// </summary>
    /// <param name="vuip">The VUI parameters instance.</param>
    /// <returns>The <see cref="MatrixCoefficients"/> value.</returns>
    public static MatrixCoefficients GetMatrixCoefficients(this VuiParameters vuip) => (MatrixCoefficients)vuip.MatrixCoefficients;

    /// <summary>
    /// Gets the <see cref="TransferCharacteristics"/> value from the specified VUI parameters.
    /// </summary>
    /// <param name="vuip">The VUI parameters instance.</param>
    /// <returns>The <see cref="TransferCharacteristics"/> value.</returns>
    public static TransferCharacteristics GetTransferCharacteristics(this VuiParameters vuip) => (TransferCharacteristics)vuip.TransferCharacteristics;

    /// <summary>
    /// Gets the <see cref="ColorPrimary"/> value from the specified VUI parameters.
    /// </summary>
    /// <param name="vuip">The VUI parameters instance.</param>
    /// <returns>The <see cref="ColorPrimary"/> value.</returns>
    public static ColorPrimary GetColorPrimary(this VuiParameters vuip) => (ColorPrimary)vuip.ColourPrimaries;

    /// <summary>
    /// Gets the <see cref="VideoFormat"/> value from the specified VUI parameters.
    /// </summary>
    /// <param name="vuip">The VUI parameters instance.</param>
    /// <returns>The <see cref="VideoFormat"/> value.</returns>
    public static VideoFormat GetVideoFormat(this VuiParameters vuip) => (VideoFormat)vuip.VideoFormat;
}
