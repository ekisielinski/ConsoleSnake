using Snake.Common.Helpers;
using Snake.Common.Logging;
using Snake.Game.Core;
using Snake.Hardware;
using Snake.Hardware.Audio;
using Snake.Hardware.Display;
using Snake.Hardware.Input;

namespace Snake.Game
{
    public static class PureDiBoostrapper
    {
        public static GameRoot CreateGameRoot (
            IInputDevice     inputDevice,
            IDisplayDevice   displayDevice,
            IAudioDevice     audioDevice,
            IHiScoresStorage hiScoresStorage,
            ILogger          logger)
        {
            Verify.NotNull (inputDevice,     nameof (inputDevice));
            Verify.NotNull (displayDevice,   nameof (displayDevice));
            Verify.NotNull (audioDevice,     nameof (audioDevice));
            Verify.NotNull (hiScoresStorage, nameof (hiScoresStorage));
            Verify.NotNull (logger,          nameof (logger));

            var safeAudioDevice = new SafeAudioDevice (audioDevice, logger);
            var hal = new HardwareAccessLayer (inputDevice, displayDevice, safeAudioDevice);

            var sleeper = new Sleeper ();

            var gameRoot = new GameRoot (hal, sleeper, logger);

            return gameRoot;
        }
    }
}
