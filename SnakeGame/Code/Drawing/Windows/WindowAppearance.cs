using Snake.Text;

namespace Snake.Game.Drawing
{
    public sealed class WindowAppearance
    {
        private WindowAppearance () { }

        //----------------------------------------------------------------------------------------------------------------------------------

        public WindowAppearance (Color16 backColor, Color16 borderColor, Color16 titleTextColor, bool thinBorder = false)
        {
            BackColor      = backColor;
            BorderColor    = borderColor;
            TitleTextColor = titleTextColor;

            ThinBorder     = thinBorder;
        }

        //====== public properties =========================================================================================================

        public Color16 BackColor      { get; } = Color16.DarkBlue;
        public Color16 BorderColor    { get; } = Color16.White;
        public Color16 TitleTextColor { get; } = Color16.Yellow;

        public bool    ThinBorder     { get; } = false;

        //====== public static properties ==================================================================================================

        public static WindowAppearance Default { get; } = new WindowAppearance ();
    }
}
