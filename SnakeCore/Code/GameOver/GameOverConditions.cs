using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class GameOverConditions : GameObject, IGameOverJustification
    {
        readonly SnakeBody      snakeBody;
        readonly Terrain        terrain;
        readonly AppleGenerator applesGenerator;

        //====== ctors

        public GameOverConditions (SnakeBody snakeBody, Terrain terrain, AppleGenerator applesGenerator, GameTime gameTime) : base (gameTime)
        {
            this.snakeBody       = Verify.NotNull (snakeBody, nameof (snakeBody));
            this.terrain         = Verify.NotNull (terrain, nameof (terrain));
            this.applesGenerator = Verify.NotNull (applesGenerator, nameof (applesGenerator));
        }

        //====== public properties

        public string Reason { get; private set; } = string.Empty;

        public bool IsGameOver => Reason != string.Empty;

        //====== override: GameObject

        protected override void UpdateImpl () => CheckConditions ();

        //====== private methods

        private void CheckConditions ()
        {
            Reason = string.Empty;

            if (snakeBody.IsConsumingTail)
            {
                Reason = "You ate yourself.";

                return;
            }

            if (terrain.Size.AsRectangle.Contains (snakeBody.Head ()) == false)
            {
                Reason = "You left the field.";

                return;
            }

            if (applesGenerator.IsApplesLimitReached)
            {
                Reason = $"Too many apples on the field ({applesGenerator.MaxApples}).";

                return;
            }
        }
    }
}
