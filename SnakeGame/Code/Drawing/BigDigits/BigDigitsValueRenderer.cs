using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Text;
using System.Linq;

namespace Snake.Game.Drawing
{
    public sealed class BigDigitsValueRenderer
    {
        public BigDigitsValueRenderer (int value, Color16 foreColor)
        {
            Value = Verify.InRange (value, 0, int.MaxValue, nameof (value));

            ForeColor = foreColor;
        }

        //====== public properties

        public int     Value     { get; }
        public Color16 ForeColor { get; }

        //====== public methods

        public ITextArrayReader RenderImage ()
        {
            byte[] digits09 = Value.ToString ().Select (x => byte.Parse (x.ToString ())).ToArray ();

            int imageWidth = (digits09.Length * 4) - 1;

            var valueImage = new TextImage (new Size (imageWidth, 5));

            for (int i = 0; i < digits09.Length; i++)
            {
                var digitImage = TextImage.CreateCopyFrom (BigDigits.GetDigit (digits09[i]));

                var digitPosition = new Point (i * 4, 0);

                digitImage.Canvas.Colorize (ForeColor, null);

                valueImage.Canvas.DrawImage (digitImage, digitPosition);
            }

            return valueImage;
        }
    }
}
