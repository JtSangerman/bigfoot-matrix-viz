using BIGFOOT.RGBMatrix.Visuals.Inputs;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Inputs
{
    public class KeyboardConsoleDriver : ControllerInputDriver
    {
        public override void Listen()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.Name = "THREAD01_KeyboardConsoleControllerInputDriverListener01";


                Console.WriteLine($"Listening for input on a new thread: {Thread.CurrentThread.Name}\n\n\n");

                while (true)
                {
                    Update();
                }

            }).Start();
        }

        private protected override void Connect()
        {
            // Assumed that a keyboard is connected
            FIRE_E_CONNECITON_SUCCESS();
        }

        private void Update()
        {
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.W:
                    FIRE_E_INPUT_UP();
                    break;
                case ConsoleKey.A:
                    FIRE_E_INPUT_LEFT();
                    break;
                case ConsoleKey.S:
                    FIRE_E_INPUT_DOWN();
                    break;
                case ConsoleKey.D:
                    FIRE_E_INPUT_RIGHT();
                    break;
            }
        }
    }
}
