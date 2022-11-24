using BIGFOOT.RGBMatrix.ControllerInput.Constants;
using BIGFOOT.RGBMatrix.ControllerInput.Enums;
using BIGFOOT.RGBMatrix.ControllerInput.Exceptions;
using SharpDX.XInput;
using System;
using System.Threading;


namespace BIGFOOT.RGBMatrix.ControllerInput.Controllers
{
    public class BTXboxOneControllerDriver : ControllerInputDriverBase
    {
        protected override string DriverName { get => ControllerDriverName.BTXboxOneController; }

        private Controller _controller;
        private Gamepad _gamepad;
        private const int _deadband = 10000;
        private bool _skipNextNoInputEvent = false;

        //public override void Listen()
        //{
        //    new Thread(() =>
        //    {
        //        Thread.CurrentThread.Name = $"INPUT_LISTENER_THREAD__{DriverName}";

        //        Console.WriteLine($"{Thread.CurrentThread.Name} => Listening \n\n\n");

        //        while (true)
        //        {
        //            Thread.Sleep(10); // ?
        //            Update();
        //        }

        //    }).Start();
        //}

        private protected override void Update()
        {
            /* TODO add support for disconnected controller                             
             * TODO need to establish multiple button with priority:      
             *  ex: UP + A + B + PAUSE => PAUSE priority   
             *  ex: UP + A + B => doesn't matter
            */
            if (!_controller.IsConnected)
            {
                FIRE_E_CONNECITON_FAIL();
                return;
                //  throw new InputControllerNotConnectedException($"Attempted to gather input info but not controller is connected on thread {Thread.CurrentThread.Name}");
            }

            _gamepad = _controller.GetState().Gamepad;

            // pause button is pressed, prioritize pause input event
            if (_gamepad.Buttons == GamepadButtonFlags.Start)
            {
                FIRE_E_CONTROLLER_INPUT_RECEIVED(new Models.ControllerInputEvent() { EventType = Enums.ControllerInput.EXT1 });
                return;
            }

            var buttons = _gamepad.Buttons;

            var dpadInputEvent = new Models.ControllerInputEvent();
            if (buttons.HasFlag(GamepadButtonFlags.DPadUp))
            {
                dpadInputEvent.EventType = Enums.ControllerInput.UP;
            }
            else if (buttons.HasFlag(GamepadButtonFlags.DPadRight))
            {
                dpadInputEvent.EventType = Enums.ControllerInput.RIGHT;
            }
            else if (buttons.HasFlag(GamepadButtonFlags.DPadDown))
            {
                dpadInputEvent.EventType = Enums.ControllerInput.DOWN;
            }
            else if (buttons.HasFlag(GamepadButtonFlags.DPadLeft))
            {
                dpadInputEvent.EventType = Enums.ControllerInput.LEFT;
            }
            else
            {
                dpadInputEvent = null;
            }

            if (dpadInputEvent != null)
            {
                FIRE_E_CONTROLLER_INPUT_RECEIVED(dpadInputEvent);
            }

            var leftThumbX = _gamepad.LeftThumbX * .95;
            var leftThumbY = _gamepad.LeftThumbY * .95;

            if (leftThumbX > short.MaxValue ||
                leftThumbY > short.MaxValue ||
                leftThumbY < short.MinValue ||
                leftThumbX < short.MinValue)
            {
                Console.WriteLine($"Erroneous input detected and discarded -- x: {leftThumbX}, y: {leftThumbY}");
                return;
            }

            // no input detected
            if (Math.Abs(leftThumbY) < _deadband &&
                Math.Abs(leftThumbX) < _deadband)
            {
                if (_skipNextNoInputEvent)
                {
                    return;
                }
                else
                {
                    //FIRE_E_CONTROLLER_INPUT_RECEIVED(new ControllerInputEvent { EventType = ControllerInput.NONE });
                    _skipNextNoInputEvent = true;
                    return;
                }
            }

            _skipNextNoInputEvent = false;



            var joystickInputEvent = new Models.ControllerInputEvent();

            // standard directonal inputs
            if (leftThumbX >= _deadband)
            {
                joystickInputEvent.EventType = Enums.ControllerInput.RIGHT;
            }
            else if (leftThumbX <= -_deadband)
            {
                joystickInputEvent.EventType = Enums.ControllerInput.LEFT;
            }
            else if (leftThumbY >= _deadband)
            {
                joystickInputEvent.EventType = Enums.ControllerInput.UP;
            }
            else if (leftThumbY <= -_deadband)
            {
                joystickInputEvent.EventType = Enums.ControllerInput.DOWN;
            }
            else
            {
                joystickInputEvent = null;
            }

            if (joystickInputEvent != null)
            {
                FIRE_E_CONTROLLER_INPUT_RECEIVED(joystickInputEvent);
            }
        }


        private protected override void Connect()
        {
            _controller = new Controller(UserIndex.One);

            if (_controller.IsConnected)
            {
                FIRE_E_CONNECITON_SUCCESS();
            }
            else
            {
                FIRE_E_CONNECITON_FAIL();
            }
        }
    }
}
