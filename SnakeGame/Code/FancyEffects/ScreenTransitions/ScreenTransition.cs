using Snake.Common.Helpers;
using Snake.Core;
using Snake.Text;
using System;

namespace Snake.Game
{
    public abstract class ScreenTransition : GameObject
    {
        protected readonly TextCanvas canvas;
        protected readonly TextImage  prevGameModuleScreenSnapshot;

        //====== ctors =====================================================================================================================

        protected ScreenTransition (ITextArrayReader prevGameModuleScreenSnapshot, TextCanvas canvas, GameTime gameTime) : base (gameTime)
        {
            Verify.NotNull (prevGameModuleScreenSnapshot, nameof (prevGameModuleScreenSnapshot));

            this.canvas = Verify.NotNull (canvas, nameof (canvas));

            if (prevGameModuleScreenSnapshot.Size.Equals (canvas.Size) == false)
            {
                throw new ArgumentException ($"Sizes are not equal.");
            }

            this.prevGameModuleScreenSnapshot = TextImage.CreateCopyFrom (prevGameModuleScreenSnapshot);
        }

        //====== abstract methods ==========================================================================================================

        public abstract void Paint ();
    }
}
