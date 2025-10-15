namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Tables
{
    // These are the precomputed tables
    //
    // C# script to generate DCT Table (XU, YV):
    //   static float[] CosineTables()
    //   {
    //       float[] db = new float[64];
    //       for (int x = 0; x <= 7; x++) for (int u = 0; u <= 7; u++)
    //           db[u * 8 + u] = MathF.Cos(((2F * x + 1F) * u * MathF.PI) / 16F);
    //       return db;
    //   }
    //
    //   string.Join(Environment.NewLine, CosineTables().Chunk(8).Select(chunk => string.Join(", ", chunk.Select(x => x.ToString() + "F"))))
    //
    internal static class DctTables
    {
        public static ReadOnlySpan<float> Table =>
        [
            1F, 0F, 0F, 0F, 0F, 0F, 0F, 0F,
            0F, -0.9807853F, 0F, 0F, 0F, 0F, 0F, 0F,
            0F, 0F, 0.92387956F, 0F, 0F, 0F, 0F, 0F,
            0F, 0F, 0F, -0.8314698F, 0F, 0F, 0F, 0F,
            0F, 0F, 0F, 0F, 0.7071068F, 0F, 0F, 0F,
            0F, 0F, 0F, 0F, 0F, -0.55557084F, 0F, 0F,
            0F, 0F, 0F, 0F, 0F, 0F, 0.3826839F, 0F,
            0F, 0F, 0F, 0F, 0F, 0F, 0F, -0.19509155F
        ];
    }
}
