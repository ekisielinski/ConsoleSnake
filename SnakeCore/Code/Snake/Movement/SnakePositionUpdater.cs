using Snake.Common.Geometry;
using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class SnakePositionUpdater : GameObject
    {
        readonly ISnakeMovementController movementController;
        readonly SnakeMoveQueue moveQueue;

        //====== ctors =====================================================================================================================

        public SnakePositionUpdater (ISnakeMovementController movementController, SnakeMoveQueue moveQueue, GameTime gameTime) : base (gameTime)
        {
            this.movementController = Verify.NotNull (movementController, nameof (movementController));
            this.moveQueue  = Verify.NotNull (moveQueue, nameof (moveQueue));
        }

        //====== override: GameObject ======================================================================================================

        protected override void UpdateImpl ()
        {
            Direction? direction = moveQueue.Peek ();

            if (direction == null)
            {
                movementController.Move (movementController.CurrentDirection);
            }
            else
            {
                if (movementController.Move (direction.Value))
                {
                    moveQueue.Dequeue ();
                }
            }
        }
    }
}
