using BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole;
using BIGFOOT.RGBMatrix.MatrixTypes.InterfacedRGBLed;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using System;

namespace BIGFOOT.RGBMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = Console.ReadKey().Key;
            var matrix = new ColoredConsoleMatrix(32);
            if (opt == ConsoleKey.D1)
            {
                var bubble = new BubbleSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                bubble.Visualize();
            }
            else if(opt == ConsoleKey.D2)
            {
                var insertion = new InsertionSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                insertion.Visualize();
            }
            else
            {
                var led = new InterfacedRGBLedMatrix(32, 1, 1);
                var bubble = new BubbleSort<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(led);
                bubble.Visualize();
            }
        }
    }
}
