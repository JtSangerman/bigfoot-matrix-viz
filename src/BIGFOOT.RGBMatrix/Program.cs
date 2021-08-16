using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using BIGFOOT.RGBMatrix.Visuals.Visualizer.ColoredConsole;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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
                bubble.Visualise();
            }
            else
            {
                var insertion = new InsertionSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                insertion.Visualise();
            } 
        }
    }
}
