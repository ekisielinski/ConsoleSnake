using Snake.Common.Geometry;
using System;
using System.Text;

namespace Snake.Text
{
    public abstract class TextArray : ITextArrayReader
    {
        protected TextArray (Size size) => Size = size;

        //====== abstract methods

        protected abstract void      Write (int x, int y, TextCell? cell);
        protected abstract TextCell? Read  (int x, int y);

        //====== ITextArrayReader

        public Size Size { get; }

        public TextCell? this[int x, int y]
        {
            get
            {
                ThrowIfInvalidCoords (x, y);
                return Read (x, y);
            }

            set // not in ITextArrayReader
            {
                ThrowIfInvalidCoords (x, y);
                Write (x, y, value);
            }
        }

        //====== public methods

        public string Stringify ()
        {
            var sb = new StringBuilder (Size.Area + (Size.Height * 2)); // with line breaks

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    char ch = this[x, y]?.Character ?? ' ';

                    sb.Append (ch);
                }

                sb.AppendLine ();
            }

            return sb.ToString ();
        }

        //====== private methods

        private void ThrowIfInvalidCoords (int x, int y)
        {
            if (x < 0 || x >= Size.Width || y < 0 || y >= Size.Height)
            {
                throw new ArgumentOutOfRangeException ($"Invalid coordinates. X={x} Y={y}, Size={Size}");
            }
        }

        //====== override: Object

        public override string ToString () => Stringify ();
    }
}
