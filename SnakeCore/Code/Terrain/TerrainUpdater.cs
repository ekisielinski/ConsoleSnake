using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class TerrainUpdater : GameObject
    {
        readonly Terrain terrain;
        readonly ISnakeBody snakeBody;

        //====== ctors

        public TerrainUpdater (Terrain terrain, ISnakeBody snakeBody, GameTime gameTime) : base (gameTime)
        {
            this.terrain   = Verify.NotNull (terrain, nameof (terrain));
            this.snakeBody = Verify.NotNull (snakeBody, nameof (snakeBody));
        }

        //====== override: gameObject

        protected override void UpdateImpl ()
        {
            terrain.RemoveAll (x => x.IsDead);

            UpdateSnakeBodyEntities ();
        }

        //====== private methods

        private void UpdateSnakeBodyEntities ()
        {
            terrain.RemoveAll (x => x is SnakeBodyPartEntity);

            terrain.Add (new SnakeBodyPartEntity (snakeBody.Head (), true));

            foreach (var item in snakeBody.Tail ())
            {
                terrain.Add (new SnakeBodyPartEntity (item, false));
            }
        }
    }
}
