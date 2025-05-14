using ContentDotNet.Extensions.Mp4.Models.Boxes;

namespace ContentDotNet.Extensions.Mp4.Models.Samples;

/// <summary>
///   Represents a base class for an MP4 sample entry.
/// </summary>
public abstract class SampleEntry
{
    /// <summary>
    ///   Represents the box header. The <see cref="BoxHeader.Size"/>
    ///   field defines the size, whereas the <see cref="BoxHeader.Type"/>
    ///   field defines the format of the sample entry - for example, mp4a
    ///   or avc1.
    /// </summary>
    public BoxHeader Header { get; set; }

    /// <summary>
    ///   The data reference index.
    /// </summary>
    public ushort DataReferenceIndex { get; set; }

    /// <summary>
    ///   The data of the sample entry.
    /// </summary>
    public ISampleEntryData Data { get; set; }

    /// <summary>
    ///   Initializes the new instance of the <see cref="SampleEntry"/> class.
    /// </summary>
    /// <remarks>
    ///   [?] The reserved 6 bits before the <see cref="DataReferenceIndex"/> field
    ///   are omitted as they're always set to 0.
    /// </remarks>
    /// <param name="header">Box header</param>
    /// <param name="dataReferenceIndex">Data reference index</param>
    /// <param name="data">Box data</param>
    protected SampleEntry(BoxHeader header, ushort dataReferenceIndex, ISampleEntryData data)
    {
        Header = header;
        DataReferenceIndex = dataReferenceIndex;
        Data = data;
    }
}
