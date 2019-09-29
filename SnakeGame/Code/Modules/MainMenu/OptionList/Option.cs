using Snake.Common.Helpers;

namespace Snake.Game.Modules
{
    public sealed class Option<T>
    {
        public Option (string name, T value)
        {
            Name  = Verify.NotNull (name, nameof (name));
            Value = value;
        }

        //====== public properties

        public string Name { get; }

        public T Value { get; }
    }
}
