using Snake.Common.Helpers;
using Snake.Hardware.Audio;
using Snake.Hardware.Display;
using Snake.Hardware.Input;

namespace Snake.Hardware
{
    public sealed class HardwareAccessLayer
    {
        public HardwareAccessLayer (IInputDevice inputDevice, IDisplayDevice displayDevice, IAudioDevice audioDevice)
        {
            InputDevice   = Verify.NotNull (inputDevice,   nameof (inputDevice  ));
            DisplayDevice = Verify.NotNull (displayDevice, nameof (displayDevice));
            AudioDevice   = Verify.NotNull (audioDevice,   nameof (audioDevice  ));
        }

        //====== public properties

        public IInputDevice   InputDevice   { get; }
        public IDisplayDevice DisplayDevice { get; }
        public IAudioDevice   AudioDevice   { get; }
    }
}
