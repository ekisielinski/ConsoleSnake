using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Game.Drawing;
using Snake.Text;
using System.Linq;

namespace Snake.Game.Modules
{
    public sealed class HiScoresWindow : Window
    {
        readonly IHiScores hiScoresList;

        //====== ctors =====================================================================================================================

        public HiScoresWindow (IHiScores hiScoresList, WindowAppearance appearance = null) : base (appearance)
        {
            this.hiScoresList = Verify.NotNull (hiScoresList, nameof (hiScoresList));

            Initialize ();
        }

        //====== override: Window ==========================================================================================================

        protected override void PaintContent (TextCanvas canvas)
        {
            var list = hiScoresList.GetList ();

            for (int i = 0; i < hiScoresList.Capacity; i++)
            {
                HiScoresEntry entry = (i < list.Count) ? list[i] : null;

                PrintHiScoreItem (canvas, new Point (2, 1 + i), i, entry);
            }
        }

        //====== private methods ===========================================================================================================

        private void Initialize ()
        {
            var list = hiScoresList.GetList ();
            int maxNameLength = list.Any () ? list.Max (x => x.Name.Length) : 0;

            if (maxNameLength < 3) maxNameLength = 3;

            Size = new Size (maxNameLength + 28, hiScoresList.Capacity + 4);

            Title = "Hall of Fame";
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private void PrintHiScoreItem (TextCanvas canvas, Point position, int itemIndex, HiScoresEntry entry)
        {
            var color = (entry == null) ? Color16.Gray : Color16.White;

            string name  = entry?.Name ?? "...";
            string score = entry?.Score.ToString () ?? "...";

            name = name.PadLeft (20, ' ');
            score = score.PadLeft (5, ' ');

            canvas.WriteText (position, $"{itemIndex + 1}. {score} {name}", color, Appearance.BackColor);
        }
    }
}
