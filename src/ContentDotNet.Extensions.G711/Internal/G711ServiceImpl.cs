using ContentDotNet.Abstractions;
using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.G711.Internal;

internal sealed class G711ServiceImpl : IG711Service
{
    public IPcmAudioCodec CreateCodec(BitStreamReader reader, G711Law law) => new G711Codec(reader, law);

    public IPcmAudioCodecWriter CreateCodecWriter(BitStreamWriter writer, G711Law law) => new G711CodecWriter(writer, law);
}
