using Snake.Common.Geometry;

namespace Snake.Core
{
    public interface ISnakeMovementController
    {
        bool Move (Direction direction);

        Direction CurrentDirection { get; }
    }
}
