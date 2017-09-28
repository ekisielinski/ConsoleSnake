using Snake.Common.Geometry;
using Snake.Core;
using Snake.Text;
using System;

namespace Snake.Game
{
    public sealed class TransitionT1 : ScreenTransition
    {
        int borderWidth;

        //====== ctors =====================================================================================================================

        public TransitionT1 (ITextArrayReader prevScreenSnapshot, TextCanvas canvas, GameTime gameTime) : base (prevScreenSnapshot, canvas, gameTime)
        {
            borderWidth = GetMaxBorderWidth ();
        }

        //====== override: GameObject ======================================================================================================

        protected override void UpdateImpl ()
        {
            borderWidth -= 2;

            if (borderWidth < 0)
            {
                borderWidth = 0;
                IsDead = true;
            }
        }

        //====== override: ScreenTransition ================================================================================================

        public override void Paint ()
        {
            if (IsDead) return;

            DrawBorder ();
        }

        //====== private methods ===========================================================================================================

        private int GetMaxBorderWidth ()
        {
            return (int) Math.Ceiling (Math.Min (canvas.Size.Width / 2d, canvas.Size.Height / 2d));
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private void DrawBorder ()
        {
            for (int i = 0; i < borderWidth; i++)
            {
                var position = new Point (i, i);
                var size = new Size (canvas.Size.Width - (i * 2), canvas.Size.Height - (i * 2));

                var rect = new Rectangle (position, size);

                bool isFrame = i == borderWidth - 1;

                DrawRectangle (canvas, rect, isFrame);
            }
        }

        //====== public static properties ==================================================================================================

        public static ScreenTransitionActivator Activator { get; } = (x, y, z) => new TransitionT1 (x, y, z);

        //====== private static methods ====================================================================================================

        private static void DrawRectangle (TextCanvas canvas, Rectangle rect, bool isFrame)
        {
            if (rect.Size.HasNoArea) return;

            var backColor = isFrame ? Color16.White : Color16.Black;

            var cell = new TextCell (' ', Color16.Black, backColor);

            canvas.HLine (rect.TopLeft,    rect.Size.Width, cell);
            canvas.HLine (rect.BottomLeft, rect.Size.Width, cell);

            canvas.VLine (rect.TopLeft,    rect.Size.Height, cell);
            canvas.VLine (rect.TopRight,   rect.Size.Height, cell);
        }
    }
}
