using Snake.Common.Geometry;
using Snake.Game.Core;
using Snake.Text;

namespace Snake.Game.Modules
{
    public sealed class HowToPlayGameModule : GameModule
    {
        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            canvas.WriteTextCenter (4, "                 ", Color16.Lime, Color16.Green);
            canvas.WriteTextCenter (5, "   How To Play   ", Color16.Yellow, Color16.Green);
            canvas.WriteTextCenter (6, "                 ", Color16.Lime, Color16.Green);

            canvas.WriteText (new Point (15, 10), "There are points for every apple eaten. Get as many points as you can.\r\n"
                + "Do not go out of the board and do not touch your own tail.", Color16.Lime, Color16.Black);

            canvas.WriteText (new Point (15, 13), "o", Color16.Lime, Color16.Black);
            canvas.WriteText (new Point (17, 13), "- normal apple, 8 points", Color16.Silver, Color16.Black);

            canvas.WriteText (new Point (15, 15), "o", Color16.Red, Color16.Black);
            canvas.WriteText (new Point (17, 15), "- transient apple, 20..60 points", Color16.Silver, Color16.Black);

            canvas.WriteText (new Point (15, 17), "Each eaten apple lengthens the body of the snake\r\nand slightly accelerates its movement.",
                Color16.LightTeal, Color16.Black);

            canvas.WriteText (new Point (15, 20), "Too many apples on the field end up losing.", Color16.Pink, Color16.Black);

            canvas.WriteTextCenter (canvas.Size.Height - 2, "-- Presc ESC to back --", Color16.Silver, Color16.Black);
        }
    }
}
