using BIGFOOT.RGBMatrix.ControllerInputDrivers.Constants;
using BIGFOOT.RGBMatrix.Inputs.Enums;
using BIGFOOT.RGBMatrix.Inputs.Models;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Inputs
{
    public class KeyboardConsoleDriver : ControllerInputDriverBase
    {
        protected override string DriverName { get => ControllerDriverName.KeyboardConsole; }

        //public override void Listen()
        //{
        //    new Thread(() =>
        //    {
        //        Thread.CurrentThread.Name = $"INPUT_LISTENER_THREAD__{DriverName}";

        //        Console.WriteLine($"{Thread.CurrentThread.Name} => Listening \n\n\n");

        //        while (true)
        //        {
        //            Update();
        //        }

        //    }).Start();
        //}

        private protected override void Connect()
        {
            // Assumed that a keyboard is connected
            FIRE_E_CONNECITON_SUCCESS();
        }

        private protected override void Update()
        {
            ControllerInput inputEventType = ControllerInput.NONE;

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.W:
                    inputEventType = ControllerInput.UP;
                    break;
                case ConsoleKey.A:
                    inputEventType = ControllerInput.LEFT;
                    break;
                case ConsoleKey.S:
                    inputEventType = ControllerInput.DOWN;
                    break;
                case ConsoleKey.D:
                    inputEventType = ControllerInput.RIGHT;
                    break;
                case ConsoleKey.Spacebar:
                    inputEventType = ControllerInput.EXT1;
                    break;
            }

            FIRE_E_CONTROLLER_INPUT_RECEIVED(new ControllerInputEvent() { EventType = inputEventType });
        }
    }
}
