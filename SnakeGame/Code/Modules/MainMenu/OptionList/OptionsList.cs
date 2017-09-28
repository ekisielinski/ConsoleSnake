using Snake.Common.Helpers;
using System.Collections.Generic;

namespace Snake.Game.Modules
{
    public sealed class OptionsList<T>
    {
        public OptionsList (string title, Option<T>[] options)
        {
            Title = title;

            Options = Verify.NotNull (options, nameof (options));
        }

        //====== public properties =========================================================================================================

        public string Title { get; }

        public IReadOnlyList<Option<T>> Options { get; }
    }
}
