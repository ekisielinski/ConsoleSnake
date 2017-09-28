using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class StatusItem
    {
        public StatusItem (string name, string value, bool isValueCritical = false)
        {
            Name  = Verify.NotNull (name, nameof (name));
            Value = Verify.NotNull (value, nameof (value));

            IsValueCritical = isValueCritical;
        }

        //====== public properties =========================================================================================================

        public string Name  { get; }
        public string Value { get; }

        public bool IsValueCritical { get; }

        //====== override: Object ==========================================================================================================

        public override string ToString () => $"{Name}: {Value}";
    }
}
