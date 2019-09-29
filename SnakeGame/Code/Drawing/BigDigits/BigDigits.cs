using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Text;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Game.Drawing
{
    /// <summary>
    /// Allows to extract from the embedded resource a text images that represents 'big' digits 0..9 (3x5 characters).
    /// </summary>

    public static class BigDigits
    {
        static readonly Dictionary<int, ITextArrayReader> digits = new Dictionary<int, ITextArrayReader> ();

        //====== ctors

        static BigDigits ()
        {
            string data = Resources.TextDigits;

            for (int i = 0; i <= 9; i++)
            {
                ITextArrayReader digit = ExtractDigit (i, data);

                digits.Add (i, digit);
            }
        }

        //====== public static methods

        public static ITextArrayReader GetDigit (int digit)
        {
            Verify.InRange (digit, 0, 9, nameof (digit));

            return digits[digit];
        }

        //====== private static methods

        private static ITextArrayReader ExtractDigit (int digit, string data)
        {
            int startRowIndex = digit * 4;

            var lines = data.Split (new[] { "\r\n" }, StringSplitOptions.None).Select (x => x.Substring (startRowIndex, 3)).ToArray ();

            var digitSize = new Size (3, 5);
            var textImage = new TextImage (digitSize);

            for (int x = 0; x < digitSize.Width; x++)
            {
                for (int y = 0; y < digitSize.Height; y++)
                {
                    if (lines[y][x] == 'X')
                    {
                        textImage[x, y] = new TextCell ('█', Color16.White, Color16.Black); // can be colorized later
                    }
                }
            }

            return textImage;
        }
    }
}
