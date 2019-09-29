using Snake.Common.Geometry;
using Snake.Core;
using Snake.Text;

namespace Snake.Game
{
    public sealed class TransitionT2 : ScreenTransition
    {
        int offset;

        //====== ctors

        public TransitionT2 (ITextArrayReader prevScreenSnapshot, TextCanvas canvas, GameTime gameTime) : base (prevScreenSnapshot, canvas, gameTime)
        {
            offset = canvas.Size.Width;
        }

        //====== override: GameObject

        protected override void UpdateImpl ()
        {
            offset -= 8;

            if (offset <= 0)
            {
                offset = 0;
                IsDead = true;
            }
        }

        //====== override: ScreenTransition

        public override void Paint ()
        {
            if (IsDead) return;

            DrawLines ();
        }

        //====== private methods

        private void DrawLines ()
        {
            var image = canvas.ToTextImage ();

            for (int i = 0; i < canvas.Size.Height; i++)
            {
                var lineRect = new Rectangle (new Point (0, i), new Size (canvas.Size.Width, 1));

                var prevImageLine = prevGameModuleScreenSnapshot.Canvas.Slice (lineRect).ToTextImage ();
                var currImageLine = image.Canvas.Slice (lineRect).ToTextImage ();

                int sign = (i % 2 == 0) ? -1 : 1;

                int lineOffset = offset * sign;
                int prevLineOffset = (lineOffset > 0) ? lineOffset - canvas.Size.Width : canvas.Size.Width + lineOffset;

                canvas.DrawImage (prevImageLine, new Point (prevLineOffset, i), false);
                canvas.DrawImage (currImageLine, new Point (lineOffset, i), false);
            }
        }

        //====== public static properties

        public static ScreenTransitionActivator Activator { get; } = (x, y, z) => new TransitionT2 (x, y, z);
    }
}
