using BIGFOOT.RGBMatrix.Inputs;
using BIGFOOT.RGBMatrix.MatrixTypes.Direct2D;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using BluetoothXboxOneControllerBIGFOOT.RGBMatrix.Visuals.Inputs;
using SnakeBIGFOOT.RGBMatrix.Visuals.Games;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix
{
    class XboxControllerBluetoothInputPOC
    {
        public static void Main(string[] args)
        {
            var matrix = new Direct2DMatrix(32);
            ControllerInputDriver inputDriver;

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
                    DotDotDot();
                    break;

                default:
                    inputDriver = new KeyboardConsoleDriver();
                    Console.WriteLine("Starting keyboard input driver");
                    DotDotDot();
                    break;
            }

            var visual = new Snake<Direct2DMatrix, Direct2DCanvas>(matrix, inputDriver);
            Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(visual);

            //var matrix = new Direct2DMatrix(32);
            //var visual = new Snake<Direct2DMatrix, Direct2DCanvas>(matrix, xboxDriver);
            //Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(visual);

            //xboxDriver.Connect();
            //xboxDriver.Up();
            //Thread.Sleep(2000);
        }
        
        private static void DotDotDot()
        {
            //Console.Write("."); Thread.Sleep(700);
            //Console.Write("."); Thread.Sleep(700);
            //Console.Write("."); Thread.Sleep(700);
            //Console.Write("."); Thread.Sleep(700);
            //Console.Write("."); Thread.Sleep(700);
            Console.Write("\n\n\n");
        }
    }
}
