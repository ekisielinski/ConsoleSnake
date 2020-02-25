using Snake.Common.Helpers;
using Snake.Hardware.Input;
using System;
using System.Collections.Generic;

namespace SnakeConsoleUI.Hardware
{
    public sealed class ConsoleInputDevice : IInputDevice
    {
        readonly Queue<KeyData> buffer = new Queue<KeyData> ();

        //====== IInputDevice

        public KeyData Peek ()
        {
            if (buffer.Count == 0) EnqueueBuffer ();

            if (buffer.Count > 0) return buffer.Peek ();

            return KeyData.Null;
        }

        public KeyData Read ()
        {
            EnqueueBuffer ();

            if (buffer.Count > 0) return buffer.Dequeue ();

            return KeyData.Null;
        }

        //====== private methods

        private void EnqueueBuffer ()
        {
            while (Console.KeyAvailable)
            {
                var consoleKeyInfo = Console.ReadKey (true);

                var keyData = ConsoleKeyInfoToKeyData (consoleKeyInfo);

                if (keyData != null) buffer.Enqueue (keyData);
            }
        }

        private KeyData ConsoleKeyInfoToKeyData (ConsoleKeyInfo cki)
        {
            if (MiscUtils.IsLatinLetter (cki.KeyChar) || char.IsDigit (cki.KeyChar) || cki.KeyChar == ' ')
            {
                return KeyData.FromCharacter (cki.KeyChar);
            }

            switch (cki.Key)
            {
                case ConsoleKey.Enter      : return KeyData.FromType (KeyType.Enter);
                case ConsoleKey.Backspace  : return KeyData.FromType (KeyType.Delete);
                case ConsoleKey.Escape     : return KeyData.FromType (KeyType.Cancel);
                case ConsoleKey.LeftArrow  : return KeyData.FromType (KeyType.Left);
                case ConsoleKey.RightArrow : return KeyData.FromType (KeyType.Right);
                case ConsoleKey.UpArrow    : return KeyData.FromType (KeyType.Up);
                case ConsoleKey.DownArrow  : return KeyData.FromType (KeyType.Down);

            }

            return null;
        }
    }
}
