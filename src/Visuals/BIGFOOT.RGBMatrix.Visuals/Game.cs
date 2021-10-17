using BIGFOOT.RGBMatrix.Inputs.Enums;
using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using System;
using System.Collections.Generic;

namespace BIGFOOT.RGBMatrix.Visuals
{
    // TODO doesn't support hardware viz at the moment
    public abstract class Game<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private readonly TMatrix _matrix;
        public readonly ControllerInputDriver _input;
        protected readonly List<ControllerInput> _inputQueue;

        public Game(TMatrix matrix, ControllerInputDriver input) : base(matrix)
        {
            _matrix = matrix;
            _input = input;
            _inputQueue = new List<ControllerInput>();
        }

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
            ConnectController();
            //Start();
        }

        private void ConnectController()
        {
            SubscribeToControllerEvents();
            _input.EstablishControllerConnection();
        }

       // public abstract void Start();


        private void SubscribeToControllerEvents()
        {
            _input.E_INPUT_UP += Handle_E_INPUT_UP;
            _input.E_INPUT_LEFT += Handle_E_INPUT_LEFT;
            _input.E_INPUT_RIGHT += Handle_E_INPUT_RIGHT;
            _input.E_INPUT_DOWN += Handle_E_INPUT_DOWN;
            _input.E_INPUT_NONE += Handle_E_INPUT_NONE;

            _input.E_CONNECTION_SUCCESS += Handle_E_CONNECTION_SUCCESS;
            _input.E_CONNECTION_FAIL += Handle_E_CONNECTION_FAIL;
        }

        ////////////////
        ///
        private void Handle_E_INPUT_UP(object sender, EventArgs e)
        {
            Debug_UpdateCurrentControllerInputOutput("L_JOYSTICK_UP");
        }

        private void Handle_E_INPUT_DOWN(object sender, EventArgs e)
        {
            Debug_UpdateCurrentControllerInputOutput("L_JOYSTICK_DOWN");
        }

        private void Handle_E_INPUT_LEFT(object sender, EventArgs e)
        {
            Debug_UpdateCurrentControllerInputOutput("L_JOYSTICK_LEFT");
        }

        private void Handle_E_INPUT_RIGHT(object sender, EventArgs e)
        {
            Debug_UpdateCurrentControllerInputOutput("L_JOYSTICK_RIGHT");
        }

        private void Handle_E_INPUT_NONE(object sender, EventArgs e)
        {
            Debug_UpdateCurrentControllerInputOutput(string.Empty);
        }

        private void Handle_E_CONNECTION_SUCCESS(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nConnection to controller was successful.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Handle_E_CONNECTION_FAIL(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nConnection to controller failed.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }


        public static void Debug_UpdateCurrentControllerInputOutput(string msg)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);

            Console.Write($"ACTIVE INPUT: ");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write($" {msg} ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.WindowWidth-1, currentLineCursor);
        }
    }
}