using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Text
{
    public sealed class TextCanvas
    {
        readonly TextArray textArray;

        //====== ctors

        public TextCanvas (TextArray textArray)
        {
            this.textArray = Verify.NotNull (textArray, nameof (textArray));
        }

        //====== public properties

        public Size Size => textArray.Size;

        //====== public methods

        #region basic

        public void Write (Point position, TextCell? cell)
        {
            if (AreCoordsOutOfRange (position)) return;

            textArray[position.X, position.Y] = cell;
        }

        public void Clear () => Fill (null);

        public void ClearColor (Color16 backColor = Color16.Black) => Fill (new TextCell (' ', backColor, backColor));

        public void Fill (TextCell? cell)
        {
            foreach (var coord in EnumerateCoords ()) Write (coord, cell);
        }

        public void WriteColor (Point position, Color16? foreColor, Color16? backColor)
        {
            if ((foreColor == null && backColor == null) || AreCoordsOutOfRange (position)) return;

            char newCharacter = ' ';

            Color16 newForeColor;
            Color16 newBackColor;

            var cell = textArray[position.X, position.Y];

            if (cell == null)
            {
                if (backColor == null) return;

                newForeColor = foreColor ?? backColor.Value;
                newBackColor = backColor.Value;
            }
            else
            {
                newForeColor = foreColor ?? cell.Value.ForeColor;
                newBackColor = backColor ?? cell.Value.BackColor;
                newCharacter = cell.Value.Character;
            }

            Write (position, new TextCell (newCharacter, newForeColor, newBackColor));
        }

        #endregion

        #region text

        public void WriteText (Point position, string message, Color16 foreColor, Color16 backColor)
        {
            var lines = message.Split (new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                WriteLine (position.AddToY (i), lines[i], foreColor, backColor);
            }
        }

        public void WriteTextCenter (int y, string message, Color16 foreColor, Color16 backColor)
        {
            int maxLineLength = message.Split (new string[] { "\r\n" }, StringSplitOptions.None).Max (x => x.Length);

            var position = new Point ((Size.Width / 2) - (maxLineLength / 2), y);

            WriteText (position, message, foreColor, backColor);
        }

        private void WriteLine (Point position, string message, Color16 foreColor, Color16 backColor)
        {
            if (Size.AsRectangle.Contains (position.JustY) == false) return;

            for (int i = 0; i < message.Length; i++) // TODO: can be optimized, no need to draw not visible chars
            {
                var letterPosition = position.AddToX (i);

                if (letterPosition.X >= Size.Width) return;

                Write (letterPosition, new TextCell (message[i], foreColor, backColor));
            }
        }

        #endregion

        #region frames

        public void HLine (Point startPosition, int length, TextCell cell)
        {
            if (length < 1 || startPosition.Y < 0 || startPosition.Y >= Size.Height) return;

            for (int i = 0; i < length; i++) // TODO: need optimization
            {
                Write (startPosition.AddToX (i), cell);
            }
        }

        public void VLine (Point startPosition, int length, TextCell cell)
        {
            if (length < 1 || startPosition.X < 0 || startPosition.X >= Size.Width) return;

            for (int i = 0; i < length; i++) // TODO: need optimization
            {
                Write (startPosition.AddToY (i), cell);
            }
        }

        public void Frame (Rectangle area, Color16 foreColor, Color16 backColor, bool thin = false)
        {
            if (Size.AsRectangle.Intersect (area).Size.HasNoArea) return;

            var view = Slice (area);

            if (view.Size.Width < 2 || view.Size.Height < 2) return;

            var hLineCell = new TextCell (thin ? '─' : '═', foreColor, backColor);
            var vLineCell = new TextCell (thin ? '│' : '║', foreColor, backColor);

            var rect = view.Size.AsRectangle;

            if (rect.Size.Width > 2)
            {
                view.HLine (rect.TopLeft, rect.Size.Width, hLineCell);
                view.HLine (rect.BottomLeft, rect.Size.Width, hLineCell);
            }

            if (rect.Size.Height > 2)
            {
                view.VLine (rect.TopLeft, rect.Size.Height, vLineCell);
                view.VLine (rect.TopRight, rect.Size.Height, vLineCell);
            }

            view.Write (rect.TopLeft,     new TextCell (thin ? '┌' : '╔', foreColor, backColor));
            view.Write (rect.TopRight,    new TextCell (thin ? '┐' : '╗', foreColor, backColor));
            view.Write (rect.BottomLeft,  new TextCell (thin ? '└' : '╚', foreColor, backColor));
            view.Write (rect.BottomRight, new TextCell (thin ? '┘' : '╝', foreColor, backColor));
        }

        #endregion

        #region images

        public void DrawImage (ITextArrayReader textImage, Point position, bool allowTransparency = true)
        {
            Verify.NotNull (textImage, nameof (textImage));

            var intersection = new RectangleIntersection (Size.AsRectangle, textImage.Size.AsRectangle.Translate (position));

            if (intersection.HasNoIntersection) return;

            var canvasSlice = intersection.RelativelyToRectOne;
            var imageSlice  = intersection.RelativelyToRectTwo;


            int width  = intersection.Intersection.Size.Width;
            int height = intersection.Intersection.Size.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var imageCell = textImage[imageSlice.Position.X + x, imageSlice.Position.Y + y];

                    if (imageCell == null && allowTransparency) continue;

                    var dstPosition = new Point (canvasSlice.Position.X + x, canvasSlice.Position.Y + y);

                    Write (dstPosition, imageCell);
                }
            }
        }

        #endregion

        #region effects

        public void Grayscale () => Colorize (Color16.Gray, Color16.Black);

        public void Colorize (Color16? foreColor, Color16? backColor)
        {
            if (foreColor == null && backColor == null) return;

            foreach (var coord in EnumerateCoords ())
            {
                WriteColor (coord, foreColor, backColor);
            }
        }

        #endregion

        #region helpers

        public IEnumerable<Point> EnumerateCoords ()
        {
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    yield return new Point (x, y);
                }
            }
        }

        public TextCanvas Slice (Rectangle viewArea) => new TextCanvas (new TextArraySlice (textArray, viewArea));

        public TextImage ToTextImage () => TextImage.CreateCopyFrom (textArray);

        #endregion

        //====== private methods

        private bool AreCoordsOutOfRange (Point position)
        {
            return position.X < 0 || position.X >= Size.Width || position.Y < 0 || position.Y >= Size.Height;
        }

        //====== override: Object

        public override string ToString () => $"{nameof (TextCanvas)} {Size}";
    }
}
