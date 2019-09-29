using Snake.Common.Geometry;
using Snake.Text;
using System;

namespace Snake.Hardware.Display
{
    public sealed class ScreenBuffer : TextArray
    {
        readonly TextImage buffer;

        //====== ctors

        public ScreenBuffer (Size size) : base (size)
        {
            buffer = new TextImage (size);
        }

        //====== override: TextArray2D

        protected override TextCell? Read (int x, int y) => buffer[x, y];

        protected override void Write (int x, int y, TextCell? cell)
        {
            if (cell?.IsValid == false) throw new ArgumentException ($"The {nameof (cell)} parameter is in invalid state.");

            var currentCell = buffer[x, y];

            if (cell.Equals (currentCell) == false)
            {
                buffer[x, y] = cell;
                Generation++;
            }
        }

        //====== public properties

        public long Generation { get; private set; } = 0;
    }
}
