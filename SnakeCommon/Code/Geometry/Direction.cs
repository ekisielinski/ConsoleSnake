using System;

namespace Snake.Common.Geometry
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    //--------------------------------------------------------------------------------------------------------------------------------------

    public static class DirectionExtensions
    {
        public static Point ToPoint (this Direction me)
        {
            switch (me)
            {
                case Direction.Up    : return new Point ( 0, -1);
                case Direction.Down  : return new Point ( 0,  1);
                case Direction.Left  : return new Point (-1,  0);
                case Direction.Right : return new Point ( 1,  0);

                default:
                    throw new InvalidOperationException ();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static bool IsOpposite (this Direction me, Direction direction)
        {
            switch (me)
            {
                case Direction.Up    : return direction == Direction.Down;
                case Direction.Down  : return direction == Direction.Up;
                case Direction.Left  : return direction == Direction.Right;
                case Direction.Right : return direction == Direction.Left;

                default:
                    throw new InvalidOperationException ();
            }
        }
    }
}
