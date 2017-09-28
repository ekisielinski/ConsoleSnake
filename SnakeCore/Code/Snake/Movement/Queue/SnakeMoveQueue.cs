using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System.Collections.Generic;

namespace Snake.Core
{
    public sealed class SnakeMoveQueue : ISnakeMoveQueue
    {
        readonly int maxCapacity;

        readonly Queue<Direction> queue = new Queue<Direction> ();

        //====== ctors =====================================================================================================================

        public SnakeMoveQueue (int maxCapacity = 6)
        {
            this.maxCapacity = Verify.InRange (maxCapacity, 1, 20, nameof (maxCapacity));
        }

        //====== public methods ============================================================================================================

        public void Add (Direction direction)
        {
            if (queue.Count == 0 || queue.ToArray ()[0] != direction)
            {
                queue.Enqueue (direction);
            }

            if (queue.Count > maxCapacity)
            {
                queue.Dequeue ();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public Direction? Peek () => queue.Count == 0 ? (Direction?) null : queue.Peek ();

        public void Dequeue () => queue.Dequeue ();
    }
}
