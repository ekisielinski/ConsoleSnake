using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Game.Drawing;
using Snake.Text;
using System.Linq;

namespace Snake.Game.Modules
{
    public sealed class OptionsListWindow<T> : Window
    {
        readonly OptionSelector<T> optionSelector;

        //====== ctors =====================================================================================================================

        public OptionsListWindow (OptionSelector<T> optionSelector, WindowAppearance appearance = null) : base (appearance)
        {
            this.optionSelector = Verify.NotNull (optionSelector, nameof (optionSelector));

            Initialize ();
        }

        //====== override: Window ==========================================================================================================

        protected override void PaintContent (TextCanvas canvas)
        {
            var list = optionSelector.OptionsList;

            for (int i = 0; i < list.Options.Count; i++)
            {
                bool isSelected = optionSelector.CurrentOptionIndex == i;

                var color = isSelected ? Color16.White : Color16.Silver;

                canvas.WriteText (new Point (3, 1 + i), list.Options[i].Name, color, Appearance.BackColor);

                if (isSelected)
                {
                    canvas.WriteText (new Point (1, 1 + i), ">", color, Appearance.BackColor);
                }
            }
        }

        //====== private methods ===========================================================================================================

        private void Initialize ()
        {
            Title = optionSelector.OptionsList.Title;

            int maxOptionNameLength = optionSelector.OptionsList.Options.Max (x => x.Name.Length);
            int optionsCount        = optionSelector.OptionsList.Options.Count;

            Size = new Size (maxOptionNameLength + 7, optionsCount + 4); // |.>.option.|
        }
    }
}
