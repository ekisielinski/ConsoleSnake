using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Game.Drawing;
using Snake.Text;

namespace Snake.Game
{
    public sealed class BigDigitsScoreUI
    {
        readonly IScoreStatus scoreStatus;

        int lastScore;
        ITextArrayReader lastImage;

        //====== ctors

        public BigDigitsScoreUI (IScoreStatus scoreStatus, Color16 foreColor = Color16.Teal)
        {
            this.scoreStatus = Verify.NotNull (scoreStatus, nameof (scoreStatus));

            ForeColor = foreColor;

            lastScore = scoreStatus.Value;
        }

        //====== public properties

        public Color16 ForeColor { get; }

        //====== public methods

        public void DrawCenter (TextCanvas canvas, int y)
        {
            int currentScore = scoreStatus.Value;

            if (lastScore != currentScore || lastImage == null)
            {
                lastImage = new BigDigitsValueRenderer (currentScore, ForeColor).RenderImage ();
                lastScore = currentScore;
            }

            int xOffset = (canvas.Size.Width / 2) - (lastImage.Size.Width / 2);

            canvas.DrawImage (lastImage, new Point (xOffset, y));
        }
    }
}
