using BIGFOOT.MatrixViz.Inputs.Drivers.Enums;
using BIGFOOT.MatrixViz.Inputs.Drivers.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.Inputs.Drivers.Models;
using BIGFOOT.MatrixViz.DriverInterfacing;

namespace BIGFOOT.MatrixViz.Visuals
{
    // TODO doesn't support hardware viz at the moment
    public abstract class Playable<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private readonly ControllerInputDriverBase _inputController;
        protected readonly List<ControllerInput> InputQueue;
        protected bool Paused;
        protected bool ControllerConnected;

        public Playable(TMatrix matrix, ControllerInputDriverBase input) : base(matrix)
        {
            _inputController = input;
            InputQueue = new List<ControllerInput>();

            SubscribeToControllerEvents();
            _inputController.EstablishControllerConnection();
        }

        // TODO Run(), Tick() should be implmeneted on this class, or virtual at the least. Would depend on 
        //      IsRunning/IsPaused/Etc lifted here as well.
        /*
         * Run()
         *      while running
         *          await Tick()
         */
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

        private async void Visualize(bool isEmulating)
        {
            Run();
        }

        private void SubscribeToControllerEvents()
        {
            _inputController.E_CONTROLLER_INPUT_RECEIVED += Handle_E_INPUT_RECEIVED;

            _inputController.E_CONNECTION_SUCCESS += Handle_E_CONNECTION_SUCCESS;
            _inputController.E_CONNECTION_FAIL += Handle_E_CONNECTION_FAIL;
        }

        private void Handle_E_INPUT_RECEIVED(object sender, EventArgs e)
        {
            var type = ((ControllerInputEvent)sender).EventType;

            switch (type)
            {
                case ControllerInput.UP:
                    InputQueue.Add(ControllerInput.UP);
                    Handle_E_INPUT_UP();
                    break;
                case ControllerInput.DOWN:
                    InputQueue.Add(ControllerInput.DOWN);
                    Handle_E_INPUT_DOWN();
                    break;
                case ControllerInput.LEFT:
                    InputQueue.Add(ControllerInput.LEFT);
                    Handle_E_INPUT_LEFT();
                    break;
                case ControllerInput.RIGHT:
                    InputQueue.Add(ControllerInput.RIGHT);
                    Handle_E_INPUT_RIGHT();
                    break;
                case ControllerInput.ACTION_A:
                    InputQueue.Add(ControllerInput.ACTION_A);
                    Handle_E_INPUT_ACTION_A();
                    break;
                case ControllerInput.ACTION_B:
                    InputQueue.Add(ControllerInput.ACTION_B);
                    Handle_E_INPUT_ACTION_B();
                    break;
                case ControllerInput.ACTION_X:
                    InputQueue.Add(ControllerInput.ACTION_X);
                    Handle_E_INPUT_ACTION_X();
                    break;
                case ControllerInput.ACTION_Y:
                    InputQueue.Add(ControllerInput.ACTION_Y);
                    Handle_E_INPUT_ACTION_Y();
                    break;
                case ControllerInput.EXT1:
                    InputQueue.Add(ControllerInput.EXT1);
                    Handle_E_INPUT_EXT1();
                    Thread.Sleep(500);
                    break;
                case ControllerInput.EXT2:
                    InputQueue.Add(ControllerInput.EXT2);
                    break;
                case ControllerInput.DELETE:
                    InputQueue.Add(ControllerInput.DELETE);
                    break;
                case ControllerInput.NONE:
                    break;
                default:
                    throw new ControllerInputNotRecognized($"{type}");
            }
        }

        protected virtual void Handle_E_INPUT_UP() { }
        protected virtual void Handle_E_INPUT_DOWN() { }
        protected virtual void Handle_E_INPUT_LEFT() { }
        protected virtual void Handle_E_INPUT_RIGHT() { }
        
        protected virtual void Handle_E_INPUT_ACTION_A() { }
        protected virtual void Handle_E_INPUT_ACTION_B() { }
        protected virtual void Handle_E_INPUT_ACTION_X() { }
        protected virtual void Handle_E_INPUT_ACTION_Y() { }

        protected virtual void Handle_E_INPUT_EXT1() => Paused = !Paused;

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