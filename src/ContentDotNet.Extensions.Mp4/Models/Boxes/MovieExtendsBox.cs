namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("mvex", "Movie Extends", "Extended movie information")]
public sealed class MovieExtendsBox : Box
{
    public MovieExtendsBox(uint size, uint type) : base(size, type)
    {
    }

    private MovieBox? _moov;

    public MovieBox? GetOwner() => _moov;
    public void UseOwner(MovieBox? moov) => _moov = moov;

    private readonly BoxStorage _boxes = new();

    public void AddChildBox(Box box) => _boxes.Add(box);

    public TBox? GetChildBox<TBox>() where TBox : Box => _boxes.Get<TBox>();

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => true;

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override void Write(BinaryWriter writer)
    {
        throw new NotImplementedException();
    }
}
