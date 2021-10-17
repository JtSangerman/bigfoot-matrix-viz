using System;
using System.Threading;
using BIGFOOT.RGBMatrix.Inputs.Exceptions;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using SharpDX.XInput;


namespace BluetoothXboxOneControllerBIGFOOT.RGBMatrix.Visuals.Inputs
{
    public class BTXboxOneControllerDriver : ControllerInputDriver
    {

        private Controller _controller;
        private Gamepad _gamepad;
        private const int _deadband = 10000;
        private bool _skipNextNoInputEvent = false;

        public BTXboxOneControllerDriver() { }

        public override void Listen()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.Name = "THREAD01_XboxBluetoothController_THREADED_LISTENER01";

                Console.WriteLine($"LISTENING: on thread {Thread.CurrentThread.Name}\n\n\n");


                while (true)
                {
                    Thread.Sleep(10);
                    Update();
                }

            }).Start();
        }

        private void Update()
        {
            // TODO add support for disconnected controller
            if (!_controller.IsConnected)
                throw new InputControllerNotConnectedException($"Attempted to gather input info but not controller is connected on thread {Thread.CurrentThread.Name}");

            _gamepad = _controller.GetState().Gamepad;
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
                    FIRE_E_INPUT_NONE();
                    _skipNextNoInputEvent = true;
                    return;
                }
            }

            _skipNextNoInputEvent = false;


            // standard directonal inputs
            if (leftThumbX >= _deadband)
            {
                FIRE_E_INPUT_RIGHT();
            }

            if (leftThumbX <= -_deadband)
            {
                FIRE_E_INPUT_LEFT();
            }

            if (leftThumbY >= _deadband)
            {
                FIRE_E_INPUT_UP();
            }

            if (leftThumbY <= -_deadband)
            {
                FIRE_E_INPUT_DOWN();
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
