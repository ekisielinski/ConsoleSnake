using Snake.Common.Geometry;
using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class SnakeDelayedMove : ISnakeMovementController
    {
        readonly ISnakeMovementController snakeMover;
        readonly SnakeSpeed  snakeSpeed;
        readonly GameTime    gameTime;

        GameTimeDelay moveDelay;

        //====== ctors

        public SnakeDelayedMove (ISnakeMovementController snakeMover, SnakeSpeed snakeSpeed, GameTime gameTime)
        {
            this.snakeMover = Verify.NotNull (snakeMover, nameof (snakeMover));
            this.snakeSpeed = Verify.NotNull (snakeSpeed, nameof (snakeSpeed));
            this.gameTime   = Verify.NotNull (gameTime,   nameof (gameTime  ));

            moveDelay = gameTime.CreateDelay (snakeSpeed.CurrentDelay);
        }

        //====== ISnakeMover

        public Direction CurrentDirection => snakeMover.CurrentDirection;

        public bool Move (Direction direction)
        {
            if (moveDelay.IsDone)
            {
                snakeMover.Move (direction);
                moveDelay = gameTime.CreateDelay (snakeSpeed.CurrentDelay);

                return true;
            }

            return false;
        }
    }
}
