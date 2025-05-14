namespace ContentDotNet.Extensions.Mp4.Models.Samples;

/// <summary>
///   Represents a collection of sample entries that handle reading
///   sample entries based on their type.
/// </summary>
public sealed class SampleEntries
{
    private readonly List<ISampleEntryHandler> _handlers;

    /// <summary>
    ///   Initializes a new instance of the <see cref="SampleEntries"/> class.
    /// </summary>
    public SampleEntries()
    {
        _handlers = [];
    }

    /// <summary>
    ///   Number of sample entry handlers.
    /// </summary>
    public int Count => _handlers.Count;

    /// <summary>
    ///   Actual list of sample entries.
    /// </summary>
    public List<ISampleEntryHandler> Backing => _handlers;

    /// <summary>
    ///   Registers a new sample entry handler.
    /// </summary>
    /// <param name="handler">The sample entry handler to register.</param>
    /// <exception cref="ArgumentException">Thrown when there are duplicate handlers.</exception>
    public void RegisterHandler(ISampleEntryHandler handler)
    {
        CheckForDuplicateHandlers(handler, nameof(handler));
        _handlers.Add(handler);
    }

    /// <summary>
    ///   Are there any already registered handlers with such identifier as <paramref name="handler"/>?
    /// </summary>
    /// <param name="handler">Handler to check for</param>
    /// <returns>A boolean, indicating whether there already is a registered handler whose identifier is <paramref name="handler"/>.</returns>
    public bool ContainsHandler(ISampleEntryHandler handler) => _handlers.Any(p => p.Identifier == handler.Identifier);

    public bool TryRead(FourCC type, BinaryReader reader, out ISampleEntryData? data)
    {
        if (!_handlers.Any(handler => handler.Identifier == type))
            throw new InvalidOperationException("No handler was registered to handle sample entries of type " + type.ValueText);

        foreach (ISampleEntryHandler handler in _handlers)
        {
            if (handler.Identifier == type)
            {
                data = handler.Read(reader);
                return true;
            }
        }

        data = null;
        return false;
    }

    private void CheckForDuplicateHandlers(ISampleEntryHandler handler, string argumentName)
    {
        if (this.ContainsHandler(handler))
            throw new ArgumentException("Duplicate sample entry handlers", argumentName);
    }
}
