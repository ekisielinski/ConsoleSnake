using System;

namespace Snake.Common.Geometry
{
    public struct Point : IEquatable<Point>
    {
        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }

        //====== public properties

        public int X { get; }
        public int Y { get; }

        public Point JustY => new Point (0, Y);
        public Point JustX => new Point (X, 0);

        public Point Negative => new Point (-X, -Y);

        //====== public methods

        public Point Add (Point other) => new Point (X + other.X, Y + other.Y);

        public Point AddToX (int x) => new Point (X + x, Y);
        public Point AddToY (int y) => new Point (X, Y + y);

        //====== IEquatable<Point>

        public bool Equals (Point other) => (X == other.X) && (Y == other.Y);

        //====== operators

        public static bool operator == (Point left, Point right) => left.Equals (right);
        public static bool operator != (Point left, Point right) => left.Equals (right) == false;

        //====== public static properties

        public static Point Zero => new Point (0, 0);
        public static Point One  => new Point (1, 1);

        //====== override: Object

        public override string ToString () => $"({X}, {Y})";

        public override bool Equals (object obj) => (obj is Point) && Equals ((Point) obj);

        public override int GetHashCode ()
        {
            int hash = 17;

            hash = hash * 23 + X;
            hash = hash * 23 + Y;

            return hash;
        }
    }
}
