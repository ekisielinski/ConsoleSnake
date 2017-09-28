using Snake.Common.Geometry;

namespace Snake.Core
{
    public interface ISnakeMoveQueue
    {
        void Add (Direction direction);
    }
}
