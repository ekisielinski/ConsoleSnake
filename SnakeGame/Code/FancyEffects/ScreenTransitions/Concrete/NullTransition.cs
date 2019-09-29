using Snake.Core;
using Snake.Text;

namespace Snake.Game
{
    public sealed class NullTransition : ScreenTransition
    {
        public NullTransition (ITextArrayReader prevScreenSnapshot, TextCanvas canvas, GameTime gameTime)
            : base (prevScreenSnapshot, canvas, gameTime) { }

        //====== override: ScreenTransition

        public override void Paint () { }

        //====== override: GameObject

        protected override void UpdateImpl () => IsDead = true;

        //====== public static properties

        public static ScreenTransitionActivator Activator { get; } = (x, y, z) => new NullTransition (x, y, z);
    }
}
