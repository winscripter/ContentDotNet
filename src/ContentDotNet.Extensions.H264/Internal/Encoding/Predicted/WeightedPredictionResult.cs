using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Internal.Encoding.Predicted;

internal struct WeightedPredictionResult
{
    public PredWeightTable WeightTable;
    public bool WeightedPredFlag;         // 0: off, 1: on (P-slices)
    public byte WeightedBiPredIdc;        // 0: off, 1: explicit, 2: implicit (B-slices)

    public WeightedPredictionResult(PredWeightTable table, bool predFlag, byte bipredIdc)
    {
        WeightTable = table;
        WeightedPredFlag = predFlag;
        WeightedBiPredIdc = bipredIdc;
    }
}
