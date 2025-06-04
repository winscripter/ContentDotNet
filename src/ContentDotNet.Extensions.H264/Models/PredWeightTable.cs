using ContentDotNet.BitStream;
using ContentDotNet.Containers;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents a prediction weight table containing luma and chroma weight denominators and their respective lists.
/// </summary>
public struct PredWeightTable : IEquatable<PredWeightTable>
{
    /// <summary>
    /// Gets or sets the base-2 logarithm of the luma weight denominator.
    /// </summary>
    public uint LumaLog2WeightDenom;

    /// <summary>
    /// Gets or sets the base-2 logarithm of the chroma weight denominator.
    /// </summary>
    public uint ChromaLog2WeightDenom;

    /// <summary>
    /// Gets or sets a value indicating whether luma weights and offsets for list 0 are present.
    /// </summary>
    public Container32Boolean LumaWeightL0Flag;

    /// <summary>
    /// Gets or sets the luma weights for list 0.
    /// </summary>
    public Container32Int32 LumaWeightL0;

    /// <summary>
    /// Gets or sets the luma offsets for list 0.
    /// </summary>
    public Container32Int32 LumaOffsetL0;

    /// <summary>
    /// Gets or sets a value indicating whether chroma weights and offsets for list 0 are present.
    /// </summary>
    public Container32Boolean ChromaWeightL0Flag;

    /// <summary>
    /// Gets or sets the chroma weights for list 0.
    /// </summary>
    public ContainerMatrix2x32 ChromaWeightL0;

    /// <summary>
    /// Gets or sets the chroma offsets for list 0.
    /// </summary>
    public ContainerMatrix2x32 ChromaOffsetL0;

    /// <summary>
    /// Gets or sets a value indicating whether luma weights and offsets for list 1 are present.
    /// </summary>
    public Container32Boolean LumaWeightL1Flag;

    /// <summary>
    /// Gets or sets the luma weights for list 1.
    /// </summary>
    public Container32Int32 LumaWeightL1;

    /// <summary>
    /// Gets or sets the luma offsets for list 1.
    /// </summary>
    public Container32Int32 LumaOffsetL1;

    /// <summary>
    /// Gets or sets a value indicating whether chroma weights and offsets for list 1 are present.
    /// </summary>
    public Container32Boolean ChromaWeightL1Flag;

    /// <summary>
    /// Gets or sets the chroma weights for list 1.
    /// </summary>
    public ContainerMatrix2x32 ChromaWeightL1;

    /// <summary>
    /// Gets or sets the chroma offsets for list 1.
    /// </summary>
    public ContainerMatrix2x32 ChromaOffsetL1;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredWeightTable"/> struct.
    /// </summary>
    /// <param name="lumaLog2WeightDenom">The base-2 logarithm of the luma weight denominator.</param>
    /// <param name="chromaLog2WeightDenom">The base-2 logarithm of the chroma weight denominator.</param>
    /// <param name="lumaWeightL0Flag">Indicates if luma weights/offsets for list 0 are present.</param>
    /// <param name="lumaWeightL0">The luma weights for list 0.</param>
    /// <param name="lumaOffsetL0">The luma offsets for list 0.</param>
    /// <param name="chromaWeightL0Flag">Indicates if chroma weights/offsets for list 0 are present.</param>
    /// <param name="chromaWeightL0">The chroma weights for list 0.</param>
    /// <param name="chromaOffsetL0">The chroma offsets for list 0.</param>
    /// <param name="lumaWeightL1Flag">Indicates if luma weights/offsets for list 1 are present.</param>
    /// <param name="lumaWeightL1">The luma weights for list 1.</param>
    /// <param name="lumaOffsetL1">The luma offsets for list 1.</param>
    /// <param name="chromaWeightL1Flag">Indicates if chroma weights/offsets for list 1 are present.</param>
    /// <param name="chromaWeightL1">The chroma weights for list 1.</param>
    /// <param name="chromaOffsetL1">The chroma offsets for list 1.</param>
    public PredWeightTable(
        uint lumaLog2WeightDenom,
        uint chromaLog2WeightDenom,
        Container32Boolean lumaWeightL0Flag,
        Container32Int32 lumaWeightL0,
        Container32Int32 lumaOffsetL0,
        Container32Boolean chromaWeightL0Flag,
        ContainerMatrix2x32 chromaWeightL0,
        ContainerMatrix2x32 chromaOffsetL0,
        Container32Boolean lumaWeightL1Flag,
        Container32Int32 lumaWeightL1,
        Container32Int32 lumaOffsetL1,
        Container32Boolean chromaWeightL1Flag,
        ContainerMatrix2x32 chromaWeightL1,
        ContainerMatrix2x32 chromaOffsetL1)
    {
        LumaLog2WeightDenom = lumaLog2WeightDenom;
        ChromaLog2WeightDenom = chromaLog2WeightDenom;
        LumaWeightL0Flag = lumaWeightL0Flag;
        LumaWeightL0 = lumaWeightL0;
        LumaOffsetL0 = lumaOffsetL0;
        ChromaWeightL0Flag = chromaWeightL0Flag;
        ChromaWeightL0 = chromaWeightL0;
        ChromaOffsetL0 = chromaOffsetL0;
        LumaWeightL1Flag = lumaWeightL1Flag;
        LumaWeightL1 = lumaWeightL1;
        LumaOffsetL1 = lumaOffsetL1;
        ChromaWeightL1Flag = chromaWeightL1Flag;
        ChromaWeightL1 = chromaWeightL1;
        ChromaOffsetL1 = chromaOffsetL1;
    }

    /// <summary>
    ///   Reads the prediction weight table from the bitstream.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="chromaArrayType"></param>
    /// <param name="sliceType"></param>
    /// <param name="numRefIdxL0ActiveMinus1"></param>
    /// <param name="numRefIdxL1ActiveMinus1"></param>
    /// <returns>Prediction weight table, read from the bitstream.</returns>
    public static PredWeightTable Read(BitStreamReader reader, int chromaArrayType, int sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1)
    {
        uint lumaLog2WeightDenom = reader.ReadUE();
        uint chromaLog2WeightDenom = 0u;
        if (chromaArrayType != 0)
            chromaLog2WeightDenom = reader.ReadUE();

        Container32Boolean lumaWeightL0Flag = default;
        Container32Boolean chromaWeightL0Flag = default;
        Container32Boolean lumaWeightL1Flag = default;
        Container32Boolean chromaWeightL1Flag = default;
        Container32Int32 lumaWeightL0 = default;
        Container32Int32 lumaOffsetL0 = default;
        ContainerMatrix2x32 chromaWeightL0 = default;
        ContainerMatrix2x32 chromaOffsetL0 = default;
        Container32Int32 lumaWeightL1 = default;
        Container32Int32 lumaOffsetL1 = default;
        ContainerMatrix2x32 chromaWeightL1 = default;
        ContainerMatrix2x32 chromaOffsetL1 = default;

        for (int i = 0; i <= numRefIdxL0ActiveMinus1; i++)
        {
            lumaWeightL0Flag[i] = reader.ReadBit();
            if (lumaWeightL0Flag[i])
            {
                lumaWeightL0[i] = reader.ReadSE();
                lumaOffsetL0[i] = reader.ReadSE();
            }

            if (chromaArrayType != 0)
            {
                chromaWeightL0Flag[i] = reader.ReadBit();
                if (chromaWeightL0Flag[i])
                {
                    for (int j = 0; j < 2; j++)
                    {
                        chromaWeightL0[j, i] = reader.ReadSE();
                        chromaOffsetL0[j, i] = reader.ReadSE();
                    }
                }
            }
        }

        if (sliceType % 5 == 1)
        {
            for (int i = 0; i <= numRefIdxL1ActiveMinus1; i++)
            {
                lumaWeightL1Flag[i] = reader.ReadBit();
                if (lumaWeightL1Flag[i])
                {
                    lumaWeightL1[i] = reader.ReadSE();
                    lumaOffsetL1[i] = reader.ReadSE();
                }

                if (chromaArrayType != 0)
                {
                    chromaWeightL1Flag[i] = reader.ReadBit();
                    if (chromaWeightL1Flag[i])
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            chromaWeightL1[j, i] = reader.ReadSE();
                            chromaOffsetL1[j, i] = reader.ReadSE();
                        }
                    }
                }
            }
        }

        return new PredWeightTable(
            lumaLog2WeightDenom, chromaLog2WeightDenom, lumaWeightL0Flag, lumaWeightL0, lumaOffsetL0, chromaWeightL0Flag, chromaWeightL0, chromaOffsetL0,
                                                        lumaWeightL1Flag, lumaWeightL1, lumaOffsetL1, chromaWeightL1Flag, chromaWeightL1, chromaOffsetL1
        );
    }

    /// <summary>
    ///   Writes the prediction weight table to the bitstream writer.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="chromaArrayType"></param>
    /// <param name="sliceType"></param>
    /// <param name="numRefIdxL0ActiveMinus1"></param>
    /// <param name="numRefIdxL1ActiveMinus1"></param>
    public readonly void Write(BitStreamWriter writer, int chromaArrayType, int sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1)
    {
        writer.WriteUE(LumaLog2WeightDenom);
        if (chromaArrayType != 0)
            writer.WriteUE(ChromaLog2WeightDenom);

        for (int i = 0; i <= numRefIdxL0ActiveMinus1; i++)
        {
            writer.WriteBit(LumaWeightL0Flag[i]);
            if (LumaWeightL0Flag[i])
            {
                writer.WriteSE(LumaWeightL0[i]);
                writer.WriteSE(LumaOffsetL1[i]);
            }

            if (chromaArrayType != 0)
            {
                writer.WriteBit(ChromaWeightL0Flag[i]);
                if (ChromaWeightL0Flag[i])
                {
                    for (int j = 0; j < 2; j++)
                    {
                        writer.WriteSE(ChromaWeightL0[j, i]);
                        writer.WriteSE(ChromaOffsetL0[j, i]);
                    }
                }
            }
        }

        if (sliceType % 5 == 1)
        {
            for (int i = 0; i <= numRefIdxL1ActiveMinus1; i++)
            {
                writer.WriteBit(LumaWeightL1Flag[i]);
                if (LumaWeightL1Flag[i])
                {
                    writer.WriteSE(LumaWeightL1[i]);
                    writer.WriteSE(LumaOffsetL1[i]);
                }

                if (chromaArrayType != 0)
                {
                    writer.WriteBit(ChromaWeightL1Flag[i]);
                    if (ChromaWeightL1Flag[i])
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            writer.WriteSE(ChromaWeightL1[j, i]);
                            writer.WriteSE(ChromaOffsetL1[j, i]);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    ///   Writes the prediction weight table to the bitstream writer.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="chromaArrayType"></param>
    /// <param name="sliceType"></param>
    /// <param name="numRefIdxL0ActiveMinus1"></param>
    /// <param name="numRefIdxL1ActiveMinus1"></param>
    public readonly async Task WriteAsync(BitStreamWriter writer, int chromaArrayType, int sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1)
    {
        await writer.WriteUEAsync(LumaLog2WeightDenom);
        if (chromaArrayType != 0)
            await writer.WriteUEAsync(ChromaLog2WeightDenom);

        for (int i = 0; i <= numRefIdxL0ActiveMinus1; i++)
        {
            await writer.WriteBitAsync(LumaWeightL0Flag[i]);
            if (LumaWeightL0Flag[i])
            {
                await writer.WriteSEAsync(LumaWeightL0[i]);
                await writer.WriteSEAsync(LumaOffsetL1[i]);
            }

            if (chromaArrayType != 0)
            {
                await writer.WriteBitAsync(ChromaWeightL0Flag[i]);
                if (ChromaWeightL0Flag[i])
                {
                    for (int j = 0; j < 2; j++)
                    {
                        await writer.WriteSEAsync(ChromaWeightL0[j, i]);
                        await writer.WriteSEAsync(ChromaOffsetL0[j, i]);
                    }
                }
            }
        }

        if (sliceType % 5 == 1)
        {
            for (int i = 0; i <= numRefIdxL1ActiveMinus1; i++)
            {
                await writer.WriteBitAsync(LumaWeightL1Flag[i]);
                if (LumaWeightL1Flag[i])
                {
                    await writer.WriteSEAsync(LumaWeightL1[i]);
                    await writer.WriteSEAsync(LumaOffsetL1[i]);
                }

                if (chromaArrayType != 0)
                {
                    await writer.WriteBitAsync(ChromaWeightL1Flag[i]);
                    if (ChromaWeightL1Flag[i])
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            await writer.WriteSEAsync(ChromaWeightL1[j, i]);
                            await writer.WriteSEAsync(ChromaOffsetL1[j, i]);
                        }
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is PredWeightTable table && Equals(table);
    }

    /// <inheritdoc/>
    public readonly bool Equals(PredWeightTable other)
    {
        return LumaLog2WeightDenom == other.LumaLog2WeightDenom &&
               ChromaLog2WeightDenom == other.ChromaLog2WeightDenom &&
               LumaWeightL0Flag == other.LumaWeightL0Flag &&
               LumaWeightL0.Equals(other.LumaWeightL0) &&
               LumaOffsetL0.Equals(other.LumaOffsetL0) &&
               ChromaWeightL0Flag == other.ChromaWeightL0Flag &&
               EqualityComparer<ContainerMatrix2x32>.Default.Equals(ChromaWeightL0, other.ChromaWeightL0) &&
               EqualityComparer<ContainerMatrix2x32>.Default.Equals(ChromaOffsetL0, other.ChromaOffsetL0) &&
               LumaWeightL1Flag == other.LumaWeightL1Flag &&
               LumaWeightL1.Equals(other.LumaWeightL1) &&
               LumaOffsetL1.Equals(other.LumaOffsetL1) &&
               ChromaWeightL1Flag == other.ChromaWeightL1Flag &&
               EqualityComparer<ContainerMatrix2x32>.Default.Equals(ChromaWeightL1, other.ChromaWeightL1) &&
               EqualityComparer<ContainerMatrix2x32>.Default.Equals(ChromaOffsetL1, other.ChromaOffsetL1);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(LumaLog2WeightDenom);
        hash.Add(ChromaLog2WeightDenom);
        hash.Add(LumaWeightL0Flag);
        hash.Add(LumaWeightL0);
        hash.Add(LumaOffsetL0);
        hash.Add(ChromaWeightL0Flag);
        hash.Add(ChromaWeightL0);
        hash.Add(ChromaOffsetL0);
        hash.Add(LumaWeightL1Flag);
        hash.Add(LumaWeightL1);
        hash.Add(LumaOffsetL1);
        hash.Add(ChromaWeightL1Flag);
        hash.Add(ChromaWeightL1);
        hash.Add(ChromaOffsetL1);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="PredWeightTable"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(PredWeightTable left, PredWeightTable right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="PredWeightTable"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(PredWeightTable left, PredWeightTable right)
    {
        return !(left == right);
    }
}
