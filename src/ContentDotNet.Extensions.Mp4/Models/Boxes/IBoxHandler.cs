namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   Handles reading Mp4 boxes.
/// </summary>
public interface IBoxHandler
{
    /// <summary>
    ///   Is the given box compatible with this handler?
    /// </summary>
    /// <param name="box">Box</param>
    /// <returns>A boolean indicating compatibility.</returns>
    bool IsCompatible(Box box);

    /// <summary>
    ///   Writes the box to the specified writer.
    /// </summary>
    /// <param name="box">The input box.</param>
    /// <param name="writer">The writer where the box is written to.</param>
    void Write(Box box, BinaryWriter writer);

    /// <summary>
    ///   Reads the box data.
    /// </summary>
    /// <param name="boxHeader">Box header.</param>
    /// <param name="reader">Binary reader.</param>
    /// <returns>Box data</returns>
    IBoxData ReadData(BoxHeader boxHeader, BinaryReader reader);
}
