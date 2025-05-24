namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   Describes the MP4 box.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class BoxInfoAttribute : Attribute
{
    /// <summary>
    ///   Four Character Code (FourCC) of the box (f.e. mdia or ftyp).
    /// </summary>
    public string BoxFourCC { get; }

    /// <summary>
    ///   Unabbreviated form of the box name (f.e. Media or File Type).
    /// </summary>
    public string UnabbreviatedForm { get; }

    /// <summary>
    ///   Purpose of the box (f.e. Contains media information or Contains file type information).
    /// </summary>
    public string Description { get; }

    public BoxInfoAttribute(string boxFourCC, string unabbreviatedForm, string description)
    {
        BoxFourCC = boxFourCC;
        UnabbreviatedForm = unabbreviatedForm;
        Description = description;
    }
}
