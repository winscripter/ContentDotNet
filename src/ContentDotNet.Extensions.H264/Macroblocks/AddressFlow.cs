namespace ContentDotNet.Extensions.H264.Macroblocks;

/// <summary>
///   H.264 macroblock address flow.
/// </summary>
public readonly struct AddressFlow
{
    private readonly int _picWidthInMbs;
    private readonly int _address;
    private readonly int _mbsInFrame;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressFlow"/> struct.
    /// </summary>
    /// <param name="address">The macroblock address.</param>
    /// <param name="picWidthInMbs">The width of the picture in macroblocks.</param>
    /// <param name="mbsInFrame">The total number of macroblocks in the frame.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="picWidthInMbs"/> is less than or equal to zero.</exception>
    public AddressFlow(int address, int picWidthInMbs, int mbsInFrame)
    {
        if (picWidthInMbs <= 0)
            throw new ArgumentOutOfRangeException(nameof(picWidthInMbs), "Picture width in macroblocks must be greater than zero.");
        _picWidthInMbs = picWidthInMbs;
        _address = address;
        _mbsInFrame = mbsInFrame;
    }

    /// <summary>
    /// Gets the <see cref="AddressFlow"/> instance representing the left neighboring macroblock.
    /// </summary>
    /// <returns>An <see cref="AddressFlow"/> for the left neighbor.</returns>
    public AddressFlow Left()
    {
        var result = new NeighboringMacroblocks();
        result.Refresh(_address, _picWidthInMbs, _mbsInFrame);
        var addr = result.MbAddrA;
        return new AddressFlow(addr, _picWidthInMbs, _mbsInFrame);
    }

    /// <summary>
    /// Gets the <see cref="AddressFlow"/> instance representing the top neighboring macroblock.
    /// </summary>
    /// <returns>An <see cref="AddressFlow"/> for the top neighbor.</returns>
    public AddressFlow Up()
    {
        var result = new NeighboringMacroblocks();
        result.Refresh(_address, _picWidthInMbs, _mbsInFrame);
        var addr = result.MbAddrB;
        return new AddressFlow(addr, _picWidthInMbs, _mbsInFrame);
    }

    /// <summary>
    /// Gets the <see cref="AddressFlow"/> instance representing the right neighboring macroblock.
    /// </summary>
    /// <returns>An <see cref="AddressFlow"/> for the right neighbor.</returns>
    public AddressFlow Right()
    {
        var result = new NeighboringMacroblocks();
        result.Refresh(_address, _picWidthInMbs, _mbsInFrame);
        var addr = result.MbAddrC;
        return new AddressFlow(addr, _picWidthInMbs, _mbsInFrame);
    }

    /// <summary>
    /// Gets the <see cref="AddressFlow"/> instance representing the bottom neighboring macroblock.
    /// </summary>
    /// <returns>An <see cref="AddressFlow"/> for the bottom neighbor.</returns>
    public AddressFlow Down()
    {
        var addr = _address + _picWidthInMbs;
        return new AddressFlow(addr, _picWidthInMbs, _mbsInFrame);
    }

    /// <summary>
    /// Gets the macroblock address value.
    /// </summary>
    public readonly int Value => _address;
}
