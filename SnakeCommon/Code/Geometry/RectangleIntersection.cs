using System;

namespace Snake.Common.Geometry
{
    public sealed class RectangleIntersection
    {
        public RectangleIntersection (Rectangle r1, Rectangle r2)
        {
            RectangleOne = r1;
            RectangleTwo = r2;

            Intersection = Intersect (r1, r2);
        }

        //====== public properties =========================================================================================================

        public Rectangle RectangleOne { get; }
        public Rectangle RectangleTwo { get; }

        public Rectangle Intersection { get; }

        //----------------------------------------------------------------------------------------------------------------------------------

        public bool HasNoIntersection => Intersection.Size.HasNoArea;

        //----------------------------------------------------------------------------------------------------------------------------------

        public Rectangle RelativelyToRectOne
        {
            get
            {
                if (HasNoIntersection) return Rectangle.Empty;

                int x = Intersection.Position.X - RectangleOne.Position.X;
                int y = Intersection.Position.Y - RectangleOne.Position.Y;

                return new Rectangle (new Point (x, y), Intersection.Size);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public Rectangle RelativelyToRectTwo
        {
            get
            {
                if (HasNoIntersection) return Rectangle.Empty;

                int x = Intersection.Position.X - RectangleTwo.Position.X;
                int y = Intersection.Position.Y - RectangleTwo.Position.Y;

                return new Rectangle (new Point (x, y), Intersection.Size);
            }
        }

        //====== public static methods =====================================================================================================

        public static Rectangle Intersect (Rectangle r1, Rectangle r2)
        {
            if (r1.Size.HasNoArea || r2.Size.HasNoArea || r2.X1 > r1.X2 || r2.X2 < r1.X1 || r2.Y1 > r1.Y2 || r2.Y2 < r1.Y1)
            {
                return Rectangle.Empty;
            }

            int x1 = Math.Max (r1.X1, r2.X1);
            int y1 = Math.Max (r1.Y1, r2.Y1);

            int x2 = Math.Min (r1.X2, r2.X2);
            int y2 = Math.Min (r1.Y2, r2.Y2);

            int width  = x2 - x1 + 1;
            int height = y2 - y1 + 1;

            var result = new Rectangle (new Point (x1, y1), new Size (width, height));

            return result;
        }

        //====== override: Object ==========================================================================================================

        public override string ToString () => $"{nameof (RectangleIntersection)}: {Intersection}";
    }
}
