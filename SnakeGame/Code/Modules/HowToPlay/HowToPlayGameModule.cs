using Snake.Common.Geometry;
using Snake.Game.Core;
using Snake.Text;

using static Snake.Text.Color16;

namespace Snake.Game.Modules
{
    public sealed class HowToPlayGameModule : GameModule
    {
        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Black);

            canvas.WriteTextCenter (4, "                 ", Lime,   Green);
            canvas.WriteTextCenter (5, "   How To Play   ", Yellow, Green);
            canvas.WriteTextCenter (6, "                 ", Lime,   Green);

            canvas.WriteText (new Point (15, 10), "There are points for every apple eaten. Get as many points as you can.\r\n"
                + "Do not go out of the board and do not touch your own tail.", Lime, Black);

            canvas.WriteText (new Point (15, 13), "o", Lime, Black);
            canvas.WriteText (new Point (17, 13), "- normal apple, 8 points", Silver, Black);

            canvas.WriteText (new Point (15, 15), "o", Red, Black);
            canvas.WriteText (new Point (17, 15), "- transient apple, 20..60 points", Silver, Black);

            canvas.WriteText (new Point (15, 17), "Each eaten apple lengthens the body of the snake\r\nand slightly accelerates its movement.",
                LightTeal, Black);

            canvas.WriteText (new Point (15, 20), "Too many apples on the field end up losing.", Pink, Black);

            canvas.WriteTextCenter (canvas.Size.Height - 2, "-- Presc ESC to back --", Silver, Black);
        }
    }
}
