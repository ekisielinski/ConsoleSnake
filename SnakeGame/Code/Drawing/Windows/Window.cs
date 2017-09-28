using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Text;

namespace Snake.Game.Drawing
{
    public abstract class Window
    {
        protected Window (WindowAppearance appearance)
        {
            Appearance = appearance ?? WindowAppearance.Default;
        }

        //====== public properties =========================================================================================================

        public Size             Size       { get; protected set; } = new Size (20, 10);
        public string           Title      { get; protected set; } = "No Name";
        public WindowAppearance Appearance { get; }

        //====== abstract ==================================================================================================================

        protected abstract void PaintContent (TextCanvas canvas);

        //====== public methods ============================================================================================================

        public void Paint (TextCanvas canvas, Point position)
        {
            Verify.NotNull (canvas, nameof (canvas));

            if (Size.Width < 3 || Size.Height < 3) return; // no reason to draw such small window...

            //--- paint background and border

            var windowView = canvas.Slice (new Rectangle (position, Size));

            windowView.ClearColor (Appearance.BackColor);
            windowView.Frame (windowView.Size.AsRectangle, Appearance.BorderColor, Appearance.BackColor, Appearance.ThinBorder);

            PaintTitle (windowView);

            //--- paint content

            var contentArea = new Rectangle (new Point (1, 1), new Size (windowView.Size.Width - 2, windowView.Size.Height - 2));

            var contentView = windowView.Slice (contentArea);

            PaintContent (contentView);
        }

        //====== private methods ===========================================================================================================

        private void PaintTitle (TextCanvas windowView)
        {
            if (windowView.Size.Width >= 7)
            {
                var titleView = windowView.Slice (new Rectangle (new Point (2, 0),
                                                       new Size (windowView.Size.Width - 6, 1)));

                titleView.WriteText (Point.Zero, " " + Title + " ", Appearance.TitleTextColor, Appearance.BackColor);
            }
        }
    }
}
