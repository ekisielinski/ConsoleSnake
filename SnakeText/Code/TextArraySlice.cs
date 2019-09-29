using Snake.Common.Geometry;
using Snake.Common.Helpers;

namespace Snake.Text
{
    internal class TextArraySlice : TextArray
    {
        readonly TextArray source;

        Rectangle intersection;

        //====== ctors

        public TextArraySlice (TextArray source, Rectangle viewArea) : base (viewArea.Size)
        {
            this.source = Verify.NotNull (source, nameof (source));

            intersection = source.Size.AsRectangle.Intersect (viewArea);
        }

        //====== override: TextArray

        protected override void Write (int x, int y, TextCell? cell)
        {
            int dstX = intersection.Position.X + x;
            int dstY = intersection.Position.Y + y;

            if (intersection.Contains (new Point (dstX, dstY)))
            {
                source[dstX, dstY] = cell;
            }
        }

        protected override TextCell? Read (int x, int y)
        {
            int dstX = intersection.Position.X + x;
            int dstY = intersection.Position.Y + y;

            if (intersection.Contains (new Point (dstX, dstY)))
            {
                return source[dstX, dstY];
            }

            return null;
        }
    }
}
