using Snake.Common.Geometry;

namespace Snake.Core
{
    public sealed class SnakeBodyPartEntity : TerrainEntity
    {
        public SnakeBodyPartEntity (Point position, bool isHead) : base (position)
        {
            IsHead = isHead;
        }

        //====== public properties =========================================================================================================

        public bool IsHead { get; }
    }
}
