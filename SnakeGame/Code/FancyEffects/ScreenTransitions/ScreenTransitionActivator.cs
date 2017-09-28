using Snake.Core;
using Snake.Text;

namespace Snake.Game
{
    public delegate ScreenTransition ScreenTransitionActivator (ITextArrayReader prevScreenSnapshot, TextCanvas canvas, GameTime gameTime);
}
