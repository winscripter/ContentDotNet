using ContentDotNet.Extensions.Mp4.Models.Boxes;

namespace ContentDotNet.Extensions.Mp4;

/// <summary>
///   Stores boxes.
/// </summary>
public sealed class BoxStorage
{
    private readonly List<Box> _boxes;

    public BoxStorage() => _boxes = [];

    public void Add(Box box)
    {
        if (!_boxes.Any(b => b.GetType() == box.GetType()))
        {
            _boxes.Add(box);
        }
        else
        {
            int index = 0;
            foreach (Box iteratingBox in _boxes)
            {
                if (iteratingBox.GetType() == box.GetType())
                    break;
                else
                    index++;
            }
            _boxes.RemoveAt(index);
            _boxes.Add(box);
        }
    }

    public TBox? Get<TBox>() where TBox : Box
    {
        return _boxes.Cast<TBox>().FirstOrDefault();
    }
}
