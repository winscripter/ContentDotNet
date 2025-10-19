namespace ContentDotNet.Extensions.Video.Mp4.Boxes.IO
{
    internal static class Check
    {
        public static void DataIs<T>(Mp4Box mp4b, out T? value)
            where T : IMp4BoxData
        {
            if (mp4b.Data is not T val)
            {
                throw new InvalidDataException($"Mp4Box data is not of expected type {typeof(T).FullName}, actual type is {mp4b.Data?.GetType().FullName ?? "null"}.");
            }
            else
            {
                value = val;
            }
        }
    }
}
