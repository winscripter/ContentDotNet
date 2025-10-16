namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    internal static class JpegScanEncoder
    {
        public static void Sos(
            BinaryWriter writer,
            ushort ls,
            byte ns,
            ReadOnlySpan<byte> cs,
            ReadOnlySpan<byte> td,
            ReadOnlySpan<byte> ta,
            byte ss,
            byte se,
            byte ah,
            byte al)
        {
            writer.Write(JpegMarkers.Sos);

            writer.Write(ls);
            writer.Write(ns);

            for (int j = 0; j < ns; j++)
            {
                writer.Write(cs[j]);
                writer.Write((byte)((td[j] << 4) | ta[j]));
            }

            writer.Write(ss);
            writer.Write(se);
            writer.Write(ah);
            writer.Write(al);
        }

        public static void Sof(
            BinaryWriter writer,
            ushort sofMarker,
            ushort lf,
            byte p,
            ushort y,
            ushort x,
            byte nf,
            ReadOnlySpan<byte> c,
            ReadOnlySpan<byte> h,
            ReadOnlySpan<byte> v,
            ReadOnlySpan<byte> tq)
        {
            writer.Write(sofMarker);
            writer.Write(lf);
            writer.Write(p);
            writer.Write(y);
            writer.Write(x);
            writer.Write(nf);

            for (int i = 0; i < nf; i++)
            {
                writer.Write(c[i]);
                writer.Write((byte)((h[i] << 4) | v[i]));
                writer.Write(tq[i]);
            }
        }

        public static void Dqt(
            BinaryWriter writer,
            ushort lq,
            byte pq,
            byte tq,
            ReadOnlySpan<ushort> q)
        {
            writer.Write(JpegMarkers.Dqt);

            writer.Write(lq);
            writer.Write((byte)((pq << 4) | tq));

            for (int i = 0; i < 64; i++)
            {
                if (pq == 0) writer.Write((byte)q[i]);
                else writer.Write(q[i]);
            }
        }
    }
}
