namespace ContentDotNet.Extensions.H264;

/// <summary>
///   A delegate that delegates creation of scaling lists in the SPS.
/// </summary>
/// <param name="index">
///   This is the index of the scaling list in the scaling matrix, starting with 0.
/// </param>
/// <param name="numberOfElements">
///   This is the number of elements in the scaling list - either 16 or 64.
/// </param>
/// <param name="output">
///   This is where the output should be stored.
/// </param>
/// <param name="isPresent">
///   This indicates whether the scaling list is present. If this is false, the
///   scaling list will not be written; just the '0' bit indicating that the scaling
///   list is not present.
/// </param>
public delegate void ScalingListBuild(int index, int numberOfElements, Span<int> output, out bool isPresent);
