using BIGFOOT.RGBMatrix.Inputs;
using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.MatrixTypes.Direct2D;
using BIGFOOT.RGBMatrix.MatrixTypes.InterfacedRGBLed;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using BIGFOOT.RGBMatrix.Visuals.Snake;
using BluetoothXboxOneControllerBIGFOOT.RGBMatrix.Visuals.Inputs;
using System;

namespace BIGFOOT.RGBMatrix
{
    class Main_ControllerPOCDemo
    {
        public static void Main(string[] args)
        {
            //var matrix = new Direct2DMatrix(32);
            //ControllerInputDriverBase inputDriver;

            //Console.WriteLine("Choose a driver: ");
            //Console.WriteLine("\t <1> Xbox Controller input via Bluetooth");
            //Console.WriteLine("\t <2> Keyboard input");
            //var key = Console.ReadKey().Key;
            //Console.Clear();

            //switch (key)
            //{
            //    case ConsoleKey.D1:
            //        inputDriver = new BTXboxOneControllerDriver();
            //        Console.Write("\nStarting Xbox Controller via Bluetooth input driver");
            //        break;

            //    default:
            //        inputDriver = new KeyboardConsoleDriver();
            //        Console.WriteLine("Starting keyboard input driver");
            //        break;
            //}

            //var visual = new Snake(matrix, inputDriver);
            //Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(visual);

            ///////

            //var matrix2 = new InterfacedRGBLedMatrix(32, 1, 1);
            //var visual2 = new Snake<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(matrix2, inputDriver);
            //visual2.VisualizeOnHardware();

            //var matrix = new Direct2DMatrix(32);
            //var visual = new Snake<Direct2DMatrix, Direct2DCanvas>(matrix, xboxDriver);
            //Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(visual);
            //Thread.Sleep(2000);
        }
    }
}
