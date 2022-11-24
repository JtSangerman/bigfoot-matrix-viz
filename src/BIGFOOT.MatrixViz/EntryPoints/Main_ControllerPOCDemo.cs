using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals.Snake;
using System;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    class Main_ControllerPOCDemo
    {
        public static void Main(string[] args)
        {
            var matrix = new Direct2DMatrix(64);
            ControllerInputDriverBase inputDriver;

            Console.WriteLine("Choose a driver: ");
            Console.WriteLine("\t <1> Xbox Controller input via Bluetooth");
            Console.WriteLine("\t <2> Keyboard input");
            var key = Console.ReadKey().Key;
            Console.Clear();

            switch (key)
            {
                case ConsoleKey.D1:
                    inputDriver = new BTXboxOneControllerDriver();
                    Console.Write("\nStarting Xbox Controller via Bluetooth input driver");
                    break;

                default:
                    inputDriver = new KeyboardConsoleDriver();
                    Console.WriteLine("Starting keyboard input driver");
                    break;
            }

            var visual = new Snake<Direct2DMatrix, Direct2DCanvas>(matrix, inputDriver);
            Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(visual);

            ///////

            //var matrix2 = new InterfacedRGBLedMatrix(64, 1, 1);
            //var visual2 = new Snake<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(matrix2, inputDriver);
            //visual2.VisualizeOnHardware();

            //var matrix = new Direct2DMatrix(64);
            //var visual = new Snake<Direct2DMatrix, Direct2DCanvas>(matrix, xboxDriver);
            //Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(visual);
            //Thread.Sleep(2000);
        }
    }
}
