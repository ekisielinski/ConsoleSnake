using Snake.Common.Geometry;

namespace Snake.Text
{
    public interface ITextArrayReader
    {
        Size Size { get; }

        TextCell? this[int x, int y] { get; }
    }
}
