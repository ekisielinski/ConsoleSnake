using Snake.Common.Geometry;
using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class SnakeFreeMove : ISnakeMovementController
    {
        readonly SnakeBody snakeBody;

        //====== ctors =====================================================================================================================

        public SnakeFreeMove (SnakeBody snakeBody, Direction initialDirection)
        {
            this.snakeBody = Verify.NotNull (snakeBody, nameof (snakeBody));

            CurrentDirection = initialDirection;
        }

        //====== ISnakeMover ===============================================================================================================

        public Direction CurrentDirection { get; private set; }

        //----------------------------------------------------------------------------------------------------------------------------------

        public bool Move (Direction direction)
        {
            Point newPosition = snakeBody.Head ().Add (direction.ToPoint ());

            snakeBody.MoveHead (newPosition);

            CurrentDirection = direction;

            return true;
        }
    }
}
