using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H263;

/// <summary>
///   Utilities to work with H.263 PSC.
/// </summary>
public static class H263PictureStartCode
{
    /// <summary>
    ///   Picture Start Code value.
    /// </summary>
    public const uint PscValue = 0b0000000000000000100000;

    public static bool GoToPictureStartCode(BitStreamReader reader)
    {
        ReaderState rs = reader.GetState();
        bool foundPSC = false;
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            if (reader.PeekBits(22) != PscValue)
            {
                reader.ReadBit();
            }
            else
            {
                foundPSC = true;
                break;
            }
        }
        if (!foundPSC)
        {
            reader.GoTo(rs);
        }
        return foundPSC;
    }

    public static bool GoToAfterPictureStartCode(BitStreamReader reader)
    {
        if (GoToPictureStartCode(reader))
        {
            reader.ReadBits(22);
            return true;
        }
        return false;
    }

    public static bool HasPrecedingPictureStartCodes(BitStreamReader reader)
    {
        ReaderState prevState = reader.GetState();
        bool result = GoToPictureStartCode(reader);
        reader.GoTo(prevState);
        return result;
    }
}
