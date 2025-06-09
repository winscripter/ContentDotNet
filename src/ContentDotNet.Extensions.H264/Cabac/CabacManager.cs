namespace ContentDotNet.Extensions.H264.Cabac;

internal sealed class CabacManager
{
    private const int TotalCabacContexts = 54;

    private readonly CabacContext[] _cabacs = new CabacContext[TotalCabacContexts];

    public CabacManager()
    {
    }
}
