using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Core
{
    public class SnakeBody : ISnakeBody, IStatus
    {
        public const int MaxSnakeLength = 200;

        //----------------------------------------------------------------------------------------------------------------------------------

        readonly List<Point> parts = new List<Point> ();

        //====== ctors =====================================================================================================================

        public SnakeBody (Point startPosition, int length = 3, Direction tail = Direction.Up)
        {
            Verify.InRange (length, 1, MaxSnakeLength, nameof (length));

            var nextPart = startPosition;

            for (int i = 0; i < length; i++)
            {
                parts.Add (nextPart);

                nextPart = nextPart.Add (tail.ToPoint ());
            }
        }

        //====== ISnakeBody ================================================================================================================

        public IReadOnlyList<Point> Parts => parts.ToArray ();

        //====== public properties =========================================================================================================

        public int Length => parts.Count;

        public bool IsConsumingTail => parts.Skip (1).Any (x => x == parts[0]);

        //====== public methods ============================================================================================================

        public void MoveHead (Point destination)
        {
            parts.Insert (0, destination);
            parts.RemoveAt (parts.Count - 1);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public void LengthenTail ()
        {
            if (parts.Count >= MaxSnakeLength) return;

            parts.Add (parts.Last ());
        }

        //====== IStatus ===================================================================================================================

        public IReadOnlyList<StatusItem> GetStatus ()
        {
            return new[] { new StatusItem ("Snake Length", $"{Length}/{MaxSnakeLength}") };
        }
    }
}
