using Snake.Common.Geometry;
using Snake.Hardware.Display;
using Snake.Text;
using System;

namespace SnakeConsoleUI.Hardware
{
    public sealed class ConsoleDisplayDevice : DisplayDevice
    {
        const int MinWidth  = 10;
        const int MaxWidth  = 200;

        const int MinHeight = 10;
        const int MaxHeight = 200;

        readonly string title;

        //====== ctors

        public ConsoleDisplayDevice (Size screenSize, string title) : base (ValidateScreenSize (screenSize))
        {
            this.title = title;
        }

        //====== override: DisplayDevice

        protected override void InitializeDevice ()
        {
            var size = ScreenBuffer.Size;

            Console.Title           = title;

            Console.CursorVisible   = false;

            Console.WindowWidth     = size.Width;
            Console.WindowHeight    = size.Height + 1; // avoid new line when writing last character from buffer

            Console.BufferWidth     = size.Width;
            Console.BufferHeight    = size.Height + 1;

            Console.BackgroundColor = ConsoleColor.Black;

            Console.Clear ();
        }

        protected override void UpdateDeviceImpl ()
        {
            Console.SetCursorPosition (0, 0);

            using (var consoleWriter = new ConsoleWriter ())
            {
                for (int y = 0; y < ScreenBuffer.Size.Height; y++)
                {
                    for (int x = 0; x < ScreenBuffer.Size.Width; x++)
                    {
                        var cell = ScreenBuffer[x, y];

                        consoleWriter.WriteCell (cell ?? TextCell.DefaultEmpty);
                    }
                }
            }

            Console.CursorVisible = false; // avoids cursor blinking after window resize
        }

        //====== private static methods

        private static Size ValidateScreenSize (Size size)
        {
            if (size.Width < MinWidth || size.Width > MaxWidth || size.Height < MinHeight || size.Height > MaxHeight)
            {
                throw new ArgumentException ("Invalid window size.", nameof (size));
            }

            return size;
        }
    }
}
