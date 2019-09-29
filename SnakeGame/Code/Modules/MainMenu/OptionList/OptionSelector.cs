using Snake.Common.Helpers;
using System;

namespace Snake.Game.Modules
{
    public sealed class OptionSelector<T>
    {
        public event Action<Option<T>, int> IndexChanged;

        //====== ctors

        public OptionSelector (OptionsList<T> optionsList)
        {
            OptionsList = Verify.NotNull (optionsList, nameof (optionsList));
        }

        //====== public properties

        public OptionsList<T> OptionsList { get; private set; }

        public int CurrentOptionIndex { get; private set; } = 0;

        public T CurrentValue => OptionsList.Options[CurrentOptionIndex].Value;

        //====== public methods

        public void SelectNext ()
        {
            int newIndex = Math.Min (CurrentOptionIndex + 1, OptionsList.Options.Count - 1);

            if (newIndex != CurrentOptionIndex)
            {
                CurrentOptionIndex = newIndex;
                IndexChanged?.Invoke (OptionsList.Options[newIndex], newIndex);
            }
        }

        public void SelectPrevious ()
        {
            int newIndex = Math.Max (CurrentOptionIndex - 1, 0);

            if (newIndex != CurrentOptionIndex)
            {
                CurrentOptionIndex = newIndex;
                IndexChanged?.Invoke (OptionsList.Options[newIndex], newIndex);
            }
        }
    }
}
