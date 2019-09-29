using Snake.Common.Geometry;
using Snake.Core;
using System;
using System.Collections.Generic;

namespace SnakeGame.Core
{
    public sealed class GameLogic : GameObject, IStatus
    {
        public event Action AppleConsumed;

        readonly Terrain            terrain;

        readonly SnakeBody          snakeBody;
        readonly SnakeSpeed         speed;

        readonly AppleGenerator     applesGenerator;
        readonly AppleConsumer      applesConsumer;

        readonly TerrainUpdater     terrainUpdater;

        readonly GameOverConditions gameOverConditions;

        readonly Score              score;

        readonly SnakeMoveQueue     moveQueue;

        readonly GameObjectsUpdater updater;

        //====== ctors

        public GameLogic (Size terrainSize, GameTime gameTime) : base (gameTime)
        {
            terrain            = new Terrain (terrainSize);

            snakeBody          = new SnakeBody (new Point (terrain.Size.Width / 2, terrain.Size.Height / 2));
            speed              = new SnakeSpeed ();

            applesGenerator    = new AppleGenerator (terrain, gameTime);
            applesConsumer     = new AppleConsumer (terrain, snakeBody, gameTime);

            terrainUpdater     = new TerrainUpdater (terrain, snakeBody, gameTime);

            gameOverConditions = new GameOverConditions (snakeBody, terrain, applesGenerator, gameTime);

            score              = new Score ();

            moveQueue          = new SnakeMoveQueue ();

            //---

            updater = new GameObjectsUpdater ();
            SetupGameObjectsUpdater ();

            applesConsumer.Consumed += AppleConsumer_AppleConsumed;
        }

        //====== override: GameObject

        protected override void UpdateImpl ()
        {
            if (gameOverConditions.IsGameOver) return;

            updater.Update ();
        }

        //====== public properties

        public ISnakeMoveQueue        DirectionChanger      => moveQueue;
        public ITerrainEntityReader   TerrainReader         => terrain;
        public IScoreStatus           ScoreStatus           => score;
        public IGameOverJustification GameOverJustification => gameOverConditions;

        //====== private methods

        private void SetupGameObjectsUpdater ()
        {
            updater.Add (gameOverConditions);
            updater.Add (applesGenerator);
            updater.Add (terrainUpdater);
            updater.Add (applesConsumer);
            updater.Add (CreateSnakePositionUpdater ());
        }

        private void AppleConsumer_AppleConsumed (AppleEntity obj)
        {
            snakeBody.LengthenTail ();
            speed.Increase ();

            score.Increase (obj.Score);

            AppleConsumed?.Invoke ();
        }

        private SnakePositionUpdater CreateSnakePositionUpdater ()
        {
            var anyMove     = new SnakeFreeMove     (snakeBody, Direction.Down);
            var validMove   = new SnakeValidMove   (anyMove);
            var delayedMove = new SnakeDelayedMove (validMove, speed, gameTime);

            return new SnakePositionUpdater (delayedMove, moveQueue, gameTime);
        }

        //====== IStatus

        public IReadOnlyList<StatusItem> GetStatus ()
        {
            List<StatusItem> items = new List<StatusItem> ();

            items.AddRange (applesConsumer.GetStatus ());
            items.AddRange (applesGenerator.GetStatus ());
            items.AddRange (speed.GetStatus ());
            items.AddRange (snakeBody.GetStatus ());

            items.Add (new StatusItem ("Game Time", gameTime.Elapsed.ToString ("mm\\:ss")));

            return items;
        }
    }
}
