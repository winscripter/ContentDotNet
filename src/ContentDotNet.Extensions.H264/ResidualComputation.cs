using ContentDotNet.Extensions.H264.Containers;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Computes H.264 residuals.
/// </summary>
public static class ResidualComputation
{
    /// <summary>
    ///   Computes the residual between two 16x16 matrices.
    /// </summary>
    /// <param name="predicted">Predicted value</param>
    /// <param name="expected">Expected value</param>
    /// <returns>A residual</returns>
    public static ContainerMatrix16x16 Differentiate(ContainerMatrix16x16 predicted, ContainerMatrix16x16 expected)
    {
        ContainerMatrix16x16 residual = default;

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                // Negative values are allowed
                residual[x, y] = expected[x, y] - predicted[x, y];
            }
        }

        return residual;
    }
}
