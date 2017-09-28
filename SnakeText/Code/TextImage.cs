using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System.Diagnostics;

namespace Snake.Text
{
    public sealed class TextImage : TextArray
    {
        readonly TextCell?[,] cells; // null = transparent cell

        TextCanvas canvas;

        //====== ctors =====================================================================================================================

        public TextImage (Size size) : base (size)
        {
            cells = new TextCell?[size.Width, size.Height];
        }

        //====== override: TextArray =======================================================================================================

        protected override void Write (int x, int y, TextCell? cell) => cells[x, y] = cell;

        protected override TextCell? Read (int x, int y) => cells[x, y];

        //====== public properties =========================================================================================================

        [DebuggerBrowsable (DebuggerBrowsableState.Never)]

        public TextCanvas Canvas => canvas ?? (canvas = new TextCanvas (this));

        //====== public static methods =====================================================================================================

        public static TextImage CreateCopyFrom (ITextArrayReader textArray)
        {
            Verify.NotNull (textArray, nameof (textArray));

            var result = new TextImage (textArray.Size);

            for (int x = 0; x < textArray.Size.Width; x++)
            {
                for (int y = 0; y < textArray.Size.Height; y++)
                {
                    result.cells[x, y] = textArray[x, y];
                }
            }

            return result;
        }
    }
}
