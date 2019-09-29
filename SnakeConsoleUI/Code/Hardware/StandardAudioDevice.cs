using Snake.Hardware.Audio;
using System;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace SnakeConsoleUI.Hardware
{
    public sealed class StandardAudioDevice : IAudioDevice, IDisposable
    {
        SpeechSynthesizer synth;

        //====== IAudioDevice

        public void Beep () => Task.Run (() => Console.Beep (1500, 50));

        public void SpeakAsync (string message)
        {
            if (synth == null) Init ();

            synth.SpeakAsyncCancelAll ();

            var prompt = new Prompt (message);
            synth.SpeakAsync (prompt);
        }

        //====== IDisposable

        public void Dispose () => synth?.Dispose ();

        //====== private methods

        private void Init ()
        {
            synth = new SpeechSynthesizer ();

            synth.SetOutputToDefaultAudioDevice ();
            synth.Volume = 50;

            var prefferedVoice = synth.GetInstalledVoices ().FirstOrDefault (x => x.VoiceInfo.Culture.EnglishName.Contains ("English"));

            if (prefferedVoice != null)
            {
                synth.SelectVoice (prefferedVoice.VoiceInfo.Name);
            }
        }
    }
}
