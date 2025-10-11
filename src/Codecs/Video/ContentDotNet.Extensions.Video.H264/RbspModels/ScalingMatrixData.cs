namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record ScalingMatrixData(
        List<List<int>> ScalingList4x4,
        List<List<int>> ScalingList8x8,
        List<bool> UseDefaultScalingMatrix4x4Flag,
        List<bool> UseDefaultScalingMatrix8x8Flag)
    {
        public static ScalingMatrixData CreateNew()
        {
            List<List<int>> ScalingList4x4 = [];
            List<List<int>> ScalingList8x8 = [];
            List<bool> UseDefaultScalingMatrix4x4Flag = [];
            List<bool> UseDefaultScalingMatrix8x8Flag = [];
            for (int i = 0; i < 6; i++)
            {
                ScalingList4x4.Add([.. new int[16]]);
                ScalingList8x8.Add([.. new int[64]]);
                UseDefaultScalingMatrix4x4Flag.Add(false);
                UseDefaultScalingMatrix8x8Flag.Add(false);
            }

            return new(ScalingList4x4, ScalingList8x8, UseDefaultScalingMatrix4x4Flag, UseDefaultScalingMatrix8x8Flag);
        }
    }
}
