namespace Snake.Hardware.Input
{
    public sealed class NullInputDevice : IInputDevice
    {
        public KeyData Peek () => KeyData.Null;
        public KeyData Read () => KeyData.Null;
    }
}
