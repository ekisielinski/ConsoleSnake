using Snake.Common.Geometry;

namespace Snake.Core
{
    public abstract class TerrainEntity
    {
        public TerrainEntity (Point position)
        {
            Position = position;
        }

        //====== public properties

        public Point Position { get; }

        //====== public virtual properties

        public virtual bool IsDead => false;

        //====== override: Object

        public override string ToString () => $"{Position} :: {GetType ().Name}" + (IsDead ? " (dead)" : string.Empty);
    }
}
