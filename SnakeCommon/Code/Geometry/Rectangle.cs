using System;
using System.Diagnostics;

namespace Snake.Common.Geometry
{
    public struct Rectangle : IEquatable<Rectangle>
    {
        public Rectangle (Point position, Size size)
        {
            Position = position;
            Size     = size;
        }

        //====== public static properties

        public static Rectangle Empty { get; } = new Rectangle (Point.Zero, Size.Zero);

        //====== public properties

        public Point Position { get; }
        public Size  Size     { get; }

        public int X1 => Position.X;
        public int Y1 => Position.Y;

        [DebuggerBrowsable (DebuggerBrowsableState.Never)]
        public int X2
        {
            get
            {
                ThrowIfCannotAccessProperty (nameof (X2));

                return X1 + (Size.Width - 1);
            }
        }

        [DebuggerBrowsable (DebuggerBrowsableState.Never)]
        public int Y2
        {
            get
            {
                ThrowIfCannotAccessProperty (nameof (Y2));

                return Y1 + (Size.Height - 1);
            }
        }

        public Point TopLeft => Position;

        [DebuggerBrowsable (DebuggerBrowsableState.Never)]
        public Point TopRight
        {
            get
            {
                ThrowIfCannotAccessProperty (nameof (TopRight));
                return new Point (X1 + Size.Width - 1, Y1);
            }
        }

        [DebuggerBrowsable (DebuggerBrowsableState.Never)]
        public Point BottomLeft
        {
            get
            {
                ThrowIfCannotAccessProperty (nameof (BottomLeft));
                return new Point (X1, Y1 + Size.Height - 1);
            }
        }

        [DebuggerBrowsable (DebuggerBrowsableState.Never)]
        public Point BottomRight
        {
            get
            {
                ThrowIfCannotAccessProperty (nameof (BottomRight));
                return new Point (X1 + Size.Width - 1, Y1 + Size.Height - 1);
            }
        }

        public Point Center
        {
            get
            {
                int x = (Size.Width  / 2);
                int y = (Size.Height / 2);

                return new Point (X1 + x, Y1 + y);
            }
        }

        public Rectangle CenterAt (Point position)
        {
            var newPosition = new Point (position.X - (Size.Width / 2), position.Y - (Size.Height / 2));

            return new Rectangle (newPosition, Size);
        }

        public Rectangle Intersect (Rectangle other) => RectangleIntersection.Intersect (this, other);

        public Rectangle ChangeWidth  (int newWidth)  => new Rectangle (Position, new Size (newWidth, Size.Height));
        public Rectangle ChangeHeight (int newHeight) => new Rectangle (Position, new Size (Size.Width, newHeight));

        //====== public methods

        public bool Contains (Point point)
        {
            return Size.HasNoArea == false && (point.X >= X1 && point.X <= X2 && point.Y >= Y1 && point.Y <= Y2);
        }

        public Rectangle Translate (Point point) => new Rectangle (Position.Add (point), Size);

        //====== private methods

        private void ThrowIfCannotAccessProperty (string propertyName)
        {
            if (Size.HasNoArea == false) return;

            throw new InvalidOperationException ($"Cannot access {propertyName} property if the current {nameof (Size)} has no area.");
        }

        //====== IEquatable<Rectangle>

        public bool Equals (Rectangle other) => Position == other.Position && Size == other.Size;

        //====== operators

        public static bool operator == (Rectangle left, Rectangle right) => left.Equals (right);
        public static bool operator != (Rectangle left, Rectangle right) => left.Equals (right) == false;

        //====== override: Object

        public override string ToString () => $"Rectangle: {Position} {Size}";

        public override bool Equals (object obj) => (obj is Rectangle) && Equals ((Rectangle) obj);

        public override int GetHashCode ()
        {
            int hash = 17;

            hash = hash * 23 + Position.GetHashCode ();
            hash = hash * 23 + Size.GetHashCode ();

            return hash;
        }
    }
}
