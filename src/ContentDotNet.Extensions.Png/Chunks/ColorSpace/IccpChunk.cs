namespace ContentDotNet.Extensions.Png.Chunks.ColorSpace;

public sealed class IccpChunk : IChunkData
{
    public string ProfileName { get; set; }
    public byte CompressionMethod { get; set; }
    public byte[] CompressedProfile { get; set; }

    public IccpChunk(string profileName, byte compressionMethod, byte[] compressedProfile)
    {
        this.ProfileName = profileName;
        this.CompressionMethod = compressionMethod;
        this.CompressedProfile = compressedProfile ?? throw new ArgumentNullException(nameof(compressedProfile), "Compressed profile cannot be null.");
    }

    public void Validate()
    {
        if (ProfileName.Length is < 1 or > 79)
            throw new ArgumentOutOfRangeException(nameof(ProfileName), "Profile name must be between 1 and 79 characters long.");
        if (ProfileName.Any(p => p < 32 || (p < 161 && p > 126) || p > 255))
            throw new InvalidOperationException("Image's Profile Name contains unprintable characters");
    }

    public void Write(BinaryWriter writer)
    {
        Validate();
        foreach (char c in ProfileName)
            writer.Write((byte)c);
        writer.Write((byte)0);
        writer.Write(CompressionMethod);
        writer.Write(CompressedProfile);
    }
}
