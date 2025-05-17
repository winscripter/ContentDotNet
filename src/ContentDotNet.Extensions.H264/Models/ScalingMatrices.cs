using ContentDotNet.BitStream;
using ContentDotNet.Primitives;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Allows reading scaling matrices.
/// </summary>
public readonly struct ScalingMatrices
{
    /// <summary>
    ///   The reader state, where scaling matrices are.
    /// </summary>
    public ReaderState State { get; }

    /// <summary>
    ///   Count of lists in the matrix.
    /// </summary>
    public int ListCount { get; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ScalingMatrices"/> structure.
    /// </summary>
    /// <param name="state">Position in the bitstream reader that marks the start of scaling lists.</param>
    /// <param name="listCount">Number of scaling lists.</param>
    public ScalingMatrices(ReaderState state, int listCount)
    {
        State = state;
        ListCount = listCount;
    }

    /// <summary>
    ///   Checks if the list at index is present.
    /// </summary>
    /// <param name="reader">Bitstream reader</param>
    /// <param name="listIndex">Zero-based index of the list</param>
    /// <returns>A boolean that indicates whether the given scaling matrix is present.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public bool IsPresent(BitStreamReader reader, int listIndex)
    {
        if (listIndex + 1 > ListCount)
            throw new ArgumentOutOfRangeException(nameof(listIndex), "The index of the list is greater than the total amount of lists");

        ReaderState prevState = reader.GetState();
        reader.GoTo(State);

        for (int i = 0; i < listIndex + 1; i++)
        {
            if (i != listIndex)
            {
                EatList(reader, i);
                continue;
            }

            bool isPresent = reader.ReadBit();
            if (isPresent)
            {
                reader.GoTo(prevState);
                return true;
            }
        }

        reader.GoTo(prevState);
        return false;
    }

    /// <summary>
    /// Parses a scaling list at given index and returns properties about
    /// the list.
    /// </summary>
    /// <param name="reader">
    ///   This is where the scaling matrix will be read from.
    /// </param>
    /// <param name="scalingMatrix">
    ///   This is the output, where the scaling matrix data will be stored.
    /// </param>
    /// <param name="isPresent">
    ///   This is the output boolean that indicates whether the scaling list
    ///   is present. If this is <see langword="false"/>, that means that
    ///   the function did not write anything to <paramref name="scalingMatrix"/>.
    /// </param>
    /// <param name="index">
    ///   Zero-based index of the list you want to read. Just make sure it's
    ///   less than <see cref="ListCount"/>.
    /// </param>
    /// <remarks>
    ///   <para>
    ///     See below for examples.
    ///   </para>
    ///   <para>
    ///     <b>Example 1 (simpler):</b>
    ///     <example>
    ///       <code>
    ///         const int desiredIndex = 8; // Example
    ///         ScalingMatrices matrices = ...;
    ///         int[] output = new int[ScalingMatrices.GetListLength(desiredIndex)];
    ///         matrices.ReadList(your_bitstream, matrices, out bool isPresent, desiredIndex);
    ///       </code>
    ///     </example>
    ///     <b>Example 2 (faster):</b>
    ///     <example>
    ///       <code>
    ///         const int desiredIndex = 8; // Example
    ///         ScalingMatrices matrices = ...;
    ///         Span&lt;int&gt; output = stackalloc int[ScalingMatrices.GetListLength(desiredIndex)];
    ///         matrices.ReadList(your_bitstream, matrices, out bool isPresent, desiredIndex);
    ///       </code>
    ///     </example>
    ///   </para>
    /// </remarks>
    public void ReadList(BitStreamReader reader, Span<int> scalingMatrix, out bool isPresent, int index)
    {
        if (index + 1 > ListCount)
            throw new ArgumentOutOfRangeException(nameof(index), "The index of the list is greater than the total amount of lists");

        ReaderState prevState = reader.GetState();
        reader.GoTo(State);

        for (int i = 0; i < index + 1; i++)
        {
            if (i != index)
            {
                EatList(reader, i);
                continue;
            }

            isPresent = reader.ReadBit();
            if (!isPresent)
            {
                reader.GoTo(prevState);
                return;
            }

            ParseScalingList(reader, scalingMatrix, i);
            return;
        }

        reader.GoTo(prevState);
        isPresent = false;
        return;
    }

    /// <summary>
    /// Parses a scaling list at given index and returns properties about
    /// the list.
    /// </summary>
    /// <param name="reader">
    ///   This is where the scaling matrix will be read from.
    /// </param>
    /// <param name="scalingMatrix">
    ///   This is the output, where the scaling matrix data will be stored.
    /// </param>
    /// <param name="isPresent">
    ///   This is the output boolean that indicates whether the scaling list
    ///   is present. If this is <see langword="false"/>, that means that
    ///   the function did not write anything to <paramref name="scalingMatrix"/>.
    /// </param>
    /// <param name="index">
    ///   Zero-based index of the list you want to read. Just make sure it's
    ///   less than <see cref="ListCount"/>.
    /// </param>
    /// <remarks>
    ///   <para>
    ///     See below for examples.
    ///   </para>
    ///   <para>
    ///     <b>Example 1 (simpler):</b>
    ///     <example>
    ///       <code>
    ///         const int desiredIndex = 8; // Example
    ///         ScalingMatrices matrices = ...;
    ///         int[] output = new int[ScalingMatrices.GetListLength(desiredIndex)];
    ///         matrices.ReadList(your_bitstream, matrices, out bool isPresent, desiredIndex);
    ///       </code>
    ///     </example>
    ///     <b>Example 2 (faster):</b>
    ///     <example>
    ///       <code>
    ///         const int desiredIndex = 8; // Example
    ///         ScalingMatrices matrices = ...;
    ///         Span&lt;int&gt; output = stackalloc int[ScalingMatrices.GetListLength(desiredIndex)];
    ///         matrices.ReadList(your_bitstream, matrices, out bool isPresent, desiredIndex);
    ///       </code>
    ///     </example>
    ///   </para>
    /// </remarks>
    public void ReadList(BitStreamReader reader, Memory<int> scalingMatrix, out bool isPresent, int index)
    {
        if (index + 1 > ListCount)
            throw new ArgumentOutOfRangeException(nameof(index), "The index of the list is greater than the total amount of lists");

        ReaderState prevState = reader.GetState();
        reader.GoTo(State);

        for (int i = 0; i < index + 1; i++)
        {
            if (i != index)
            {
                EatList(reader, i);
                continue;
            }

            isPresent = reader.ReadBit();
            if (!isPresent)
            {
                reader.GoTo(prevState);
                return;
            }

            ParseScalingList(reader, scalingMatrix.Span, i);
            return;
        }

        reader.GoTo(prevState);
        isPresent = false;
        return;
    }

    // NOTE: This does not account for the 'is present' flag.
    private static void EatList(BitStreamReader reader, int index)
    {
        Span<int> sp = stackalloc int[index < 6 ? 16 : 64];
        ParseScalingList(reader, sp, index);
    }

    private static void ParseScalingList(BitStreamReader reader, Span<int> output, int index)
    {
        int elementsCount = 0;
        int size = index < 6 ? 16 : 64;
        int lastScale = 8;
        int nextScale = 8;
        for (int j = 0; j < size; j++)
        {
            if (nextScale != 0)
            {
                int deltaScale = reader.ReadSE();
                nextScale = (lastScale + deltaScale + 256) % 256;
                output[elementsCount++] = deltaScale;
            }
            lastScale = nextScale == 0 ? lastScale : nextScale;
        }
    }

    /// <summary>
    /// Returns the length of the elements in the scaling list based on its zero-based index.
    /// </summary>
    /// <param name="index">Zero-based scaling list index</param>
    /// <returns>Number of elements inside of it (either 16 or 64).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetListLength(int index) => index < 6 ? 16 : 64;

    /// <summary>
    ///   Parses a single scaling list.
    /// </summary>
    /// <param name="reader">The bitstream reader that marks the start of the scaling list.</param>
    /// <param name="size">Size of the scaling list.</param>
    /// <returns>An array referrer where the scaling list is located.</returns>
    public static ArrayReferrer ParseScalingList(BitStreamReader reader, int size)
    {
        int elementsCount = 0;
        int lastScale = 8;
        int nextScale = 8;
        for (int j = 0; j < size; j++)
        {
            if (nextScale != 0)
            {
                int deltaScale = reader.ReadSE();
                nextScale = (lastScale + deltaScale + 256) % 256;
                elementsCount++;
            }
            lastScale = nextScale == 0 ? lastScale : nextScale;
        }
        return new ArrayReferrer(elementsCount);
    }

    /// <summary>
    /// Writes the specified scaling list and its index.
    /// </summary>
    /// <param name="writer">Bitstream to write the scaling list to.</param>
    /// <param name="index">Index of the scaling list</param>
    /// <param name="scalingList">Scaling list elements.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void WriteScalingList(BitStreamWriter writer, int index, ReadOnlySpan<int> scalingList)
    {
        int scalingMatrixSize = GetListLength(index);
        if (scalingMatrixSize is not 16 or 64)
            throw new InvalidOperationException("Scaling matrix of the SPS must have either 16 or 64 elements");
        if (scalingList.Length < scalingMatrixSize)
            throw new InvalidOperationException("Provided scaling matrix must have at minimum of " + scalingMatrixSize + " elements");

        int elementsCount = 0;
        int lastScale = 8;
        int nextScale = 8;
        for (int j = 0; j < scalingMatrixSize; j++)
        {
            if (nextScale != 0)
            {
                int deltaScale = scalingList[elementsCount++];
                writer.WriteSE(deltaScale);
                nextScale = (lastScale + deltaScale + 256) % 256;
                elementsCount++;
            }
            lastScale = nextScale == 0 ? lastScale : nextScale;
        }
    }

    /// <summary>
    /// Writes the scaling list.
    /// </summary>
    /// <param name="writer">Bitstream to write the scaling list to.</param>
    /// <param name="index">Scaling list index</param>
    /// <param name="scalingMatrix">Scaling list elements.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void WriteScalingList(BitStreamWriter writer, int index, ReadOnlyMemory<int> scalingMatrix) => WriteScalingList(writer, index, scalingMatrix.Span);

    /// <summary>
    /// Writes just the scaling list.
    /// </summary>
    /// <param name="writer">Bitstream to write the scaling list to.</param>
    /// <param name="index">Index of the scaling list</param>
    /// <param name="scalingList">Scaling list elements.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task WriteScalingListAsync(BitStreamWriter writer, int index, ReadOnlyMemory<int> scalingList)
    {
        int scalingMatrixSize = ScalingMatrices.GetListLength(index);
        if (scalingMatrixSize is not 16 or 64)
            throw new InvalidOperationException("Scaling matrix of the SPS must have either 16 or 64 elements");
        if (scalingList.Length < scalingMatrixSize)
            throw new InvalidOperationException("Provided scaling matrix must have at minimum of " + scalingMatrixSize + " elements");

        int elementsCount = 0;
        int lastScale = 8;
        int nextScale = 8;
        for (int j = 0; j < scalingMatrixSize; j++)
        {
            if (nextScale != 0)
            {
                int deltaScale = scalingList.Span[elementsCount++];
                await writer.WriteSEAsync(deltaScale);
                nextScale = (lastScale + deltaScale + 256) % 256;
                elementsCount++;
            }
            lastScale = nextScale == 0 ? lastScale : nextScale;
        }
    }

    /// <summary>
    /// Writes the individual scalingl ist.
    /// </summary>
    /// <param name="writer">Bitstream to write the scaling list to.</param>
    /// <param name="index">Scaling list index</param>
    /// <param name="scalingMatrix">Scaling list elements.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task WriteScalingListAsync(BitStreamWriter writer, int index, int[] scalingMatrix) => await WriteScalingListAsync(writer, index, scalingMatrix);
}
