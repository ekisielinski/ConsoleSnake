using Snake.Common.Geometry;
using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class SnakeValidMove : ISnakeMovementController
    {
        readonly ISnakeMovementController snakeMover;

        //====== ctors =====================================================================================================================

        public SnakeValidMove (ISnakeMovementController snakeMover)
        {
            this.snakeMover = Verify.NotNull (snakeMover, nameof (snakeMover));
        }

        //====== ISnakeMover ===============================================================================================================

        public Direction CurrentDirection => snakeMover.CurrentDirection;

        //----------------------------------------------------------------------------------------------------------------------------------

        public bool Move (Direction direction)
        {
            if (direction.IsOpposite (CurrentDirection)) direction = CurrentDirection;

            snakeMover.Move (direction);

            return true;
        }
    }
}
