namespace ContentDotNet;

/// <summary>
///   Stride-based image factory.
/// </summary>
public interface IStrideImageFactory
{
    int LWidth { get; }
    int CbCrWidth { get; }

    byte GetLuma(int x, int y);
    byte GetLuma(int index);
    void SetLuma(int x, int y, byte with);
    void SetLuma(int index, byte with);

    byte GetCb(int x, int y);
    byte GetCb(int index);
    void SetCb(int x, int y, byte with);
    void SetCr(int index, byte with);

    byte GetCr(int x, int y);
    byte GetCr(int index);
    void SetCr(int x, int y, byte with);
    void SetCb(int index, byte with);

    CbCr GetCbCr(int x, int y);
    CbCr GetCbCr(int index);
    void SetCbCr(int x, int y, CbCr with);
    void SetCbCr(int index, CbCr with);
}
