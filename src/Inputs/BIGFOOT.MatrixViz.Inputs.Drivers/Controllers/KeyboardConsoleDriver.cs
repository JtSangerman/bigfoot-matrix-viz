using BIGFOOT.MatrixViz.Inputs.Drivers.Constants;
using System;

namespace BIGFOOT.MatrixViz.Inputs.Drivers.Controllers
{
    public class KeyboardConsoleDriver : ControllerInputDriverBase
    {
        protected override string DriverName { get => ControllerDriverName.KeyboardConsole; }

        private protected override void Connect()
        {
            // Assumed that a keyboard is connected
            FIRE_E_CONNECITON_SUCCESS();
        }

        private protected override void Update()
        {
            Enums.ControllerInput inputEventType = Enums.ControllerInput.NONE;

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.W:
                    inputEventType = Enums.ControllerInput.UP;
                    break;
                case ConsoleKey.A:
                    inputEventType = Enums.ControllerInput.LEFT;
                    break;
                case ConsoleKey.S:
                    inputEventType = Enums.ControllerInput.DOWN;
                    break;
                case ConsoleKey.D:
                    inputEventType = Enums.ControllerInput.RIGHT;
                    break;
                case ConsoleKey.E:
                    inputEventType = Enums.ControllerInput.ACTION_A;
                    break;
                case ConsoleKey.Spacebar:
                    inputEventType = Enums.ControllerInput.EXT1;
                    break;
            }

            FIRE_E_CONTROLLER_INPUT_RECEIVED(new Models.ControllerInputEvent() { EventType = inputEventType });
        }
    }
}
