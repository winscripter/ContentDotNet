using ContentDotNet.Extensions.Mp4.Models.Boxes;

namespace ContentDotNet.Extensions.Mp4;

/// <summary>
///   Represents annotations for MP4 boxes.
/// </summary>
public static class BoxAnnotations
{
    /// <summary>
    ///   Retrieves the <see cref="BoxInfoAttribute"/> associated with the specified <see cref="IBoxData"/> instance.
    /// </summary>
    /// <param name="data">The box data instance to retrieve the attribute from.</param>
    /// <returns>
    ///   The <see cref="BoxInfoAttribute"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public static BoxInfoAttribute? GetAttribute(Box data)
    {
        if (data.GetType().GetCustomAttributes(typeof(BoxInfoAttribute), true)
            .FirstOrDefault() is not BoxInfoAttribute attribute)
            return null;

        return attribute;
    }

    /// <summary>
    ///   Gets the Four Character Code (FourCC) of the specified <see cref="IBoxData"/> instance.
    /// </summary>
    /// <param name="data">The box data instance.</param>
    /// <returns>
    ///   The FourCC string if available; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    ///   If one also needs to retrieve other annotations alongside just the FourCC, consider using <see cref="GetAttribute(IBoxData)"/> instead
    ///   to achieve better performance by avoiding multiple attribute lookups.
    /// </remarks>
    public static string? GetBoxFourCC(Box data)
    {
        return GetAttribute(data)?.BoxFourCC;
    }

    /// <summary>
    ///   Gets the unabbreviated form of the box name for the specified <see cref="IBoxData"/> instance.
    /// </summary>
    /// <param name="data">The box data instance.</param>
    /// <returns>
    ///   The unabbreviated form string if available; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    ///   If one also needs to retrieve other annotations alongside just the unabbreviated form, consider using
    ///   <see cref="GetAttribute(IBoxData)"/> instead to achieve better performance by avoiding multiple attribute lookups.
    /// </remarks>
    public static string? GetUnabbreviatedForm(Box data)
    {
        return GetAttribute(data)?.UnabbreviatedForm;
    }

    /// <summary>
    ///   Gets the description of the box for the specified <see cref="IBoxData"/> instance.
    /// </summary>
    /// <param name="data">The box data instance.</param>
    /// <returns>
    ///   The description string if available; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    ///   If one also needs to retrieve other annotations alongside just the description, consider using <see cref="GetAttribute(IBoxData)"/> instead
    ///   to achieve better performance by avoiding multiple attribute lookups.
    /// </remarks>
    public static string? GetDescription(Box data)
    {
        return GetAttribute(data)?.Description;
    }
}
