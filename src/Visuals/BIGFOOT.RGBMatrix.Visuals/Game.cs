using BIGFOOT.RGBMatrix.Inputs.Enums;
using BIGFOOT.RGBMatrix.Inputs.Exceptions;
using BIGFOOT.RGBMatrix.Inputs.Models;
using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Visuals
{
    // TODO doesn't support hardware viz at the moment
    public abstract class Game<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private readonly TMatrix _matrix;
        public readonly ControllerInputDriverBase _input;
        protected readonly List<ControllerInput> _inputQueue;
        protected bool Paused;
        protected bool ControllerConnected;

        public Game(TMatrix matrix, ControllerInputDriverBase input) : base(matrix)
        {
            _matrix = matrix;
            _input = input;
            _inputQueue = new List<ControllerInput>();

            SubscribeToControllerEvents();
            _input.EstablishControllerConnection();
        }

        protected abstract void Run();
        protected abstract void Draw();

        protected virtual void DrawPauseScreen() { }

        public override void VisualizeVirtually()
        {
            Visualize(true);
        }
        public override void VisualizeOnHardware()
        {
            Visualize(false);
        }

        private void Visualize(bool isEmulating)
        {
            Run();
        }

        private void ConnectController()
        {
            //SubscribeToControllerEvents();
            //_input.EstablishControllerConnection();
        }

        private void SubscribeToControllerEvents()
        {
            _input.E_CONTROLLER_INPUT_RECEIVED += Handle_E_INPUT_RECEIVED;

            _input.E_CONNECTION_SUCCESS += Handle_E_CONNECTION_SUCCESS;
            _input.E_CONNECTION_FAIL += Handle_E_CONNECTION_FAIL;
        }

        private void Handle_E_INPUT_RECEIVED(object sender, EventArgs e)
        {
            var type = ((ControllerInputEvent)sender).EventType;

            switch (type)
            {
                case ControllerInput.UP:
                    Handle_E_INPUT_UP();
                    break;
                case ControllerInput.DOWN:
                    Handle_E_INPUT_DOWN();
                    break;
                case ControllerInput.LEFT:
                    Handle_E_INPUT_LEFT();
                    break;
                case ControllerInput.RIGHT:
                    Handle_E_INPUT_RIGHT();
                    break;
                case ControllerInput.EXT1:
                    Handle_E_INPUT_EXT1();
                    break;
                case ControllerInput.EXT2:
                    break;
                case ControllerInput.NONE:
                    break;
                default:
                    throw new ControllerInputNotRecognized($"{type}");
            }
        }


        ////////////////
        ///
        protected abstract void Handle_E_INPUT_UP();
        protected abstract void Handle_E_INPUT_DOWN();

        protected abstract void Handle_E_INPUT_LEFT();

        protected abstract void Handle_E_INPUT_RIGHT();

        //protected abstract void Handle_E_INPUT_NONE();

        protected virtual async void Handle_E_INPUT_EXT1()
        {
            Paused = !Paused;
        }

        protected virtual void Handle_E_CONNECTION_SUCCESS(object sender, EventArgs e)
        {
            ControllerConnected = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nConnection to controller was successful.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal virtual void Handle_E_CONNECTION_FAIL(object sender, EventArgs e)
        {
            ControllerConnected = false;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nConnection to controller failed.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Debug_UpdateCurrentControllerInputOutput(string msg, string from)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);

            Console.Write($"{Thread.CurrentThread.Name} INPUT: ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write($"{from}: {msg} ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.WindowWidth-1, currentLineCursor);
        }
    }
}